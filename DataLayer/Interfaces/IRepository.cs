using ModelLayer.Classes;
using System.Collections.ObjectModel;

namespace DataLayer {
	public interface IRepository {
		public bool Insert<T>( T item ) where T : class;
		public T GetById<T>( int id ) where T : class;
		public ObservableCollection<Category> LoadAll();
		public bool Update<T>( T item ) where T : class;
		public bool Delete<T>( T item ) where T : class;
		public bool Delete<T>( int id ) where T : class;
		public bool Save();
	}
}
