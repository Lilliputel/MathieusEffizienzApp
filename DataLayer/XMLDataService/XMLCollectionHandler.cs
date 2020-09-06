using DataLayer.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace DataLayer.XMLDataService {

	public class XMLCollectionHandler<T> : IDataService<ObservableCollection<T>, T>, IErrorHandler {

		#region fields

		private string _FilePath;
		private string _FileName;

		#endregion

		#region public Events

		/// <summary>
		/// Handles Exceptions: <see cref="FileNotFoundException"/>
		/// </summary>
		public event ErrorEventHandler ErrorOccured;

		#endregion

		#region constructor

		public XMLCollectionHandler( string fileName, string filePath ) {

			this._FileName = fileName;
			this._FilePath = filePath;

			if( _FileName.EndsWith(".xml") is false )
				_FileName = string.Concat(fileName, ".xml");
			if( _FilePath.EndsWith("/") is false )
				_FilePath = string.Concat(filePath, '/');
		}

		#endregion

		#region methods

		protected void OnErrorOccured( Exception e ) {
			ErrorOccured?.Invoke(this, new ErrorEventArgs(e));
		}

		#endregion

		#region save

		public void SaveData( ObservableCollection<T> Collection ) {
			try {
				using( FileStream fileStream = new FileStream(_FilePath + _FileName, FileMode.Create) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<T>));
					Serializer.Serialize(fileStream, Collection);
				}
			}
			catch( FileNotFoundException e ) {
				OnErrorOccured(e);
			}
		}

		#endregion

		#region load

		public ObservableCollection<T> LoadData() {

			try {
				using( FileStream fileStream = new FileStream(_FilePath + _FileName, FileMode.Open) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<T>));
					return Serializer.Deserialize(fileStream) as ObservableCollection<T>;
				}
			}
			catch( FileNotFoundException e ) {
				OnErrorOccured(e);
			}

			return new ObservableCollection<T>();
		}

		#endregion

	}
}
