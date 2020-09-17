using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class DashboardView : UserControl {

		public DashboardView() {
			InitializeComponent();

			this.DataContext = ViewModelManager.Dashboard;

		}

		~DashboardView() { }

	}
}
