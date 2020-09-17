﻿using ModelLayer.Utility;
using System;
using System.Xml.Serialization;

namespace ModelLayer.Classes {
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

		public DateSpan( DateTime start, DateTime end ) {
			// reduziert die eingegebene Zeit auf Tag, Monat, Jahr
			DateTime _start = new DateTime(start.Year, start.Month, start.Day);
			DateTime _ende = new DateTime(end.Year, end.Month, end.Day);
			UpdateValues(_start, _ende);
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