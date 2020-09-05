using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class NewGoalView : UserControl {

		public NewGoalView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.NewGoal);
		}

	}
}
