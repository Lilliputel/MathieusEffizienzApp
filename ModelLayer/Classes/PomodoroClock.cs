using ModelLayer.Enums;
using ModelLayer.Extensions;
using ModelLayer.Utility;
using System;
using System.Windows.Threading;

namespace ModelLayer.Classes {

	public class PomodoroClock : ObservableObject {

		#region fields

		private TimeSpan _time = TimeSpan.Zero;
		private TimeSpan _totalTime = TimeSpan.Zero;

		private TimeSpan _inputWorkTime;
		private TimeSpan _inputBreakTime;
		private TimeSpan _inputDelayTime;

		private EnumWorkMode _workMode = EnumWorkMode.Stop;
		private string _displayNextMode = "Start timer!";

		// private Timers for the different WorkModes
		private DispatcherTimer WorkTimer = new DispatcherTimer();
		private DispatcherTimer BreakTimer = new DispatcherTimer();
		private DispatcherTimer DelayTimer = new DispatcherTimer();


		#endregion

		#region properties

		public TimeSpan Time {
			get {
				return _time;
			}
			private set {
				_time = value;
				OnPropertyChanged(nameof(Time));
			}
		}
		public TimeSpan TotalTime {
			get {
				return _totalTime;
			}
			private set {
				_totalTime = value;
				OnPropertyChanged(nameof(TotalTime));
			}
		}

		public TimeSpan InputWorkTime {
			get {
				return _inputWorkTime;
			}
			set {
				_inputWorkTime = value;
				OnPropertyChanged(nameof(InputWorkTime));
			}
		}
		public TimeSpan InputBreakTime {
			get {
				return _inputBreakTime;
			}
			set {
				_inputBreakTime = value;
				OnPropertyChanged(nameof(InputBreakTime));
			}
		}
		public TimeSpan InputDelayTime {
			get {
				return _inputDelayTime;
			}
			set {
				_inputDelayTime = value;
				OnPropertyChanged(nameof(InputDelayTime));
			}
		}

		public EnumWorkMode NextWorkMode {
			get {
				return _workMode;
			}
			set {
				_workMode = value;
				OnPropertyChanged(nameof(NextWorkMode));
			}
		}
		public string DisplayNextMode {
			get {
				return _displayNextMode;
			}
			set {
				_displayNextMode = value;
				OnPropertyChanged(nameof(DisplayNextMode));
			}
		}

		public DispatcherTimer Counter { get; private set; } = new DispatcherTimer();

		/// <summary>
		/// Event gets Invoked, when a Timer is Elapsed and gets Passed the Elapsed Time and the WorkMode
		/// </summary>
		public event EventHandler? Elapsed;

		#endregion

		#region constructor

		public PomodoroClock( TimeSpan inputWorkTime, TimeSpan inputBreakTime, TimeSpan inputDelayTime ) {
			this.InputWorkTime = inputWorkTime;
			this.InputBreakTime = inputBreakTime;
			this.InputDelayTime = inputDelayTime;

			DefineAllTimers();
		}

		#endregion

		#region methods

		/// <summary>
		/// Call to Invoke the event Elapsed, if a Timer Tick gets Elapsed
		/// </summary>
		protected virtual void OnElapsed( PomodoroEventArgs e ) {
			Elapsed?.Invoke(this, e);
		}

		public void ChangeWorkMode() {
			StopAllTimers();
			UpdateWorkMode();
		}

		public void DelayWorkMode() {
			StopAllTimers();
			NextWorkMode = EnumWorkMode.Delay;
			UpdateWorkMode();
		}

		#endregion

		#region private TimerMethods

		/// <summary>
		/// Sets all Timers with their respective intervalltime and the corresponding EventHandler
		/// </summary>
		private void DefineAllTimers() {
			// initialize the Counter, which elapses every second to update the Time
			this.Counter.Tick += this.Counter_Tick;
			this.Counter.Interval = TimeSpan.FromSeconds(1);

			// initialize the Counter, which elapse at their corresponding Times
			this.WorkTimer.Tick += this.WorkTimer_Tick;
			this.WorkTimer.Interval = this.InputWorkTime;

			this.BreakTimer.Tick += this.BreakTimer_Tick;
			this.BreakTimer.Interval = this.InputWorkTime;

			this.DelayTimer.Tick += this.DelayTimer_Tick;
			this.DelayTimer.Interval = this.InputWorkTime;
		}

		private void Counter_Tick( object? sender, EventArgs e ) {
			Time = Time.Add(TimeSpan.FromSeconds(1));
		}

		private void WorkTimer_Tick( object? sender, EventArgs e ) {
			WorkTimer.Stop();
			UpdateWorkMode();

			OnElapsed(new PomodoroEventArgs() {
				Time = Time,
				WorkMode = CurrentWorkMode()
			});
		}
		private void BreakTimer_Tick( object? sender, EventArgs e ) {
			BreakTimer.Stop();
			UpdateWorkMode();

			OnElapsed(new PomodoroEventArgs() {
				Time = Time,
				WorkMode = CurrentWorkMode()
			});
		}
		private void DelayTimer_Tick( object? sender, EventArgs e ) {
			DelayTimer.Stop();
			UpdateWorkMode();

			OnElapsed(new PomodoroEventArgs() {
				Time = Time,
				WorkMode = CurrentWorkMode()
			});
		}

		#endregion

		#region private HelperMethods

		private void UpdateWorkMode() {
			switch( NextWorkMode ) {

			// going to Stopmode, stopping all Timers
			case EnumWorkMode.Stop:
				NextWorkMode = EnumWorkMode.Work;
				DisplayNextMode = "Start timer!";

				// stop all timers (unnecessary, but more comfortable, than switch)
				StopAllTimers();
				break;

			// going to Workmode
			case EnumWorkMode.Work:
				NextWorkMode = EnumWorkMode.Break;
				WorkTimer.Start();
				DisplayNextMode = "Make a pause!";
				break;

			// going to Breakmode
			case EnumWorkMode.Break:
				AddTimeToTotal();
				NextWorkMode = EnumWorkMode.Work;
				BreakTimer.Start();
				DisplayNextMode = "Back to work!";
				break;

			// going to Delaymode
			case EnumWorkMode.Delay:

				// coming from Breakmode
				if( DisplayNextMode == "Back to work!" ) {
					NextWorkMode = EnumWorkMode.Work;
					DelayTimer.Start();
					DisplayNextMode = "Stop procrastinating!";
				}

				// coming from Workmode
				else {
					AddTimeToTotal();
					NextWorkMode = EnumWorkMode.Break;
					DelayTimer.Start();
					DisplayNextMode = "Finished!";
				}
				break;

			}
		}

		private void StopAllTimers() {
			Counter.Stop();
			WorkTimer.Stop();
			BreakTimer.Stop();
			DelayTimer.Stop();
		}

		private void AddTimeToTotal() {
			TotalTime = TotalTime.Add(Time);
		}

		private EnumWorkMode CurrentWorkMode() {
			switch( DisplayNextMode ) {
			case "Start timer!":
				return EnumWorkMode.Stop;
			case "Make a pause!":
				return EnumWorkMode.Work;
			case "Back to work!":
				return EnumWorkMode.Break;
			case "Finished!":
				return EnumWorkMode.Delay;
			case "Stop procrastinating!":
				return EnumWorkMode.Delay;
			default:
				return EnumWorkMode.Stop;
			}
		}

		#endregion

	}

}
