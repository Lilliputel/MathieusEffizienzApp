using ModelLayer.Enums;
using ModelLayer.Utility;
using System;

namespace ModelLayer.Classes {
	public class PomodoroTimes : ObservableObject {

		#region public properties
		public TimeSpan DurationWorkCycle { get; private set; }
		public TimeSpan DurationBreakCycle { get; private set; }
		public TimeSpan DurationDelayCycle { get; private set; }
		#endregion

		#region constructor
		public PomodoroTimes( TimeSpan durationWorkCycle, TimeSpan durationBreakCycle, TimeSpan durationDelayCycle ) {
			DurationWorkCycle = durationWorkCycle;
			DurationBreakCycle = durationBreakCycle;
			DurationDelayCycle = durationDelayCycle;
		}
		#endregion

		#region public methods
		public TimeSpan GetModeTime( WorkModeEnum workMode )
			=> workMode switch
			{
				WorkModeEnum.Work => DurationWorkCycle,
				WorkModeEnum.Break => DurationBreakCycle,
				WorkModeEnum.DelayWork => DurationDelayCycle,
				WorkModeEnum.DelayBreak => DurationDelayCycle,
				_ => DurationWorkCycle
			};
		#endregion
	}
}
