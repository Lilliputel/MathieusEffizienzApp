using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class NewGoalView : UserControl {

		public NewGoalView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.NewGoal);
		}

	}
}
