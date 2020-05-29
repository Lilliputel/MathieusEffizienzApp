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
		
		public Aufgabe() {
			ID = Guid.NewGuid();
			ArbeitsZeit = TimeSpan.Zero;
		}

		public Aufgabe( string _Titel, string _Beschreibung, Guid _ParentID, DateTime _EndDatum ) : this() {
			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.ParentID = _ParentID;
			this.EndDatum = _EndDatum;
		}

		~Aufgabe() { }

		#endregion

		#region Methods

		#endregion

	}
}
