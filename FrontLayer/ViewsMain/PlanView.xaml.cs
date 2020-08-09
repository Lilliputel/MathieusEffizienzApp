using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class PlanView : UserControl {

		public PlanView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.Plan);
		}

		~PlanView() { }
	}
}
