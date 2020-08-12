using ModelLayer.Utility;

namespace LogicLayer.Manager {
	public static class FileManager {

		#region fields

		private static string _FilePath = @"S:\TESTING\Effizienz\";
		private static XMLHandler _XMLHandler = new XMLHandler(_FilePath);

		#endregion

		#region properties

		#endregion

		#region initializer

		#endregion

		#region methods

		public static void SaveCategories() {
			_XMLHandler.SaveCollection(ObjectManager.CategoryList, nameof(ObjectManager.CategoryList));
		}

		public static void LoadCategories() {
			_XMLHandler.LoadCollection(ObjectManager.CategoryList, nameof(ObjectManager.CategoryList));
		}

		#endregion

	}
}
