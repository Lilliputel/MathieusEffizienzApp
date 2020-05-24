using EffizienzNeu.Interfaces;
using EffizienzNeu.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EffizienzNeu.Classes {

	public class Kategorie : ObservableObject, IIdentifizierbar {

		public int ID { get; set; }
		public Color Farbe { get; set; }
		public string Titel { get; set; }

		public Kategorie() { }

		public Kategorie( string _Titel, Color _Farbe) {
			ID = IIdentifizierbar.IdCounter++;
			this.Titel = _Titel;
			this.Farbe = _Farbe;
		}

	}
}
