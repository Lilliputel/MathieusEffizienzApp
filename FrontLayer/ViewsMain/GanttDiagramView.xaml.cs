using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class GanttDiagramView : UserControl {

		public GanttDiagramView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.GanttDiagram);
		}

		~GanttDiagramView() { }
	}
}
