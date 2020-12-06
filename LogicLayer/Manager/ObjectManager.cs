using DataLayer;
using ModelLayer.Classes;
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
		public static ObservableCollection<Category> CategoryList { get; } = new ObservableCollection<Category>();
		public static WeekPlan WeekPlan { get; } = new WeekPlan();
		#endregion

		#region constructor
		static ObjectManager() {
			CategoryList.CollectionChanged += SubscribeWorkPlans;

			_ObjectDataService = new MockDataService();
			//string _FilePath = @"S:\TESTING\Effizienz\";
			//_ObjectDataService = new XMLCollectionHandler<Category>(nameof(CategoryList), _FilePath);
			//( _ObjectDataService as XMLCollectionHandler<Category> )!.ErrorOccured += ErrorOccured;
		}
		#endregion

		#region public methods
		public static void SaveCategories()
			=> _ObjectDataService.SaveData( CategoryList );
		public static void LoadCategories() {
			foreach( Category? category in _ObjectDataService.LoadData() ) {
				CategoryList.Add( category );
				foreach( (DayOfWeek Day, DoubleTime Time) daytime in category.WorkPlan ) {
					Task.Run( () =>
					 WeekPlan.AddItemToDayAsync( daytime.Day, daytime.Time )
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
							if( item is (DayOfWeek day, DoubleTime time ) )
								Task.Run( () => WeekPlan.AddItemToDayAsync( day, time ) );
				}
				else if( e.Action is NotifyCollectionChangedAction.Remove ) {
					if( e.OldStartingIndex >= 0 )
						foreach( (DayOfWeek, DoubleTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DoubleTime time ) )
								Task.Run( () => WeekPlan.RemoveItemFromDay( day, time ) );
				}
				else if( e.Action is NotifyCollectionChangedAction.Replace ) {
					if( e.NewStartingIndex >= 0 && e.OldStartingIndex >= 0 ) {
						foreach( (DayOfWeek, DoubleTime)? item in e.OldItems )
							if( item is (DayOfWeek day, DoubleTime time ) )
								Task.Run( () => WeekPlan.RemoveItemFromDay( day, time ) );
						foreach( (DayOfWeek, DoubleTime)? item in e.NewItems )
							if( item is (DayOfWeek day, DoubleTime time ) )
								Task.Run( () => WeekPlan.AddItemToDayAsync( day, time ) );

					}
				}
			}
		}
		private static void ErrorOccured( object sender, ErrorEventArgs e ) {
			switch( e.GetException() ) {
				case FileNotFoundException fNFE:
					AlertManager.FileNotFound( fNFE.FileName ?? $"unknown, from: {sender}", "" );
					return;
				case ArgumentException aE:
					AlertManager.InputInkorrekt( $"{aE.Message}" );
					return;
				default:
					Debug.WriteLine( $"{sender} threw {e.GetException()} \nwith the Message: {e.GetException().Message}" );
					return;
			}
		}

		#endregion
	}
}
