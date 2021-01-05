using ModelLayer.Interfaces;
using ModelLayer.Utility;
using System;
#if SQLite
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endif
namespace ModelLayer.Classes {
	public class WorkItem : ObservableObject, IHasTime {

		#region private fields
		private DateTime _Date;
		#endregion

		#region public properties
#if SQLite
		[Key]
		[Column( TypeName = "TEXT" )]
#endif
		public TimeSpan Start { get; set; }
#if SQLite
		[Key]
		[Column( TypeName = "TEXT" )]
#endif
		public TimeSpan End { get; set; }
#if SQLite
		[Column( TypeName = "TEXT" )]
#endif
		public TimeSpan Time { get; set; }
#if SQLite
		[Key]
		[Column( TypeName = "TEXT" )]
#endif
		public DateTime Date { get => _Date; set => _Date = value.Date; }

#if SQLite
		[ForeignKey( nameof( Category ) )]
		public int CategoryId { get; set; }
		public Category? Category { get; set; }

		[ForeignKey( nameof( Goal ) )]
		public int GoalId { get; set; }
		public Goal? Goal { get; set; }
#endif
		#endregion

		#region constructor
		public WorkItem( DateTime date, TimeSpan time, TimeSpan start, TimeSpan end ) {
			Time = time;
			Date = date;
			Start = start;
			End = end;
		}
		#endregion
	}
}
