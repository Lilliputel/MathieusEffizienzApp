using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using LogicLayer.Extensions;
using LogicLayer.Stores;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;

namespace LogicLayer.Views {

#warning refactoring nötig?
	public class SettingsViewModel : ViewModelBase {

		#region private fields
		private readonly SettingsStore _SettingsStore;
		private readonly ViewModelStore _ViewModels;
		private ICommand? _CommandChangeTheme;
		private ICommand? _CommandChangeCountDirection;
		#endregion

		#region public properties
		public string ThemeButton { get; private set; }
		public CultureInfo SelectedCulture {
			get => _SettingsStore.CurrentCulture;
			set {
				if( value != _SettingsStore.CurrentCulture )
					_SettingsStore.SetCulture( value );
			}
		}
		public CultureInfo[] Cultures { get; private set; }
		#endregion

		#region public commands
		public ICommand CommandChangeTheme => _CommandChangeTheme ??=
			new RelayCommand( parameter => _SettingsStore.SwitchTheme() );
		public ICommand CommandChangeCountDirection => _CommandChangeCountDirection ??=
			new RelayCommand( parameter => _ViewModels.Pomodoro.Clock.UpdateCountDirection() );
		#endregion

		#region constructor
		public SettingsViewModel( ViewModelStore viewModelStore, SettingsStore settingsStore ) {
			_ViewModels = viewModelStore;
			_SettingsStore = settingsStore;
			_SettingsStore.BoolSettingChanged += UpdateBoolSettings;
			_SettingsStore.ObjectSettingChanged += UpdateObjectSettings;

			ThemeButton = _SettingsStore.DarkMode ? "Light!" : "Dark!";
			Cultures = CultureInfo.GetCultures( CultureTypes.SpecificCultures );
		}
		#endregion

		#region private helper methods
		private void UpdateBoolSettings( BoolSettingsEnum setting, bool value ) {
			if( setting == BoolSettingsEnum.DarkMode )
				ThemeButton = value ? "Light!" : "Dark!";
		}
		private void UpdateObjectSettings( ObjectSettingsEnum setting, object value ) {
			if( setting == ObjectSettingsEnum.Culture )
				Debug.WriteLine( $"Updated the Culture with {value}" );
		}
		#endregion

	}
}
