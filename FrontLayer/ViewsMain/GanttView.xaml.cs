using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class GanttView : UserControl {

		public GanttView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Gantt);
		}

		~GanttView() { }
	}
}
