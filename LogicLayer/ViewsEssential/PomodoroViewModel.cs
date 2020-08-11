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

		public ICommand StartStopCommand => _StartStopCommand
			??= new RelayCommand(parameter => {
				Clock.StartStopClock();
			});
		public ICommand DelayCommand => _DelayCommand
			??= new RelayCommand(parameter => {
				Clock.DelayWorkMode();
			});
		public ICommand SaveTimeCommand => _SaveTimeCommand
			??= new RelayCommand(parameter => {

			});

		#endregion

		#region properties

		public PomodoroClock Clock { get; set; }

		#endregion

		#region constructor

		public PomodoroViewModel() {

			Clock = new PomodoroClock(TimeSpan.FromMinutes(45), TimeSpan.FromMinutes(12), TimeSpan.FromMinutes(8));

		}

		#endregion

		#region methods

		#endregion

	}
}
