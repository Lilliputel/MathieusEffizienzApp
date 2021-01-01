using DataLayer;
using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using LogicLayer.Services;
using ModelLayer.Classes;
using PropertyChanged;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewDayTimeViewModel : ValidationViewModel {

		#region private fields
		private readonly IRepository _DataService;
		private ICommand? _SaveDayTimeCommand;
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
#warning this does not update the PlanView
		public ICommand SaveDayTimeCommand => _SaveDayTimeCommand ??=
			 new RelayCommand(
				 parameter => {
					 var newDT = new DoubleTime( (DayOfWeek)DayOfWeek!, StartTime, EndTime, SelectedCategory! );
					 _DataService.Insert( newDT );
					 _DataService.Save();
					 NotificationService.ObjektErstellt( nameof( Category ), newDT.ToString() );
				 },
				 parameter => NoErrors );
		#endregion

		#region constructor
		public NewDayTimeViewModel( IRepository dataService ) {
			_DataService = dataService;
			ErrorsChanged += OnErrorsChanged;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
		}
		#endregion

		#region private methods
		[SuppressPropertyChangedWarnings]
		private void OnErrorsChanged( object? sender, DataErrorsChangedEventArgs e )
			=> (SaveDayTimeCommand as RelayCommand)?.RaiseCanExecuteChanged( sender );
		#endregion

	}
}
