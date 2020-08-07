using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class GoalOverviewView : UserControl {

		public GoalOverviewView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.GoalOverview);
		}

		~GoalOverviewView() { }

	}
}
