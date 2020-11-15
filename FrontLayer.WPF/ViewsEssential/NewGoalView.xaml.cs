using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.NewGoal, true )]
	public partial class NewGoalView : UserControl {

		public NewGoalView() {
			InitializeComponent();
		}

	}
}
