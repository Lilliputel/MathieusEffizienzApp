using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.Generic;

namespace LogicLayer.Views {
	public class GoalOverviewViewModel : ViewModelBase {

		#region private fields

		#endregion

		#region public properties
		public ICollection<Category> CategoryList { get; }
		#endregion

		#region constructors
		public GoalOverviewViewModel( ICollection<Category> categoryList ) {
			CategoryList = categoryList;
		}
		#endregion

		#region methods

		#endregion
	}
}
