using Effizienz.Classes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using Effizienz.Commands;

namespace Effizienz.Views {
	public class ViewModelAufgabe : ViewModelBase {

		#region fields

		private ICommand commandSaveAufgabe;

		#endregion

		#region properties

		public ObservableCollection<Kategorie> Kategorien 
			=> ( Application.Current as App ).KategorienListe;
		public Kategorie SelectedKategorie { get; set; }

		public ObservableCollection<Projekt> Projekte 
			=> new ObservableCollection<Projekt>(from Kategorie in ( Application.Current as App ).KategorienListe
																						   select Kategorie.Projekte into ProjektListe
																						   from Projekt in ProjektListe
																						   select Projekt);

		public ICommand CommandSaveAufgabe => commandSaveAufgabe ??
			( commandSaveAufgabe = new CommandRelay(parameter => {

			}));


		#endregion

		#region constructor

		#endregion

		#region methods

		#endregion

	}
}
