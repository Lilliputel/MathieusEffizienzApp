using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.Generic;

namespace LogicLayer.Views {
	public class DashboardViewModel : ViewModelBase {

		#region private fields

		#endregion

		#region public properties
		public ICollection<Category> CategoryList { get; }
		#endregion

		#region constructor
		public DashboardViewModel( ICollection<Category> categoryList ) {
			CategoryList = categoryList;
		}
		#endregion

		#region methods

		#endregion

	}
}
