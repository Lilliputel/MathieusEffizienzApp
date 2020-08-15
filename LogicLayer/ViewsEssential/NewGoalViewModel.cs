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

		private ICommand? _SaveGoalCommand;
		private Category? _SelectedCategory;
		private Goal? _SelectedGoal;

		private DateTime? _StartDate;
		private DateTime? _EndDate;

		#endregion

		#region properties

		public ObservableCollection<Category> Categories
			=> ObjectManager.CategoryList;

		public Category? SelectedCategory {
			get {
				return _SelectedCategory;
			}
			set {
				_SelectedCategory = value;
				OnPropertyChanged(nameof(SelectedCategory));
			}
		}
		public Goal? SelectedGoal {
			get {
				return _SelectedGoal;
			}
			set {
				_SelectedGoal = value;
				OnPropertyChanged(nameof(SelectedGoal));
			}
		}

		public string? Title { get; set; }
		public string? Description { get; set; }

		public DateTime? StartDate {
			get {
				return _StartDate ??= DateTime.Today;
			}
			set {
				if( value is { } Value ) {
					_StartDate = Value;
				}
			}
		}
		public DateTime? EndDate {
			get {
				return _EndDate ??= DateTime.Today.AddDays(1);
			}
			set {
				if( value is { } Value ) {
					_EndDate = Value;
				}
			}
		}

		public ICommand SaveGoalCommand => _SaveGoalCommand ??
			( _SaveGoalCommand = new RelayCommand(parameter => {

				MessageBoxDisplayer.ObjektErstellt(nameof(Goal), Title ??= "unkown_Goal");
			}) );

		#endregion

		#region constructors

		#endregion

		#region methods

		#endregion

	}
}
