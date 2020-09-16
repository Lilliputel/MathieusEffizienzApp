using System;
using System.Collections.ObjectModel;

namespace ModelLayer.Interfaces {
	public interface IAccountable {

		public TimeSpan Time { get; set; }

		public ObservableCollection<(DateTime Date, TimeSpan Time)> WorkHours { get; set; }

	}
}
