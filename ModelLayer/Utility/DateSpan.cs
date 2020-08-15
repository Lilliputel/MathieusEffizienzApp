using System;
using System.Xml.Serialization;

namespace ModelLayer.Utility {
	/// <summary>
	/// Enthält ein StartDatum, ein EndDatum generierte Zeitspanne.
	/// </summary>
	public class DateSpan : ObservableObject {

		#region fields

		private DateTime _Start;
		private DateTime _End;
		private TimeSpan _Duration;
		#endregion

		#region properties

		[XmlAttribute("Start")]
		public DateTime Start {
			get {
				return _Start;
			}
			set {
				if( value == _Start )
					return;
				UpdateValues(value, this._End);
				OnPropertyChanged(nameof(Start));
			}
		}
		[XmlAttribute("End")]
		public DateTime End {
			get {
				return _End;
			}
			set {
				if( value == _End )
					return;
				UpdateValues(this._Start, value);
				OnPropertyChanged(nameof(End));
			}
		}
		[XmlAttribute("Duration")]
		public TimeSpan Duration {
			get {
				return _Duration;
			}
			set {
				if( value == _Duration )
					return;
				UpdateValues(this._Start, this._End - Duration + value);
				OnPropertyChanged(nameof(Duration));

			}
		}

		#endregion

		#region constructor

		public DateSpan() {

		}

		/// <summary>
		/// Erzeugt ein DateSpan mit dem Standardformat ch-DE (01.01.00 / dd.MM.YY)
		/// </summary>
		public DateSpan( string start, string end, IFormatProvider formatProvider )
			: this(DateTime.ParseExact(start, @"dd.MM.yy", formatProvider), DateTime.ParseExact(end, @"dd.MM.yy", formatProvider)) { }

		public DateSpan( DateTime _start, DateTime _end ) {
			// reduziert die eingegebene Zeit auf Tag, Monat, Jahr
			DateTime start = new DateTime(_start.Year, _start.Month, _start.Day);
			DateTime ende = new DateTime(_end.Year, _end.Month, _end.Day);
		}

		#endregion

		#region methods

		private void UpdateValues( DateTime start, DateTime end ) {

			// setzt die Korrekte reihenfolge der beiden Daten
			if( end >= start ) {
				this._Start = start;
				this._End = end;
			}
			else {
				this._Start = end;
				this._End = start;
			}

			// setzt die Dauer
			this._Duration = this._End - this._Start;
		}

		#endregion

	}
}
