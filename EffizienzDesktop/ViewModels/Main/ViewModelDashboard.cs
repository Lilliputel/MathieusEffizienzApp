using Effizienz.Classes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace Effizienz.Views {

	public class ViewModelDashboard : ViewModelBase {

		#region fields

		#endregion

		#region properties

		public ObservableCollection<Kategorie> KategorienListe { get; private set; }
		public ObservableCollection<Projekt> ProjektListe { get; private set; }
		public ObservableCollection<Aufgabe> AufgabenListe { get; private set; }

		#endregion

		#region constructor
		public ViewModelDashboard() {
			KategorienListe = new ObservableCollection<Kategorie>();
			ProjektListe = new ObservableCollection<Projekt>();
			AufgabenListe = new ObservableCollection<Aufgabe>();
			( Application.Current as App ).KategorienListe.CollectionChanged += UpdateListen;
		}


		#endregion

		#region methods
		private void UpdateListen( object sender, NotifyCollectionChangedEventArgs e ) {
			KategorienListe.Clear();
			foreach( var item in ( Application.Current as App ).KategorienListe ) {
				KategorienListe.Add(item);
			}
			ProjektListe.Clear();
			var updateProjekte = (from Kat in ( Application.Current as App ).KategorienListe
								  where Kat != null
								  select Kat.Projekte into Projekte
								  from Item in Projekte
								  select Item);
			foreach( Projekt item in updateProjekte ) {
				ProjektListe.Add(item);
			}

			AufgabenListe.Clear();
			var updateAufgaben = ( from Kat in ( Application.Current as App ).KategorienListe
								   where Kat != null
								   from Aufgaben in Kat.Aufgaben
								   select Aufgaben )
								   .Concat(from Kat in ( Application.Current as App ).KategorienListe
										   where Kat != null
										   select Kat.Projekte into ProjektListen
										   from Projekt in ProjektListen
										   select Projekt into Projekt
										   from Aufgaben in Projekt.Aufgaben
										   select Aufgaben);
			foreach( Aufgabe item in updateAufgaben ) {
				AufgabenListe.Add(item);
			}
		}

		#endregion
	}
}
