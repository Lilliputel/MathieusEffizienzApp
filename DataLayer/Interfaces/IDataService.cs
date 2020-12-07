using System.Collections.Generic;

namespace DataLayer {
	public interface IDataService {

		public T LoadAll<T>() where T : ICollection<object>;
		public bool SaveAll<T>( T Collection ) where T : ICollection<object>;
		public T Load<T, U>( U id );
		public bool Save<T>( T item );
		public bool Update<T>( T item );
		public bool Delete<T>( T item );
	}
}
