using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class SettingsView : UserControl {

		public SettingsView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Settings);
		}

		~SettingsView() { }

	}
}
