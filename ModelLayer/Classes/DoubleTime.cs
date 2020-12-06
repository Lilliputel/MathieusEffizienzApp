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

		public Category Category { get; set; }
		public DayOfWeek Day { get; set; }

		[AlsoNotifyFor( nameof( Duration ) )]
		public double Start {
			get => _Start;
			set => UpdateValues( value, _End );
		}
		[AlsoNotifyFor( nameof( Duration ) )]
		public double End {
			get => _End;
			set => UpdateValues( _Start, value );
		}
		public double Duration
			=> End - Start;
		#endregion

		#region constructor
		public DoubleTime( DayOfWeek day, TimeSpan start, TimeSpan end, Category category ) {
			Category = category;
			Day = day;
			double realstart = start.Hours + start.Minutes / 60;
			double realend = end.Hours + end.Minutes / 60;
			UpdateValues( realstart, realend );
		}
		public DoubleTime( DayOfWeek day, double start, double end, Category category ) {
			Category = category;
			Day = day;
			UpdateValues( start, end );
		}
		#endregion

		#region public methods
		public (TimeSpan Start, TimeSpan End, TimeSpan Duration) GetTimeSpans() {
			var _start = new TimeSpan( (int) Math.Floor( Start ), (int) getMinutes( Start ), 0 );
			var _end = new TimeSpan( (int) Math.Floor( End ), (int) getMinutes( End ), 0 );
			var _duration = new TimeSpan( (int) Math.Floor( Duration ), (int) getMinutes( Duration ), 0 );
			return (_start, _end, _duration);
		}
		public override string ToString()
			=> $"{new TimeSpan( (int) Math.Floor( Start ), (int) getMinutes( Start ), 0 ).ToString( @"hh\:mm" )} - {new TimeSpan( (int) Math.Floor( End ), (int) getMinutes( End ), 0 ).ToString( @"hh\:mm" )}";
		#endregion

		#region private helper methods
		private double getMinutes( double number )
			=> (number - Math.Floor( number )) * 60;
		private void UpdateValues( double start, double end ) {
			double roundedStart = RoundToQuarter( start );
			double roundedEnd = RoundToQuarter( end );
			// setzt die Korrekte reihenfolge der beiden Daten
			if( roundedEnd > roundedStart ) {
				_Start = roundedStart;
				_End = roundedEnd;
			}
			else {
				_Start = roundedEnd;
				_End = roundedStart;
			}
		}
		private double RoundToQuarter( double val )
			=> Math.Round( val * 4, MidpointRounding.ToEven ) / 4;
		#endregion
	}
}
