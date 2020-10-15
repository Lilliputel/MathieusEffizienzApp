using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Enums;
using ModelLayer.Interfaces;
using System;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewGoalViewModel : ViewModelBase {

		#region private fields
		private ICommand? _SaveGoalCommand;
		#endregion

		#region public properties
		public IAccountableParent<Category> CategoryList { get; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public Category? SelectedCategory { get; set; }
		public Goal? SelectedGoal { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public StateEnum State { get; set; }
		#endregion

		#region public Commands
		public ICommand SaveGoalCommand => _SaveGoalCommand ??=
			new RelayCommand(
				parameter => {
					var neu = new Goal(
						new UserText(Title, Description, SelectedCategory.UserText.Color),
						new DateSpan(StartDate, EndDate),
						State);
					if( SelectedGoal is Goal goal )
						goal.Children.Add(neu);
					else
						SelectedCategory.Children.Add(neu);
					AlertManager.ObjektErstellt(nameof(Goal), Title);
				},
				_ => ( SelectedCategory is Category && Title is string && Description is string )
			);

		#endregion

		#region constructors
		public NewGoalViewModel( IAccountableParent<Category> categoryList )
			=> CategoryList = categoryList;
		#endregion


	}
}
