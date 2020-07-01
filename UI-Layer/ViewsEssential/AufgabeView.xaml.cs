using System.Windows.Controls;
using UiLayer.Utility;

namespace UiLayer.Views {

	public partial class AufgabeView : UserControl {

		public AufgabeView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Aufgabe);
		}

	}
}
