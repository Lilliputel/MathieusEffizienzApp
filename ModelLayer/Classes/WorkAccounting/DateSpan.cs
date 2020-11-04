using ModelLayer.Utility;
using PropertyChanged;
using System;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	[Serializable]
	public class DateSpan : ObservableObject {

		#region private fields
		private DateTime _Start;
		private DateTime _End;
		#endregion

		#region public properties
		[XmlAttribute( nameof( Start ) )]
		[AlsoNotifyFor( nameof( Duration ) )]
		public DateTime Start {
			get => _Start;
			set => UpdateValues( value.Date, _End );
		}
		[XmlAttribute( nameof( End ) )]
		[AlsoNotifyFor( nameof( Duration ) )]
		public DateTime End {
			get => _End;
			set => UpdateValues( _Start, value.Date );
		}
		[XmlIgnore]
		public TimeSpan Duration
			=> End - Start;
		#endregion

		#region constructor
		public DateSpan( string start, string end, IFormatProvider formatProvider )
			: this( DateTime.ParseExact( start, @"dd.MM.yy", formatProvider ), DateTime.ParseExact( end, @"dd.MM.yy", formatProvider ) ) { }
		public DateSpan( DateTime start, DateTime end ) {
			UpdateValues( start.Date, end.Date );
		}

		public DateSpan() { }
		#endregion

		#region private helper methods
		private void UpdateValues( DateTime start, DateTime end ) {
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
