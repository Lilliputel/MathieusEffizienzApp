using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.ObjectModel;

namespace LogicLayer.Views {
	public class GanttDiagramViewModel : ViewModelBase {

		#region public properties
		public ObservableCollection<Category> Categories { get; private set; }
		#endregion

		#region constructor
		public GanttDiagramViewModel( ObservableCollection<Category> categories )
			=> Categories = categories;
		#endregion

		#region methods

		#endregion

	}
}
