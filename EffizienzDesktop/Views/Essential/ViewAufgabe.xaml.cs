using Effizienz.Utility;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewAufgabe : UserControl {

		public ViewAufgabe() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Aufgabe);
		}

	}
}
