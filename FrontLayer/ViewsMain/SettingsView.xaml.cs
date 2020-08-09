using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class SettingsView : UserControl {

		public SettingsView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.Settings);
		}

		~SettingsView() { }

	}
}
