using System;
using System.Collections.Generic;
using System.Text;

namespace EffizienzNeu.Interfaces {

	public interface IIdentifizierbar {

		public static int IdCounter { get; set; }
		int ID { get; }

	}

}
