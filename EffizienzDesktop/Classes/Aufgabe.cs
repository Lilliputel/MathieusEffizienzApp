using Effizienz.Utility;
using System;

namespace Effizienz.Classes {

	public class Aufgabe : ObservableObject{

		#region fields

		private DateTime startDatum;
		private DateTime endDatum;

		#endregion

		#region Properties

		public Guid ID { get; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }

		public Guid ParentID { get; set; }

		public EnumStatus Status { get; set; }
		public DateTime StartDatum {
			get {
				return startDatum;
			}
			set {
				startDatum = value;
				OnPropertyChanged(nameof(StartDatum));
			}
		}
		public DateTime EndDatum {
			get {
				return endDatum;
			}
			set {
				endDatum = value;
				OnPropertyChanged(nameof(EndDatum));
			}
		}

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
			Status = EnumStatus.ToDo;
		}

		public Aufgabe( string _Titel, Guid _ParentID, DateTime _StartDatum, DateTime _EndDatum ) : this() {
			this.Titel = _Titel;
			this.ParentID = _ParentID;
			this.StartDatum = _StartDatum;
			this.EndDatum = _EndDatum;
		}

		public Aufgabe( string _Titel, Guid _ParentID, DateTime _StartDatum, DateTime _EndDatum, TimeSpan _ArbeitsZeit, EnumStatus _Status, string _Beschreibung = "Das ist eine neue Aufgabe!" ) : this() {
			this.Titel = _Titel;
			this.ParentID = _ParentID;
			this.StartDatum = _StartDatum;
			this.EndDatum = _EndDatum;
			this.ArbeitsZeit = _ArbeitsZeit;
			this.Status = _Status;
			this.Beschreibung = _Beschreibung;
		}

		~Aufgabe() { }

		#endregion

		#region Methods

		#endregion

	}
}
