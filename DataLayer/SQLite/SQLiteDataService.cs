using System.Collections.Generic;

namespace DataLayer {
	public class SQLiteDataService : IDataService {

		public T LoadAll<T>() where T : ICollection<object>
			=> throw new System.NotImplementedException();
		public bool SaveAll<T>( T Collection ) where T : ICollection<object>
			=> throw new System.NotImplementedException();

		public T Load<T>( int id )
			=> throw new System.NotImplementedException();
		public bool Save<T>( T item )
			=> throw new System.NotImplementedException();
		public bool Delete<T>( T item )
			=> throw new System.NotImplementedException();
		public bool Update<T>( T item )
			=> throw new System.NotImplementedException();
	}
}
