using DataLayer;
using LogicLayer.Commands;
using LogicLayer.Manager;
using ModelLayer.Classes;
using PropertyChanged;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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
		public ICommand SaveDayTimeCommand => _SaveDayTimeCommand ??=
			 new RelayCommand(
				 parameter => {
					 var newDT = new DoubleTime( (DayOfWeek)DayOfWeek!, StartTime, EndTime, SelectedCategory! );
					 AddToWeekPlan( newDT );
					 _DataService.Insert( newDT );
					 _DataService.Save();
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
		private Task AddToWeekPlan( DoubleTime newDT )
			=> ObjectManager.WeekPlan.AddItemToDayAsync( newDT );
		[SuppressPropertyChangedWarnings]
		private void OnErrorsChanged( object? sender, DataErrorsChangedEventArgs e )
			=> (SaveDayTimeCommand as RelayCommand)?.RaiseCanExecuteChanged( sender );
		#endregion

	}
}
