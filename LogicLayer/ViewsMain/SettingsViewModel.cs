using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class SettingsViewModel : ViewModelBase {

		#region fields

		private ICommand? _CommandChangeTheme;
		private ICommand? _CommandChangeCountDirection;

		private string themeButton;

		#endregion

		#region properties

		public ICommand CommandChangeTheme => _CommandChangeTheme ??=
			new RelayCommand(parameter => {
				ThemeManager.SwitchTheme();
			});
		public ICommand CommandChangeCountDirection => _CommandChangeCountDirection ??=
			new RelayCommand(parameter => {
				ViewModelManager.Pomodoro.Clock.UpdateCountDirection();
			});

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
			ThemeManager.DarkModeChanged += UpdateThemeButton;
			this.themeButton = ThemeManager.IsDarkMode ? "Light!" : "Dark!";
		}

		#endregion

		#region methods

		private void UpdateThemeButton( bool isDarkMode ) {
			this.ThemeButton = isDarkMode ? "Light!" : "Dark!";
		}

		#endregion
	}
}
