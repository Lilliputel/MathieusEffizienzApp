using ModelLayer.Planning;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace FrontLayer.Components {

	public partial class DayPlanView : HeaderedItemsControl {

		public DayPlanView() {
			InitializeComponent();
		}

		private void MouseDown( object sender, MouseButtonEventArgs e ) {
			double y = e.GetPosition(this).Y;

			if( e.OriginalSource is Border border ) {
				Debug.WriteLine("The origin is a Border!");
				if( border.DataContext is PlanItem planItem ) {



				}
			}
			else if( e.OriginalSource is Grid grid ) {
				Debug.WriteLine("The origin is the Grid!");


				double test = grid.RowDefinitions[1].ActualHeight;
			}
		}

		private void MouseMove( object sender, MouseEventArgs e ) {

		}

		private void MouseLeftButtonUp( object sender, MouseButtonEventArgs e ) {

		}

	}
}
