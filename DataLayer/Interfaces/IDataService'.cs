using System.Collections.Generic;

namespace DataLayer {
	public interface IDataService<T, U> where T : ICollection<U> {

		#region methods
		T LoadData();
		void SaveData( T Collection );
		#endregion

	}
}
