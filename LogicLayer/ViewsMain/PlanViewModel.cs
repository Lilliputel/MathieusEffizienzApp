using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LogicLayer.Views {
	public class PlanViewModel : ViewModelBase {

		#region private fields

		#endregion

		#region public properties
		public ObservableCollection<Category> Categories { get; private set; }
		public WeekPlan WeekPlan { get; private set; }
		#endregion

		#region constructor
		public PlanViewModel( ObservableCollection<Category> categories, WeekPlan weekPlan ) {
			Categories = categories;
			WeekPlan = weekPlan;
			WeekPlan.PropertyChanged += Test;
		}
		#endregion

		#region private helper methods
		private void Test( object sender, System.ComponentModel.PropertyChangedEventArgs e ) {
			Debug.WriteLine($"Executed PropertyChanged {e.PropertyName}");
		}
		#endregion

	}
}
