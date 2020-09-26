using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Extensions;
using System;
using System.Collections.ObjectModel;

namespace LogicLayer.Views {
	public class StatisticsViewModel : ViewModelBase {

		#region fields

		private TimeSpan maxTime;

		#endregion

		#region properties

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
						bridge.AddUniqueRange(goal.GetWorkedDates());
				return bridge;
			}
		}

		public ObservableCollection<Category> Categories
			=> ObjectManager.CategoryList;

		#endregion

		#region constructors

		#endregion

		#region methods

		#endregion

	}
}
