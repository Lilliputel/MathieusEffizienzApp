using System;

namespace ModelLayer.Interfaces {

	public interface IIdentifyable {

		public Guid ID { get; }
		public string Title { get; set; }
		public string Description { get; set; }

	}
}
