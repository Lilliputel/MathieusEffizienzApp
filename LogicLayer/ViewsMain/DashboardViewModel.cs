using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Interfaces;

namespace LogicLayer.Views {
	public class DashboardViewModel : ViewModelBase {

		#region private fields

		#endregion

		#region public properties
		public IAccountableParent<Category> CategoryList { get; }
		#endregion

		#region constructor
		public DashboardViewModel( IAccountableParent<Category> categoryList )
			=> CategoryList = categoryList;
		#endregion

		#region methods

		#endregion

	}
}
