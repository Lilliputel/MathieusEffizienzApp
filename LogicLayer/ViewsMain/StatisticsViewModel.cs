using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Extensions;
using System;
using System.Collections.ObjectModel;

namespace LogicLayer.Views {
	public class StatisticsViewModel : ViewModelBase {

		#region private fields
		private TimeSpan maxTime;
		#endregion

		#region public properties
		public TimeSpan MaximalWorkedTime {
			get {
				if( maxTime == new TimeSpan() )
					foreach( var date in Dates )
						foreach( var category in Categories ) {
							var maxCatTime = category.GetTotalTimeOnDate(date);
							if( maxCatTime > maxTime )
								maxTime = maxCatTime;
						}


				return maxTime;
			}
		}
		public ObservableCollection<DateTime> Dates {
			get {
				var bridge = new ObservableCollection<DateTime>();
				foreach( var cat in Categories )
					foreach( var goal in cat.Children )
						bridge.AddUniqueRange(goal.WorkHours.GetWorkedDates());
				return bridge;
			}
		}
		public ObservableCollection<Category> Categories { get; private set; }
		#endregion

		#region constructors
		public StatisticsViewModel( ObservableCollection<Category> categories )
			=> Categories = categories;
		#endregion

		#region methods

		#endregion

	}
}
