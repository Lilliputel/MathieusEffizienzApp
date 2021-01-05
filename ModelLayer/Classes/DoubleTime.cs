using ModelLayer.Utility;
using PropertyChanged;
using System;
using System.ComponentModel.DataAnnotations;
#if SQLite
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace ModelLayer.Classes {
	public class DoubleTime : ObservableObject {

		#region private fields

		private double _Start;
		private double _End;

		#endregion

		#region public properties
#if SQLite
		[Required, ForeignKey( nameof( Category ) )]
		public int CategoryId { get; set; }
#endif
		public Category Category { get; set; }
#if SQLite
		[Column( TypeName = "TEXT" )]
		[Key]
#endif
		[Required( AllowEmptyStrings = false )]
		public DayOfWeek Day { get; set; }

#if SQLite
		[Key]
#endif
		[AlsoNotifyFor( nameof( Duration ) )]
		public double Start {
			get => _Start;
			set => UpdateValues( value, _End );
		}
#if SQLite
		[Key]
#endif
		[AlsoNotifyFor( nameof( Duration ) )]
		public double End {
			get => _End;
			set => UpdateValues( _Start, value );
		}
#if SQLite
		[NotMapped]
#endif
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
		public (TimeSpan start, TimeSpan end, TimeSpan duration) GetTimeSpans() {
			var start1 = new TimeSpan( (int)Math.Floor( Start ), (int)GetMinutes( Start ), 0 );
			var end1 = new TimeSpan( (int)Math.Floor( End ), (int)GetMinutes( End ), 0 );
			var duration1 = new TimeSpan( (int)Math.Floor( Duration ), (int)GetMinutes( Duration ), 0 );
			return (start1, end1, duration1);
		}
		public override string ToString()
			=> $"{new TimeSpan( (int)Math.Floor( Start ), (int)GetMinutes( Start ), 0 ).ToString( @"hh\:mm" )} - {new TimeSpan( (int)Math.Floor( End ), (int)GetMinutes( End ), 0 ).ToString( @"hh\:mm" )}";
		#endregion

		#region private helper methods
		private double GetMinutes( double number )
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
