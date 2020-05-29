using Effizienz.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Effizienz.Classes {

	public class Projekt : ObservableObject {

		#region Properties

		public Guid ID { get; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public DateTime StartDatum { get; set; }
		public DateTime EndDatum { get; set; }

		public Guid KategorieID { get; set; }

		private ObservableCollection<Aufgabe> aufgaben;
		public ObservableCollection<Aufgabe> Aufgaben {
			get {
				return aufgaben;
			}
			set {
				try {
					StartDatum = ( from aufgabe in value
								   where aufgabe.StartDatum > StartDatum
								   select aufgabe.StartDatum ).First();
				}
				catch( InvalidOperationException ) {
				}
				aufgaben = value;
				OnPropertyChanged(nameof(Aufgaben));
			}
		}

		public TimeSpan GesamtZeit { get; set; }

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Project must have a Titel and a KategorieID
		/// </summary>
		public Projekt() {
			this.ID = Guid.NewGuid();
			this.Aufgaben = new ObservableCollection<Aufgabe>();
		}

		public Projekt( string _Titel, Guid _KategorieID, DateTime _EndDatum )
			: this(_Titel, _KategorieID, DateTime.Today, _EndDatum) { }

		public Projekt( string _Titel, Guid _KategorieID, DateTime _StartDatum, DateTime _EndDatum )
			: this(_Titel, _KategorieID, _StartDatum, _EndDatum, TimeSpan.Zero ) { }

		public Projekt( string _Titel, Guid _KategorieID, DateTime _StartDatum, DateTime _EndDatum, TimeSpan _GesamtZeit, string _Beschreibung = "Das ist ein neues Projekt!") : this() {
			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.KategorieID = _KategorieID;
			this.StartDatum = _StartDatum.Date;
			this.EndDatum = _EndDatum.Date;
			this.GesamtZeit = _GesamtZeit;
		}

		~Projekt() { }

		#endregion

		#region Methods

		#endregion
	}
}
