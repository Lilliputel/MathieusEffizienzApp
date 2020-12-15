using System.Collections.Generic;

namespace DataLayer {
	public interface IRepository<T, U> where T : ICollection<U> {

		#region methods
		T LoadData();
		void SaveData( T Collection );
		#endregion

	}
}
