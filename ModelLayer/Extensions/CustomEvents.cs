using ModelLayer.Classes;
using ModelLayer.Enums;
using System;

namespace ModelLayer.Extensions {

	#region plan Events
	public delegate void PlanEventHandler( DateSpan plan );
	#endregion

	#region pomodoro Events
#warning I could simplify this, see above
	public delegate void PomodoroEventHandler( PomodoroClock sender, PomodoroEventArgs e );
	public class PomodoroEventArgs : EventArgs {
		public WorkModeEnum WorkMode { get; set; }
		public TimeSpan Time { get; set; }
	}
	#endregion
}
