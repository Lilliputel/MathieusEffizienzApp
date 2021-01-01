using DataLayer;
using LogicLayer.BaseViewModels;
using ModelLayer.Classes;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LogicLayer.Views {
	public class PlanViewModel : ViewModelBase {

		#region private fields
		private readonly IRepository _DataService;
		#endregion

		#region public properties
		public ICollectionView CategoryList { get; }
		public WeekPlan WeekPlan { get; }
		#endregion

		#region constructor
		public PlanViewModel( IRepository dataService ) {
			_DataService = dataService;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
			WeekPlan = new WeekPlan();
			_DataService.LoadAll().CollectionChanged += SubscribeWeekPlans;
			foreach( Category category in _DataService.LoadAll() )
				foreach( DoubleTime time in category.WorkPlan )
					Task.Run( () => WeekPlan.AddItemToDayAsync( time ) );

		}
		#endregion

		#region private eventhandler
#warning DEBUG this!! I am not sure if New- and OldItems can be casted maybe
		private void SubscribeWeekPlans( object? sender, NotifyCollectionChangedEventArgs e ) {
			if( e.OldItems is IList<Category> oldItems )
				foreach( Category item in oldItems )
					item.WorkPlan.CollectionChanged -= UpdateWeekPlan;
			if( e.NewItems is IList<Category> newItems )
				foreach( Category item in newItems )
					item.WorkPlan.CollectionChanged += UpdateWeekPlan;
		}
		private void UpdateWeekPlan( object? sender, NotifyCollectionChangedEventArgs e ) {
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
