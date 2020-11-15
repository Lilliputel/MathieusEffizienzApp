using ModelLayer.Enums;
using ModelLayer.Utility;
using System;
using System.Timers;

namespace ModelLayer.Classes {
	public class PomodoroClock : ObservableObject {

		//TODO i could implement a method to update the counter, whenever the inputTime gets changed
		#region private fields
		private readonly PomodoroTimes _Times;
		private readonly PomodoroModes _Modes;

		private TimeSpan _DestinationTime;

		private readonly Timer _Counter;
		private Action<TimeSpan> _Tick;
		#endregion

		#region public properties
		public TimeSpan Time { get; private set; }
		public TimeSpan TotalTime { get; private set; }
		public bool CountDown { get; private set; }
		public WorkModeEnum WorkMode { get; private set; }
		#endregion

		#region public Events
		public event EventHandler<(WorkModeEnum, TimeSpan)>? Elapsed;
		protected virtual void RaiseElapsed() {
			Elapsed?.Invoke( this, (WorkMode, GetActualTime()) );

			AddTimeIfWork();
			ResetCounter();
			StartClock();
		}
		#endregion

		#region constructor
		public PomodoroClock( TimeSpan inputWorkTime, TimeSpan inputBreakTime, TimeSpan inputDelayTime ) {
			Time = TimeSpan.Zero;
			TotalTime = TimeSpan.Zero;
			WorkMode = WorkModeEnum.Stop;

			_Times = new PomodoroTimes( inputWorkTime, inputBreakTime, inputDelayTime );
			_Modes = new PomodoroModes();
			_Counter = new Timer() { Interval = TimeSpan.FromSeconds( 1 ).TotalMilliseconds };
			_Counter.Elapsed += CounterTick;
		}
		#endregion

		#region public methods
		public void UpdateCountDirection( bool? countDown = null ) {
			double memory = GetActualTime().TotalSeconds;
			CountDown = countDown ?? !CountDown;

			if( _Counter.Enabled is true ) {
				_Counter.Stop();
				WorkMode = _Modes.Next( WorkMode );
				ResetCounter();
				if( CountDown is true )
					memory = _Times.GetModeTime( WorkMode ).TotalSeconds - memory;
				StartClock( TimeSpan.FromSeconds( memory ) );
			}
		}
		public void StartStopClock() {
			if( WorkMode is WorkModeEnum.Stop )
				StartClock();
			else
				StopClock();
		}
		public void DelayWorkMode() {
			AddTimeIfWork();
			ResetCounter();

			WorkMode = _Modes.Delay( WorkMode );

			StartClock();
		}
		public TimeSpan GetTotalAndReset() {
			double bridge = TotalTime.TotalSeconds;
			StopClock();
			TotalTime = TimeSpan.Zero;
			return TimeSpan.FromSeconds( bridge );
		}
		public void StartClock( TimeSpan? countStart = null ) {
			var time = _Times.GetModeTime( WorkMode );

			_DestinationTime = CountDown ? TimeSpan.Zero : time;
			Time = countStart ?? (CountDown ? time : TimeSpan.Zero);
			// Attaches the Correct CounterTick
			_Tick = CountDown is true
				? new Action<TimeSpan>( time => time -= TimeSpan.FromSeconds( 1 ) )
				: new Action<TimeSpan>( time => time += TimeSpan.FromSeconds( 1 ) );
			_Counter.Start();
		}
		public void StopClock() {
			AddTimeIfWork();
			ResetCounter();
			WorkMode = WorkModeEnum.Stop;
		}
		#endregion

		#region private Timer-Ticks
		private void CounterTick( object? sender, EventArgs e ) {
			_Tick( Time );
			if( (CountDown && Time <= _DestinationTime)
				|| (CountDown is false && Time >= _DestinationTime) )
				RaiseElapsed();
		}
		#endregion

		#region private helperMethods
		private void AddTimeIfWork() {
			if( WorkMode is WorkModeEnum.DelayBreak || WorkMode is WorkModeEnum.Work )
				TotalTime = TotalTime.Add( GetActualTime() );
		}
		private TimeSpan GetActualTime()
			=> CountDown is true
			? _Times.GetModeTime( WorkMode ) - Time
			: Time;
		private void ResetCounter() {
			// removes all EventHandler for a bugfree experience
			_Counter.Enabled = false;
			_Counter.Elapsed -= CounterTick;

			// resets DestinationTime and Time to zero
			_DestinationTime = TimeSpan.Zero;
			Time = TimeSpan.Zero;
		}
		#endregion

	}
}
