using ModelLayer.Classes;
using System.Collections.ObjectModel;

namespace ModelLayer.Interfaces {
	public interface IPlannedWork {
		ObservableCollection<DoubleTime> WorkPlan { get; }

	}
}
