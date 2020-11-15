using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.Settings, true )]
	public partial class SettingsView : UserControl {

		public SettingsView() {
			InitializeComponent();
		}

		~SettingsView() { }

	}
}
