using ModelLayer.Utility;
using System;
using System.Xml.Serialization;

namespace ModelLayer.Planning {
	/// <summary>
	/// Enthält eine StartZeit und eine EndZeit
	/// </summary>
	public class DoubleTime : ObservableObject {

		#region fields

		private double _Start;
		private double _End;
		private double _Duration;

		#endregion

		#region properties

		[XmlAttribute("Start")]
		public double Start {
			get {
				return _Start;
			}
			set {
				if( value == _Start )
					return;
				UpdateValues(RoundToQuarter(value), this._End);
				OnPropertyChanged(nameof(Start));
			}
		}
		[XmlAttribute("End")]
		public double End {
			get {
				return _End;
			}
			set {
				if( value == _End )
					return;
				UpdateValues(this._Start, RoundToQuarter(value));
				OnPropertyChanged(nameof(End));
			}
		}
		[XmlAttribute("Duration")]
		public double Duration {
			get {
				return _Duration;
			}
			set {
				if( value == _Duration )
					return;
				UpdateValues(this._Start, this._End - this._Duration + RoundToQuarter(value));
				OnPropertyChanged(nameof(Duration));
			}
		}

		#endregion

		#region constructor

		public DoubleTime() { }

		public DoubleTime( (double start, double end) doubles ) {
			UpdateValues(RoundToQuarter(doubles.start), RoundToQuarter(doubles.end));
		}

		public DoubleTime( TimeSpan start, TimeSpan end ) {
			double startmins = start.Minutes / 60;
			double endmins = end.Minutes / 60;
			double realstart = start.Hours + startmins;
			double realend = end.Hours + endmins;
			UpdateValues(RoundToQuarter(realstart), RoundToQuarter(realend));
		}

		#endregion

		#region methods

		public (TimeSpan Start, TimeSpan End, TimeSpan Duration) GetTimeSpans() {
			TimeSpan _start = new TimeSpan( (int)Math.Floor(Start), (int)getMinutes(Start), 0 );
			TimeSpan _end = new TimeSpan( (int)Math.Floor(End), (int)getMinutes(End), 0 );
			TimeSpan _duration = new TimeSpan( (int)Math.Floor(Duration), (int)getMinutes(Duration), 0 );

			return (_start, _end, _duration);
		}

		private double RoundToQuarter( double val )
			=> Math.Round(val * 4, MidpointRounding.ToEven) / 4;

		private double getMinutes( double number )
			=> ( number - Math.Floor(number) ) * 60;

		private void UpdateValues( double start, double end ) {

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

		public override string ToString()
			=> $"{Math.Floor(Start)}:{getMinutes(Start)} - {Math.Floor(End)}:{getMinutes(End)}";

		#endregion
	}
}
