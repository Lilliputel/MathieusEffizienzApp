using LogicLayer.Commands;
using LogicLayer.Manager;
using ModelLayer.Classes;
using ModelLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewGoalViewModel : ValidationViewModel {

		#region private fields
		private ICommand? _SaveGoalCommand;
		#endregion

		#region public properties
		public ICollection<Category> CategoryList { get; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The title has to be specified!" )]
		public string? Title { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The description has to be specified!" )]
		public string? Description { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "A parent has to be selected!" )]
		public Category? SelectedCategory { get; set; }
		public Goal? SelectedGoal { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The startdate has to be defined!" )]
		public DateTime StartDate { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The enddate has to be defined!" )]
		public DateTime EndDate { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "A state has to be selected!" )]
		public StateEnum State { get; set; }
		#endregion

		#region public commands
		public ICommand SaveGoalCommand => _SaveGoalCommand ??=
			new RelayCommand(
				parameter => {
					var neu = new Goal(
						new UserText( Title, Description, SelectedCategory!.UserText.Color ),
						new DateSpan( StartDate, EndDate ),
						State );
					if( SelectedGoal is Goal goal )
						goal.Children.Add( neu );
					else
						SelectedCategory.Children.Add( neu );
					AlertManager.ObjektErstellt( nameof( Goal ), Title );
				},
				parameter => (SelectedCategory is Category && Title is string && Description is string)
			);
		#endregion

		#region constructor
		public NewGoalViewModel( ICollection<Category> categoryList ) {
			CategoryList = categoryList;
			ErrorsChanged += OnErrorsChanged;
		}
		#endregion

		#region private methods
		private void OnErrorsChanged( object sender, DataErrorsChangedEventArgs e )
			=> (SaveGoalCommand as RelayCommand)?.RaiseCanExecuteChanged( sender );
		#endregion

	}
}
