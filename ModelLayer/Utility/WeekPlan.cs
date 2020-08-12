using System;
using System.Threading.Tasks;

namespace ModelLayer.Utility {
	public class WeekPlan {

		#region fields

		#endregion

		#region properties

		public DayPlan Monday { get; private set; } = new DayPlan();
		public DayPlan Tuesday { get; private set; } = new DayPlan();
		public DayPlan Wednesday { get; private set; } = new DayPlan();
		public DayPlan Thursday { get; private set; } = new DayPlan();
		public DayPlan Friday { get; private set; } = new DayPlan();
		public DayPlan Saturday { get; private set; } = new DayPlan();
		public DayPlan Sunday { get; private set; } = new DayPlan();


		#endregion

		#region constructor

		public WeekPlan() {
		}

		#endregion

		#region methods

		public async Task AddTimeAsync( DayOfWeek day, DayTime time ) {
			DayPlan? dayPlan = GetDayPlan(day);
			DayTime? result = null;
			await Task.Run(() => result = dayPlan?.GetOverlappingAsync(time).Result);
			if( result is { } )
				return;
			else
				dayPlan?.Add(time);
		}
		public async Task<DayTime?> GetOverlapping( DayOfWeek day, DayTime time ) {
			DayTime? result = null;
			await Task.Run(() => result = GetDayPlan(day)?.GetOverlappingAsync(time).Result);
			return result;
		}

		public DayPlan? GetDayPlan( DayOfWeek day ) => day switch
		{
			DayOfWeek.Monday => this.Monday,
			DayOfWeek.Tuesday => this.Tuesday,
			DayOfWeek.Wednesday => this.Wednesday,
			DayOfWeek.Thursday => this.Thursday,
			DayOfWeek.Friday => this.Friday,
			DayOfWeek.Saturday => this.Saturday,
			DayOfWeek.Sunday => this.Sunday,
			_ => null
		};


		#endregion
	}
}
