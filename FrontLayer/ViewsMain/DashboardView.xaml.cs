using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class DashboardView : UserControl {

		public DashboardView() {
			InitializeComponent();

			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Dashboard);

		}

		~DashboardView() { }

	}
}
