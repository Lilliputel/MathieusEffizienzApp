using System.Windows.Controls;
using FrontLayer.Utility;

namespace FrontLayer.Views {

	public partial class GanttView : UserControl {

		public GanttView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Gantt);
		}

		~GanttView() { }
	}
}
