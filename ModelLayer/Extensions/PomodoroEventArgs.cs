using ModelLayer.Enums;
using System;

namespace ModelLayer.Extensions {
	public class PomodoroEventArgs : EventArgs {

		public EnumWorkMode WorkMode { get; set; }
		public TimeSpan Time { get; set; }

	}
}
