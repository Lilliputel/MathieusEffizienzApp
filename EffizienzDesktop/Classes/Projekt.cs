using Effizienz.Utility;
using System;
using System.Collections.ObjectModel;

namespace Effizienz.Classes {

	public class Projekt : ObservableObject {

		#region Properties

		public Guid ID { get; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public DateTime StartDatum { get; set; }
		public DateTime EndDatum { get; set; }

		public Guid KategorieID { get; set; }

		public ObservableCollection<Aufgabe> Aufgaben { get; set; }
		public TimeSpan ZeitGesamt { get; set; }

		#endregion

		#region Constructors

		public Projekt() {
			ID = Guid.NewGuid();
			ZeitGesamt = TimeSpan.Zero;
			//Aufgaben = new ObservableCollection<Aufgabe>();
		}

		public Projekt(string _Titel, Guid _KategorieID, DateTime _EndDatum)
			: this(_Titel, "Das ist ein neues Projekt", _KategorieID, _EndDatum) { }

		public Projekt( string _Titel, string _Beschreibung, Guid _KategorieID, DateTime _EndDatum ) 
			: this(_Titel, _Beschreibung, _KategorieID, DateTime.Now, _EndDatum) { }

		public Projekt( string _Titel, string _Beschreibung, Guid _KategorieID, DateTime _StartDatum, DateTime _EndDatum ) : this() {
			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.KategorieID = _KategorieID;
			this.StartDatum = _StartDatum.Date;
			this.EndDatum = _EndDatum.Date;
		}

		~Projekt() { }

		#endregion

		#region Methods

		public void AddAufgabe( Aufgabe _neueAufgabe ) {
			this.Aufgaben.Add(_neueAufgabe);
			OnPropertyChanged( nameof(Aufgaben) );
		}

		#endregion
	}
}
