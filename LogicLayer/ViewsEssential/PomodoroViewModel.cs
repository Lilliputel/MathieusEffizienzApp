using LogicLayer.Commands;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System;
using System.Windows.Input;

namespace LogicLayer.Views {

	public class PomodoroViewModel : ViewModelBase {

		#region fields

		private ICommand? _StartStopCommand;
		private ICommand? _DelayCommand;
		private ICommand? _SaveTimeCommand;

		#endregion

		#region commands

		public ICommand StartStopCommand => _StartStopCommand ??=
			new RelayCommand(
				parameter => {
					Clock.StartStopClock();
				}
			);
		public ICommand DelayCommand => _DelayCommand ??=
			new RelayCommand(
				parameter => {
					Clock.DelayWorkMode();
				}
			);
		public ICommand SaveTimeCommand => _SaveTimeCommand ??=
			new RelayCommand(
				parameter => {
					if( this.WorkItem is WorkItem workItem )
						workItem.Time += ( this.Clock.GetTotalAndReset() );
				},
				obj => this.WorkItem is { }
			);

		#endregion

		#region properties

		public PomodoroClock Clock { get; set; }

		public WorkItem? WorkItem { get; set; }

		#endregion

		#region constructor

		public PomodoroViewModel() {
			this.Clock = new PomodoroClock(TimeSpan.FromMinutes(45), TimeSpan.FromMinutes(12), TimeSpan.FromMinutes(8));
		}

		public PomodoroViewModel( WorkItem workItem ) : this() {
			this.WorkItem = workItem;
		}

		#endregion


	}
}
