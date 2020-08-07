using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.ObjectModel;

namespace LogicLayer.Views {
	public class GoalOverviewViewModel : ViewModelBase {

		#region fields

		#endregion

		#region properties

		public ObservableCollection<Category> Categories
			=> ObjectManager.CategoryList;

		#endregion

		#region constructors

		#endregion

		#region methods

		#endregion
	}
}
