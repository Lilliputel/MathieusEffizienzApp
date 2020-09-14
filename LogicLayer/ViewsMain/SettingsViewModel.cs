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
				( ViewModelManager.GetViewModel(EnumViewModels.Pomodoro) as PomodoroViewModel )!.Clock.UpdateCountDirection();
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
			UpdateThemeButton(ThemeManager.IsDarkMode);
		}

		#endregion

		#region methods

		private void UpdateThemeButton( bool isDarkMode ) {
			this.ThemeButton = isDarkMode ? "Light!" : "Dark!";
		}

		#endregion
	}
}
