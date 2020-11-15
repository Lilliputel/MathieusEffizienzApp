using ModelLayer.Classes;
using ModelLayer.Enums;
using System;

namespace ModelLayer.Interfaces {
	public interface ICompleteable {
		event EventHandler<DateSpan>? PlanChanged;
		DateSpan Plan { get; set; }
		StateEnum State { get; set; }
	}
}
