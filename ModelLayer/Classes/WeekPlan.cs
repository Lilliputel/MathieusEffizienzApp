using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

		[XmlIgnore]
		public ObservableCollection<TimeSpan> HoursOfDay { get; }
		#endregion

		#region constructor
		public WeekPlan() {
			HoursOfDay = new ObservableCollection<TimeSpan>();
			for( int h = 0; h < 24; h++ ) {
				HoursOfDay.Add( TimeSpan.FromHours( h ) );
			}
		}
		#endregion

		#region methods
		public async Task AddItemToDayAsync( DoubleTime newItem ) {
			var day = newItem.Day;
			ObservableCollection<DoubleTime>? dayPlan = GetDayPlan( day );
			DoubleTime? result = null;

			await Task.Run( () => {
				foreach( DoubleTime oldItem in dayPlan ) {

					#region Conditionals
					double oldStart = oldItem.Start;
					double oldEnd = oldItem.End;
					double newStart = newItem.Start;
					double newEnd = newItem.End;
					bool newEndsDuringOld = newStart <= oldStart && newEnd > oldStart && newEnd <= oldEnd;
					bool newIsInsideOld = newStart > oldStart && newEnd < oldEnd;
					bool newBeginsDuringOld = newStart >= oldStart && newStart < oldEnd && newEnd >= oldEnd;
					bool newEqualsOld = newStart == oldStart && oldEnd == newEnd;
					bool newSurroundsOld = newStart <= oldStart && newEnd >= oldEnd;
					#endregion

					if( newEqualsOld || newSurroundsOld )
						result = oldItem;
					else if( newEndsDuringOld )
						result = new DoubleTime( day, oldStart, newEnd, oldItem.Category );
					else if( newIsInsideOld )
						result = new DoubleTime( day, newStart, newEnd, oldItem.Category );
					else if( newBeginsDuringOld )
						result = new DoubleTime( day, newStart, oldEnd, oldItem.Category );

					if( result is { } )
						throw new ArgumentException( result.ToString() );
				}
			} );

			dayPlan?.Add( newItem );
			RaisePropertyChanged( day.ToString() );
		}
		public void RemoveItemFromDay( DoubleTime item )
			=> GetDayPlan( item.Day )?.Remove( item );
		public ObservableCollection<DoubleTime>? GetDayPlan( DayOfWeek day )
			=> typeof( WeekPlan ).GetProperty( day.ToString() )?.GetValue( this ) as ObservableCollection<DoubleTime>; // using reflection to get the correct DayPlan
		#endregion

	}
}
