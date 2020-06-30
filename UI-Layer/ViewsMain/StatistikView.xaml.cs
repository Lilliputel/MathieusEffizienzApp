using UiLayer.Utility;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class ViewStatistik : UserControl {

		public ViewStatistik() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Statistik);
		}

		~ViewStatistik() { }
	}
}
