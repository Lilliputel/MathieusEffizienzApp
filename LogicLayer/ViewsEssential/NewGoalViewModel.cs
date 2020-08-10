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

		private ICommand? _commandSaveAufgabe;
		private Category? _selectedKategorie;

		private DateTime? startDatum;
		private DateTime? endDatum;

		#endregion

		#region properties

		public ObservableCollection<Category> Categories
			=> ObjectManager.CategoryList;

		public Category SelectedKategorie {
			get {
				return _selectedKategorie;
			}
			set {
				_selectedKategorie = value;
				OnPropertyChanged(nameof(SelectedKategorie));
			}
		}

		public string? Titel { get; set; }
		public string? Beschreibung { get; set; }

		public DateTime? StartDatum {
			get {
				return startDatum ??= DateTime.Today;
			}
			set {
				if( value is { } Value ) {
					startDatum = Value;
				}
			}
		}
		public DateTime? EndDatum {
			get {
				return endDatum ??= DateTime.Today.AddDays(1);
			}
			set {
				if( value is { } Value ) {
					endDatum = Value;
				}
			}
		}

		public ICommand CommandSaveAufgabe => _commandSaveAufgabe ??
			( _commandSaveAufgabe = new RelayCommand(parameter => {

				MessageBoxDisplayer.ObjektErstellt(nameof(Goal), Titel);
			}) );

		#endregion

		#region constructors

		#endregion

		#region methods

		#endregion

	}
}
