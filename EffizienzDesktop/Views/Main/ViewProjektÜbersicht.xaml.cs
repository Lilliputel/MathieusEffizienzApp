using Effizienz.Utility;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewProjektÜbersicht : UserControl {

		public ViewProjektÜbersicht() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Statistik);
		}

		~ViewProjektÜbersicht() { }

	}
}
