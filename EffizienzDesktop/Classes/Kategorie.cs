using System;
using System.Windows.Media;

namespace Effizienz.Classes {

	public class Kategorie {

		public Guid ID { get; }
		public Color Farbe { get; set; }
		public string Titel { get; set; }


		public Kategorie() {
			ID = Guid.NewGuid();
		}

		public Kategorie( string _Titel, Color _Farbe ) {
			this.Titel = _Titel;
			this.Farbe = _Farbe;
		}

		~Kategorie() { }

	}
}
