using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class PlanView : UserControl {

		public PlanView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Plan);
		}

		~PlanView() { }
	}
}
