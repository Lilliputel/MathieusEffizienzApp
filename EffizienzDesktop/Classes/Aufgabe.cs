using Effizienz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Effizienz.Classes {
	
	public class Aufgabe : IIdentifizierbar {

		private static int ID_Counter { get; set; }

		public int ID { get; set; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public int ProjektID { get; set; }
		public int KategorieID { get; set; }
		public DateTime EndDatum { get; set; }
		public TimeSpan Zeit { get; set; }

		static Aufgabe() {
			ID_Counter = 101;
		}

		public Aufgabe() { }

		public Aufgabe( string _Titel, string _Beschreibung, int _ProjektID, int _KategorieID, DateTime _EndDatum ) {
			ID = ID_Counter++;
			Zeit = TimeSpan.Zero;

			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.ProjektID = _ProjektID;
			this.KategorieID = _KategorieID;
			this.EndDatum = _EndDatum;

		}

		~Aufgabe() {

		}

		public void SetStartID( int _StartID ) {
			ID_Counter = _StartID;
		}

	}
}
