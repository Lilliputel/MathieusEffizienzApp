using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class PlanungView : UserControl {

		public PlanungView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Planung);
		}

		~PlanungView() { }
	}
}
