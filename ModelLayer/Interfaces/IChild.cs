using System;

namespace ModelLayer.Interfaces {
	public interface IChild {

		public Guid? ParentID { get; set; }
		public bool IsChild { get; }

	}
}
