using EffizienzNeu.Interfaces;
using EffizienzNeu.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffizienzNeu.Classes {
	
	public class Aufgabe : ObservableObject, IIdentifizierbar {

		public int ID { get; set; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public int ProjektID { get; set; }
		public int KategorieID { get; set; }
		public DateTime EndDatum { get; set; }
		public TimeSpan Zeit { get; set; }

		public Aufgabe() { }

		public Aufgabe( string _Titel, string _Beschreibung, int _ProjektID, int _KategorieID, DateTime _EndDatum ) {
			ID = IIdentifizierbar.IdCounter++;
			Zeit = TimeSpan.Zero;

			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.ProjektID = _ProjektID;
			this.KategorieID = _KategorieID;
			this.EndDatum = _EndDatum;

		}

	}
}
