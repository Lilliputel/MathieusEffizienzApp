using DataLayer;
using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using LogicLayer.Stores;
using ModelLayer.Classes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewDayTimeViewModel : ContentValidationViewModel<DoubleTime> {

		#region private fields
		private bool _Editing = false;
		private readonly IRepository _DataService;
		private readonly AlertStore _AlertService;

		private ICommand? _SaveDayTimeCommand;
		private ICommand? _ChangedCategoryCommand;

		private Category? _SelectedCategoryPrev;
		#endregion

		#region public properties
		public ICollectionView CategoryList { get; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "A parent has to be selected!" )]
		public Category? SelectedCategory { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The Day has to be defined!" )]
		public DayOfWeek? DayOfWeek { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public string? Warning { get; set; }
		#endregion

		#region public commands
		public ICommand ChangedCategoryCommand => _ChangedCategoryCommand
			??= new RelayCommand( parameter => {
				if( parameter is Category pCategory && pCategory == _SelectedCategoryPrev )
					SelectedCategory = null;
				_SelectedCategoryPrev = SelectedCategory;
			} );
		public ICommand SaveDayTimeCommand => _SaveDayTimeCommand ??=
			 new RelayCommand(
				 parameter => {
					 try {
						 Warning = null;
						 var newDT = new DoubleTime( (DayOfWeek)DayOfWeek!, StartTime, EndTime, SelectedCategory! );
						 _ = _Editing ? _DataService.Update( newDT ) : _DataService.Insert( newDT );
						 _DataService.Save();
						 Clear();
						 _AlertService.ObjektErstellt( nameof( Category ), newDT.ToString() );
					 }
					 catch( ArgumentException e ) {
						 Warning = e.Message;
					 }
				 },
				 parameter => NoErrors );
		#endregion

		#region constructor
		public NewDayTimeViewModel( IRepository dataService, AlertStore alertService ) {
			_DataService = dataService;
			_AlertService = alertService;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
		}
		#endregion

		#region public methods
		public override bool Clear() {
			_Editing = false;
			try {
				SelectedCategory = null;
				DayOfWeek = System.DayOfWeek.Monday;
				StartTime = TimeSpan.FromHours( DateTime.Now.Hour );
				EndTime = TimeSpan.FromHours( DateTime.Now.Hour + 1 );
				Warning = null;
				return true;
			}
			catch( Exception e ) {
				Warning = e.Message;
				return false;
			}
		}
		public override bool Fill( DoubleTime item ) {
			_Editing = true;
			try {
				SelectedCategory = item.Category;
				DayOfWeek = item.Day;
				StartTime = item.GetTimeSpans().start;
				EndTime = item.GetTimeSpans().end;
				return true;
			}
			catch( Exception e ) {
				Clear();
				Warning = e.Message;
				return false;
			}
		}
		#endregion

	}
}
