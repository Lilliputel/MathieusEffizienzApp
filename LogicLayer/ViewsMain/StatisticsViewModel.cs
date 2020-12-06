using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LogicLayer.Views {
	public class StatisticsViewModel : ViewModelBase {

		#region private fields
		private TimeSpan maxTime;
		#endregion

		#region public properties
		public TimeSpan MaximalWorkedTime {
			get {
				foreach( DateTime date in Dates )
					foreach( Category? category in CategoryList ) {
						TimeSpan maxCatTime = (category as IAccountableParent<Goal>).GetTotalTimeOnDate( date );
						if( maxCatTime > maxTime )
							maxTime = maxCatTime;
					}
				return maxTime;
			}
		}
		public ObservableCollection<DateTime> Dates
			=> new ObservableCollection<DateTime>( CategoryList.SelectMany( cat => cat.GetTotalWorkedDates() ) );
		public ICollection<Category> CategoryList { get; }
		#endregion

		#region constructors
		public StatisticsViewModel( ICollection<Category> categoryList ) {
			CategoryList = categoryList;
		}
		#endregion

		#region methods

		#endregion

	}
}
