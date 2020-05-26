using Effizienz.Utility;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewPlanung : UserControl {

		public ViewPlanung() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Planung);
		}

		~ViewPlanung() { }

	}
}
