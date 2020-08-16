using ModelLayer.Classes;
using ModelLayer.Planning;
using ModelLayer.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LogicLayer.Manager {
	public sealed class SingletonObjectManager : ObservableObject {

		#region fields

		private static readonly SingletonObjectManager _Instance = new SingletonObjectManager();

		private WeekPlan _WeekPlan;

		#endregion

		#region properties

		/// <summary>
		/// Singleton-Instance
		/// </summary>
		public static SingletonObjectManager Instance {
			get {
				return _Instance;
			}
		}

		/// <summary>
		/// Die Globale CategoryList, welche gespeichert wird und alle Categories enthalten sollte.
		/// </summary>
		public ObservableCollection<Category> CategoryList { get; set; }

		/// <summary>
		/// Der Globale WeekPlan, welche nicht gespeichert wird und alle DayTimes mit den Kategorien und Farben enthalten sollte.
		/// </summary>
		public WeekPlan WeekPlan {
			get {
				return _WeekPlan;
			}
			set {
				if( value == _WeekPlan )
					return;
				_WeekPlan = value;
				OnPropertyChanged(nameof(WeekPlan));
			}
		}

		/// <summary>
		/// Die Globale SettingsList, welche gespeichert wird und alle Settings enthalten sollte.
		/// </summary>
		public Dictionary<string, bool> Settings { get; set; }

		private int counter = 0;

		#endregion

		#region constructor

		static SingletonObjectManager() {
		}

		private SingletonObjectManager() {
			CategoryList = new ObservableCollection<Category>();
			CategoryList.CollectionChanged += SubscribeWorkPlans;
			_WeekPlan = new WeekPlan();
			Settings = new Dictionary<string, bool>();
		}

		#endregion

		#region methods

		public void GenerateObjects() {
			Random randomGen = new Random();
			Color randomColor =
				Color.FromArgb(
				(byte)randomGen.Next(255),
				(byte)randomGen.Next(255),
				(byte)randomGen.Next(255),
				(byte)randomGen.Next(255));

			// new Category
			Category CBCategory1 = new Category($"Generated-Category{counter}", randomColor);

			Goal CBGoal1 = new Goal($"Generated-Goal{counter}_1", new DateSpan( DateTime.Today.AddDays(1), DateTime.Today.AddDays(8)) ){
				Time = new TimeSpan(1, 2, 3)
			};
			Goal CBGoal1_1 = new Goal($"Generated-Goal{counter}_1.1", new DateSpan( DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5)) ){
				Time = new TimeSpan(3, 12, 20)
			};
			Goal CBGoal2 = new Goal($"Generated-Goal{counter}_2", new DateSpan( DateTime.Today, DateTime.Today.AddDays(10)) ){
				Time = new TimeSpan(10, 30, 0)
			};

			CBGoal1.AddChild(CBGoal1_1);

			CBCategory1.AddChild(CBGoal1);
			CBCategory1.AddChild(CBGoal2);

			CategoryList.Add(CBCategory1);
			CategoryList[CategoryList.IndexOf(CBCategory1)].WorkTimes.Add(((DayOfWeek)randomGen.Next(7), new DayTime((0.0 + counter, 1.0 + counter))));

			counter++;
			if( counter > 23 )
				counter = 0;
		}

		private void SubscribeWorkPlans( object sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is ObservableCollection<Category> ) {
				if( e.Action == NotifyCollectionChangedAction.Add ) {
					if( e.NewStartingIndex >= 0 )
						foreach( Category? item in e.NewItems! )
							item!.WeekPlanChanged += UpdateWeekPlan;
				}
				else if( e.Action == NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( Category? item in e.OldItems )
							item!.WeekPlanChanged -= UpdateWeekPlan;
				}
				else if( e.Action == NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( Category? item in e.OldItems )
							item!.WeekPlanChanged -= UpdateWeekPlan;
						foreach( Category? item in e.NewItems )
							item!.WeekPlanChanged += UpdateWeekPlan;
					}
				}
			}
		}

		private void UpdateWeekPlan( object? sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is Category category ) {
				if( e.Action == NotifyCollectionChangedAction.Add ) {
					if( e.NewStartingIndex >= 0
						&& e.NewItems is IList newItems )
						foreach( (DayOfWeek day, DayTime time) item in newItems ) {
							Task.Run(() =>
							WeekPlan.AddItemToDayAsync(item.day,
								new PlanItem(item.time, category.ID, category.Color, category.Title))
							);
						}
				}
				else if( e.Action == NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						// && e.OldItems is ICollection<(DayOfWeek, DayTime)> oldItems )
						foreach( (DayOfWeek day, DayTime time) item in e.OldItems )
							Task.Run(() =>
							WeekPlan.RemoveItemFromDay(item.day,
								new PlanItem(item.time, category.ID, category.Color, category.Title))
							);
				}
				else if( e.Action == NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						//&& e.NewItems is ICollection<(DayOfWeek, DayTime)> newItems
						//&& e.OldItems is ICollection<(DayOfWeek, DayTime)> oldItems ) {
						foreach( (DayOfWeek day, DayTime time) item in e.OldItems )
							Task.Run(() =>
							WeekPlan.RemoveItemFromDay(item.day,
								new PlanItem(item.time, category.ID, category.Color, category.Title))
							);
						foreach( (DayOfWeek day, DayTime time) item in e.NewItems )
							Task.Run(() =>
							WeekPlan.AddItemToDayAsync(item.day,
								new PlanItem(item.time, category.ID, category.Color, category.Title))
							);
					}
				}
			}
		}

		#endregion
	}
}
