using DataLayer;
using LogicLayer.BaseViewModels;
using ModelLayer.Classes;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;

namespace LogicLayer.Views {
	public class PlanViewModel : ViewModelBase {

		#region private fields
		private readonly IRepository _DataService;
		#endregion

		#region public properties
		public ICollectionView CategoryList { get; }
		public WeekPlan WeekPlan { get; } = new WeekPlan();
		#endregion

		#region constructor
		public PlanViewModel( IRepository dataService ) {
			_DataService = dataService;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );

			CategoryList.CollectionChanged += SubscribeWorkplans;
			foreach( Category category in CategoryList ) {
				category.WorkPlan.CollectionChanged += UpdateWeekPlan;
				foreach( DoubleTime time in category.WorkPlan )
					WeekPlan.AddItemToDay( time );
			}
		}
		#endregion

		#region private eventhandler
		private void SubscribeWorkplans( object? sender, NotifyCollectionChangedEventArgs e ) {
			if( e.OldItems?.Count > 0 )
				foreach( Category item in e.OldItems )
					item.WorkPlan.CollectionChanged -= UpdateWeekPlan;
			if( e.NewItems?.Count > 0 )
				foreach( Category item in e.NewItems )
					item.WorkPlan.CollectionChanged += UpdateWeekPlan;
		}
		private void UpdateWeekPlan( object? sender, NotifyCollectionChangedEventArgs e ) {
			if( e.OldItems?.Count > 0 )
				foreach( DoubleTime time in e.OldItems )
					WeekPlan.RemoveItemFromDay( time );
			if( e.NewItems?.Count > 0 )
				foreach( DoubleTime time in e.NewItems )
					WeekPlan.AddItemToDay( time );
		}
		#endregion

	}
}
