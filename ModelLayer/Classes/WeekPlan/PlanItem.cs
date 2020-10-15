using ModelLayer.Utility;

namespace ModelLayer.Classes {
	public class PlanItem : ObservableObject {

		#region public properties
		public DoubleTime Time { get; set; }
		public Category Category { get; set; }
		#endregion

		#region constructors
		public PlanItem( DoubleTime time, Category category ) {
			Time = time;
			Category = category;
		}
		#endregion

	}
}
