using System.Windows.Controls;
using UiLayer.Utility;

namespace UiLayer.Views {

	public partial class GanttView : UserControl {

		public GanttView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Gantt);
		}

		~GanttView() { }
	}
}
