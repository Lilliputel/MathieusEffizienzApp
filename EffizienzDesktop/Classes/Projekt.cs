using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Effizienz.Classes {

	public class Projekt : ObservableObject {

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

		public ObservableCollection<Aufgabe> Aufgaben { get; set; }
		public ObservableCollection<Meilenstein> Meilensteine { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Project must have a Titel and a KategorieID
		/// </summary>
		public Projekt() {
			this.ID = Guid.NewGuid();
			this.Aufgaben = new ObservableCollection<Aufgabe>();

			Aufgaben.CollectionChanged += Aufgaben_CollectionChanged;
		}

		public Projekt( string _Titel, Guid _KategorieID, DateTime _StartDatum, DateTime _EndDatum ) : this() {
			this.Titel = _Titel;
			this.ParentID = _KategorieID;
			this.StartDatum = _StartDatum;
			this.EndDatum = _EndDatum;
		}

		public Projekt( string _Titel, Guid _KategorieID, DateTime _StartDatum, DateTime _EndDatum, TimeSpan _GesamtZeit, string _Beschreibung = "Das ist ein neues Projekt!" ) : this() {
			this.Titel = _Titel;
			this.Beschreibung = _Beschreibung;
			this.ParentID = _KategorieID;
			this.StartDatum = _StartDatum.Date;
			this.EndDatum = _EndDatum.Date;
			this.Zeit = _GesamtZeit;
		}

		~Projekt() { }

		#endregion

		#region Methods

		private void Aufgaben_CollectionChanged( object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e ) {

			var minDat = ( from aufgabe in Aufgaben
						   where aufgabe.StartDatum < this.StartDatum
						   select aufgabe.StartDatum );
			StartDatum = minDat.Any() ? minDat.Min() : StartDatum;

			var maxDat = ( from aufgabe in Aufgaben
						   where aufgabe.EndDatum > this.EndDatum
						   select aufgabe.EndDatum );
			EndDatum = maxDat.Any() ? maxDat.Max() : EndDatum;

		}

		#endregion
	}
}
