using ModelLayer.Enums;
using ModelLayer.Extensions;
using ModelLayer.Utility;
using System;
using System.Diagnostics;
using System.Timers;

namespace ModelLayer.Classes {

#warning Issue: If I change the Countdirection, when the timer is not running, it starts to count!?

	public class PomodoroClock : ObservableObject {

		#region private fields

		private TimeSpan _Time = TimeSpan.Zero;
		private TimeSpan _TotalTime = TimeSpan.Zero;

		private bool _CountDown = false;

		private TimeSpan _DurationWorkCycle;
		private TimeSpan _DurationBreakCycle;
		private TimeSpan _DurationDelayCycle;

		private EnumWorkMode _CurrentWorkMode = EnumWorkMode.Stop;
		private EnumWorkMode _NextWorkMode = EnumWorkMode.Work;

		private string _DelayText = "Start work!";
		private string _StartStopText = "Start timer!";

		// private Timer to Update the Time
		private Timer _Counter = new Timer( ){ Interval = TimeSpan.FromSeconds(1).TotalMilliseconds };
		// TimeSpan to keep track when to elapse the Counter
		private TimeSpan _DestinationTime;

		#endregion

		#region public properties

		/// <summary>
		/// The Time in the current Cycle
		/// </summary>
		public TimeSpan Time {
			get {
				return _Time;
			}
			private set {
				_Time = value;
				OnPropertyChanged(nameof(Time));
			}
		}
		/// <summary>
		/// The total Time worked on the same Task
		/// </summary>
		public TimeSpan TotalTime {
			get {
				return _TotalTime;
			}
			private set {
				_TotalTime = value;
				OnPropertyChanged(nameof(TotalTime));
			}
		}

		/// <summary>
		/// If true, the Timer Counts down from the corresponding inputTime
		/// </summary>
		public bool CountDown {
			get {
				return _CountDown;
			}
			private set {
				if( value == _CountDown )
					return;
				_CountDown = value;
				OnPropertyChanged(nameof(CountDown));
			}
		}

#warning i could implement a method to update the counter, whenever the inputTime gets changed
		/// <summary>
		/// The duration of the work cycle
		/// </summary>
		public TimeSpan DurationWorkCycle {
			get {
				return _DurationWorkCycle;
			}
			set {
				_DurationWorkCycle = value;
				OnPropertyChanged(nameof(DurationWorkCycle));
			}
		}
		/// <summary>
		/// The duration of the break cycle
		/// </summary>
		public TimeSpan DurationBreakCycle {
			get {
				return _DurationBreakCycle;
			}
			set {
				_DurationBreakCycle = value;
				OnPropertyChanged(nameof(DurationBreakCycle));
			}
		}
		/// <summary>
		/// The duration to delay the next cycle
		/// </summary>
		public TimeSpan DurationDelayCycle {
			get {
				return _DurationDelayCycle;
			}
			set {
				_DurationDelayCycle = value;
				OnPropertyChanged(nameof(DurationDelayCycle));
			}
		}

		/// <summary>
		/// workMode in which the clock is in
		/// </summary>
		public EnumWorkMode CurrentWorkMode {
			get {
				return _CurrentWorkMode;
			}
			set {
				_CurrentWorkMode = value;
				OnPropertyChanged(nameof(CurrentWorkMode));
			}
		}
		/// <summary>
		/// predefined workMode to calculate what action will be executed
		/// </summary>
		public EnumWorkMode NextWorkMode {
			get {
				return _NextWorkMode;
			}
			set {
				_NextWorkMode = value;
				OnPropertyChanged(nameof(NextWorkMode));
			}
		}

		/// <summary>
		/// Text, to which a Button can be bound (shows the next action of that button)
		/// </summary>
		public string DelayText {
			get {
				return _DelayText;
			}
			set {
				_DelayText = value;
				OnPropertyChanged(nameof(DelayText));
			}
		}
		/// <summary>
		/// Text, to which a Button can be bound (shows the next action of that button)
		/// </summary>
		public string StartStopText {
			get {
				return _StartStopText;
			}
			set {
				_StartStopText = value;
				OnPropertyChanged(nameof(StartStopText));
			}
		}

		#endregion

		#region public Events

		/// <summary>
		/// Event gets Invoked, when a Timer is Elapsed and gets Passed the Elapsed Time and the WorkMode
		/// </summary>
		public event PomodoroEventHandler? Elapsed;

		/// <summary>
		/// Call to Invoke the event Elapsed, if a Timer Tick gets Elapsed
		/// </summary>
		protected virtual void OnElapsed() {
			PomodoroEventArgs e = new PomodoroEventArgs(){
				Time = GetActualTime(),
				WorkMode = CurrentWorkMode
			};
			Elapsed?.Invoke(this, e);

			AddTimeIfWork();
			ResetCounter();
			StartClock();
			UpdateButtonText();
		}

		#endregion

		#region constructor

		public PomodoroClock( TimeSpan inputWorkTime, TimeSpan inputBreakTime, TimeSpan inputDelayTime ) {
			this.DurationWorkCycle = inputWorkTime;
			this.DurationBreakCycle = inputBreakTime;
			this.DurationDelayCycle = inputDelayTime;
		}

		#endregion

		#region public methods

		/// <summary>
		/// changes the direction, in which the counter runs
		/// </summary>
		/// <param name="countDown">if <see langword="true"/> it counts down, if <see langword="false"/> the <see cref="PomodoroClock"/> will cound Up and if <see langword="null"/> it switches the countdirection </param>
		public void UpdateCountDirection( bool? countDown = null ) {
			double memory = GetActualTime().TotalSeconds;
			this.CountDown = countDown ?? !this.CountDown;
			this.NextWorkMode = this.CurrentWorkMode;
			ResetCounter();
			if( this.CountDown is true )
				this.Time = TimeSpan.FromSeconds(GetCountStart().TotalSeconds - memory);
			else
				this.Time = TimeSpan.FromSeconds(memory);
			StartClock(this.Time);
		}

		/// <summary>
		/// method to switch between stop and start
		/// </summary>
		public void StartStopClock() {
			// Start new Timer
			if( CurrentWorkMode is EnumWorkMode.Stop )
				StartClock();
			// Stop the Timer
			else {
				StopClock();
			}
			UpdateButtonText();
		}

		/// <summary>
		/// method to delay the next workmode (it is only possible to delay once)
		/// </summary>
		public void DelayWorkMode() {
			AddTimeIfWork();
			ResetCounter();

			switch( CurrentWorkMode ) {
			case EnumWorkMode.Stop:
				break;
			case EnumWorkMode.Work:
				this.NextWorkMode = EnumWorkMode.DelayBreak;
				break;
			case EnumWorkMode.DelayBreak:
				this.NextWorkMode = EnumWorkMode.Break;
				break;
			case EnumWorkMode.Break:
				this.NextWorkMode = EnumWorkMode.DelayWork;
				break;
			case EnumWorkMode.DelayWork:
				this.NextWorkMode = EnumWorkMode.Work;
				break;
			default:
				Debug.WriteLine($"PomodoroClock hat einen EnumWorkMode erreicht, den es nicht gibt: {CurrentWorkMode}");
				break;
			}

			StartClock();
			UpdateButtonText();
		}

		/// <summary>
		/// Gets the actual TotalTime, stops the Clock and resets everything
		/// </summary>
		/// <returns> a <see cref="TimeSpan"/> with the total time worked </returns>
		public TimeSpan GetTotalAndReset() {
			double bridge = TotalTime.TotalSeconds;
			StopClock();
			TotalTime = TimeSpan.Zero;
			UpdateButtonText();
			return TimeSpan.FromSeconds(bridge);
		}

		/// <summary>
		/// Initializes the Clock and starts with a workcycle
		/// </summary>
		public void StartClock( TimeSpan? countStart = null ) {
			// Set the Next WorkMode to the new Cicle
			CurrentWorkMode = NextWorkMode;
			NextWorkMode = GetNextWorkMode();
			// Update the destinationTime of the new Circle
			_DestinationTime = GetCountGoal();
			// Update the startTime for the new Circle
			Time = countStart ?? GetCountStart();

			// Attaches the Correct CounterTick
			if( CountDown is true )
				_Counter.Elapsed += DownCounter_Tick;
			else
				_Counter.Elapsed += UpCounter_Tick;

			// Starts the new Counter
			_Counter.Start();
		}

		/// <summary>
		/// Stops the Clock
		/// </summary>
		public void StopClock() {
			AddTimeIfWork();
			ResetCounter();
			CurrentWorkMode = EnumWorkMode.Stop;
			NextWorkMode = EnumWorkMode.Work;
		}

		#endregion

		#region private Timer-Ticks

		/// <summary>
		/// Tickevent which gets called on every intervall from zero upwards
		/// </summary>
		private void UpCounter_Tick( object? sender, EventArgs e ) {
			Time = Time.Add(TimeSpan.FromSeconds(1));
			if( Time == _DestinationTime )
				OnElapsed();
		}
		/// <summary>
		/// Tickevent which gets called on every intervall from a startTime down to zero
		/// </summary>
		private void DownCounter_Tick( object? sender, EventArgs e ) {
			Time = Time.Subtract(TimeSpan.FromSeconds(1));
			if( Time == _DestinationTime )
				OnElapsed();
		}

		#endregion

		#region private helperMethods

		/// <summary>
		/// changes the Texts, which will be displayed to control the clock
		/// </summary>
		private void UpdateButtonText() {
			switch( CurrentWorkMode ) {
			case EnumWorkMode.Stop:
				StartStopText = "Start timer!";
				DelayText = "Start work!";
				break;
			case EnumWorkMode.Work:
				StartStopText = "Stop timer!";
				DelayText = "Delay break!";
				break;
			case EnumWorkMode.Break:
				StartStopText = "Stop timer!";
				DelayText = "Delay work!";
				break;
			case EnumWorkMode.DelayWork:
				StartStopText = "Stop timer!";
				DelayText = "Back to work!";
				break;
			case EnumWorkMode.DelayBreak:
				StartStopText = "Stop timer!";
				DelayText = "Take a break!";
				break;
			default:
				StartStopText = "Stop timer!";
				DelayText = "Start work!";
				break;
			}
		}

		/// <summary>
		/// checks if the current time can be added to the total and adds it
		/// </summary>
		private void AddTimeIfWork() {
			if( CurrentWorkMode is EnumWorkMode.DelayBreak || CurrentWorkMode is EnumWorkMode.Work )
				TotalTime = TotalTime.Add(GetActualTime());
		}

		/// <summary>
		/// returns the correct Time, no matter, if the clock counts upwards or downwards
		/// </summary>
		private TimeSpan GetActualTime() {
			if( CountDown is true )
				return _DestinationTime - Time;
			return Time;
		}
		/// <summary>
		/// returns the Timespan, where the clock has to start
		/// </summary>
		private TimeSpan GetCountStart() => CurrentWorkMode switch
		{
			EnumWorkMode.Work => CountDown ? DurationWorkCycle : TimeSpan.Zero,
			EnumWorkMode.Break => CountDown ? DurationBreakCycle : TimeSpan.Zero,
			EnumWorkMode.DelayWork => CountDown ? DurationDelayCycle : TimeSpan.Zero,
			EnumWorkMode.DelayBreak => CountDown ? DurationDelayCycle : TimeSpan.Zero,
			_ => CountDown ? DurationWorkCycle : TimeSpan.Zero,
		};
		/// <summary>
		/// returns the TimeSpan, where the clock has to stop
		/// </summary>
		private TimeSpan GetCountGoal() => CurrentWorkMode switch
		{
			EnumWorkMode.Work => CountDown ? TimeSpan.Zero : DurationWorkCycle,
			EnumWorkMode.Break => CountDown ? TimeSpan.Zero : DurationBreakCycle,
			EnumWorkMode.DelayWork => CountDown ? TimeSpan.Zero : DurationDelayCycle,
			EnumWorkMode.DelayBreak => CountDown ? TimeSpan.Zero : DurationDelayCycle,
			_ => CountDown ? TimeSpan.Zero : DurationWorkCycle,
		};
		/// <summary>
		/// returns a workMode where the clock will go if there is no interruption
		/// </summary>
		private EnumWorkMode GetNextWorkMode() => CurrentWorkMode switch
		{
			EnumWorkMode.Stop => EnumWorkMode.Work,
			EnumWorkMode.Work => EnumWorkMode.Break,
			EnumWorkMode.Break => EnumWorkMode.Work,
			EnumWorkMode.DelayWork => EnumWorkMode.Work,
			EnumWorkMode.DelayBreak => EnumWorkMode.Break,
			_ => EnumWorkMode.Stop,
		};
		/// <summary>
		/// resets the counter (cleanup)
		/// </summary>
		private void ResetCounter() {
			// removes all EventHandler for a bugfree experience
			_Counter.Elapsed -= UpCounter_Tick;
			_Counter.Elapsed -= DownCounter_Tick;

			// Sets DestinationTime and Time to zero
			_DestinationTime = TimeSpan.Zero;
			Time = TimeSpan.Zero;
		}

		#endregion

	}
}
