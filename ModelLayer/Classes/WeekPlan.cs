using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;

namespace ModelLayer.Classes {
	public class WeekPlan : ObservableObject {

		#region public properties
		public ObservableCollection<DoubleTime> Monday { get; } = new ObservableCollection<DoubleTime>();
		public ObservableCollection<DoubleTime> Tuesday { get; } = new ObservableCollection<DoubleTime>();
		public ObservableCollection<DoubleTime> Wednesday { get; } = new ObservableCollection<DoubleTime>();
		public ObservableCollection<DoubleTime> Thursday { get; } = new ObservableCollection<DoubleTime>();
		public ObservableCollection<DoubleTime> Friday { get; } = new ObservableCollection<DoubleTime>();
		public ObservableCollection<DoubleTime> Saturday { get; } = new ObservableCollection<DoubleTime>();
		public ObservableCollection<DoubleTime> Sunday { get; } = new ObservableCollection<DoubleTime>();
		public ObservableCollection<TimeSpan> HoursOfDay { get; } = new ObservableCollection<TimeSpan>();
		#endregion

		#region constructor
		public WeekPlan() {
			for( int h = 0; h < 24; h++ )
				HoursOfDay.Add( TimeSpan.FromHours( h ) );
		}
		#endregion

		#region public methods
		public DoubleTime? GetCollapseTime( DoubleTime newItem ) {
			DayOfWeek day = newItem.Day;

			foreach( DoubleTime oldItem in this[day] )
				if( oldItem.GetCollapseTime( newItem ) is DoubleTime result )
					return result;
			return null;
		}
		public void AddItemToDay( DoubleTime newItem ) {
			DayOfWeek day = newItem.Day;

			if( GetCollapseTime( newItem ) is DoubleTime result )
				throw new ArgumentException( result.ToString() );

			this[day].Add( newItem );
			RaisePropertyChanged( day.ToString() );
		}
		public void RemoveItemFromDay( DoubleTime item )
			=> this[item.Day].Remove( item );
		#endregion

		#region public indexer
		public ObservableCollection<DoubleTime> this[DayOfWeek day]
			=> typeof( WeekPlan ).GetProperty( day.ToString() )?.GetValue( this ) is not ObservableCollection<DoubleTime> dayPlan
				? throw new ArgumentException( $"Day {day} not found in Weekplan!" )
				: dayPlan;
		#endregion

	}
}
