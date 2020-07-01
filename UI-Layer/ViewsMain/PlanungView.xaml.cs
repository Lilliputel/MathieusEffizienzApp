using UiLayer.Utility;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class PlanungView : UserControl {

		public PlanungView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Planung);
		}

		~PlanungView() { }

	}
}
