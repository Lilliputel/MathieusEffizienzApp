using Effizienz.Classes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Effizienz.Views {
	public class ViewModelProjektÜbersicht : ViewModelBase {

		public ViewModelProjektÜbersicht() {
			Projekte = new ObservableCollection<Projekt>(from Kategorie in ( Application.Current as App ).KategorienListe
														 select Kategorie.Projekte into ProjektListe
														 from Projekt in ProjektListe
														 select Projekt);
		}

		public ObservableCollection<Projekt> Projekte { get; }

	}
}
