using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ModelLayer.Utility {
	public class DayPlan : ObservableCollection<DayTime> {

		#region fields

		#endregion

		#region properties

		#endregion

		#region constructors

		#endregion

		#region methods

		public async Task<bool> IsOverlappingAsync( DayTime newDayTime ) {
			bool result = true;

			await Task.Run(() => {
				Parallel.ForEach(this, ( oldDayTime ) => {
					bool case1 = newDayTime.Start < oldDayTime.Start && newDayTime.End > oldDayTime.Start;
					bool case2 = newDayTime.Start > oldDayTime.Start && newDayTime.End < oldDayTime.End;
					bool case3 = newDayTime.Start < oldDayTime.End && newDayTime.End > oldDayTime.End;
					if( case1 || case2 || case3 == true )
						result = false;
				});
			});

			return result;
		}

		public async Task<DayTime?> GetOverlappingAsync( DayTime newDayTime ) =>
			await Task<DayTime?>.Run(() => {
				DayTime? result = null;
				foreach( DayTime oldDayTime in this ) {
					bool case1 = newDayTime.Start < oldDayTime.Start && newDayTime.End > oldDayTime.Start;
					bool case2 = newDayTime.Start > oldDayTime.Start && newDayTime.End < oldDayTime.End;
					bool case3 = newDayTime.Start < oldDayTime.End && newDayTime.End > oldDayTime.End;

					if( case1 == true ) {
						result = new DayTime(oldDayTime.Start, newDayTime.End);
					}
					else if( case2 == true ) {
						result = newDayTime;
					}
					else if( case3 == true ) {
						result = new DayTime(newDayTime.Start, oldDayTime.End);
					}
				}
				return result;
			});


		#endregion


	}
}
