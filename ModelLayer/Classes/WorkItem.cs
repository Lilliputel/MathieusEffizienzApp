using ModelLayer.Interfaces;
using ModelLayer.Utility;
using System;
#if XML
using System.Xml.Serialization;
#elif SQLite
using System.ComponentModel.DataAnnotations.Schema;
#endif
namespace ModelLayer.Classes {
	public class WorkItem : ObservableObject, IHasTime {

		#region private fields
		private DateTime _Date;
		#endregion

		#region public properties
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }
#if XML
		[XmlAttribute( nameof( Time ) )]
#endif
		public TimeSpan Time { get; set; }
#if XML
		[XmlAttribute( nameof( Date ) )]
#elif SQLite
		[NotMapped]
#endif
		public DateTime Date { get => _Date; set => _Date = value.Date; }
		#endregion

		#region constructor
		public WorkItem( DateTime date, TimeSpan time ) {
			Time = time;
			Date = date;
		}
#if XML
		public WorkItem() { }
#endif
		#endregion
	}
}
