using ModelLayer.Enums;
using ModelLayer.Extensions;
using ModelLayer.Utility;
using System;
using System.Diagnostics;
using System.Timers;

namespace ModelLayer.Classes {
	public class PomodoroClock : ObservableObject {

		#region private fields

		// private Timer to Update the Time
		private Timer _Counter = new Timer( ){ Interval = TimeSpan.FromSeconds(1).TotalMilliseconds };
		// TimeSpan to keep track when to elapse the Counter
		private TimeSpan _DestinationTime;

		#endregion

		#region public properties

		/// <summary>
		/// The Time in the current Cycle
		/// </summary>
		public TimeSpan Time { get; set; }
		/// <summary>
		/// The total Time worked on the same Task
		/// </summary>
		public TimeSpan TotalTime { get; set; }

		/// <summary>
		/// If true, the Timer Counts down from the corresponding inputTime
		/// </summary>
		public bool CountDown { get; set; }

#warning i could implement a method to update the counter, whenever the inputTime gets changed
		/// <summary>
		/// The duration of the work cycle
		/// </summary>
		public TimeSpan DurationWorkCycle { get; set; }
		/// <summary>
		/// The duration of the break cycle
		/// </summary>
		public TimeSpan DurationBreakCycle { get; set; }
		/// <summary>
		/// The duration to delay the next cycle
		/// </summary>
		public TimeSpan DurationDelayCycle { get; set; }

		/// <summary>
		/// workMode in which the clock is in
		/// </summary>
		public WorkModeEnum CurrentWorkMode { get; set; }
		/// <summary>
		/// predefined workMode to calculate what action will be executed
		/// </summary>
		public WorkModeEnum NextWorkMode { get; set; }

		/// <summary>
		/// Text, to which a Button can be bound (shows the next action of that button)
		/// </summary>
		public string DelayText { get; set; }
		/// <summary>
		/// Text, to which a Button can be bound (shows the next action of that button)
		/// </summary>
		public string StartStopText { get; set; }

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

			Elapsed?.Invoke(CurrentWorkMode, GetActualTime());

			AddTimeIfWork();
			ResetCounter();
			StartClock();
			UpdateButtonText();
		}

		#endregion

		#region constructor

		public PomodoroClock( TimeSpan inputWorkTime, TimeSpan inputBreakTime, TimeSpan inputDelayTime ) {
			DurationWorkCycle = inputWorkTime;
			DurationBreakCycle = inputBreakTime;
			DurationDelayCycle = inputDelayTime;

			Time = TimeSpan.Zero;
			TotalTime = TimeSpan.Zero;

			CurrentWorkMode = WorkModeEnum.Stop;
			NextWorkMode = WorkModeEnum.Work;

			DelayText = "Start work!";
			StartStopText = "Start timer!";
		}

		#endregion

		#region public methods

		/// <summary>
		/// changes the direction, in which the counter runs
		/// </summary>
		/// <param name="countDown">if <see langword="true"/> it counts down, if <see langword="false"/> the <see cref="PomodoroClock"/> will cound Up and if <see langword="null"/> it switches the countdirection </param>
		public void UpdateCountDirection( bool? countDown = null ) {
			double memory = GetActualTime().TotalSeconds;
			CountDown = countDown ?? !CountDown;

			if( _Counter.Enabled is true ) {
				_Counter.Stop();
				NextWorkMode = CurrentWorkMode;
				ResetCounter();
				if( CountDown is true )
					memory = GetCountStart().TotalSeconds - memory;
				StartClock(TimeSpan.FromSeconds(memory));
			}
		}

		/// <summary>
		/// method to switch between stop and start
		/// </summary>
		public void StartStopClock() {
			// Start new Timer
			if( CurrentWorkMode is WorkModeEnum.Stop )
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
			case WorkModeEnum.Stop:
				break;
			case WorkModeEnum.Work:
				NextWorkMode = WorkModeEnum.DelayBreak;
				break;
			case WorkModeEnum.DelayBreak:
				NextWorkMode = WorkModeEnum.Break;
				break;
			case WorkModeEnum.Break:
				NextWorkMode = WorkModeEnum.DelayWork;
				break;
			case WorkModeEnum.DelayWork:
				NextWorkMode = WorkModeEnum.Work;
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
			CurrentWorkMode = WorkModeEnum.Stop;
			NextWorkMode = WorkModeEnum.Work;
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
			case WorkModeEnum.Stop:
				StartStopText = "Start timer!";
				DelayText = "Start work!";
				break;
			case WorkModeEnum.Work:
				StartStopText = "Stop timer!";
				DelayText = "Delay break!";
				break;
			case WorkModeEnum.Break:
				StartStopText = "Stop timer!";
				DelayText = "Delay work!";
				break;
			case WorkModeEnum.DelayWork:
				StartStopText = "Stop timer!";
				DelayText = "Back to work!";
				break;
			case WorkModeEnum.DelayBreak:
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
			if( CurrentWorkMode is WorkModeEnum.DelayBreak || CurrentWorkMode is WorkModeEnum.Work )
				TotalTime = TotalTime.Add(GetActualTime());
		}

		/// <summary>
		/// returns the correct Time, no matter, if the clock counts upwards or downwards
		/// </summary>
		private TimeSpan GetActualTime() {
			if( CountDown is true )
				return GetCountStart() - Time;
			return Time;
		}
		/// <summary>
		/// returns the Timespan, where the clock has to start
		/// </summary>
		private TimeSpan GetCountStart() => CurrentWorkMode switch
		{
			WorkModeEnum.Work => CountDown ? DurationWorkCycle : TimeSpan.Zero,
			WorkModeEnum.Break => CountDown ? DurationBreakCycle : TimeSpan.Zero,
			WorkModeEnum.DelayWork => CountDown ? DurationDelayCycle : TimeSpan.Zero,
			WorkModeEnum.DelayBreak => CountDown ? DurationDelayCycle : TimeSpan.Zero,
			_ => CountDown ? DurationWorkCycle : TimeSpan.Zero,
		};
		/// <summary>
		/// returns the TimeSpan, where the clock has to stop
		/// </summary>
		private TimeSpan GetCountGoal() => CurrentWorkMode switch
		{
			WorkModeEnum.Work => CountDown ? TimeSpan.Zero : DurationWorkCycle,
			WorkModeEnum.Break => CountDown ? TimeSpan.Zero : DurationBreakCycle,
			WorkModeEnum.DelayWork => CountDown ? TimeSpan.Zero : DurationDelayCycle,
			WorkModeEnum.DelayBreak => CountDown ? TimeSpan.Zero : DurationDelayCycle,
			_ => CountDown ? TimeSpan.Zero : DurationWorkCycle,
		};
		/// <summary>
		/// returns a workMode where the clock will go if there is no interruption
		/// </summary>
		private WorkModeEnum GetNextWorkMode() => CurrentWorkMode switch
		{
			WorkModeEnum.Stop => WorkModeEnum.Work,
			WorkModeEnum.Work => WorkModeEnum.Break,
			WorkModeEnum.Break => WorkModeEnum.Work,
			WorkModeEnum.DelayWork => WorkModeEnum.Work,
			WorkModeEnum.DelayBreak => WorkModeEnum.Break,
			_ => WorkModeEnum.Stop,
		};
		/// <summary>
		/// resets the counter (cleanup)
		/// </summary>
		private void ResetCounter() {
			// removes all EventHandler for a bugfree experience
			_Counter.Elapsed -= UpCounter_Tick;
			_Counter.Elapsed -= DownCounter_Tick;

			_Counter.Enabled = false;

			// Sets DestinationTime and Time to zero
			_DestinationTime = TimeSpan.Zero;
			Time = TimeSpan.Zero;
		}

		#endregion

	}
}
