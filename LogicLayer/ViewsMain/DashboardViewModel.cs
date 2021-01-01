using DataLayer;
using LogicLayer.BaseViewModels;
using System.ComponentModel;
using System.Windows.Data;

namespace LogicLayer.Views {
	public class DashboardViewModel : ViewModelBase {

		#region private fields
		private readonly IRepository _DataService;
		#endregion

		#region public properties
		public ICollectionView CategoryList { get; }
		#endregion

		#region constructor
		public DashboardViewModel( IRepository dataService ) {
			_DataService = dataService;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
		}
		#endregion

		#region methods

		#endregion

	}
}
