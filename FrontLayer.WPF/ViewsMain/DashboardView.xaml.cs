using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.Dashboard, true )]
	public partial class DashboardView : UserControl {

		public DashboardView() {
			InitializeComponent();
		}

		~DashboardView() { }

	}
}
