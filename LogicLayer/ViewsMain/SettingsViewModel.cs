using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class SettingsViewModel : ViewModelBase {

		#region fields

		private ICommand? _commandChangeTheme;

		private string themeButton;

		#endregion

		#region properties

		public ICommand CommandChangeTheme => _commandChangeTheme ??
			( _commandChangeTheme = new RelayCommand(parameter => {
				ThemeManager.SwitchTheme();
				ThemeButton = UpdateThemeButton();
			}) );

		public string ThemeButton {
			get {
				return themeButton;
			}
			set {
				themeButton = value;
				OnPropertyChanged(nameof(ThemeButton));
			}
		}

		#endregion

		#region constructors

		public SettingsViewModel() {
			this.themeButton = UpdateThemeButton();
		}

		#endregion

		#region methods

		private string UpdateThemeButton() {
			return ( ThemeManager.DarkMode ? "Light!" : "Dark!" );
		}
		#endregion
	}
}
