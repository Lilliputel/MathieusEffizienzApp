using System;
using System.Collections.ObjectModel;

namespace Effizienz.Classes {

	public class Projekt {

		public Guid ID { get; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public Guid KategorieID { get; set; }
		public ObservableCollection<Aufgabe> Aufgaben { get; set; }
		public TimeSpan ZeitGesamt { get; set; }

		public DateTime StartDatum { get; set; }
		public DateTime EndDatum { get; set; }


		public Projekt() {
			ID = Guid.NewGuid();
			ZeitGesamt = TimeSpan.Zero;
		}

		public Projekt( string _Titel, string _Beschreibung, Guid _KategorieID, DateTime _EndDatum ) {
			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.KategorieID = _KategorieID;
			this.EndDatum = _EndDatum;
		}

		~Projekt() { }

	}
}
