using DataLayer.Interfaces;
using DataLayer.MockDataService;
using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace LogicLayer.Manager {
	public static class ObjectManager {

		#region fields
		private static IDataService<ObservableCollection<Category>, Category> _ObjectDataService;
		#endregion

		#region properties
		/// <summary>
		/// Die Globale CategoryList, welche gespeichert wird und alle Categories enthalten sollte.
		/// </summary>
		public static ObservableCollection<Category> CategoryList { get; set; }
		/// <summary>
		/// Der Globale WeekPlan, welche nicht gespeichert wird und alle DayTimes mit den Kategorien und Farben enthalten sollte.
		/// </summary>
		public static WeekPlan WeekPlan { get; set; }
		#endregion

		#region constructor
		static ObjectManager() {
			CategoryList = new ObservableCollection<Category>();
			CategoryList.CollectionChanged += SubscribeWorkPlans;
			WeekPlan = new WeekPlan();

			_ObjectDataService = new MockDataService();
			//string _FilePath = @"S:\TESTING\Effizienz\";
			//_ObjectDataService = new XMLCollectionHandler<Category>(nameof(CategoryList), _FilePath);
			//( _ObjectDataService as XMLCollectionHandler<Category> )!.ErrorOccured += ErrorOccured;
		}
		#endregion

		#region public methods
		public static Category? GetCategory( Guid ID ) {
			foreach( Category category in CategoryList ) {
				if( category.ID.Guid == ID )
					return category;
			}
			return null;
		}
		public static void SaveCategories() {
			_ObjectDataService.SaveData(CategoryList);
		}
		public static void LoadCategories() {
			foreach( var category in _ObjectDataService.LoadData() ) {
				CategoryList.Add(category);
				foreach( var daytime in category.WorkSessions ) {
					Task.Run(() =>
					WeekPlan.AddItemToDayAsync(daytime.Day,
					new PlanItem(daytime.Time, category.ID.Guid, category.ID.Color, category.ID.Title))
					);
				}
			}
		}
		#endregion

		#region private helper methods
		private static void SubscribeWorkPlans( object sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is ObservableCollection<Category> ) {
				if( e.Action is NotifyCollectionChangedAction.Add ) {
					if( e.NewStartingIndex >= 0 )
						foreach( Category? item in e.NewItems! )
							item!.WorkSessions.CollectionChanged += UpdateWeekPlan;
				}
				else if( e.Action is NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( Category? item in e.OldItems )
							item!.WorkSessions.CollectionChanged -= UpdateWeekPlan;
				}
				else if( e.Action is NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( Category? item in e.OldItems )
							item!.WorkSessions.CollectionChanged -= UpdateWeekPlan;
						foreach( Category? item in e.NewItems )
							item!.WorkSessions.CollectionChanged += UpdateWeekPlan;
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
								new PlanItem(time, category.ID.Guid, category.ID.Color, category.ID.Title))
								);
							}
				}
				else if( e.Action is NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( (DayOfWeek, DoubleTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category.ID.Guid, category.ID.Color, category.ID.Title))
								);
							}
				}
				else if( e.Action is NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( (DayOfWeek, DoubleTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category.ID.Guid, category.ID.Color, category.ID.Title))
								);
							}
						foreach( (DayOfWeek, DoubleTime)? item in e.NewItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.AddItemToDayAsync(day,
								new PlanItem(time, category.ID.Guid, category.ID.Color, category.ID.Title))
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
