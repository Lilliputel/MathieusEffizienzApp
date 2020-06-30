using UiLayer.Utility;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class ViewDashboard : UserControl {

		public ViewDashboard() {
			InitializeComponent();

			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Dashboard);

		}

		~ViewDashboard() { }

	}
}
