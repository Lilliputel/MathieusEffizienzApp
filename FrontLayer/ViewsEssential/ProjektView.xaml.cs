using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class ProjektView : UserControl {

		public ProjektView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Projekt);
		}
	}
}
