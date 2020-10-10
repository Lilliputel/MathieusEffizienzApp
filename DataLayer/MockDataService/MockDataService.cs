using DataLayer.Interfaces;
using ModelLayer.Classes;
using ModelLayer.Extensions;
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
					(byte)255,
					(byte)randomGen.Next(255),
					(byte)randomGen.Next(255),
					(byte)randomGen.Next(255));

				// new Category
				Category CBCategory1 = new Category(new Identification($"Generated-Category{counter}",null, randomColor));

				Goal goalX_1 = new Goal( new Identification($"Generated-Goal{counter}_1", null, randomColor), new DateSpan(DateTime.Today.AddDays(1), DateTime.Today.AddDays(8)));
				var Date = DateTime.Today.AddDays(randomGen.Next(-10, 11));
				var Time = TimeSpan.FromHours(randomGen.NextDouble() * 5);
				goalX_1.WorkHours.Add(new WorkItem(Date, Time));
				Goal goalX_1_1 = new Goal( new Identification($"Generated-Goal{counter}_1.1", null, randomColor), new DateSpan(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5)));
				goalX_1.Children.Add(goalX_1_1);
				Goal goalX_2 = new Goal(new Identification($"Generated-Goal{counter}_2", null, randomColor), new DateSpan(DateTime.Today, DateTime.Today.AddDays(10)));

				CBCategory1.Children.AddRange(new Collection<Goal> { goalX_1, goalX_2 });
				CBCategory1.WorkSessions.Add(new DayTime((DayOfWeek)randomGen.Next(7), new DoubleTime((0.0 + counter, 1.0 + counter))));
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
