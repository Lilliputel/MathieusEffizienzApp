using System;
using System.Globalization;
using System.Xml.Serialization;

namespace ModelLayer.Structs {

	/// <summary>
	/// Ein struct um eine StartZeit, EndZeit und eine automatisch generierte Zeitspanne zu definieren.
	/// </summary>
	public struct DateSpan {

		#region properties
		[XmlAttribute("Start")]
		public DateTime Start { get; set; }
		[XmlAttribute("End")]
		public DateTime End { get; set; }
		[XmlIgnore]
		public TimeSpan TimeSpan { get; set; }


		#endregion

		#region constructor
		/// <summary>
		/// Erzeugt ein DateSpan mit dem Standardformat ch-DE (01.01.00 / dd.MM.YY)
		/// </summary>
		public DateSpan( string start, string end )
			: this(start, end, new CultureInfo("ch-DE")) { }

		public DateSpan( string start, string end, IFormatProvider formatProvider )
			: this(DateTime.ParseExact(start, @"dd.MM.yy", formatProvider), DateTime.ParseExact(end, @"dd.MM.yy", formatProvider)) { }

		public DateSpan( DateTime _start, DateTime _end ) {
			// reduziert die eingegebene Zeit auf Tag, Monat, Jahr
			DateTime start = new DateTime(_start.Year, _start.Month, _start.Day);
			DateTime ende = new DateTime(_end.Year, _end.Month, _end.Day);

			// setzt die Korrekte reihenfolge der beiden Daten
			if( ende >= start ) {
				this.Start = start;
				this.End = ende;
				this.TimeSpan = ende - start;
			}
			else {
				this.Start = ende;
				this.End = start;
				this.TimeSpan = start - ende;
			}
		}


		#endregion

		#region methods

		#endregion

	}
}
