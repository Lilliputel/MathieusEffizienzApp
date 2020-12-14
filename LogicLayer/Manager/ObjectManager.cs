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


		#region public properties
		public static IRepository DataService { get; }
		public static ObservableCollection<Category> CategoryList { get; private set; }
			= new ObservableCollection<Category>();
		public static WeekPlan WeekPlan { get; }
			= new WeekPlan();
		#endregion

		#region constructor
		static ObjectManager() {
#if XML
			string _FilePath = @"S:\TESTING\Effizienz\";
			DataService = new XMLCollectionHandler<Category>(nameof(CategoryList), _FilePath);
			( DataService as XMLCollectionHandler<Category> )!.ErrorOccured += ErrorOccured;  
#elif SQLite
			DataService = new SQLiteRepository();
#else
			DataService = new MockDataService();
#endif
		}
		#endregion

		#region public methods
		public static void LoadCategories() {
			CategoryList = DataService.LoadAll();
			foreach( Category? category in CategoryList ) {
				foreach( DoubleTime time in category.WorkPlan )
					Task.Run( () => WeekPlan.AddItemToDayAsync( time ) );
			}
			CategoryList.CollectionChanged += SubscribeWorkPlans;
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
			if( sender is Category category is false )
				return;
			if( e.Action == NotifyCollectionChangedAction.Add ) {
				if( e.NewItems is { } )
					foreach( DoubleTime time in e.NewItems )
						Task.Run( () => WeekPlan.AddItemToDayAsync( time ) );
			}
			else if( e.Action is NotifyCollectionChangedAction.Remove ) {
				if( e.OldItems is { } )
					foreach( DoubleTime time in e.OldItems )
						Task.Run( () => WeekPlan.RemoveItemFromDay( time ) );
			}
			else if( e.Action is NotifyCollectionChangedAction.Replace ) {
				if( e.OldItems is { } && e.NewItems is { } ) {
					foreach( DoubleTime time in e.OldItems )
						Task.Run( () => WeekPlan.RemoveItemFromDay( time ) );
					foreach( DoubleTime time in e.NewItems )
						Task.Run( () => WeekPlan.AddItemToDayAsync( time ) );
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
