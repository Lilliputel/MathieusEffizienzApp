using System;
using System.Collections.ObjectModel;

namespace ModelLayer.Classes {
	public class WorkPlan : ObservableCollection<(DayOfWeek Day, DoubleTime Time)> {
		public void Add( DayOfWeek day, DoubleTime time ) {
			base.Add((day, time));
		}
	}
}
