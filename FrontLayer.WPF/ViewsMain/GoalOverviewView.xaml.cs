using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class GoalOverviewView : UserControl {

		public GoalOverviewView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GoalOverview;
		}

		~GoalOverviewView() { }

	}
}
