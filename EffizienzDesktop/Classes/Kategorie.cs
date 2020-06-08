using Effizienz.Utility;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Effizienz.Classes {

	public class Kategorie : ObservableObject {

		#region fields

		private Color farbe;
		private string titel;

		#endregion

		#region Properties

		public Guid ID { 
			get; 
		}

		public Color Farbe {
			get {
				return farbe;
			}
			set {
				farbe = value;
				OnPropertyChanged(nameof(Farbe));
			}
		}
		public string Titel {
			get {
				return titel;
			}
			set {
				titel = value;
				OnPropertyChanged(nameof(Titel));
			}
		}

		public ObservableCollection<Aufgabe> Aufgaben { get; set; }
		public ObservableCollection<Projekt> Projekte { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Kategorie must have a Titel and a Color!
		/// </summary>
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

		#endregion

	}
}
