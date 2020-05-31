using Effizienz.Classes;
using Effizienz.Commands;
using Effizienz.Utility;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System;
using System.Linq;

namespace Effizienz.Views {
	public class ViewModelProjekt : ViewModelBase {

		#region fields

		private ICommand commandSaveProjekt;
		private Kategorie selectedKategorie;

		private DateTime startDatum;
		private DateTime endDatum;

		#endregion

		#region properties

		public ObservableCollection<Kategorie> KategorienListe { get; set; }
		
		public Kategorie SelectedKategorie {
			get {
				return selectedKategorie;
			}
			set {
				selectedKategorie = value;
				OnPropertyChanged(nameof(SelectedKategorie));
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

		public ICommand CommandSaveProjekt => commandSaveProjekt ??
			( commandSaveProjekt = new CommandRelay(parameter => {
				(from kat in (Application.Current as App).KategorienListe
				where kat == SelectedKategorie
				select kat).First().Projekte.Add(
					new Projekt(
						this.Titel,
						this.SelectedKategorie.ID,
						this.StartDatum.Value,
						this.EndDatum.Value) {
						Beschreibung = this.Beschreibung
					});
				MessageBoxDisplayer.ObjektErstellt(nameof(Aufgabe), Titel);
			}) );

		#endregion

		#region constructor

		public ViewModelProjekt() {
			KategorienListe = ( Application.Current as App ).KategorienListe;
		}
		#endregion
	}
}
