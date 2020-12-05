using System.Collections.Generic;

namespace DataLayer {
	public interface IDataService {

		public T LoadAll<T>() where T : ICollection<object>;
		public bool SaveAll<T>( T Collection ) where T : ICollection<object>;
		public T Load<T>( int id ); // TODO i could use another Generic to define the Key
		public bool Save<T>( T item ); // TODO implement an interface with a Key Property or attribute
		public bool Update<T>( T item );
		public bool Delete<T>( T item );
	}
}
