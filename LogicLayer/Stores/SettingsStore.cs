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
		public void ChangeDakrMode( bool? isDarkMode = null ) {
			if( isDarkMode is bool isDarkModeNew )
				DarkMode = isDarkModeNew;
			else
				DarkMode = !DarkMode;
			DarkModeChanged?.Invoke( DarkMode );
		}
		public void ChangeCountDirection( bool? countsUp = null ) {
			if( countsUp is bool up )
				CountdirectionUp = up;
			else
				CountdirectionUp = !CountdirectionUp;
			CountDirectionChanged?.Invoke( CountdirectionUp );
		}
		public void SetCultureInfo( CultureInfo culture ) {
			CurrentCulture = culture;
			CultureInfoChanged?.Invoke( CurrentCulture );
		}
		#endregion

	}
}
