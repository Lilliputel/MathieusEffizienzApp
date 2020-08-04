using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class AufgabeView : UserControl {

		public AufgabeView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Aufgabe);
		}

	}
}
