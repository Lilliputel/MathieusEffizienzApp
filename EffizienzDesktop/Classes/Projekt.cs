using Effizienz.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Effizienz.Classes {

	public class Projekt : ObservableObject {

		#region fields

		private DateTime startDatum;
		private DateTime endDatum;
		private ObservableCollection<Aufgabe> aufgaben;
		private ObservableCollection<Meilenstein> meilensteine;

		private TimeSpan gesamteZeit;

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

		public TimeSpan GesamtZeit { 
			get {
				return gesamteZeit;
			}
			set {
				gesamteZeit = value;
				OnPropertyChanged(nameof(GesamtZeit));
			} 
		}

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

		private void Aufgaben_CollectionChanged( object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e ) {
			try {
				StartDatum = ( from aufgabe in Aufgaben
							   where aufgabe.StartDatum < this.StartDatum
							   select aufgabe.StartDatum ).Min();
				EndDatum = ( from aufgabe in Aufgaben
							 where aufgabe.EndDatum > this.EndDatum
							 select aufgabe.EndDatum ).Max();
			}
			catch( InvalidOperationException iE ) {
			}
		}

		#endregion
	}
}
