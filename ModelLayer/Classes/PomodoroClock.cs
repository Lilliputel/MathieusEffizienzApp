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
			private set {
				_inputWorkTime = value;
				OnPropertyChanged(nameof(InputWorkTime));
			}
		}
		public TimeSpan InputBreakTime {
			get {
				return _inputBreakTime;
			}
			private set {
				_inputBreakTime = value;
				OnPropertyChanged(nameof(InputBreakTime));
			}
		}
		public TimeSpan InputDelayTime {
			get {
				return _inputDelayTime;
			}
			private set {
				_inputDelayTime = value;
				OnPropertyChanged(nameof(InputDelayTime));
			}
		}

		public EnumWorkMode WorkMode {
			get {
				return _workMode;
			}
			set {
				_workMode = value;
				OnPropertyChanged(nameof(WorkMode));
			}
		}

		public DispatcherTimer Counter { get; private set; } = new DispatcherTimer();

		/// <summary>
		/// Event gets Invoked, when a Timer is Elapsed and gets Passed the Elapsed Time and the WorkMode
		/// </summary>
		public event EventHandler Elapsed;

		#endregion

		#region initializer

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
			EventHandler elapsed = Elapsed;
			elapsed?.Invoke(this, e);
		}

		#endregion

		#region private

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

			TotalTime = TotalTime.Add(Time);
			WorkMode = EnumWorkMode.Break;

		}
		private void BreakTimer_Tick( object? sender, EventArgs e ) {
			BreakTimer.Stop();

		}
		private void DelayTimer_Tick( object? sender, EventArgs e ) {
			DelayTimer.Stop();

		}

		#endregion

	}

}
