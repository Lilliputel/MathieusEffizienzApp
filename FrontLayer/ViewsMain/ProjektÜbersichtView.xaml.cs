using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class ProjektÜbersichtView : UserControl {

		public ProjektÜbersichtView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.ProjektÜbersicht);
		}

		~ProjektÜbersichtView() { }

	}
}
