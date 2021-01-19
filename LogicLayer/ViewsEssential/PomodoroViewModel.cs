using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using ModelLayer.Classes;
using System;
using System.Windows.Input;

namespace LogicLayer.Views {

	public class PomodoroViewModel : ContentValidationViewModel<WorkItem> {

		#region private fields
		private ICommand? _StartStopCommand;
		private ICommand? _DelayCommand;
		private ICommand? _SaveTimeCommand;
		private ICommand? _UpdateTimesCommand;
		#endregion

		#region public properties
		public PomodoroClock Clock { get; }
		public WorkItem? WorkItem { get; private set; }
		#endregion

		#region public commands
		public ICommand StartStopCommand => _StartStopCommand ??=
			new RelayCommand(
				parameter => Clock.StartStopClock(),
				parameter => NoErrors
			);
		public ICommand DelayCommand => _DelayCommand ??=
			new RelayCommand(
				parameter => Clock.DelayWorkMode(),
				parameter => NoErrors
			);
		public ICommand SaveTimeCommand => _SaveTimeCommand ??=
			new RelayCommand(
				parameter => {
					if( WorkItem is WorkItem workItem )
						workItem.Time += (Clock.GetTotalAndReset());
				},
				parameter => NoErrors
			);
		public ICommand UpdateTimesCommand => _UpdateTimesCommand ??=
			new RelayCommand(
				parameter => {
				},
				parameter => NoErrors
			);
		#endregion

		#region constructor
		public PomodoroViewModel()
			=> Clock = new PomodoroClock( TimeSpan.FromMinutes( 45 ), TimeSpan.FromMinutes( 12 ), TimeSpan.FromMinutes( 8 ) );
		#endregion

		#region public methods
		public override void Clear()
			=> WorkItem = null;
		public override void Fill( WorkItem item )
			=> WorkItem = item;
		#endregion

	}
}
