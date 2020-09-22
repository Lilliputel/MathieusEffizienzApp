using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Extensions;
using System;
using System.Collections.ObjectModel;

namespace LogicLayer.Views {
	public class StatisticsViewModel : ViewModelBase {

		#region fields

		#endregion

		#region properties

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
