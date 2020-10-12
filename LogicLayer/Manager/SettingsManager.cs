using LogicLayer.Extensions;
using System.Globalization;

namespace LogicLayer.Manager {
	public static class SettingsManager {

		#region public properties
		public static bool DarkMode { get; private set; }
		public static bool PomodoroCountUp { get; private set; }
		public static CultureInfo CurrentCulture { get; private set; } = CultureInfo.CreateSpecificCulture("en-US");
		#endregion

		#region public events
		public static event BoolSettingChangedEventHandler? BoolSettingChanged;
		public static event ObjectSettingChangedEventHandler? ObjectSettingChanged;
		#endregion

		#region public methods
		public static void SwitchTheme( bool? isDarkMode = null ) {
			if( isDarkMode is bool isDarkModeNew )
				DarkMode = isDarkModeNew;
			else
				DarkMode = !DarkMode;
			OnBoolChanged(BoolSettingsEnum.DarkMode, DarkMode);
		}
		public static void ChangeCountDirection( bool? CountsUp = null ) {
			if( CountsUp is bool up )
				PomodoroCountUp = up;
			else
				PomodoroCountUp = !PomodoroCountUp;
			OnBoolChanged(BoolSettingsEnum.CountDirection, PomodoroCountUp);
		}
		public static void SetCulture( CultureInfo culture ) {
			CurrentCulture = culture;
			OnObjectChanged(ObjectSettingsEnum.Culture, culture);
		}
		#endregion

		#region private helper methods
		private static void OnBoolChanged( BoolSettingsEnum setting, bool value )
			=> BoolSettingChanged?.Invoke(setting, value);
		private static void OnObjectChanged( ObjectSettingsEnum setting, object value )
			=> ObjectSettingChanged?.Invoke(setting, value);
		#endregion

	}
}
