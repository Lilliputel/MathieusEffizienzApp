using DataLayer.Interfaces;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using ModelLayer.Planning;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;

namespace DataLayer.MockDataService {
	public class MockDataService : IDataService<ObservableCollection<Category>, Category> {

		public ObservableCollection<Category> LoadData() {
			var zwischenSpeicher = new ObservableCollection<Category>();

			for( int counter = 0; counter <= 3; counter++ ) {
				Random randomGen = new Random();
				Color randomColor =
					Color.FromArgb(
					(byte)randomGen.Next(255),
					(byte)randomGen.Next(255),
					(byte)randomGen.Next(255),
					(byte)randomGen.Next(255));

				// new Category
				Category CBCategory1 = new Category($"Generated-Category{counter}", randomColor);

				Goal goalX_1 = new Goal($"Generated-Goal{counter}_1", new DateSpan(DateTime.Today.AddDays(1), DateTime.Today.AddDays(8)));
				var Date = DateTime.Today.AddDays(randomGen.Next(-10, 11));
				var Time = TimeSpan.FromHours(randomGen.NextDouble() * 5);
				( goalX_1 as IWorkItem ).AddWorkedTime(Date, Time);
				Goal goalX_1_1 = new Goal($"Generated-Goal{counter}_1.1", new DateSpan(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5)));
				goalX_1.AddChild(goalX_1_1);
				Goal goalX_2 = new Goal($"Generated-Goal{counter}_2", new DateSpan(DateTime.Today, DateTime.Today.AddDays(10)));

				CBCategory1.AddChildren(new Collection<Goal> { goalX_1, goalX_2 });
				CBCategory1.WorkSessions.Add(((DayOfWeek)randomGen.Next(7), new DoubleTime((0.0 + counter, 1.0 + counter))));
				zwischenSpeicher.Add(CBCategory1);

				Debug.WriteLine($"Created Cat{counter} with {Time} worked Time on the {Date}.");
			}

			return zwischenSpeicher;
		}

		public void SaveData( ObservableCollection<Category> Collection ) {
			Debug.WriteLine($"Tried to save the {nameof(Collection)}[{Collection}] in the MockDataService");
		}

	}
}
