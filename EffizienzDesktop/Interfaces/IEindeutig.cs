using System;

namespace Effizienz.Interfaces {

	public interface IEindeutig {

		Guid ID { get; }
		string Titel { get; set; }

	}
}
