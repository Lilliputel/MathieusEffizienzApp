using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class SettingsView : UserControl {

		public SettingsView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.Settings);
		}

		~SettingsView() { }

	}
}
