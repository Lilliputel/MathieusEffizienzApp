using System.Windows.Controls;
using UiLayer.Utility;

namespace UiLayer.Views {

	public partial class ProjektÜbersichtView : UserControl {

		public ProjektÜbersichtView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.ProjektÜbersicht);
		}

		~ProjektÜbersichtView() { }

	}
}
