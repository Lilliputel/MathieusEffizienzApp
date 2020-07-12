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

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Project must have a Titel, a ParentID, a Start and an EndDate
		/// </summary>
		public ZeitSpanne() { }

		public ZeitSpanne( DateTime start_ende )
			: this(start_ende, start_ende) { }

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
