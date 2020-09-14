using LogicLayer.Extensions;

namespace LogicLayer.Manager {
	public static class ThemeManager {

		#region public properties

		public static bool IsDarkMode { get; private set; }

		#endregion

		#region public events

		public static event BoolChangedEventHandler? DarkModeChanged;

		#endregion

		#region methods

		public static void SwitchTheme( bool? isDarkMode = null ) {
			if( isDarkMode is bool isDarkModeNew )
				IsDarkMode = isDarkModeNew;
			else
				IsDarkMode = !IsDarkMode;
			DarkModeChanged?.Invoke(IsDarkMode);
		}

		#endregion

	}
}
