using ModelLayer.Enums;
using System;

namespace ModelLayer.Classes {
	public class PomodoroModes {

		#region public methods
		public WorkModeEnum Next( WorkModeEnum currentWorkMode )
			=> currentWorkMode switch
			{
				WorkModeEnum.Stop => WorkModeEnum.Work,
				WorkModeEnum.Work => WorkModeEnum.Break,
				WorkModeEnum.Break => WorkModeEnum.Work,
				WorkModeEnum.DelayWork => WorkModeEnum.Work,
				WorkModeEnum.DelayBreak => WorkModeEnum.Break,
				_ => throw new NotImplementedException( "A nonexisting workMode was passed to the WorkModeCycler!" )
			};
		public WorkModeEnum Delay( WorkModeEnum currentWorkMode )
			 => currentWorkMode switch
			 {
				 WorkModeEnum.Stop => WorkModeEnum.DelayWork,
				 WorkModeEnum.Work => WorkModeEnum.DelayBreak,
				 WorkModeEnum.Break => WorkModeEnum.DelayWork,
				 WorkModeEnum.DelayWork => WorkModeEnum.DelayWork,
				 WorkModeEnum.DelayBreak => WorkModeEnum.DelayBreak,
				 _ => throw new NotImplementedException( "A nonexisting workMode was passed to the WorkModeCycler!" )
			 };
		#endregion
	}
}
