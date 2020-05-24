using EffizienzNeu.Interfaces;
using EffizienzNeu.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffizienzNeu.Classes {

	public class Projekt : ObservableObject, IIdentifizierbar {

		public int ID { get; set; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public int KategorieID { get; set; }
		public DateTime StartDatum { get; set; }
		public DateTime EndDatum { get; set; }
		public TimeSpan ZeitGesamt { get; set; }

		public ObservableCollection<Aufgabe> Aufgaben { get; set; }

		public Projekt() { }

		public Projekt( string _Titel, string _Beschreibung, int _KategorieID, DateTime[] _Daten ) {
			ID = IIdentifizierbar.IdCounter++;
			ZeitGesamt = TimeSpan.Zero;
			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.KategorieID = _KategorieID;
			this.EndDatum = _Daten.Max();
			this.StartDatum = _Daten.Min() == _Daten.Max() ? DateTime.Now : _Daten.Min();
		}

		public void AddAufgabe( Aufgabe neueAufgabe ) {
			if( !Aufgaben.Contains(neueAufgabe) ) {
				Aufgaben.Add(neueAufgabe);
			}
		}

	}
}
