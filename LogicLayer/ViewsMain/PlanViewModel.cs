using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using System.ComponentModel;
using System.Diagnostics;

namespace LogicLayer.Views {
	public class PlanViewModel : ViewModelBase {

		#region private fields

		#endregion

		#region public properties
		public IAccountableParent<Category> CategoryList { get; }
		public WeekPlan WeekPlan { get; }
		#endregion

		#region constructor
		public PlanViewModel( IAccountableParent<Category> categoryList, WeekPlan weekPlan ) {
			CategoryList = categoryList;
			WeekPlan = weekPlan;
			WeekPlan.PropertyChanged += Test;
		}
		#endregion

		#region private helper methods
		private void Test( object sender, PropertyChangedEventArgs e ) {
			Debug.WriteLine($"Executed PropertyChanged {e.PropertyName}");
		}
		#endregion

	}
}
