using UiLayer.Utility;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class ViewProjektÜbersicht : UserControl {

		public ViewProjektÜbersicht() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.ProjektÜbersicht);
		}

		~ViewProjektÜbersicht() { }

	}
}
