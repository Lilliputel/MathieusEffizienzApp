using Effizienz.Utility;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewOptionen : UserControl {

		public ViewOptionen() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Optionen);
		}

		~ViewOptionen() { }

	}
}
