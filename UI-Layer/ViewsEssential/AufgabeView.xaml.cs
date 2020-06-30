using UiLayer.Utility;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class ViewAufgabe : UserControl {

		public ViewAufgabe() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Aufgabe);
		}

	}
}
