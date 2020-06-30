using UiLayer.Utility;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class ViewOptionen : UserControl {

		public ViewOptionen() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Optionen);
		}

		~ViewOptionen() { }

	}
}
