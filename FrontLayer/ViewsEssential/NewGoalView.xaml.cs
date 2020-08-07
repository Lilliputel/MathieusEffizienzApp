using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class NewGoalView : UserControl {

		public NewGoalView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.NewGoal);
		}

	}
}
