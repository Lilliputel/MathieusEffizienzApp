﻿using ModelLayer.Utility;
using PropertyChanged;
using System;
#if SQLite
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace ModelLayer.Classes {
	public class DateSpan : ObservableObject {

		#region private fields
		private DateTime _Start;
		private DateTime _End;
		#endregion

		#region public properties
		[Key]
		public int Id { get; set; }
#if SQLite
		[Column( TypeName = "TEXT" )]
#endif
		[AlsoNotifyFor( nameof( Duration ) )]
		public DateTime Start {
			get => _Start;
			set => UpdateValues( value.Date, _End );
		}
#if SQLite
		[Column( TypeName = "TEXT" )]
#endif
		[AlsoNotifyFor( nameof( Duration ) )]
		public DateTime End {
			get => _End;
			set => UpdateValues( _Start, value.Date );
		}
#if SQLite
		[NotMapped]
#endif
		public TimeSpan Duration
			=> End - Start;
		#endregion

		#region constructor
		public DateSpan( string start, string end, IFormatProvider formatProvider )
			: this( DateTime.ParseExact( start, @"dd.MM.yy", formatProvider ), DateTime.ParseExact( end, @"dd.MM.yy", formatProvider ) ) { }
		public DateSpan( DateTime start, DateTime end )
			=> UpdateValues( start.Date, end.Date );
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
