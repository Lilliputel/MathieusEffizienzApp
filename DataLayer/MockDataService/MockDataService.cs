using DataLayer.Interfaces;
using ModelLayer.Classes;
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
				Category CBCategory1 = new Category($"Generated-Category{counter}", randomColor){
					Children = new ObservableCollection<Goal> {
						new Goal($"Generated-Goal{counter}_1", new DateSpan( DateTime.Today.AddDays(1), DateTime.Today.AddDays(8)) ){
							Time = new TimeSpan(1, 2, 3),
							Color = randomColor,
							Children = new ObservableCollection<Goal>{
								new Goal($"Generated-Goal{counter}_1.1", new DateSpan( DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5)) ){
									Time = new TimeSpan(3, 12, 20),
									Color = randomColor
								}
							}
						},
						new Goal($"Generated-Goal{counter}_2", new DateSpan( DateTime.Today, DateTime.Today.AddDays(10)) ){
							Time = new TimeSpan(10, 30, 0),
							Color = randomColor
						}
					}
				};
				CBCategory1.WorkTimes.Add(((DayOfWeek)randomGen.Next(7), new DoubleTime((0.0 + counter, 1.0 + counter))));
				zwischenSpeicher.Add(CBCategory1);
			}

			return zwischenSpeicher;
		}

		public void SaveData( ObservableCollection<Category> Collection ) {
			Debug.WriteLine($"Tried to save the {nameof(Collection)}[{Collection}] in the MockDataService");
		}

	}
}
