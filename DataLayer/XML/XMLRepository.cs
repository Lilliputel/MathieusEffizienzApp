using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DataLayer {

	public class XMLRepository : IRepository, IErrorHandler {

		#region private fields
		private readonly string _FilePath;
		private readonly string _FileName;
		private readonly XmlSerializer _Serializer = new XmlSerializer( typeof( ObservableCollection<Category> ) );
		private readonly FileStream _FileStream;
		private ObservableCollection<Category> _Categories = new ObservableCollection<Category>();
		#endregion

		#region public Events

		/// <summary>
		/// Handles Exceptions: <see cref="FileNotFoundException"/>
		/// </summary>
		public event ErrorEventHandler? ErrorOccured;

		#endregion

		#region constructor
		public XMLRepository( string fileName, string filePath ) {
			_FileName = fileName;
			_FilePath = filePath;
			if( _FileName.EndsWith( ".xml" ) is false )
				_FileName = string.Concat( fileName, ".xml" );
			if( _FilePath.EndsWith( "/" ) is false )
				_FilePath = string.Concat( filePath, '/' );

			_FileStream = new FileStream( _FilePath + _FileName, FileMode.Create );
			using( _FileStream )
				_Categories = _Serializer.Deserialize( _FileStream ) as ObservableCollection<Category> ?? new ObservableCollection<Category>();
		}
		#endregion

		#region protected methods
		protected void OnErrorOccured( Exception e )
			=> ErrorOccured?.Invoke( this, new ErrorEventArgs( e ) );
		#endregion

		#region public methods
		public ObservableCollection<Category> LoadAll()
			=> _Categories;
		public bool Insert<T>( T item ) where T : class {
			_Categories.Add( item as Category );
			return _Categories.Contains( item as Category );
		}
		public T GetById<T>( int id ) where T : class
			=> (T)(_Categories.FirstOrDefault( c => c.Id == id ) as object);
		public bool Update<T>( T item ) where T : class
			=> true;
		public bool Delete<T>( T item ) where T : class
			=> _Categories.Remove( item as Category );
		public bool Delete<T>( int id ) where T : class
			=> _Categories.Remove( GetById<T>( id ) as Category );
		public bool Save() {
			ObservableCollection<Category>? testCase;
			using( _FileStream ) {
				_Serializer.Serialize( _FileStream, _Categories );
				testCase = _Serializer.Deserialize( _FileStream ) as ObservableCollection<Category>;
			}
			return _Categories == testCase;
		}

		#endregion

	}
}
