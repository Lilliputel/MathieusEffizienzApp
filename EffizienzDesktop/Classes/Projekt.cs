using Effizienz.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Effizienz.Classes {

	public class Projekt : ObservableObject {

		#region fields

		private DateTime startDatum;
		private DateTime endDatum;
		private ObservableCollection<Aufgabe> aufgaben;
		private ObservableCollection<Meilenstein> meilensteine;

		#endregion

		#region Properties

		public Guid ID { get; }
		public string Titel { get; set; }
		public string Beschreibung { get; set; }
		public Guid KategorieID { get; set; }

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

		public ObservableCollection<Aufgabe> Aufgaben {
			get {
				return aufgaben;
			}
			set {
				try {
					StartDatum = ( from aufgabe in value
								   where aufgabe.StartDatum < StartDatum
								   select aufgabe.StartDatum ).Min();
					EndDatum = ( from aufgabe in value
								 where aufgabe.EndDatum > EndDatum
								 select aufgabe.EndDatum ).Max();
				}
				catch( InvalidOperationException e ) {
				}
				
				aufgaben = value;
				OnPropertyChanged(nameof(Aufgaben));
			}
		}
		public ObservableCollection<Meilenstein> Meilensteine {
			get {
				return meilensteine;
			}
			set {
				meilensteine = value;
				OnPropertyChanged(nameof(Meilensteine));
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

		public Projekt( string _Titel, Guid _KategorieID, DateTime _StartDatum, DateTime _EndDatum ) : this() {
			this.Titel = _Titel;
			this.KategorieID = _KategorieID;
			this.StartDatum = _StartDatum;
			this.EndDatum = _EndDatum;
		}

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
