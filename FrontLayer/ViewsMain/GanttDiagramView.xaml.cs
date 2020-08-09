using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class GanttDiagramView : UserControl {

		public GanttDiagramView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.GanttDiagram);
		}

		~GanttDiagramView() { }
	}
}
