using Effizienz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Effizienz.Classes {

	public class Kategorie {

		public Guid ID { get; }
		public Color Farbe { get; set; }
		public string Titel { get; set; }


		public Kategorie() { }

		public Kategorie( string _Titel, Color _Farbe) {
			ID = Guid.NewGuid();
			this.Titel = _Titel;
			this.Farbe = _Farbe;
		}

		~Kategorie() {

		}

	}
}
