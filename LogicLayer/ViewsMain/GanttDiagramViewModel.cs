using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.Generic;

namespace LogicLayer.Views {
	public class GanttDiagramViewModel : ViewModelBase {

		#region public properties
		public ICollection<Category> CategoryList { get; }
		#endregion

		#region constructor
		public GanttDiagramViewModel( ICollection<Category> categoryList ) {
			CategoryList = categoryList;
		}
		#endregion

		#region methods

		#endregion

	}
}
