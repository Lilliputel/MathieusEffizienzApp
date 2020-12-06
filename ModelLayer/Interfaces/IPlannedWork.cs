using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;

namespace ModelLayer.Interfaces {
	public interface IPlannedWork {
		ObservableCollection<(DayOfWeek Day, DoubleTime Time)> WorkPlan { get; }

	}
}
