using ModelLayer.Enums;
using ModelLayer.Extensions;
using ModelLayer.Utility;
using System;
using System.Windows.Threading;

namespace ModelLayer.Classes {

	public class PomodoroClock : ObservableObject {

		#region fields

		private TimeSpan _Time = TimeSpan.Zero;
		private TimeSpan _TotalTime = TimeSpan.Zero;

		private bool _CountDown = false;

		private TimeSpan _InputWorkTime;
		private TimeSpan _InputBreakTime;
		private TimeSpan _InputDelayTime;

		private EnumWorkMode _CurrentWorkMode = EnumWorkMode.Stop;
		private EnumWorkMode _NextWorkMode = EnumWorkMode.Work;

		private string _DelayText = "Start work!";
		private string _StartStopText = "Start timer!";

		// private Timer to Update the Time
		private DispatcherTimer _Counter = new DispatcherTimer(){ Interval = TimeSpan.FromSeconds(1) };
		// TimeSpan to keep track when to elapse the Counter
		private TimeSpan _DestinationTime;

		#endregion

		#region properties

		// Time, to count
		public TimeSpan Time {
			get {
				return _Time;
			}
			private set {
				_Time = value;
				OnPropertyChanged(nameof(Time));
			}
		}
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
			set {
				if( value == _CountDown )
					return;
				_CountDown = value;
				OnPropertyChanged(nameof(CountDown));

				UpdateCountDirection();
			}
		}

		// Times, which get set externally and define the countTimes
		public TimeSpan InputWorkTime {
			get {
				return _InputWorkTime;
			}
			set {
				_InputWorkTime = value;
				OnPropertyChanged(nameof(InputWorkTime));
			}
		}
		public TimeSpan InputBreakTime {
			get {
				return _InputBreakTime;
			}
			set {
				_InputBreakTime = value;
				OnPropertyChanged(nameof(InputBreakTime));
			}
		}
		public TimeSpan InputDelayTime {
			get {
				return _InputDelayTime;
			}
			set {
				_InputDelayTime = value;
				OnPropertyChanged(nameof(InputDelayTime));
			}
		}

		// Workmodes, in which the Clock can be
		public EnumWorkMode CurrentWorkMode {
			get {
				return _CurrentWorkMode;
			}
			set {
				_CurrentWorkMode = value;
				OnPropertyChanged(nameof(CurrentWorkMode));
			}
		}
		public EnumWorkMode NextWorkMode {
			get {
				return _NextWorkMode;
			}
			set {
				_NextWorkMode = value;
				OnPropertyChanged(nameof(NextWorkMode));
			}
		}

		// Text, to which a Button can be bound (shows the next action of that button)
		public string DelayText {
			get {
				return _DelayText;
			}
			set {
				_DelayText = value;
				OnPropertyChanged(nameof(DelayText));
			}
		}
		public string StartStopText {
			get {
				return _StartStopText;
			}
			set {
				_StartStopText = value;
				OnPropertyChanged(nameof(StartStopText));
			}
		}

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
		}

		#endregion

		#region methods

		public void StartStopClock() {
			// Start new Timer
			if( CurrentWorkMode == EnumWorkMode.Stop )
				SetCounter();
			// Stop the Timer
			else {
				AddTimeIfWork();

				ResetCounter();

				CurrentWorkMode = EnumWorkMode.Stop;
				NextWorkMode = EnumWorkMode.Work;
			}

			UpdateButtonText();
		}

		public void DelayWorkMode() {

			if( CurrentWorkMode == EnumWorkMode.Stop )
				goto Finish;

			AddTimeIfWork();
			NextWorkMode = EnumWorkMode.DelayBreak;

			if( CurrentWorkMode == EnumWorkMode.Break || CurrentWorkMode == EnumWorkMode.DelayBreak )
				NextWorkMode = EnumWorkMode.DelayWork;

Finish:
			SetCounter();
			UpdateButtonText();
		}

		#endregion

		#region private HelperMethods

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
			SetCounter();
			UpdateButtonText();
		}

		private void UpCounter_Tick( object? sender, EventArgs e ) {
			Time = Time.Add(TimeSpan.FromSeconds(1));
			if( Time == _DestinationTime )
				OnElapsed();
		}
		private void DownCounter_Tick( object? sender, EventArgs e ) {
			Time = Time.Subtract(TimeSpan.FromSeconds(1));
			if( Time == _DestinationTime )
				OnElapsed();
		}

		private void UpdateCountDirection() {
#warning I want to Implement an Option, to change the Countdirection
		}

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

		private void AddTimeIfWork() {
			if( CurrentWorkMode == EnumWorkMode.DelayBreak || CurrentWorkMode == EnumWorkMode.Work )
				AddTimeToTotal();
		}
		private void AddTimeToTotal() {
			TotalTime = TotalTime.Add(GetActualTime());
		}

		private TimeSpan GetActualTime() {
			if( CountDown == true )
				return _DestinationTime - Time;
			else
				return Time;
		}
		private TimeSpan GetCountStart() => CurrentWorkMode switch
		{
			EnumWorkMode.Work => CountDown ? InputWorkTime : TimeSpan.Zero,
			EnumWorkMode.Break => CountDown ? InputBreakTime : TimeSpan.Zero,
			EnumWorkMode.DelayWork => CountDown ? InputDelayTime : TimeSpan.Zero,
			EnumWorkMode.DelayBreak => CountDown ? InputDelayTime : TimeSpan.Zero,
			_ => CountDown ? InputWorkTime : TimeSpan.Zero,
		};
		private TimeSpan GetCountGoal() => CurrentWorkMode switch
		{
			EnumWorkMode.Work => CountDown ? TimeSpan.Zero : InputWorkTime,
			EnumWorkMode.Break => CountDown ? TimeSpan.Zero : InputBreakTime,
			EnumWorkMode.DelayWork => CountDown ? TimeSpan.Zero : InputDelayTime,
			EnumWorkMode.DelayBreak => CountDown ? TimeSpan.Zero : InputDelayTime,
			_ => CountDown ? TimeSpan.Zero : InputWorkTime,
		};
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
		/// Initiates a new Cicle, starts the Timer
		/// </summary>
		private void SetCounter() {
			ResetCounter();
			// Set the Next WorkMode to the new Cicle
			CurrentWorkMode = NextWorkMode;
			NextWorkMode = GetNextWorkMode();
			// Update the destinationTime of the new Circle
			_DestinationTime = GetCountGoal();
			// Update the startTime for the new Circle
			Time = GetCountStart();

			// Attaches the Correct CounterTick
			if( CountDown == true )
				_Counter.Tick += DownCounter_Tick;
			else
				_Counter.Tick += UpCounter_Tick;

			// Starts the new Counter
			_Counter.Start();
		}

		private void ResetCounter() {
			// removes all EventHandler for a bugfree experience
			_Counter.Tick -= UpCounter_Tick;
			_Counter.Tick -= DownCounter_Tick;

			// Sets DestinationTime and Time to zero
			_DestinationTime = TimeSpan.Zero;
			Time = TimeSpan.Zero;
		}

		#endregion

	}

}
