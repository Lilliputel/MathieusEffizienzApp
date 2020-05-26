using Effizienz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Effizienz.Classes {

	public class Projekt {

		public Guid ID { get; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public Guid KategorieID { get; set; }
		public DateTime EndDatum { get; set; }
		public TimeSpan ZeitGesamt { get; set; }

		public Projekt() { }

		public Projekt( string _Titel, string _Beschreibung, Guid _KategorieID, DateTime _EndDatum ) {
			ID = Guid.NewGuid();
			ZeitGesamt = TimeSpan.Zero;
			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.KategorieID = _KategorieID;
			this.EndDatum = _EndDatum;
		}

		~Projekt() {

		}

	}
}
