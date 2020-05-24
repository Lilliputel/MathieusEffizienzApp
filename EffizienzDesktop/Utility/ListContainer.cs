using Effizienz.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Effizienz.Utility {
	public static class ListContainer {

		public static ListeIIdentifizierbar<Kategorie> KategorienListe { get; set; } = new ListeIIdentifizierbar<Kategorie>();
		public static ListeIIdentifizierbar<Projekt> ProjektListe { get; set; } = new ListeIIdentifizierbar<Projekt>();
		public static ListeIIdentifizierbar<Aufgabe> AufgabenListe { get; set; } = new ListeIIdentifizierbar<Aufgabe>();

	}
}
