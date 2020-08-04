using ModelLayer.Interfaces;
using ModelLayer.Utility;
using ModelLayer.ValueTypes;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ModelLayer.Classes {

	public class Projekt : ObservableObject, IEindeutig, IBeschreibung, IChild, IBearbeitbar {

		#region fields

		private string titel;
		private string beschreibung;

		private Guid parentID;
		private EnumStatus status;
		private ZeitSpanne planung;
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
		public ZeitSpanne Planung {
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

		public ObservableCollection<Aufgabe> Aufgaben { get; set; }
		public ObservableCollection<Meilenstein> Meilensteine { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Project must have a Titel, a ParentID, a Start and an EndDate
		/// </summary>
		public Projekt() {
			this.ID = Guid.NewGuid();
			this.Status = EnumStatus.ToDo;

			this.Aufgaben = new ObservableCollection<Aufgabe>();
			// this.Aufgaben.CollectionChanged += Aufgaben_CollectionChanged;
		}

		public Projekt( string _Titel, Guid _ParentID, DateTime _StartDatum, DateTime _EndDatum ) : this() {
			this.Titel = _Titel;
			this.ParentID = _ParentID;
			this.Planung = new ZeitSpanne(_StartDatum, _EndDatum);
		}

		public Projekt( string _Titel, Guid _ParentID, DateTime _StartDatum, DateTime _EndDatum, TimeSpan _GesamtZeit, string _Beschreibung = "Das ist ein neues Projekt!" )
			: this(_Titel, _ParentID, _StartDatum, _EndDatum) {
			this.Beschreibung = _Beschreibung;
			this.Zeit = _GesamtZeit;
		}

		~Projekt() { }

		#endregion

		#region Methods

		private void Aufgaben_CollectionChanged( object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e ) {

			// selects the minimal StartDate in Aufgaben
			var minDat = ( from aufgabe in Aufgaben
						   where aufgabe.Planung.Start < this.Planung.Start
						   select aufgabe.Planung.Start );

			// selects the minimal EndDate in Aufgaben
			var maxDat = ( from aufgabe in Aufgaben
						   where aufgabe.Planung.Ende > this.Planung.Ende
						   select aufgabe.Planung.Ende );

			// Updates the Property Planung with the new Dates
			Planung = new ZeitSpanne(
				start: minDat.Any() ? minDat.Min() : this.Planung.Start,
				ende: maxDat.Any() ? maxDat.Max() : this.Planung.Ende);

		}

		#endregion

	}
}
