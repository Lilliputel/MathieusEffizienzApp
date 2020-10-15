using ModelLayer.Classes;
using ModelLayer.Enums;
using ModelLayer.Extensions;

namespace ModelLayer.Interfaces {
	public interface ICompleteable {
		event PlanEventHandler? PlanChanged;
		DateSpan Plan { get; set; }
		StateEnum State { get; set; }
	}
}
