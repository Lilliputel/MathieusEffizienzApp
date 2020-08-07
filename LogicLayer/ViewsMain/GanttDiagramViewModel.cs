using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.ObjectModel;

namespace LogicLayer.Views {
	public class GanttDiagramViewModel : ViewModelBase {

		#region properties

		public ObservableCollection<Category> Categories
			=> ObjectManager.CategoryList;

		#endregion

		#region constructor

		#endregion

		#region methods

		#endregion

	}
}
