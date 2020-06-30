using UiLayer.Utility;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class ViewPlanung : UserControl {

		public ViewPlanung() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Planung);
		}

		~ViewPlanung() { }

	}
}
