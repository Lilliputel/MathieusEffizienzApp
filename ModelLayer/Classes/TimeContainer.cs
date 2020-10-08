using ModelLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	[Serializable]
	public class TimeContainer : ObservableCollection<WorkItem> {

		#region public properties
		[XmlIgnore]
		public TimeSpan Time {
			get {
				var placeholder = TimeSpan.Zero;
				new List<WorkItem>(this).ForEach(item => placeholder += item.Time);
				return placeholder;
			}
		}
		#endregion

		#region public methods
		public TimeSpan GetTimeOnDate( DateTime date ) {
			var placeholder = TimeSpan.Zero;
			var _Date = date.Date;
			foreach( var workItem in this )
				if( workItem.Date == _Date )
					placeholder += workItem.Time;
			return placeholder;
		}
		public ICollection<DateTime> GetWorkedDates() {
			ICollection<DateTime> placeholder = new List<DateTime>();
			if( this.Time == TimeSpan.Zero )
				return placeholder;
			foreach( var item in this )
				placeholder.AddUnique(item.Date);
			return placeholder;
		}
		#endregion

	}
}
