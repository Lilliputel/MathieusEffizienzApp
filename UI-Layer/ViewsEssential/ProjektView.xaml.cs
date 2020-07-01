using System.Windows.Controls;
using UiLayer.Utility;

namespace UiLayer.Views {

	public partial class ProjektView : UserControl {

		public ProjektView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Projekt);
		}
	}
}
