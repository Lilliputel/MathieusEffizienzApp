using ModelLayer.Enums;
using ModelLayer.Extensions;
using ModelLayer.Utility;
using System;
using System.Diagnostics;
using System.Timers;

namespace ModelLayer.Classes {
	public class PomodoroClock : ObservableObject {

		//TODO i could implement a method to update the counter, whenever the inputTime gets changed
		#region private fields
		private readonly Timer _Counter;
		private TimeSpan _DestinationTime;
		private WorkModeEnum _CurrentWorkMode;
		private WorkModeEnum _NextWorkMode;
		#endregion

		#region public properties
		public TimeSpan Time { get; private set; }
		public TimeSpan TotalTime { get; private set; }
		public bool CountDown { get; private set; }
		public TimeSpan DurationWorkCycle { get; set; }
		public TimeSpan DurationBreakCycle { get; set; }
		public TimeSpan DurationDelayCycle { get; set; }
		public string DelayText { get; private set; }
		public string StartStopText { get; private set; }
		#endregion

		#region public Events
		public event PomodoroEventHandler? Elapsed;
		protected virtual void RaiseElapsed() {

			Elapsed?.Invoke( _CurrentWorkMode, GetActualTime() );

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

			_Counter = new Timer() { Interval = TimeSpan.FromSeconds( 1 ).TotalMilliseconds };
			_CurrentWorkMode = WorkModeEnum.Stop;
			_NextWorkMode = WorkModeEnum.Work;

			DelayText = "Start work!";
			StartStopText = "Start timer!";
		}
		#endregion

		#region public methods
		public void UpdateCountDirection( bool? countDown = null ) {
			double memory = GetActualTime().TotalSeconds;
			CountDown = countDown ?? !CountDown;

			if( _Counter.Enabled is true ) {
				_Counter.Stop();
				_NextWorkMode = _CurrentWorkMode;
				ResetCounter();
				if( CountDown is true )
					memory = GetCountStart().TotalSeconds - memory;
				StartClock( TimeSpan.FromSeconds( memory ) );
			}
		}
		public void StartStopClock() {
			if( _CurrentWorkMode is WorkModeEnum.Stop )
				StartClock();
			else
				StopClock();
			UpdateButtonText();
		}
		public void DelayWorkMode() {
			AddTimeIfWork();
			ResetCounter();

			switch( _CurrentWorkMode ) {
				case WorkModeEnum.Stop:
					break;
				case WorkModeEnum.Work:
					_NextWorkMode = WorkModeEnum.DelayBreak;
					break;
				case WorkModeEnum.DelayBreak:
					_NextWorkMode = WorkModeEnum.Break;
					break;
				case WorkModeEnum.Break:
					_NextWorkMode = WorkModeEnum.DelayWork;
					break;
				case WorkModeEnum.DelayWork:
					_NextWorkMode = WorkModeEnum.Work;
					break;
				default:
					Debug.WriteLine( $"PomodoroClock hat einen EnumWorkMode erreicht, den es nicht gibt: {_CurrentWorkMode}" );
					break;
			}

			StartClock();
			UpdateButtonText();
		}
		public TimeSpan GetTotalAndReset() {
			double bridge = TotalTime.TotalSeconds;
			StopClock();
			TotalTime = TimeSpan.Zero;
			UpdateButtonText();
			return TimeSpan.FromSeconds( bridge );
		}
		public void StartClock( TimeSpan? countStart = null ) {
			// Set the Next WorkMode to the new Cicle
			_CurrentWorkMode = _NextWorkMode;
			_NextWorkMode = GetNextWorkMode();
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
		public void StopClock() {
			AddTimeIfWork();
			ResetCounter();
			_CurrentWorkMode = WorkModeEnum.Stop;
			_NextWorkMode = WorkModeEnum.Work;
		}
		#endregion

		#region private Timer-Ticks
		private void UpCounter_Tick( object? sender, EventArgs e ) {
			Time = Time.Add( TimeSpan.FromSeconds( 1 ) );
			if( Time == _DestinationTime )
				RaiseElapsed();
		}
		private void DownCounter_Tick( object? sender, EventArgs e ) {
			Time = Time.Subtract( TimeSpan.FromSeconds( 1 ) );
			if( Time == _DestinationTime )
				RaiseElapsed();
		}
		#endregion

		#region private helperMethods
		private void UpdateButtonText() {
			switch( _CurrentWorkMode ) {
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
		private void AddTimeIfWork() {
			if( _CurrentWorkMode is WorkModeEnum.DelayBreak || _CurrentWorkMode is WorkModeEnum.Work )
				TotalTime = TotalTime.Add( GetActualTime() );
		}
		private TimeSpan GetActualTime()
			=> CountDown is true ? GetCountStart() - Time : Time;
		private TimeSpan GetCountStart() => _CurrentWorkMode switch {
			WorkModeEnum.Work => CountDown ? DurationWorkCycle : TimeSpan.Zero,
			WorkModeEnum.Break => CountDown ? DurationBreakCycle : TimeSpan.Zero,
			WorkModeEnum.DelayWork => CountDown ? DurationDelayCycle : TimeSpan.Zero,
			WorkModeEnum.DelayBreak => CountDown ? DurationDelayCycle : TimeSpan.Zero,
			_ => CountDown ? DurationWorkCycle : TimeSpan.Zero,
		};
		private TimeSpan GetCountGoal() => _CurrentWorkMode switch {
			WorkModeEnum.Work => CountDown ? TimeSpan.Zero : DurationWorkCycle,
			WorkModeEnum.Break => CountDown ? TimeSpan.Zero : DurationBreakCycle,
			WorkModeEnum.DelayWork => CountDown ? TimeSpan.Zero : DurationDelayCycle,
			WorkModeEnum.DelayBreak => CountDown ? TimeSpan.Zero : DurationDelayCycle,
			_ => CountDown ? TimeSpan.Zero : DurationWorkCycle,
		};
		private WorkModeEnum GetNextWorkMode() => _CurrentWorkMode switch {
			WorkModeEnum.Stop => WorkModeEnum.Work,
			WorkModeEnum.Work => WorkModeEnum.Break,
			WorkModeEnum.Break => WorkModeEnum.Work,
			WorkModeEnum.DelayWork => WorkModeEnum.Work,
			WorkModeEnum.DelayBreak => WorkModeEnum.Break,
			_ => WorkModeEnum.Stop,
		};
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
