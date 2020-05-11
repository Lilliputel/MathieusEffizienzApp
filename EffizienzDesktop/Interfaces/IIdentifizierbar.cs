using System;
using System.Collections.Generic;
using System.Text;

namespace EffizienzNeu.Interfaces {

	public interface IIdentifizierbar {

		int ID { get; }
		void SetStartID( int _StartID );

	}

}
