using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.ObjectModel;

namespace LogicLayer.Views {
	public class GoalOverviewViewModel : ViewModelBase {

		#region private fields

		#endregion

		#region public properties
		public ObservableCollection<Category> Categories { get; private set; }
		#endregion

		#region constructors
		public GoalOverviewViewModel( ObservableCollection<Category> categories )
			=> Categories = categories;
		#endregion

		#region methods

		#endregion
	}
}
