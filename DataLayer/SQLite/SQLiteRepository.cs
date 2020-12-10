using Microsoft.EntityFrameworkCore;
using ModelLayer.Classes;
using System.Collections.ObjectModel;

namespace DataLayer {
	public class SQLiteRepository : IRepository {

		#region private fields
		private readonly EffizienzDBContext _Context;
		#endregion

		#region constructor
		public SQLiteRepository() {
			_Context = new EffizienzDBContext();
			_Context.Database.EnsureCreated();
			_Context.Categories.Load();
			_Context.Goals.Load();
			_Context.UserTexts.Load();
			_Context.WorkItems.Load();
			_Context.DateSpans.Load();
			_Context.DoubleTimes.Load();
			_Context.SaveChanges();
		}
		#endregion

		#region IRepository
		public bool Insert<T>( T item ) where T : class
			=> _Context.Add( item ).State == EntityState.Added;
		public ObservableCollection<Category> LoadAll()
			=> _Context.Categories?.Local.ToObservableCollection() ?? new ObservableCollection<Category>();
		public T GetById<T>( int id ) where T : class
			=> _Context.Find<T>( id );
		public bool Update<T>( T item ) where T : class {
			if( _Context.Entry( item ).State == EntityState.Detached )
				_Context.Attach( item );
			_Context.Entry( item ).State = EntityState.Modified;
			return (_Context.Find<T>( item ) != null);
		}
		public bool Delete<T>( T item ) where T : class {
			if( _Context.Entry( item ).State == EntityState.Detached )
				_Context.Attach( item );
			return _Context.Remove( item ).State == EntityState.Deleted;
		}
		public bool Delete<T>( int id ) where T : class {
			T item = _Context.Find<T>( id );
			return _Context.Remove( item ).State == EntityState.Deleted;
		}
		public bool Save()
			=> _Context.SaveChanges() > 0;
		#endregion

	}
}
