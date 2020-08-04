using System;

namespace ModelLayer.Classes {

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
			this.Start = roundDateTime(start);
			this.Ende = roundDateTime(ende);
		}

		#endregion

		#region methods

		private DateTime roundDateTime( DateTime datum ) {
			return new DateTime(
				datum.Year,
				datum.Month,
				datum.Day,
				datum.Hour,
				roundViertel(datum.Minute),
				0);
		}
		private int roundViertel( int wert ) {
			if( wert <= 7 ) {
				return 0;
			}
			else if( wert <= 22 ) {
				return 15;
			}
			else if( wert <= 37 ) {
				return 30;
			}
			else if( wert <= 52 ) {
				return 45;
			}
			else {
				return 60;
			}
		}

		public TimeSpan GetTimeSpan() {
			return Ende - Start;
		}

		#endregion

	}
}
