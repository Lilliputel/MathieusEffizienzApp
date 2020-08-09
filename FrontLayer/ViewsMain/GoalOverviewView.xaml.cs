using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class GoalOverviewView : UserControl {

		public GoalOverviewView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.GoalOverview);
		}

		~GoalOverviewView() { }

	}
}
