using System;

namespace Effizienz.Classes {

	public class Aufgabe {

		public Guid ID { get; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public Guid ProjektID { get; set; }
		public Guid KategorieID { get; set; }
		public DateTime EndDatum { get; set; }
		public TimeSpan ArbeitsZeit { get; set; }

		public Aufgabe() {
			ID = Guid.NewGuid();
			ArbeitsZeit = TimeSpan.Zero;
		}

		public Aufgabe( string _Titel, string _Beschreibung, Guid _ProjektID, Guid _KategorieID, DateTime _EndDatum ) {
			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.ProjektID = _ProjektID;
			this.KategorieID = _KategorieID;
			this.EndDatum = _EndDatum;
		}

		~Aufgabe() { }

	}
}
