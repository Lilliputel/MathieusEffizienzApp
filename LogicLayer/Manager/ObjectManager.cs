using DataLayer.Interfaces;
using DataLayer.MockDataService;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace LogicLayer.Manager {
	public static class ObjectManager {

		#region private fields
		private static IDataService<ObservableCollection<Category>, Category> _ObjectDataService;
		#endregion

		#region public properties
		/// <summary>
		/// Die Globale CategoryList, welche gespeichert wird und alle Categories enthalten sollte.
		/// </summary>
		public static IAccountableParent<Category> CategoryList { get; } = new CategoryList();
		/// <summary>
		/// Der Globale WeekPlan, welche nicht gespeichert wird und alle DayTimes mit den Kategorien und Farben enthalten sollte.
		/// </summary>
		public static WeekPlan WeekPlan { get; } = new WeekPlan();
		#endregion

		#region constructor
		static ObjectManager() {
			CategoryList.Children.CollectionChanged += SubscribeWorkPlans;

			_ObjectDataService = new MockDataService();
			//string _FilePath = @"S:\TESTING\Effizienz\";
			//_ObjectDataService = new XMLCollectionHandler<Category>(nameof(CategoryList), _FilePath);
			//( _ObjectDataService as XMLCollectionHandler<Category> )!.ErrorOccured += ErrorOccured;
		}
		#endregion

		#region public methods
		public static void SaveCategories() {
			_ObjectDataService.SaveData(CategoryList.Children);
		}
		public static void LoadCategories() {
			foreach( var category in _ObjectDataService.LoadData() ) {
				CategoryList.Children.Add(category);
				foreach( var daytime in category.WorkPlan ) {
					Task.Run(() =>
					WeekPlan.AddItemToDayAsync(daytime.Day,
					new PlanItem(daytime.Time, category))
					);
				}
			}
		}
		#endregion

		#region private helper methods
		private static void SubscribeWorkPlans( object sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is CategoryList ) {
				if( e.Action is NotifyCollectionChangedAction.Add ) {
					if( e.NewStartingIndex >= 0 )
						foreach( Category? item in e.NewItems! )
							item!.WorkPlan.CollectionChanged += UpdateWeekPlan;
				}
				else if( e.Action is NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( Category? item in e.OldItems )
							item!.WorkPlan.CollectionChanged -= UpdateWeekPlan;
				}
				else if( e.Action is NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( Category? item in e.OldItems )
							item!.WorkPlan.CollectionChanged -= UpdateWeekPlan;
						foreach( Category? item in e.NewItems )
							item!.WorkPlan.CollectionChanged += UpdateWeekPlan;
					}
				}
			}
		}
		private static void UpdateWeekPlan( object? sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is Category category ) {
				if( e.Action == NotifyCollectionChangedAction.Add ) {
					if( e.NewStartingIndex >= 0 )
						foreach( (DayOfWeek, DoubleTime)? item in e.NewItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.AddItemToDayAsync(day,
								new PlanItem(time, category))
								);
							}
				}
				else if( e.Action is NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( (DayOfWeek, DoubleTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category))
								);
							}
				}
				else if( e.Action is NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( (DayOfWeek, DoubleTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category))
								);
							}
						foreach( (DayOfWeek, DoubleTime)? item in e.NewItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.AddItemToDayAsync(day,
								new PlanItem(time, category))
								);
							}
					}
				}
			}
		}
		private static void ErrorOccured( object sender, ErrorEventArgs e ) {
			switch( e.GetException() ) {
			case FileNotFoundException fNFE:
				AlertManager.FileNotFound(fNFE.FileName ?? $"unknown, from: {sender}", "");
				return;
			case ArgumentException aE:
				AlertManager.InputInkorrekt($"{aE.Message}");
				return;
			default:
				Debug.WriteLine($"{sender} threw {e.GetException()} \nwith the Message: {e.GetException().Message}");
				return;
			}
		}

		#endregion
	}
}
