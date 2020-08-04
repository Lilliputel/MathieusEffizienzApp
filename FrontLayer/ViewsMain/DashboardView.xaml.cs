using System.Windows.Controls;
using FrontLayer.Utility;

namespace FrontLayer.Views {

	public partial class DashboardView : UserControl {

		public DashboardView() {
			InitializeComponent();

			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Dashboard);

		}

		~DashboardView() { }

	}
}
