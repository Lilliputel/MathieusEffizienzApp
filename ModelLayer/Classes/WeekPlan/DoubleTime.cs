using ModelLayer.Utility;
using PropertyChanged;
using System;

namespace ModelLayer.Classes {
	public class DoubleTime : ObservableObject {

		#region private fields

		private double _Start;
		private double _End;

		#endregion

		#region public properties
		[AlsoNotifyFor(nameof(Duration))]
		public double Start {
			get { return _Start; }
			set { UpdateValues(RoundToQuarter(value), _End); }
		}
		[AlsoNotifyFor(nameof(Duration))]
		public double End {
			get { return _End; }
			set { UpdateValues(_Start, RoundToQuarter(value)); }
		}
		public double Duration
			=> RoundToQuarter(Start - End);
		#endregion

		#region constructor
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
		public DoubleTime() { }
		#endregion

		#region public methods
		public (TimeSpan Start, TimeSpan End, TimeSpan Duration) GetTimeSpans() {
			TimeSpan _start = new TimeSpan( (int)Math.Floor(Start), (int)getMinutes(Start), 0 );
			TimeSpan _end = new TimeSpan( (int)Math.Floor(End), (int)getMinutes(End), 0 );
			TimeSpan _duration = new TimeSpan( (int)Math.Floor(Duration), (int)getMinutes(Duration), 0 );
			return (_start, _end, _duration);
		}
		public override string ToString()
			=> $"{new TimeSpan((int)Math.Floor(Start), (int)getMinutes(Start), 0).ToString(@"hh\:mm")} - {new TimeSpan((int)Math.Floor(End), (int)getMinutes(End), 0).ToString(@"hh\:mm")}";
		#endregion

		#region private helper methods
		private double RoundToQuarter( double val )
			=> Math.Round(val * 4, MidpointRounding.ToEven) / 4;
		private double getMinutes( double number )
			=> ( number - Math.Floor(number) ) * 60;
		private void UpdateValues( double start, double end ) {

			// setzt die Korrekte reihenfolge der beiden Daten
			if( end >= start ) {
				_Start = start;
				_End = end;
			}
			else {
				_Start = end;
				_End = start;
			}
		}
		#endregion
	}
}
