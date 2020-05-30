using Effizienz.Classes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Effizienz.Views {

	public class ViewModelDashboard : ViewModelBase {

		#region properties

		public ObservableCollection<Kategorie> KategorienListe {
			get {
				return ( Application.Current as App ).KategorienListe;
			}
		}
		public ObservableCollection<Projekt> ProjektListe {
			get {
				return new ObservableCollection<Projekt>(from Kat in ( Application.Current as App ).KategorienListe
														 select Kat.Projekte into Projekte
														 from Item in Projekte
														 select Item);
			}
		}
		public ObservableCollection<Aufgabe> AufgabenListe {
			get {
				return new ObservableCollection<Aufgabe>(( from Kat in ( Application.Current as App ).KategorienListe
														   from Aufgaben in Kat.Aufgaben
														   select Aufgaben )
														   .Concat(from Kat in ( Application.Current as App ).KategorienListe
																   select Kat.Projekte into ProjektListen
																   from Projekt in ProjektListen
																   select Projekt into Projekt
																   from Aufgaben in Projekt.Aufgaben
																   select Aufgaben));
			} 
		}

		#endregion

		#region constructor

		#endregion

		#region methods

		#endregion
	}
}
