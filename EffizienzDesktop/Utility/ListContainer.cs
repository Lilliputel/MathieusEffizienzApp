using Effizienz.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Effizienz.Utility {
	public static class ListContainer {

		public static ObservableCollection<Kategorie> KategorienListe { get; set; } = new ObservableCollection<Kategorie>();
		public static ObservableCollection<Projekt> ProjektListe { get; set; } = new ObservableCollection<Projekt>();
		public static ObservableCollection<Aufgabe> AufgabenListe { get; set; } = new ObservableCollection<Aufgabe>();

	}
}
