using ModelLayer.Classes;
using ModelLayer.Planning;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LogicLayer.Manager {
	public static class ObjectManager {

		#region properties

		/// <summary>
		/// Die Globale CategoryList, welche gespeichert wird und alle Categories enthalten sollte.
		/// </summary>
		public static ObservableCollection<Category> CategoryList { get; set; }

		/// <summary>
		/// Der Globale WeekPlan, welche nicht gespeichert wird und alle DayTimes mit den Kategorien und Farben enthalten sollte.
		/// </summary>
		public static WeekPlan WeekPlan { get; set; }



		/// <summary>
		/// Die Globale SettingsList, welche gespeichert wird und alle Settings enthalten sollte.
		/// </summary>
		public static Dictionary<string, bool> Settings { get; set; }

		private static int counter = 0;

		#endregion

		#region constructor
		static ObjectManager() {

			CategoryList = new ObservableCollection<Category>();
			CategoryList.CollectionChanged += SubscribeWorkPlans;
			WeekPlan = new WeekPlan();
			Settings = new Dictionary<string, bool>();
		}


		#endregion

		#region methods

		public static void GenerateObjects() {
			Random randomGen = new Random();
			Color randomColor =
				Color.FromArgb(
				(byte)randomGen.Next(255),
				(byte)randomGen.Next(255),
				(byte)randomGen.Next(255),
				(byte)randomGen.Next(255));

			// new Category
			Category CBCategory1 = new Category($"Generated-Category{counter}", randomColor);

			Goal CBGoal1 = new Goal($"Generated-Goal{counter}_1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(8)){
				Time = new TimeSpan(1, 2, 3)
			};
			Goal CBGoal1_1 = new Goal($"Generated-Goal{counter}_1.1", DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5)){
				Time = new TimeSpan(3, 12, 20)
			};
			Goal CBGoal2 = new Goal($"Generated-Goal{counter}_2", DateTime.Today, DateTime.Today.AddDays(10)){
				Time = new TimeSpan(10, 30, 0)
			};

			CBGoal1.AddChild(CBGoal1_1);

			CBCategory1.AddChild(CBGoal1);
			CBCategory1.AddChild(CBGoal2);

			CategoryList.Add(CBCategory1);
			CategoryList[CategoryList.IndexOf(CBCategory1)].WorkTimes.Add(((DayOfWeek)randomGen.Next(7), new DayTime(0.0 + counter, 1.0 + counter)));

			counter++;
			if( counter > 23 )
				counter = 0;
		}

		private static void SubscribeWorkPlans( object sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is ObservableCollection<Category> ) {
				if( e.Action == NotifyCollectionChangedAction.Add ) {
					if( e.NewStartingIndex >= 0 )
						foreach( Category item in e.NewItems )
							item.WeekPlanChanged += UpdateWeekPlan;
				}
				else if( e.Action == NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( Category item in e.OldItems )
							item.WeekPlanChanged -= UpdateWeekPlan;
				}
				else if( e.Action == NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( Category item in e.OldItems )
							item.WeekPlanChanged -= UpdateWeekPlan;
						foreach( Category item in e.NewItems )
							item.WeekPlanChanged += UpdateWeekPlan;
					}
				}
			}
		}

		private static void UpdateWeekPlan( object sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is Category category ) {
				if( e.Action == NotifyCollectionChangedAction.Add ) {
					if( e.NewStartingIndex >= 0 )
						foreach( (DayOfWeek day, DayTime time) item in e.NewItems ) {
							Task.Run(() =>
							WeekPlan.AddItemToDayAsync(item.day,
								new PlanItem(item.time, category.ID, category.Color))
							);
						}
				}
				else if( e.Action == NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0
						&& e.OldItems is ICollection<(DayOfWeek, DayTime)> oldItems )
						foreach( (DayOfWeek day, DayTime time) in oldItems )
							Task.Run(() =>
							WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category.ID, category.Color))
							);
				}
				else if( e.Action == NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0
						&& e.NewItems is ICollection<(DayOfWeek, DayTime)> newItems
						&& e.OldItems is ICollection<(DayOfWeek, DayTime)> oldItems ) {
						foreach( (DayOfWeek day, DayTime time) in oldItems )
							Task.Run(() =>
							WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category.ID, category.Color))
							);
						foreach( (DayOfWeek day, DayTime time) in newItems )
							Task.Run(() =>
							WeekPlan.AddItemToDayAsync(day,
								new PlanItem(time, category.ID, category.Color))
							);
					}
				}
			}
		}

		#endregion
	}
}
