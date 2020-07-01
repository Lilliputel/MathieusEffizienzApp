using System.Windows.Controls;
using UiLayer.Utility;

namespace UiLayer.Views {

	public partial class DashboardView : UserControl {

		public DashboardView() {
			InitializeComponent();

			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Dashboard);

		}

		~DashboardView() { }

	}
}
