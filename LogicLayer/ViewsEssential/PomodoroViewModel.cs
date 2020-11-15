using LogicLayer.Commands;
using ModelLayer.Classes;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace LogicLayer.Views {

	public class PomodoroViewModel : ValidationViewModel {

		#region private fields
		private ICommand? _StartStopCommand;
		private ICommand? _DelayCommand;
		private ICommand? _SaveTimeCommand;
		private ICommand? _UpdateTimesCommand;
		#endregion

		#region public properties
		public PomodoroClock Clock { get; }
		public WorkItem? WorkItem { get; private set; }
		public TimeSpan DurationWorkCycle { get; set; }
		public TimeSpan DurationBreakCycle { get; set; }
		public TimeSpan DurationDelayCycle { get; set; }
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
		public ICommand UpdateTimesCommand => _UpdateTimesCommand ??= //TODO refactoring of PomodoroClock
			new RelayCommand(
				parameter => {
				},
				parameter => NoErrors
			);
		#endregion

		#region constructor
		public PomodoroViewModel() {
			ErrorsChanged += OnErrorsChanged;
			DurationWorkCycle = TimeSpan.FromMinutes( 45 );
			DurationBreakCycle = TimeSpan.FromMinutes( 12 );
			DurationDelayCycle = TimeSpan.FromMinutes( 8 );
			Clock = new PomodoroClock( DurationWorkCycle, DurationBreakCycle, DurationDelayCycle );
		}
		public PomodoroViewModel( WorkItem workItem ) : this() {
			WorkItem = workItem;
		}
		#endregion

		#region private methods
		private void OnErrorsChanged( object sender, DataErrorsChangedEventArgs e ) {
			(StartStopCommand as RelayCommand)?.RaiseCanExecuteChanged( sender );
			(DelayCommand as RelayCommand)?.RaiseCanExecuteChanged( sender );
			(SaveTimeCommand as RelayCommand)?.RaiseCanExecuteChanged( sender );
		}
		#endregion

	}
}
