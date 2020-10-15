using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Interfaces;

namespace LogicLayer.Views {
	public class GoalOverviewViewModel : ViewModelBase {

		#region private fields

		#endregion

		#region public properties
		public IAccountableParent<Category> CategoryList { get; }
		#endregion

		#region constructors
		public GoalOverviewViewModel( IAccountableParent<Category> categoryList )
			=> CategoryList = categoryList;
		#endregion

		#region methods

		#endregion
	}
}
