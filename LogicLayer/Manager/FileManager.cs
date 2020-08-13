using LogicLayer.Utility;
using ModelLayer.Utility;

namespace LogicLayer.Manager {
	public static class FileManager {

		#region fields

		private static string _FilePath = @"S:\TESTING\Effizienz\";
		private static XMLCollectionHandler _XMLCollectionHandler = new XMLCollectionHandler(_FilePath);
		private static XMLDictionaryHandler _XMLDictionaryHandler = new XMLDictionaryHandler(_FilePath);

		#endregion

		#region properties

		#endregion

		#region initializer

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

	}
}
