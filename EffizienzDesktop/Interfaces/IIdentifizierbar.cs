using System;
using System.Collections.Generic;
using System.Text;

namespace Effizienz.Interfaces {

	public interface IIdentifizierbar {

		int ID { get; }
		void SetStartID( int _StartID );

	}

}
