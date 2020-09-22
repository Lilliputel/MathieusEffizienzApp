using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ModelLayer.Interfaces {
	public interface IWorkable {

		public ObservableCollection<(DateTime Date, TimeSpan Time)> WorkHours { get; }

		public void AddWorkedTime( TimeSpan workedTime )
			=> AddWorkedTime(DateTime.Today, workedTime);
		public void AddWorkedTime( DateTime date, TimeSpan workedTime ) {
			var _Date = date.Date;
			var dates = from WorkDate in WorkHours
						select WorkDate.Date.Date;
			if( dates.Contains(_Date) ) {
				var WorkDate = (from WorkItem in WorkHours
								where WorkItem.Date == _Date
								select WorkItem).First();
				WorkDate.Time.Add(workedTime);
			}
			else
				WorkHours.Add((_Date, workedTime));
		}

		public ICollection<DateTime> GetWorkedDates();

	}
}
