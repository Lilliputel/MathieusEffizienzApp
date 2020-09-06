using DataLayer.XMLDataService;
using LogicLayer.Utility;
using System;
using System.Diagnostics;
using System.IO;

namespace LogicLayer.Manager {
	public static class FileManager {

		#region fields

		private static string _FilePath = @"S:\TESTING\Effizienz\";
		private static XMLCollectionHandler _XMLCollectionHandler;
		private static XMLDictionaryHandler _XMLDictionaryHandler;

		#endregion

		#region properties

		#endregion

		#region initializer

		static FileManager() {
			_XMLCollectionHandler = new XMLCollectionHandler(_FilePath);
			_XMLCollectionHandler.ErrorOccured += ErrorOccured;
			_XMLDictionaryHandler = new XMLDictionaryHandler(_FilePath);
			_XMLDictionaryHandler.ErrorOccured += ErrorOccured;
		}

		#endregion

		#region methods

		public static void SaveCategories() {
			_XMLCollectionHandler.SaveCollection(ObjectManager.CategoryList, nameof(ObjectManager.CategoryList));
		}
		public static void LoadCategories() {
			_XMLCollectionHandler.LoadCollection(ObjectManager.CategoryList, nameof(ObjectManager.CategoryList));
		}

		public static void SaveSettings() {
			_XMLDictionaryHandler.SaveDictionary(ObjectManager.Settings, nameof(ObjectManager.Settings));
		}
		public static void LoadSettings() {
			_XMLDictionaryHandler.LoadDictionary(ObjectManager.Settings, nameof(ObjectManager.Settings));
		}

		#endregion

		#region private helpers

		private static void ErrorOccured( object sender, ErrorEventArgs e ) {
			switch( e.GetException() ) {
			case FileNotFoundException fNFE:
				MessageBoxDisplayer.FileNotFound(fNFE.FileName ?? $"unknown, from: {sender}", "");
				return;
			case ArgumentException aE:
				MessageBoxDisplayer.InputInkorrekt($"{aE.Message}");
				return;
			default:
				Debug.WriteLine($"{sender} threw {e.GetException()} \nwith the Message: {e.GetException().Message}");
				return;
			}
		}

		#endregion

	}
}
