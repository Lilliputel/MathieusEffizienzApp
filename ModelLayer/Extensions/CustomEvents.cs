using ModelLayer.Classes;
using ModelLayer.Enums;
using System;

namespace ModelLayer.Extensions {

	#region plan Events
	public delegate void PlanEventHandler( DateSpan plan );
	#endregion

	#region worktime Events
	public delegate void WorkTimeEventHandler( DateSpan plan );
	#endregion

	#region pomodoro Events
	public delegate void PomodoroEventHandler( WorkModeEnum workMode, TimeSpan time );
	#endregion
}
