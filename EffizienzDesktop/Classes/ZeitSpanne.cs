using System;

namespace Effizienz.Classes {

	/// <summary>
	/// Eine Klasse um eine StartZeit, EndZeit und eine automatisch generierte Zeitspanne zu definieren.
	/// </summary>
	public class ZeitSpanne {

		#region properties

		public DateTime Start { get; set; }
		public DateTime Ende { get; set; }

		#endregion

		#region constructor

		public ZeitSpanne( DateTime start, DateTime ende ) {
			this.Start = start;
			this.Ende = ende;
		}

		#endregion

		#region methods

		public TimeSpan GetTimeSpan() {
			return Ende - Start;
		}

		#endregion

	}
}
