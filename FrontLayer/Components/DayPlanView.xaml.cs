using System.Windows.Controls;

namespace FrontLayer.Components {

	public partial class DayPlanView : HeaderedItemsControl {

		public DayPlanView() {
			InitializeComponent();
		}

		private void Root_MouseDown( object sender, System.Windows.Input.MouseButtonEventArgs e ) {
			double y = e.GetPosition(this).Y;
		}
	}
}
