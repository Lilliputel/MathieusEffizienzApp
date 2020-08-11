using ModelLayer.Utility;
using System;
using System.Globalization;
using System.Xml.Serialization;

namespace ModelLayer.Classes {
	/// <summary>
	/// Enthält eine StartZeit und eine EndZeit
	/// </summary>
	public class DayTime : ObservableObject {

		#region fields

		private TimeSpan _Start;
		private TimeSpan _End;
		private TimeSpan _Duration;

		#endregion

		#region properties

		[XmlAttribute("Start")]
		public TimeSpan Start {
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
		public TimeSpan End {
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
				UpdateValues(this._Start, this._End - this._Duration + value);
				OnPropertyChanged(nameof(Duration));
			}
		}

		#endregion

		#region constructor

		public DayTime() {

		}

		/// <summary>
		/// Erzeugt ein DateSpan mit dem Standardformat ch-DE (01.01.00 / dd.MM.YY)
		/// </summary>
		public DayTime( string start, string end )
			: this(start, end, new CultureInfo("ch-DE")) { }

		public DayTime( string start, string end, IFormatProvider formatProvider )
			: this(TimeSpan.ParseExact(start, @"hh:mm:ss", formatProvider), TimeSpan.ParseExact(end, @"hh:mm:ss", formatProvider)) { }

		public DayTime( TimeSpan _start, TimeSpan _end ) {
			// reduziert die eingegebene Zeit auf Tag, Monat, Jahr
			TimeSpan start = new TimeSpan(_start.Hours, _start.Minutes, _start.Seconds);
			TimeSpan end = new TimeSpan(_end.Hours, _end.Minutes, _end.Seconds);

			UpdateValues(start, end);

		}

		#endregion

		#region methods

		private void UpdateValues( TimeSpan start, TimeSpan end ) {

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
