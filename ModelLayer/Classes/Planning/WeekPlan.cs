using ModelLayer.Utility;
using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelLayer.Planning {
	public class WeekPlan : ObservableObject {

		#region fields

		private DayPlan _Monday;
		private DayPlan _Tuesday;
		private DayPlan _Wednesday;
		private DayPlan _Thursday;
		private DayPlan _Friday;
		private DayPlan _Saturday;
		private DayPlan _Sunday;

		#endregion

		#region properties

		[XmlElement("Monday")]
		public DayPlan Monday {
			get {
				return _Monday;
			}
			set {
				if( value == _Monday )
					return;
				_Monday = value;
				OnPropertyChanged(nameof(Monday));
			}
		}
		[XmlElement("Tuesday")]
		public DayPlan Tuesday {
			get {
				return _Tuesday;
			}
			set {
				if( value == _Tuesday )
					return;
				_Tuesday = value;
				OnPropertyChanged(nameof(Tuesday));
			}
		}
		[XmlElement("Wednesday")]
		public DayPlan Wednesday {
			get {
				return _Wednesday;
			}
			set {
				if( value == _Wednesday )
					return;
				_Wednesday = value;
				OnPropertyChanged(nameof(Wednesday));
			}
		}
		[XmlElement("Thursday")]
		public DayPlan Thursday {
			get {
				return _Thursday;
			}
			set {
				if( value == _Thursday )
					return;
				_Thursday = value;
				OnPropertyChanged(nameof(Thursday));
			}
		}
		[XmlElement("Friday")]
		public DayPlan Friday {
			get {
				return _Friday;
			}
			set {
				if( value == _Friday )
					return;
				_Friday = value;
				OnPropertyChanged(nameof(Friday));
			}
		}
		[XmlElement("Saturday")]
		public DayPlan Saturday {
			get {
				return _Saturday;
			}
			set {
				if( value == _Saturday )
					return;
				_Saturday = value;
				OnPropertyChanged(nameof(Saturday));
			}
		}
		[XmlElement("Sunday")]
		public DayPlan Sunday {
			get {
				return _Sunday;
			}
			set {
				if( value == _Sunday )
					return;
				_Sunday = value;
				OnPropertyChanged(nameof(Sunday));
			}
		}

		#endregion

		#region constructor

		public WeekPlan() {
			this._Monday = new DayPlan();
			this._Tuesday = new DayPlan();
			this._Wednesday = new DayPlan();
			this._Thursday = new DayPlan();
			this._Friday = new DayPlan();
			this._Saturday = new DayPlan();
			this._Sunday = new DayPlan();
		}

		#endregion

		#region methods

		public async Task<DoubleTime?> AddItemToDayAsync( DayOfWeek day, PlanItem item ) {
			DayPlan dayPlan = GetDayPlan(day);
			DoubleTime? result = null;
			await Task.Run(() => result = dayPlan.GetDayOverlappingAsync(item.Time).Result);
			if( result is { } )
				return result;
			else {
				dayPlan.Add(item);
				OnPropertyChanged(day.ToString());
			}
			return null;
		}
		public void RemoveItemFromDay( DayOfWeek day, PlanItem item ) => GetDayPlan(day).Remove(item);

		public async Task<DoubleTime?> GetWeekOverlappingAsync( DayOfWeek day, DoubleTime time ) {
			DoubleTime? result = null;
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
			DayOfWeek.Sunday => this.Sunday,
			_ => new DayPlan()
		};

		#endregion
	}
}
