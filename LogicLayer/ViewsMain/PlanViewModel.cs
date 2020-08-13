using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Planning;
using System.Collections.ObjectModel;

namespace LogicLayer.Views {
	public class PlanViewModel : ViewModelBase {

		#region properties

		public ObservableCollection<Category> Categories
			=> ObjectManager.CategoryList;

		public WeekPlan WeekPlan
			=> ObjectManager.WeekPlan;

		#endregion

		#region constructor

		public PlanViewModel() {

		}

		#endregion

		#region methods

		#endregion

	}
}
