using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DataLayer.XMLDataService {

	public class XMLCollectionHandler {

		#region fields

		private string _FilePath;

		#endregion

		#region public Events

		/// <summary>
		/// throws exceptions <see cref="FileNotFoundException"/>
		/// </summary>
		public event ErrorEventHandler ErrorOccured;

		#endregion

		#region constructor

		public XMLCollectionHandler( string filePath ) {
			this._FilePath = filePath;
		}

		#endregion

		#region methods

		public void SetFilePath( string filePath ) {
			_FilePath = filePath;
		}

		protected void OnErrorOccured( Exception e ) {
			ErrorOccured?.Invoke(this, new ErrorEventArgs(e));
		}

		#endregion

		#region save

		public void SaveCollection<T>( ObservableCollection<T> savingList, string fileName )
			=> SaveCollection<T>(savingList, fileName, _FilePath);
		public void SaveCollection<T>( ObservableCollection<T> savingList, string fileName, string filePath ) {
			if( fileName.EndsWith(".xml") == false )
				fileName = string.Concat(fileName, ".xml");

			try {
				using( FileStream fileStream = new FileStream(filePath + fileName, FileMode.Create) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<T>));
					Serializer.Serialize(fileStream, savingList);
				}
			}
			catch( FileNotFoundException e ) {
				OnErrorOccured(e);
			}

		}

		#endregion

		#region load

		public void LoadCollection<T>( ObservableCollection<T> loadingList, string fileName ) =>
			LoadCollection<T>(loadingList, fileName, _FilePath);
		public void LoadCollection<T>( ObservableCollection<T> loadingList, string fileName, string filePath ) {
			if( loadingList is null )
				loadingList = new ObservableCollection<T>();
			if( fileName.EndsWith(".xml") == false )
				fileName = string.Concat(fileName, ".xml");
			try {
				using( FileStream fileStream = new FileStream(filePath + fileName, FileMode.Open) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<T>));
					loadingList = new ObservableCollection<T>(loadingList.Concat(Serializer.Deserialize(fileStream) as ObservableCollection<T>));
				}
			}
			catch( FileNotFoundException e ) {
				OnErrorOccured(e);
			}
		}

		#endregion

	}
}
