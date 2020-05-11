using EffizienzNeu.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EffizienzNeu.Classes {

	public class Kategorie : IIdentifizierbar {

		private static int ID_Counter { get; set; }

		public int ID { get; set; }
		public Color Farbe { get; set; }
		public string Titel { get; set; }

		static Kategorie() {
			ID_Counter = 201;
		}

		public Kategorie() { }

		public Kategorie( string _Titel, Color _Farbe) {
			ID = ID_Counter++;
			this.Titel = _Titel;
			this.Farbe = _Farbe;
		}

		~Kategorie() {

		}

		public void SetStartID( int _StartID ) {
			ID_Counter = _StartID;
		}

	}
}
