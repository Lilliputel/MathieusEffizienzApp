using ModelLayer.Utility;
using PropertyChanged;
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

		#endregion

		#region properties

		[XmlAttribute(nameof(Start))]
		[AlsoNotifyFor(nameof(Duration))]
		public double Start {
			get { return _Start; }
			set { UpdateValues(RoundToQuarter(value), this._End); }
		}
		[XmlAttribute(nameof(End))]
		[AlsoNotifyFor(nameof(Duration))]
		public double End {
			get { return _End; }
			set { UpdateValues(this._Start, RoundToQuarter(value)); }
		}
		[XmlIgnore]
		public double Duration
			=> RoundToQuarter(this.Start - this.End);

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
		}

		public override string ToString()
			=> $"{new TimeSpan((int)Math.Floor(Start), (int)getMinutes(Start), 0).ToString(@"hh\:mm")} - {new TimeSpan((int)Math.Floor(End), (int)getMinutes(End), 0).ToString(@"hh\:mm")}";

		#endregion
	}
}
