using System.Windows.Controls;
using FrontLayer.Utility;

namespace FrontLayer.Views {

	public partial class ProjektÜbersichtView : UserControl {

		public ProjektÜbersichtView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.ProjektÜbersicht);
		}

		~ProjektÜbersichtView() { }

	}
}
