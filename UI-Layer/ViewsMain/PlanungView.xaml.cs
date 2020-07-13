using System.Windows.Controls;
using UiLayer.Utility;

namespace UiLayer.Views {

	public partial class PlanungView : UserControl {

		public PlanungView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Planung);
		}

		~PlanungView() { }
	}
}
