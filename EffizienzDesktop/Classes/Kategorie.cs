using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Effizienz.Classes {

	public class Kategorie {

		#region Properties
		
		public Guid ID { get; }

		public Color Farbe { get; set; }
		public string Titel { get; set; }

		public ObservableCollection<Aufgabe> Aufgaben { get; set; }
		public ObservableCollection<Projekt> Projekte { get; set; }

		#endregion

		#region Constructors
		
		public Kategorie() {
			ID = Guid.NewGuid();
			Aufgaben = new ObservableCollection<Aufgabe>();
			Projekte = new ObservableCollection<Projekt>();
		}

		public Kategorie( string _Titel, Color _Farbe ) : this() {
			this.Titel = _Titel;
			this.Farbe = _Farbe;
		}

		~Kategorie() { }

		#endregion

		#region Methods

		public void AddAufgabe( Aufgabe _neueAufgabe ) {
			Aufgaben.Add(_neueAufgabe);
		}

		public void AddProjekt( Projekt _neuesProjekt ) {
			Projekte.Add(_neuesProjekt);
		}

		#endregion

	}
}
