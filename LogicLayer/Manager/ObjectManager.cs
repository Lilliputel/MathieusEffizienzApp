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
			Goal CBGoal1_1 = new Goal("Generated-Goal{counter}_1.1", DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5)){
				Time = new TimeSpan(3, 12, 20)
			};
			Goal CBGoal2 = new Goal("Generated-Goal{counter}_2", DateTime.Today, DateTime.Today.AddDays(10)){
				Time = new TimeSpan(10, 30, 0)
			};

			CBGoal1.AddChild(CBGoal1_1);

			CBCategory1.AddChild(CBGoal1);
			CBCategory1.AddChild(CBGoal2);

			CategoryList.Add(CBCategory1);

			CBCategory1.WorkTimes.Add(((DayOfWeek)randomGen.Next(7), new DayTime(TimeSpan.FromHours(0 + counter), TimeSpan.FromHours(1 + counter))));

			counter++;
		}

		private static void SubscribeWorkPlans( object sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is Category cat )
				cat.WorkTimes.CollectionChanged += UpdateWeekPlan;
		}

		private static void UpdateWeekPlan( object sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is Category cat )
				switch( e.Action ) {
				case NotifyCollectionChangedAction.Add:
					foreach( var item in e.NewItems )
						if( item is (DayOfWeek day, DayTime time) )
							Task.Run(() => WeekPlan.AddItemToDayAsync(day, new PlanItem(time, cat.ID, cat.Color)));
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach( var item in e.OldItems )
						if( item is (DayOfWeek day, DayTime time) )
							WeekPlan.RemoveItemFromDay(day, new PlanItem(time, cat.ID, cat.Color));
					break;
				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					break;
				}

		}

		#endregion
	}
}
