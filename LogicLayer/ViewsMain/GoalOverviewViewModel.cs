using DataLayer;
using LogicLayer.BaseViewModels;
using System.ComponentModel;
using System.Windows.Data;

namespace LogicLayer.Views {
	public class GoalOverviewViewModel : ViewModelBase {

		#region private fields
		private readonly IRepository _DataService;
		#endregion

		#region public properties
		public ICollectionView CategoryList { get; }
		#endregion

		#region constructors
		public GoalOverviewViewModel( IRepository dataService ) {
			_DataService = dataService;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
		}
		#endregion

		#region methods

		#endregion
	}
}
