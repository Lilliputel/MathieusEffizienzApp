using ModelLayer.Classes;
using ModelLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ModelLayer.Interfaces {
	public interface IAccountable : IHasTime {
		ObservableCollection<WorkItem> WorkHours { get; }
		TimeSpan GetTimeOnDate( DateTime date ) {
			var placeholder = new TimeSpan();
			new List<WorkItem>( WorkHours )
				.Where( item => item.Date.Date == date.Date )
				.ToList().ForEach( item => placeholder += item.Time );
			return placeholder;
		}
		ICollection<DateTime> GetWorkedDates() {
			var placeholder = new List<DateTime>();
			new List<WorkItem>( WorkHours ).ForEach( workItem =>
				   placeholder.AddUnique( workItem.Date ) );
			return placeholder;
		}
	}
}
