using ModelLayer.Utility;
using System;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	[Serializable]
	public class WorkItem : ObservableObject {

		#region fields
		private DateTime _Date;
		#endregion

		#region public properties
		[XmlAttribute(nameof(Time))]
		public TimeSpan Time { get; set; }
		[XmlAttribute(nameof(Date))]
		public DateTime Date { get => _Date; set { _Date = value.Date; } }
		#endregion

		#region constructor
		public WorkItem( DateTime date, TimeSpan time ) {
			this.Time = time;
			this.Date = date;
		}
		public WorkItem() { }
		#endregion
	}
}
