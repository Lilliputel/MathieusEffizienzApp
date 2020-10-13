using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ModelLayer.Classes {
	public class DayPlan : ObservableCollection<PlanItem> {

		#region constructors
		public DayPlan() { }
		~DayPlan() { }
		#endregion

		#region methods
		public async Task<DoubleTime?> GetDayOverlappingAsync( DoubleTime newDayTime ) =>
			await Task.Run(() => {
				DoubleTime? result = null;
				foreach( PlanItem oldItem in this ) {

					var oldStart = oldItem.Time.Start;
					var oldEnd = oldItem.Time.End;

					bool case1 = newDayTime.Start < oldStart && newDayTime.End >= oldStart;
					bool case2 = newDayTime.Start >= oldStart && newDayTime.End <= oldEnd;
					bool case3 = newDayTime.Start < oldEnd && newDayTime.End >= oldEnd;

					if( case1 is true )
						result = new DoubleTime((oldStart, newDayTime.End));
					else if( case2 is true )
						result = newDayTime;
					else if( case3 is true )
						result = new DoubleTime((newDayTime.Start, oldEnd));

				}
				return result;
			});
		#endregion


	}
}
