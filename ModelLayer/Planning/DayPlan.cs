using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ModelLayer.Planning {
	public class DayPlan : ObservableCollection<PlanItem> {

		#region fields

		#endregion

		#region properties

		#endregion

		#region constructors

		#endregion

		#region methods

		public async Task<DayTime?> GetDayOverlappingAsync( DayTime newDayTime ) =>
			await Task<DayTime?>.Run(() => {
				DayTime? result = null;
				foreach( PlanItem oldItem in this ) {

					var oldStart = oldItem.Time.Start;
					var oldEnd = oldItem.Time.End;

					bool case1 = newDayTime.Start < oldStart && newDayTime.End > oldStart;
					bool case2 = newDayTime.Start > oldStart && newDayTime.End < oldEnd;
					bool case3 = newDayTime.Start < oldEnd && newDayTime.End > oldEnd;

					if( case1 == true ) {
						result = new DayTime(oldStart, newDayTime.End);
					}
					else if( case2 == true ) {
						result = newDayTime;
					}
					else if( case3 == true ) {
						result = new DayTime(newDayTime.Start, oldEnd);
					}

				}
				return result;
			});


		#endregion


	}
}
