using DataLayer;
using ModelLayer.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
			DataService = new XMLRepository(nameof(CategoryList), _FilePath);
#elif SQLite
			DataService = new SQLiteRepository();
#else
			DataService = new MockRepository();
#endif
			CategoryList.CollectionChanged += SubscribeWeekPlans;
		}
		#endregion

		#region public methods
		public static void LoadCategories() {
			CategoryList = DataService.LoadAll();
			foreach( Category category in CategoryList )
				foreach( DoubleTime time in category.WorkPlan )
					Task.Run( () => WeekPlan.AddItemToDayAsync( time ) );
			CategoryList.CollectionChanged += SubscribeWeekPlans;
		}
		#endregion

		#region private helper methods
#warning DEBUG this!! I am not sure if New- and OldItems can be casted 
		private static void SubscribeWeekPlans( object? sender, NotifyCollectionChangedEventArgs e ) {
			if( e.OldItems is IList<Category> oldItems )
				foreach( Category item in oldItems )
					item.WorkPlan.CollectionChanged -= UpdateWeekPlan;
			if( e.NewItems is IList<Category> newItems )
				foreach( Category item in newItems )
					item.WorkPlan.CollectionChanged += UpdateWeekPlan;
		}
		private static void UpdateWeekPlan( object? sender, NotifyCollectionChangedEventArgs e ) {
			if( e.OldItems is IList<DoubleTime> oldItems )
				foreach( DoubleTime time in oldItems )
					Task.Run( () => WeekPlan.RemoveItemFromDay( time ) );
			if( e.NewItems is IList<DoubleTime> newItems )
				foreach( DoubleTime time in newItems )
					Task.Run( () => WeekPlan.AddItemToDayAsync( time ) );
		}
		#endregion

	}
}
