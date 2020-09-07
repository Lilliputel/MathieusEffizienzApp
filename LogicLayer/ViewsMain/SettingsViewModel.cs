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
				ThemeButton = UpdateThemeButton();
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
