using System;
using System.Globalization;

namespace LogicLayer.Stores {
	public class SettingsStore {

		#region CountingUp
		private bool _CountingUp;
		public bool CountingUp {
			get => _CountingUp;
			set {
				_CountingUp = value;
				CountingUpChanged?.Invoke( _CountingUp );
			}
		}
		public Action<bool>? CountingUpChanged { get; init; }
		public void SwitchCountingUp()
			=> CountingUp = CountingUp is false;
		#endregion

		#region CurrentCulture
		private CultureInfo _CurrentCulture = CultureInfo.CreateSpecificCulture( "ch-de" );
		public CultureInfo CurrentCulture {
			get => _CurrentCulture;
			set {
				_CurrentCulture = value;
				CurrentCultureChanged?.Invoke( _CurrentCulture );
			}
		}
		public Action<CultureInfo>? CurrentCultureChanged { get; init; }
		#endregion

		#region DarkMode
		private bool _DarkMode;
		public bool DarkMode {
			get => _DarkMode;
			set {
				_DarkMode = value;
				DarkModeChanged?.Invoke( _DarkMode );
			}
		}
		public Action<bool>? DarkModeChanged { get; init; }
		public void SwitchDarkMode()
			=> DarkMode = DarkMode is false;
		#endregion

		#region PlanIntervallMinutes
		private TimeSpan _PlanIntervall;
		public TimeSpan PlanIntervall {
			get => _PlanIntervall;
			set {
				_PlanIntervall = value;
				PlanIntervallMinutesChanged?.Invoke( _PlanIntervall );
			}
		}
		public Action<TimeSpan>? PlanIntervallMinutesChanged { get; init; }
		#endregion

	}
}
