using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelLayer.Classes {
	public class WeekPlan : ObservableObject {

		#region public properties
		public DayPlan Monday { get; } = new DayPlan();
		public DayPlan Tuesday { get; } = new DayPlan();
		public DayPlan Wednesday { get; } = new DayPlan();
		public DayPlan Thursday { get; } = new DayPlan();
		public DayPlan Friday { get; } = new DayPlan();
		public DayPlan Saturday { get; } = new DayPlan();
		public DayPlan Sunday { get; } = new DayPlan();

		[XmlIgnore]
		public ObservableCollection<TimeSpan> HoursOfDay { get; }
		#endregion

		#region constructor
		public WeekPlan() {
			HoursOfDay = new ObservableCollection<TimeSpan>();
			for( int h = 0; h < 24; h++ ) {
				HoursOfDay.Add(TimeSpan.FromHours(h));
			}
		}
		#endregion

		#region methods
		public async Task AddItemToDayAsync( DayOfWeek day, PlanItem item ) {
			DayPlan dayPlan = GetDayPlan(day);
			DoubleTime? result = null;
			await Task.Run(() => result = dayPlan.GetDayOverlappingAsync(item.Time).Result);
			if( result is { } )
				throw new ArgumentException(result.ToString());
			else {
				dayPlan.Add(item);
				RaisePropertyChanged(day.ToString());
			}
		}
		public void RemoveItemFromDay( DayOfWeek day, PlanItem item )
			=> GetDayPlan(day).Remove(item);
		public DayPlan GetDayPlan( DayOfWeek day ) => day switch
		{
			DayOfWeek.Monday => Monday,
			DayOfWeek.Tuesday => Tuesday,
			DayOfWeek.Wednesday => Wednesday,
			DayOfWeek.Thursday => Thursday,
			DayOfWeek.Friday => Friday,
			DayOfWeek.Saturday => Saturday,
			DayOfWeek.Sunday => Sunday,
			_ => new DayPlan()
		};
		#endregion

	}
}
