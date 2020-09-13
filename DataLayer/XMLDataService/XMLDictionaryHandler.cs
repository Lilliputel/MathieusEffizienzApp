﻿using DataLayer.Interfaces;
using DataLayer.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DataLayer.XMLDataService {
	public class XMLDictionaryHandler<TKey, TValue> : IDataService<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>, IErrorHandler {

		#region fields

		private string _FilePath;
		private string _FileName;

		#endregion

		#region events

		/// <summary>
		/// Handles Exceptions: <see cref="FileNotFoundException"/>/>
		/// </summary>
		public event ErrorEventHandler? ErrorOccured;

		#endregion

		#region constructor

		public XMLDictionaryHandler( string fileName, string filePath ) {

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

		public void SaveData( Dictionary<TKey, TValue> Collection ) {
			List<CustomPair<TKey, TValue>> customPairs = new List<CustomPair<TKey, TValue>>();
			foreach( var pair in Collection )
				customPairs.Add(new CustomPair<TKey, TValue>(pair));

			try {
				using( FileStream fileStream = new FileStream(_FilePath + _FileName, FileMode.Create) ) {
					XmlSerializer Serializer = new XmlSerializer(customPairs.GetType());
					Serializer.Serialize(fileStream, customPairs);
				}
			}
			catch( FileNotFoundException e ) {
				OnErrorOccured(e);
			}
		}

		#endregion

		#region load

		public Dictionary<TKey, TValue> LoadData() {
			Dictionary<TKey, TValue> loadingDictionary = new Dictionary<TKey, TValue>();
			try {
				using( FileStream fileStream = new FileStream(_FilePath + _FileName, FileMode.Open) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(List<CustomPair<TKey, TValue>>));

					//Tries to Deserialize the list to a certain Type
					if( Serializer.Deserialize(fileStream) is List<CustomPair<TKey, TValue>> deserializedList ) {
						//Adds every Item in the list to the LoadedList
						deserializedList.ForEach(( pair ) => {
							loadingDictionary.Add(pair.Key, pair.Value);
						});
					}
				}
			}
			catch( FileNotFoundException e ) {
				OnErrorOccured(e);
			}

			return loadingDictionary;
		}

		#endregion

	}
}
