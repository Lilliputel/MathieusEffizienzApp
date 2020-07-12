using Effizienz.Interfaces;
using Effizienz.Utility;
using Effizienz.ValueTypes;
using System;

namespace Effizienz.Classes {

	public class Aufgabe : ObservableObject, IGuid, ITitel, IBeschreibung, IChild, IStatus, IPlanbar, IAbrechenbar {

		#region fields

		private string titel;
		private string beschreibung;

		private Guid parentID;
		private EnumStatus status;

		private StructDaten planung;
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

		public StructDaten Planung {
			get {
				return planung;
			}
			set {
				planung = value;
				OnPropertyChanged(nameof(Planung));
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
			this.ID = Guid.NewGuid();
			this.Zeit = TimeSpan.Zero;
			this.Status = EnumStatus.ToDo;
		}

		public Aufgabe( string _Titel, Guid _ParentID, DateTime _StartDatum, DateTime _EndDatum )
			: this() {
			this.Titel = _Titel;
			this.ParentID = _ParentID;
			this.Planung = new StructDaten(_StartDatum, _EndDatum);
		}

		public Aufgabe( string _Titel, Guid _ParentID, DateTime _StartDatum, DateTime _EndDatum, TimeSpan _ArbeitsZeit, EnumStatus _Status, string _Beschreibung = "Das ist eine neue Aufgabe!" )
			: this(_Titel, _ParentID, _StartDatum, _EndDatum) {
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
