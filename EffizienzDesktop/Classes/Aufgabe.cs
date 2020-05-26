using Effizienz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Effizienz.Classes {
	
	public class Aufgabe {

		public Guid ID { get; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public Guid ProjektID { get; set; }
		public Guid KategorieID { get; set; }
		public DateTime EndDatum { get; set; }
		public TimeSpan Zeit { get; set; }

		public Aufgabe() { }

		public Aufgabe( string _Titel, string _Beschreibung, Guid _ProjektID, Guid _KategorieID, DateTime _EndDatum ) {
			ID = Guid.NewGuid();
			Zeit = TimeSpan.Zero;

			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.ProjektID = _ProjektID;
			this.KategorieID = _KategorieID;
			this.EndDatum = _EndDatum;

		}

		~Aufgabe() {

		}

	}
}
