using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Interfaces;

namespace LogicLayer.Views {
	public class GanttDiagramViewModel : ViewModelBase {

		#region public properties
		public IAccountableParent<Category> CategoryList { get; }
		#endregion

		#region constructor
		public GanttDiagramViewModel( IAccountableParent<Category> categoryList )
			=> CategoryList = categoryList;
		#endregion

		#region methods

		#endregion

	}
}
