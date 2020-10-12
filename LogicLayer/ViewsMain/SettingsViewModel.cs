using LogicLayer.Commands;
using LogicLayer.Extensions;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class SettingsViewModel : ViewModelBase {

		#region fields
		private ICommand? _CommandChangeTheme;
		private ICommand? _CommandChangeCountDirection;
		#endregion

		#region public properties
		public string ThemeButton { get; private set; }
		public CultureInfo SelectedCulture {
			get { return SettingsManager.CurrentCulture; }
			set {
				if( value != SettingsManager.CurrentCulture )
					SettingsManager.SetCulture(value);
			}
		}
		public CultureInfo[] Cultures { get; private set; }
		#endregion

		#region public commands
		public ICommand CommandChangeTheme => _CommandChangeTheme ??=
			new RelayCommand(parameter => {
				SettingsManager.SwitchTheme();
			});
		public ICommand CommandChangeCountDirection => _CommandChangeCountDirection ??=
			new RelayCommand(parameter => {
				ViewModelManager.Pomodoro.Clock.UpdateCountDirection();
			});
		#endregion

		#region constructor
		public SettingsViewModel() {
			SettingsManager.BoolSettingChanged += UpdateBoolSettings;
			SettingsManager.ObjectSettingChanged += UpdateObjectSettings;

			this.ThemeButton = SettingsManager.DarkMode ? "Light!" : "Dark!";
			this.Cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
		}
		#endregion

		#region private helper methods
		private void UpdateBoolSettings( BoolSettingsEnum setting, bool value ) {
			if( setting == BoolSettingsEnum.DarkMode )
				this.ThemeButton = value ? "Light!" : "Dark!";
		}
		private void UpdateObjectSettings( ObjectSettingsEnum setting, object value ) {
			if( setting == ObjectSettingsEnum.Culture )
				Debug.WriteLine($"Updated the Culture with {value}");
		}

		#endregion
	}
}
