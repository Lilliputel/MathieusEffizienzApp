using DataLayer.Interfaces;
using DataLayer.MockDataService;
using DataLayer.XMLDataService;
using ModelLayer.Classes;
using ModelLayer.Planning;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace LogicLayer.Manager {
	public static class ObjectManager {

		#region fields

		private static string _FilePath = @"S:\TESTING\Effizienz\";
		private static IDataService<ObservableCollection<Category>, Category> _ObjectDataService;
		private static IDataService<Dictionary<string, bool>, KeyValuePair<string, bool>> _SettingsDataService;

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

		/// <summary>
		/// Die Globale SettingsList, welche gespeichert wird und alle Settings enthalten sollte.
		/// </summary>
		public static Dictionary<string, bool> Settings { get; set; }

		#endregion

		#region constructor

		static ObjectManager() {
			CategoryList = new ObservableCollection<Category>();
			CategoryList.CollectionChanged += SubscribeWorkPlans;
			WeekPlan = new WeekPlan();
			Settings = new Dictionary<string, bool>();


			_ObjectDataService = new MockDataService();

			//_ObjectDataService = new XMLCollectionHandler<Category>(nameof(CategoryList), _FilePath);
			//( _ObjectDataService as XMLCollectionHandler<Category> )!.ErrorOccured += ErrorOccured;

			_SettingsDataService = new XMLDictionaryHandler<string, bool>(nameof(Settings), _FilePath);
			( _SettingsDataService as XMLDictionaryHandler<string, bool> )!.ErrorOccured += ErrorOccured;

		}

		#endregion

		#region methods

		public static Category? GetCategory( Guid ID ) {
			foreach( Category category in CategoryList ) {
				if( category.ID == ID )
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
				foreach( var daytime in category.WorkTimes ) {
					Task.Run(() =>
					WeekPlan.AddItemToDayAsync(daytime.Day,
					new PlanItem(daytime.Time, category.ID, category.Color, category.Title))
					);
				}
			}
		}

		public static void SaveSettings() {
			_SettingsDataService.SaveData(Settings);
		}
		public static void LoadSettings() {
			Settings = _SettingsDataService.LoadData();
		}

		#endregion

		#region private helpers

		private static void SubscribeWorkPlans( object sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is ObservableCollection<Category> ) {
				if( e.Action is NotifyCollectionChangedAction.Add ) {
					if( e.NewStartingIndex >= 0 )
						foreach( Category? item in e.NewItems! )
							item!.WeekPlanChanged += UpdateWeekPlan;
				}
				else if( e.Action is NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( Category? item in e.OldItems )
							item!.WeekPlanChanged -= UpdateWeekPlan;
				}
				else if( e.Action is NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( Category? item in e.OldItems )
							item!.WeekPlanChanged -= UpdateWeekPlan;
						foreach( Category? item in e.NewItems )
							item!.WeekPlanChanged += UpdateWeekPlan;
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
								new PlanItem(time, category.ID, category.Color, category.Title))
								);
							}
				}
				else if( e.Action is NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( (DayOfWeek, DoubleTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category.ID, category.Color, category.Title))
								);
							}
				}
				else if( e.Action is NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( (DayOfWeek, DoubleTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category.ID, category.Color, category.Title))
								);
							}
						foreach( (DayOfWeek, DoubleTime)? item in e.NewItems )
							if( item is (DayOfWeek day, DoubleTime time) ) {
								Task.Run(() =>
								WeekPlan.AddItemToDayAsync(day,
								new PlanItem(time, category.ID, category.Color, category.Title))
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
