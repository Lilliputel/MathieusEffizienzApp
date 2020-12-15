using DataLayer;
using LogicLayer.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace LogicLayer.Views {
	public class StatisticsViewModel : ViewModelBase {

		#region private fields
		private readonly IRepository _DataService;
		#endregion

		#region public properties
		public TimeSpan MaximalWorkedTime
			=> TimeSpan.FromMilliseconds( (double)Dates.Max( d => _DataService.LoadAll().Max(
				   cat => (decimal)cat.GetTotalTimeOnDate( d ).TotalMilliseconds ) ) );
		public ObservableCollection<DateTime> Dates
			=> new ObservableCollection<DateTime>( _DataService.LoadAll().SelectMany( cat => cat.GetTotalWorkedDates() ).Distinct() );
		public ICollectionView CategoryList { get; }
		#endregion

		#region constructors
		public StatisticsViewModel( IRepository dataService ) {
			_DataService = dataService;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
		}
		#endregion

	}
}
