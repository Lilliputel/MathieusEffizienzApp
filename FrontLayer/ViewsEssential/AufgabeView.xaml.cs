using System.Windows.Controls;
using FrontLayer.Utility;

namespace FrontLayer.Views {

	public partial class AufgabeView : UserControl {

		public AufgabeView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Aufgabe);
		}

	}
}
