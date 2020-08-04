using System.Windows.Controls;
using FrontLayer.Utility;

namespace FrontLayer.Views {

	public partial class PlanungView : UserControl {

		public PlanungView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Planung);
		}

		~PlanungView() { }
	}
}
