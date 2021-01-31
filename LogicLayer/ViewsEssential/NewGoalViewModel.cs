using DataLayer;
using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using LogicLayer.Stores;
using ModelLayer.Classes;
using ModelLayer.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewGoalViewModel : ContentValidationViewModel<Goal> {

		#region private fields
		private bool _Editing = false;
		private readonly IRepository _DataService;
		private readonly AlertStore _AlertService;

		private ICommand? _SaveGoalCommand;
		private ICommand? _ChangedCategoryCommand;
		private ICommand? _ChangedGoalCommand;

		private Goal? _SelectedGoalPrev;
		private Category? _SelectedCategoryPrev;
		#endregion

		#region public properties
		public ICollectionView CategoryList { get; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The title has to be specified!" )]
		public string? Title { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The description has to be specified!" )]
		public string? Description { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "A parent has to be selected!" )]
		public Category? SelectedCategory { get; set; }
		public Goal? SelectedGoal { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The startdate has to be defined!" )]
		public DateTime StartDate { get; set; } = DateTime.Today;
		[Required( AllowEmptyStrings = false, ErrorMessage = "The enddate has to be defined!" )]
		public DateTime EndDate { get; set; } = DateTime.Today.AddDays( 1 );
		[Required( AllowEmptyStrings = false, ErrorMessage = "A state has to be selected!" )]
		public StateEnum State { get; set; }
		#endregion

		#region public commands
		public ICommand ChangedGoalCommand => _ChangedGoalCommand
			??= new RelayCommand( parameter => {
				if( parameter is Goal pGoal )
					SelectedGoal = (pGoal == _SelectedGoalPrev) ? null : pGoal;
				_SelectedGoalPrev = SelectedGoal;
			} );
		public ICommand ChangedCategoryCommand => _ChangedCategoryCommand
			??= new RelayCommand( parameter => {
				if( parameter is Category pCategory && pCategory == _SelectedCategoryPrev )
					SelectedCategory = null;
				_SelectedCategoryPrev = SelectedCategory;
			} );
		public ICommand SaveGoalCommand => _SaveGoalCommand ??=
			new RelayCommand(
				parameter => {
					var neu = new Goal(
						new UserText( Title!, Description, SelectedCategory!.UserText.Color ),
						new DateSpan( StartDate, EndDate ),
						State );
					if( _Editing )
						_DataService.Update( neu );
					else {
						if( SelectedGoal is Goal goal )
							goal.Children.Add( neu );
						else
							SelectedCategory.Children.Add( neu );
						_DataService.Insert( neu );
					}
					_DataService.Save();
					Clear();
					_AlertService.ObjektErstellt( nameof( Goal ), Title! );
				},
				parameter => NoErrors );
		#endregion

		#region constructor
		public NewGoalViewModel( IRepository dataService, AlertStore alertService ) {
			_DataService = dataService;
			_AlertService = alertService;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
		}
		#endregion

		#region public methods
		public override void Clear() {
			_Editing = false;
			Title = null;
			Description = null;
			SelectedCategory = null;
			_SelectedCategoryPrev = null;
			SelectedGoal = null;
			_SelectedGoalPrev = null;
			StartDate = DateTime.Today;
			EndDate = DateTime.Today.AddDays( 1 );
			State = StateEnum.ToDo;
		}
		public override void Fill( Goal item ) {
			_Editing = true;
			Title = item.UserText.Title;
			Description = item.UserText.Description;
			SelectedCategory = item.ParentCategory;
			SelectedGoal = item.ParentGoal;
			StartDate = item.Plan.Start;
			EndDate = item.Plan.End;
			State = item.State;
		}
		#endregion

	}
}
