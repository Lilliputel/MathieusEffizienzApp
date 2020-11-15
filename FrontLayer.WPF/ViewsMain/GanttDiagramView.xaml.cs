using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.Gantt, true )]
	public partial class GanttDiagramView : UserControl {

		public GanttDiagramView() {
			InitializeComponent();
		}

		~GanttDiagramView() { }
	}
}
