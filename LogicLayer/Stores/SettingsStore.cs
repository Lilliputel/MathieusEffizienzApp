using LogicLayer.Extensions;
using System.Globalization;

namespace LogicLayer.Stores {
	public class SettingsStore {

		#region public properties
		public bool DarkMode { get; private set; }
		public bool PomodoroCountUp { get; private set; }
		public CultureInfo CurrentCulture { get; private set; } = CultureInfo.CreateSpecificCulture( "en-US" );
		#endregion

		#region public events
		public event BoolSettingChangedEventHandler? BoolSettingChanged;
		public event ObjectSettingChangedEventHandler? ObjectSettingChanged;
		#endregion

		#region public methods
		public void SwitchTheme( bool? isDarkMode = null ) {
			if( isDarkMode is bool isDarkModeNew )
				DarkMode = isDarkModeNew;
			else
				DarkMode = !DarkMode;
			OnBoolChanged( BoolSettingsEnum.DarkMode, DarkMode );
		}
		public void ChangeCountDirection( bool? countsUp = null ) {
			if( countsUp is bool up )
				PomodoroCountUp = up;
			else
				PomodoroCountUp = !PomodoroCountUp;
			OnBoolChanged( BoolSettingsEnum.CountDirection, PomodoroCountUp );
		}
		public void SetCulture( CultureInfo culture ) {
			CurrentCulture = culture;
			OnObjectChanged( ObjectSettingsEnum.Culture, culture );
		}
		#endregion

		#region private helper methods
		private void OnBoolChanged( BoolSettingsEnum setting, bool value )
			=> BoolSettingChanged?.Invoke( setting, value );
		private void OnObjectChanged( ObjectSettingsEnum setting, object value )
			=> ObjectSettingChanged?.Invoke( setting, value );
		#endregion

	}
}
