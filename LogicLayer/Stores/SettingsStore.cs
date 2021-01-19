using System;
using System.Globalization;

namespace LogicLayer.Stores {
	public class SettingsStore {

		#region public properties
		public bool DarkMode { get; private set; }
		public bool CountdirectionUp { get; private set; }
		public CultureInfo CurrentCulture { get; private set; } = CultureInfo.CreateSpecificCulture( "en-US" );
		#endregion

		#region propertychanged-Actions
		public Action<bool>? DarkModeChanged { get; init; }
		public Action<bool>? CountDirectionChanged { get; init; }
		public Action<CultureInfo>? CultureInfoChanged { get; init; }
		#endregion

		#region public methods
		public void ChangeDarkMode( bool? isDarkMode = null ) {
			DarkMode = isDarkMode is bool isDarkModeNew ? isDarkModeNew : !DarkMode;
			DarkModeChanged?.Invoke( DarkMode );
		}
		public void ChangeCountDirection( bool? countsUp = null ) {
			CountdirectionUp = countsUp is bool countsUpNew ? countsUpNew : !CountdirectionUp;
			CountDirectionChanged?.Invoke( CountdirectionUp );
		}
		public void SetCultureInfo( CultureInfo culture ) {
			CurrentCulture = culture;
			CultureInfoChanged?.Invoke( CurrentCulture );
		}
		#endregion

	}
}
