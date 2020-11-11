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
		#endregion

		#region constructor
		public PomodoroViewModel() {
			ErrorsChanged += OnErrorsChanged;
			Clock = new PomodoroClock( TimeSpan.FromMinutes( 45 ), TimeSpan.FromMinutes( 12 ), TimeSpan.FromMinutes( 8 ) );
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
