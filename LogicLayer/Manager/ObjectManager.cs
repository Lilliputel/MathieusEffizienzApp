using DataLayer.Interfaces;
using DataLayer.XMLDataService;
using LogicLayer.Utility;
using ModelLayer.Classes;
using ModelLayer.Planning;
using ModelLayer.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;

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

		private static int counter = 0;

		#endregion

		#region constructor

		static ObjectManager() {
			CategoryList = new ObservableCollection<Category>();
			CategoryList.CollectionChanged += SubscribeWorkPlans;
			WeekPlan = new WeekPlan();
			Settings = new Dictionary<string, bool>();


			_ObjectDataService = new XMLCollectionHandler<Category>(nameof(CategoryList), _FilePath);
			( _ObjectDataService as XMLCollectionHandler<Category> )!.ErrorOccured += ErrorOccured;
			_SettingsDataService = new XMLDictionaryHandler<string, bool>(nameof(Settings), _FilePath);
			( _SettingsDataService as XMLDictionaryHandler<string, bool> )!.ErrorOccured += ErrorOccured;

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
			CategoryList = _ObjectDataService.LoadData();
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

		private static void UpdateWeekPlan( object? sender, NotifyCollectionChangedEventArgs e ) {
			if( sender is Category category ) {
				if( e.Action == NotifyCollectionChangedAction.Add ) {
					if( e.NewStartingIndex >= 0 )
						foreach( (DayOfWeek, DayTime)? item in e.NewItems )
							if( item is (DayOfWeek day, DayTime time) ) {
								Task.Run(() =>
								WeekPlan.AddItemToDayAsync(day,
								new PlanItem(time, category.ID, category.Color, category.Title))
								);
							}
				}
				else if( e.Action == NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( (DayOfWeek, DayTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DayTime time) ) {
								Task.Run(() =>
								WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category.ID, category.Color, category.Title))
								);
							}
				}
				else if( e.Action == NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( (DayOfWeek, DayTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DayTime time) ) {
								Task.Run(() =>
								WeekPlan.RemoveItemFromDay(day,
								new PlanItem(time, category.ID, category.Color, category.Title))
								);
							}
						foreach( (DayOfWeek, DayTime)? item in e.NewItems )
							if( item is (DayOfWeek day, DayTime time) ) {
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
				MessageBoxDisplayer.FileNotFound(fNFE.FileName ?? $"unknown, from: {sender}", "");
				return;
			case ArgumentException aE:
				MessageBoxDisplayer.InputInkorrekt($"{aE.Message}");
				return;
			default:
				Debug.WriteLine($"{sender} threw {e.GetException()} \nwith the Message: {e.GetException().Message}");
				return;
			}
		}

		#endregion
	}
}
