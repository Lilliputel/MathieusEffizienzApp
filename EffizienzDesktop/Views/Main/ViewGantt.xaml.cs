using Effizienz.Utility;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewGantt : UserControl {

		public ViewGantt() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Gantt);
		}

		~ViewGantt() { }
	}
}
