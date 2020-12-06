using ModelLayer.Interfaces;
using ModelLayer.Utility;
using System;
using System.Xml.Serialization;

namespace ModelLayer.Classes {
	[Serializable]
	public class WorkItem : ObservableObject, IHasTime {

		#region private fields
		private DateTime _Date;
		#endregion

		#region public properties
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }

		[XmlAttribute( nameof( Time ) )]
		public TimeSpan Time { get; set; }
		[XmlAttribute( nameof( Date ) )]
		public DateTime Date { get => _Date; set => _Date = value.Date; }
		#endregion

		#region constructor
		public WorkItem( DateTime date, TimeSpan time ) {
			Time = time;
			Date = date;
		}
		public WorkItem() { }
		#endregion
	}
}
