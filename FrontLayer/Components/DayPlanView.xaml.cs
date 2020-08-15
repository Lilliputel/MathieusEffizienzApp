using System.Windows.Controls;
using System.Windows.Input;

namespace FrontLayer.Components {

	public partial class DayPlanView : HeaderedItemsControl {

		public DayPlanView() {
			InitializeComponent();
		}

		private void Root_MouseDown( object sender, MouseButtonEventArgs e ) {
			double y = e.GetPosition(this).Y;
		}

		private void Grid_MouseMove( object sender, MouseEventArgs e ) {

		}

		private void Grid_MouseLeftButtonUp( object sender, MouseButtonEventArgs e ) {

		}
		 
	}
}
