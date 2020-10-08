using ModelLayer.Planning;
using System;

namespace ModelLayer.Classes {
	public class DayTime {

		#region public properties
		public DoubleTime Time { get; set; }
		public DayOfWeek Day { get; set; }
		#endregion

		#region constructor
		public DayTime( DayOfWeek day, DoubleTime time ) {
			this.Time = time;
			this.Day = day;
		}
		public DayTime() { }
		#endregion
	}
}
