using ModelLayer.Classes;
using ModelLayer.Extensions;
using System;
using System.Collections.Generic;

namespace ModelLayer.Interfaces {
	public interface IAccountableParent<T> : IParent<T>, IAccountable where T : class {
		TimeSpan GetTotalTimeOnDate( DateTime date ) {
			var placeholder = TimeSpan.Zero;
			new List<T>(Children).ForEach(Child =>
				placeholder += ( Child as IAccountableParent<T> ).GetTotalTimeOnDate(date));
			placeholder += GetTimeOnDate(date);
			return placeholder;
		}
		ICollection<DateTime> GetTotalWorkedDates() {
			var placeholder = new List<DateTime>();
			new List<T>(Children).ForEach(Child =>
				placeholder.AddUniqueRange(( Child as IAccountableParent<T> ).GetTotalWorkedDates()));
			new List<WorkItem>(WorkHours).ForEach(workItem =>
				placeholder.AddUnique(workItem.Date));
			return placeholder;
		}
	}
}
