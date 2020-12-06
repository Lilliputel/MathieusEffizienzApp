using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace LogicLayer.Views {
	public class PlanViewModel : ViewModelBase {

		#region private fields

		#endregion

		#region public properties
		public ICollection<Category> CategoryList { get; }
		public WeekPlan WeekPlan { get; }
		#endregion

		#region constructor
		public PlanViewModel( ICollection<Category> categoryList, WeekPlan weekPlan ) {
			CategoryList = categoryList;
			WeekPlan = weekPlan;
			WeekPlan.PropertyChanged += Test;
		}
		#endregion

		#region private helper methods
		private void Test( object sender, PropertyChangedEventArgs e ) => Debug.WriteLine( $"Executed PropertyChanged {e.PropertyName}" );
		#endregion

	}
}
