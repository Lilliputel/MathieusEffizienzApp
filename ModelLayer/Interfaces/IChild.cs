using System;

namespace ModelLayer.Interfaces {
	public interface IChild {

		Guid ParentID { get; set; }
		bool IsChild { get; }

	}
}
