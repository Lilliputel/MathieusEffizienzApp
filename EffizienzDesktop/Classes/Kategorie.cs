using Effizienz.Interfaces;
using Effizienz.Utility;
using Effizienz.ValueTypes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Effizienz.Classes {

	public class Kategorie : ObservableObject, IGuid, ITitel, IStatus {

		#region fields

		private Color farbe;
		private string titel;

		private EnumStatus status;

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

		public EnumStatus Status {
			get {
				return status;
			}
			set {
				status = value;
				OnPropertyChanged(nameof(Status));
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
			this.ID = Guid.NewGuid();
			this.Status = EnumStatus.ToDo;

			this.Aufgaben = new ObservableCollection<Aufgabe>();
			this.Projekte = new ObservableCollection<Projekt>();
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
