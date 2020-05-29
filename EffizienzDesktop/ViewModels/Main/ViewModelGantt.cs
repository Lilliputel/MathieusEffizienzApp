using Effizienz.Classes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Effizienz.Views {
	public class ViewModelGantt : ViewModelBase {

		#region properties

		public ObservableCollection<Kategorie> Kategorien { get; set; } = new ObservableCollection<Kategorie>(( Application.Current as App ).KategorienListe);

		#endregion

		#region constructor

		public ViewModelGantt() {
			Kategorie CBKategorie = new Kategorie("CodeBehind-Kategorie", Colors.Magenta);
			Projekt CBProjekt = new Projekt("CodeBehind-Projekt", CBKategorie.ID, DateTime.Today.AddDays(1), DateTime.Today.AddDays(10));
			Aufgabe CBAufgabe = new Aufgabe("CodeBehind-Aufgabe", CBProjekt.ID,	DateTime.Today.AddDays(2),	DateTime.Today.AddDays(5));

			CBProjekt.Aufgaben.Add(CBAufgabe);
			CBKategorie.Projekte.Add(CBProjekt);

			( Application.Current as App ).KategorienListe.Add(CBKategorie);
			Kategorien.Add(CBKategorie);
		}

		#endregion

	}
}
