using Effizienz.Classes;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using DynamicData.Binding;
using DynamicData.Alias;
using System.Runtime.CompilerServices;
using System.Reactive.Linq;

namespace Effizienz.Views {

	public class ViewModelDashboard : ViewModelBase {

		#region properties

		public ICollectionView KategorienListe { get; }
		public ICollectionView ProjektListe { get; }
		public ICollectionView AufgabenListe { get;  }

		#endregion

		#region constructor

		public ViewModelDashboard() {

			var observable = ( Application.Current as App ).KategorienListe.ToObservableChangeSet();
			var linq = observable.Select(Kat => Kat.Projekte);
			var neu = linq.ToListObservable();

			KategorienListe = CollectionViewSource.GetDefaultView(
				( Application.Current as App ).KategorienListe);
			ProjektListe = CollectionViewSource.GetDefaultView(
					from Kat in ( Application.Current as App ).KategorienListe
					select Kat.Projekte into Projekte
					from Item in Projekte
					select Item
					);
			AufgabenListe = CollectionViewSource.GetDefaultView((
				// Alle Aufgaben, die direkt in den Kategorien gespeichert sind.
				from Kat in ( Application.Current as App ).KategorienListe
				from Aufgaben in Kat.Aufgaben
				select Aufgaben )
				.Concat(
				// Alle Aufgaben aus den Projekten (Untergeordnet von den Kategorien)
				from Kat in ( Application.Current as App ).KategorienListe
				select Kat.Projekte into ProjektListen
				from Projekt in ProjektListen
				select Projekt into Projekt
				from Aufgaben in Projekt.Aufgaben
				select Aufgaben));
		}

		#endregion

		#region methods

		#endregion
	}
}
