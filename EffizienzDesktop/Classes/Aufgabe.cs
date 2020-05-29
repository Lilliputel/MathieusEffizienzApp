using System;

namespace Effizienz.Classes {

	public class Aufgabe {

		#region Properties
		
		public Guid ID { get; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public Guid ParentID { get; set; }

		public DateTime StartDatum { get; set; }
		public DateTime EndDatum { get; set; }
		public TimeSpan ArbeitsZeit { get; set; }

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Aufgabe must have a Titel and a ParentID
		/// </summary>
		public Aufgabe() {
			ID = Guid.NewGuid();
			ArbeitsZeit = TimeSpan.Zero;
		}

		public Aufgabe( string _Titel, Guid _ParentID, DateTime _EndDatum )
			: this(_Titel, _ParentID, DateTime.Today, _EndDatum) { }

		public Aufgabe( string _Titel, Guid _ParentID, DateTime _StartDatum, DateTime _EndDatum )
			: this(_Titel, _ParentID, _StartDatum, _EndDatum, TimeSpan.Zero ) { }

		public Aufgabe( string _Titel, Guid _ParentID, DateTime _StartDatum, DateTime _EndDatum, TimeSpan _ArbeitsZeit, string _Beschreibung = "Das ist eine neue Aufgabe!" ) : this() {
			this.Titel = _Titel;
			this.ParentID = _ParentID;
			this.StartDatum = _StartDatum;
			this.EndDatum = _EndDatum;
			this.ArbeitsZeit = _ArbeitsZeit;
			this.Beschreibung = _Beschreibung;
		}

		~Aufgabe() { }

		#endregion

		#region Methods

		#endregion

	}
}
