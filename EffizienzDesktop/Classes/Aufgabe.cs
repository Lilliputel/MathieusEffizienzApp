using System;

namespace Effizienz.Classes {

	public class Aufgabe : ObservableObject {

		#region fields

		private string titel;
		private string beschreibung;

		private Guid parentID;
		private EnumStatus status;

		private DateTime startDatum;
		private DateTime endDatum;
		private TimeSpan zeit;

		#endregion

		#region Properties

		public Guid ID {
			get;
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
		public string Beschreibung {
			get {
				return beschreibung;
			}
			set {
				beschreibung = value;
				OnPropertyChanged(nameof(Beschreibung));
			}
		}

		public Guid ParentID {
			get {
				return parentID;
			}
			set {
				parentID = value;
				OnPropertyChanged(nameof(ParentID));
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
		public TimeSpan Zeit {
			get {
				return zeit;
			}
			set {
				zeit = value;
				OnPropertyChanged(nameof(Zeit));
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Aufgabe must have a Titel and a ParentID
		/// </summary>
		public Aufgabe() {
			ID = Guid.NewGuid();
			Zeit = TimeSpan.Zero;
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
			this.Zeit = _ArbeitsZeit;
			this.Status = _Status;
			this.Beschreibung = _Beschreibung;
		}

		~Aufgabe() { }

		#endregion

		#region Methods

		#endregion

	}
}
