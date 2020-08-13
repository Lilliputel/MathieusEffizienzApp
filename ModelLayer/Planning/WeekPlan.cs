using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelLayer.Planning {
	public class WeekPlan {

		#region fields

		#endregion

		#region properties
		[XmlElement("Monday")]
		public DayPlan Monday { get; private set; } = new DayPlan();
		[XmlElement("Tuesday")]
		public DayPlan Tuesday { get; private set; } = new DayPlan();
		[XmlElement("Wednesday")]
		public DayPlan Wednesday { get; private set; } = new DayPlan();
		[XmlElement("Thursday")]
		public DayPlan Thursday { get; private set; } = new DayPlan();
		[XmlElement("Friday")]
		public DayPlan Friday { get; private set; } = new DayPlan();
		[XmlElement("Saturday")]
		public DayPlan Saturday { get; private set; } = new DayPlan();
		[XmlElement("Sunday")]
		public DayPlan Sunday { get; private set; } = new DayPlan();


		#endregion

		#region constructor

		public WeekPlan() {
		}

		#endregion

		#region methods

		public async Task AddItemToDayAsync( DayOfWeek day, PlanItem item ) {
			DayPlan dayPlan = GetDayPlan(day);
			DayTime? result = null;
			await Task.Run(() => result = dayPlan.GetDayOverlappingAsync(item.Time).Result);
			if( result is { } )
				return;
			else
				dayPlan.Add(item);
		}
		public async Task<DayTime?> GetWeekOverlappingAsync( DayOfWeek day, DayTime time ) {
			DayTime? result = null;
			await Task.Run(() => result = GetDayPlan(day).GetDayOverlappingAsync(time).Result);
			return result;
		}

		public DayPlan GetDayPlan( DayOfWeek day ) => day switch
		{
			DayOfWeek.Monday => this.Monday,
			DayOfWeek.Tuesday => this.Tuesday,
			DayOfWeek.Wednesday => this.Wednesday,
			DayOfWeek.Thursday => this.Thursday,
			DayOfWeek.Friday => this.Friday,
			DayOfWeek.Saturday => this.Saturday,
			DayOfWeek.Sunday => this.Sunday
		};


		#endregion
	}
}
