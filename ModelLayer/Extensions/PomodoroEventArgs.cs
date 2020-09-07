using ModelLayer.Classes;
using ModelLayer.Enums;
using System;

namespace ModelLayer.Extensions {

	public delegate void PomodoroEventHandler( PomodoroClock sender, PomodoroEventArgs e );

	public class PomodoroEventArgs : EventArgs {

		public EnumWorkMode WorkMode { get; set; }
		public TimeSpan Time { get; set; }

	}
}
