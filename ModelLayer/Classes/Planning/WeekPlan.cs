using ModelLayer.Utility;
using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelLayer.Planning {
	public class WeekPlan : ObservableObject {

		#region properties

		[XmlElement(nameof(Monday))]
		public DayPlan Monday { get; set; }
		[XmlElement(nameof(Tuesday))]
		public DayPlan Tuesday { get; set; }
		[XmlElement(nameof(Wednesday))]
		public DayPlan Wednesday { get; set; }
		[XmlElement(nameof(Thursday))]
		public DayPlan Thursday { get; set; }
		[XmlElement(nameof(Friday))]
		public DayPlan Friday { get; set; }
		[XmlElement(nameof(Saturday))]
		public DayPlan Saturday { get; set; }
		[XmlElement(nameof(Sunday))]
		public DayPlan Sunday { get; set; }

		#endregion

		#region constructor

		public WeekPlan() {
			this.Monday = new DayPlan();
			this.Tuesday = new DayPlan();
			this.Wednesday = new DayPlan();
			this.Thursday = new DayPlan();
			this.Friday = new DayPlan();
			this.Saturday = new DayPlan();
			this.Sunday = new DayPlan();
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
				OnPropertyChanged(day.ToString());
			}
		}
		public void RemoveItemFromDay( DayOfWeek day, PlanItem item )
			=> GetDayPlan(day).Remove(item);

		public DayPlan GetDayPlan( DayOfWeek day ) => day switch
		{
			DayOfWeek.Monday => this.Monday,
			DayOfWeek.Tuesday => this.Tuesday,
			DayOfWeek.Wednesday => this.Wednesday,
			DayOfWeek.Thursday => this.Thursday,
			DayOfWeek.Friday => this.Friday,
			DayOfWeek.Saturday => this.Saturday,
			DayOfWeek.Sunday => this.Sunday,
			_ => new DayPlan()
		};

		#endregion

	}
}
