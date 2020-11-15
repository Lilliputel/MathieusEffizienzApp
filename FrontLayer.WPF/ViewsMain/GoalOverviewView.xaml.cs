using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.Overview, true )]
	public partial class GoalOverviewView : UserControl {

		public GoalOverviewView() {
			InitializeComponent();
		}

		~GoalOverviewView() { }

	}
}
