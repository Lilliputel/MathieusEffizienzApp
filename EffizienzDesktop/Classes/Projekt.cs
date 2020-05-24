using Effizienz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Effizienz.Classes {

	public class Projekt : IIdentifizierbar {

		private static int ID_Counter { get; set; }

		public int ID { get; set; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public int KategorieID { get; set; }
		public DateTime EndDatum { get; set; }
		public TimeSpan ZeitGesamt { get; set; }

		static Projekt() {
			ID_Counter = 1;
		}

		public Projekt() { }

		public Projekt( string _Titel, string _Beschreibung, int _KategorieID, DateTime _EndDatum ) {
			ID = ID_Counter++;
			ZeitGesamt = TimeSpan.Zero;
			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.KategorieID = _KategorieID;
			this.EndDatum = _EndDatum;
		}

		~Projekt() {

		}

		public void SetStartID( int _StartID ) {
			ID_Counter = _StartID;
		}

	}
}
