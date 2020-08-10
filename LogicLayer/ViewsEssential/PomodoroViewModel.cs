using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System;

namespace LogicLayer.Views {
	public class PomodoroViewModel : ViewModelBase {

		#region fields

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
