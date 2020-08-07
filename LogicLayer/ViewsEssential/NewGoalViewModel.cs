using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.Utility;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewGoalViewModel : ViewModelBase {

		#region fields

		private ICommand commandSaveAufgabe;
		private Category selectedKategorie;

		private DateTime startDatum;
		private DateTime endDatum;

		#endregion

		#region properties

		public ObservableCollection<Category> Categories
			=> ObjectManager.CategoryList;

		public Category SelectedKategorie {
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

		public ICommand CommandSaveAufgabe => commandSaveAufgabe ??
			( commandSaveAufgabe = new RelayCommand(parameter => {


#warning speichermechanismus überarbeiten!

				MessageBoxDisplayer.ObjektErstellt(nameof(Goal), Titel);
			}) );

		#endregion

		#region constructors

		#endregion

		#region methods

		#endregion

	}
}
