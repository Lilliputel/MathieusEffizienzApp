using System;
using System.Collections.Generic;

namespace ModelLayer.Interfaces {
	public interface IAccountableParent<T> : IParent<T>, IAccountable where T : class {
		TimeSpan GetTotalTimeOnDate( DateTime date );
		ICollection<DateTime> GetTotalWorkedDates();
	}
}
