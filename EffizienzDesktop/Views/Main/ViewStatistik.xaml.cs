using Effizienz.Utility;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewStatistik : UserControl {

		public ViewStatistik() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Statistik);
		}

		~ViewStatistik() { }
	}
}
