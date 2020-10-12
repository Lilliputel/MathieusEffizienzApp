using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Enums;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewGoalViewModel : ViewModelBase {

		#region fields

		private string? _Title;
		private string? _Description;

		private Category? _SelectedCategory;
		private Goal? _SelectedGoal;

		private DateTime? _StartDate;
		private DateTime? _EndDate;

		private StateEnum _State = StateEnum.ToDo;

		private ICommand? _SaveGoalCommand;

		#endregion

		#region properties
		public ObservableCollection<Category> CategoryList { get; private set; }
		public string? Title {
			get {
				return _Title;
			}
			set {
				if( value == _Title )
					return;
				_Title = value;
				OnPropertyChanged(nameof(Title));
			}
		}
		public string? Description {
			get {
				return _Description;
			}
			set {
				if( value == _Description )
					return;
				_Description = value;
				OnPropertyChanged(nameof(Description));
			}
		}
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
		public DateTime StartDate {
			get {
				return _StartDate ??= DateTime.Today;
			}
			set {
				if( value is { } Value ) {
					_StartDate = Value;
				}
			}
		}
		public DateTime EndDate {
			get {
				return _EndDate ??= DateTime.Today.AddDays(1);
			}
			set {
				if( value is { } Value ) {
					_EndDate = Value;
				}
			}
		}
		public StateEnum State {
			get {
				return _State;
			}
			set {
				if( value == _State )
					return;
				_State = value;
				OnPropertyChanged(nameof(State));
			}
		}
		#endregion

		#region public Commands
		public ICommand SaveGoalCommand => _SaveGoalCommand ??=
			new RelayCommand(parameter => {
				if( SelectedCategory is Category category && Title is string && Description is string ) {
					var neu = new Goal(
						new Identification(Title, Description, category.ID.Color),
						new DateSpan(StartDate, EndDate),
						State);
					if( SelectedGoal is Goal goal )
						goal.Children.Add(neu);
					else
						category.Children.Add(neu);
					AlertManager.ObjektErstellt(nameof(Goal), Title);
				}
				else {
					AlertManager.InputInkorrekt("");
				}
			});

		#endregion

		#region constructors
		public NewGoalViewModel( ObservableCollection<Category> categoryList )
			=> this.CategoryList = categoryList;
		#endregion

		#region methods

		#endregion

	}
}
