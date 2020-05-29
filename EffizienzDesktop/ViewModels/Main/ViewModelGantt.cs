using Effizienz.Classes;
using System.Collections.ObjectModel;
using System.Windows;

namespace Effizienz.Views {
	public class ViewModelGantt : ViewModelBase {

		#region properties
		public ObservableCollection<Kategorie> Kategorien { get; set; } = new ObservableCollection<Kategorie>();
		public ObservableCollection<Projekt> Projekte { get; set; } = new ObservableCollection<Projekt>();
		#endregion

		#region constructor

		public ViewModelGantt() {
			var KatListe = ( Application.Current as App ).KategorienListe;
			foreach( Kategorie kat in KatListe ) {
				foreach( Projekt proj in kat.Projekte ) {
					Projekte.Add(proj);
				}
			}
			this.Kategorien = KatListe;
		}

		#endregion

	}
}
