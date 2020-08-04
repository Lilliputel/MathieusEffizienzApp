using FrontLayer.Commands;
using ModelLayer.Classes;
using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FrontLayer.Views {
	public class AufgabeViewModel : ViewModelBase {

		#region fields

		private ICommand commandSaveAufgabe;
		private Kategorie selectedKategorie;
		private Projekt selectedProjekt;

		private DateTime startDatum;
		private DateTime endDatum;

		#endregion

		#region properties

		public ObservableCollection<Kategorie> KategorienListe
			=> ( Application.Current as App ).KategorienListe;

		public Kategorie SelectedKategorie {
			get {
				return selectedKategorie;
			}
			set {
				selectedKategorie = value;
				OnPropertyChanged(nameof(SelectedKategorie));
			}
		}
		public Projekt SelectedProjekt {
			get {
				return selectedProjekt;
			}
			set {
				selectedProjekt = value;
				OnPropertyChanged(nameof(SelectedProjekt));
			}
		}

		public string Titel { get; set; }
		public string Beschreibung { get; set; }


		public DateTime? StartDatum {
			get {
				return startDatum != null ? startDatum : DateTime.Today;
			}
			set {
				if( value != null ) {
					startDatum = value.Value;
				}
			}
		}
		public DateTime? EndDatum {
			get {
				return endDatum != null ? endDatum : DateTime.Today.AddDays(1);
			}
			set {
				if( value != null ) {
					endDatum = value.Value;
				}
			}
		}

		public ICommand CommandSaveAufgabe => commandSaveAufgabe ??
			( commandSaveAufgabe = new CommandRelay(parameter => {
				( from kat in ( Application.Current as App ).KategorienListe
				  where kat == SelectedKategorie
				  select kat.Projekte into proje
				  from proj in proje
				  where proj == SelectedProjekt
				  select proj ).First().Aufgaben.Add(
					new Aufgabe(
						Titel,
						SelectedProjekt == null ? SelectedKategorie.ID : SelectedProjekt.ID,
						this.startDatum,
						this.endDatum) {
						Beschreibung = this.Beschreibung != "" ? this.Beschreibung : "Das ist eine neue Aufgabe",
					});
				MessageBoxDisplayer.ObjektErstellt(nameof(Aufgabe), Titel);
			}) );

		#endregion

	}
}
