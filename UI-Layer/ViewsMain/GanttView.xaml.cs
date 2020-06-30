using UiLayer.Utility;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class ViewGantt : UserControl {

		public ViewGantt() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Gantt);
		}

		~ViewGantt() { }
	}
}
