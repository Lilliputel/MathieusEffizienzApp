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

		public ICollectionView KategorienListe { get; private set; }
		public ICollectionView ProjektListe { get; private set; }
		public ICollectionView AufgabenListe { get; private set; }

		#endregion

		#region constructor
		public ViewModelDashboard() {
			( Application.Current as App ).KategorienListe.CollectionChanged += UpdateListen;

			KategorienListe = CollectionViewSource.GetDefaultView(( Application.Current as App ).KategorienListe);
			ProjektListe = CollectionViewSource.GetDefaultView(from Kat in ( Application.Current as App ).KategorienListe
															   where Kat != null
															   select Kat.Projekte into Projekte
															   from Item in Projekte
															   select Item);
			AufgabenListe = CollectionViewSource.GetDefaultView(( from Kat in ( Application.Current as App ).KategorienListe
																  where Kat != null
																  from Aufgaben in Kat.Aufgaben
																  select Aufgaben )
																  .Concat(from Kat in ( Application.Current as App ).KategorienListe
																		  where Kat != null
																		  select Kat.Projekte into ProjektListen
																		  from Projekt in ProjektListen
																		  select Projekt into Projekt
																		  from Aufgaben in Projekt.Aufgaben
																		  select Aufgaben));
		}


		#endregion

		#region methods
		private void UpdateListen( object sender, NotifyCollectionChangedEventArgs e ) {
			KategorienListe.Refresh();
			ProjektListe.Refresh();
			AufgabenListe.Refresh();
		}

		#endregion
	}
}
