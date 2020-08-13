using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LogicLayer.Utility {
	public class XMLDictionaryHandler {

		#region fields

		private string _FilePath;

		#endregion

		#region constructor

		public XMLDictionaryHandler( string filePath ) {
			this._FilePath = filePath;
		}

		#endregion

		#region methods

		public void SetFilePath( string filePath ) {
			_FilePath = filePath;
		}

		#endregion

		#region save

		public void SaveDictionary<T>( Dictionary<string, T> savingDic, string fileName )
		=> SaveDictionary(savingDic, fileName, _FilePath);
		public void SaveDictionary<T>( Dictionary<string, T> savingDic, string fileName, string filePath ) {
			if( fileName.EndsWith(".xml") == false )
				fileName = string.Concat(fileName, ".xml");
			try {
				using( FileStream fileStream = new FileStream(filePath + fileName, FileMode.Create) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(Dictionary<string, T>));
					Serializer.Serialize(fileStream, savingDic);
				}
			}
			catch( FileNotFoundException ) {
				MessageBoxDisplayer.FileNotFound(fileName, filePath);
			}

		}

		#endregion

		#region load

		public void LoadDictionary<T>( Dictionary<string, T> loadingDic, string fileName ) =>
			LoadDictionary<T>(loadingDic, fileName, _FilePath);
		public void LoadDictionary<T>( Dictionary<string, T> loadingDic, string fileName, string filePath ) {
			if( loadingDic is null )
				loadingDic = new Dictionary<string, T>();
			if( fileName.EndsWith(".xml") == false )
				fileName = string.Concat(fileName, ".xml");
			try {
				using( FileStream fileStream = new FileStream(filePath + fileName, FileMode.Open) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(Dictionary<string, T>));
					var loadedDic = Serializer.Deserialize(fileStream) as Dictionary<string, T>;
					foreach( var pair in loadedDic ??= new Dictionary<string, T>() ) {
						loadedDic.TryAdd(pair.Key, pair.Value);
					}
				}
			}
			catch( FileNotFoundException ) {
				MessageBoxDisplayer.FileNotFound(fileName, filePath);
			}
		}

		#endregion

	}
}
