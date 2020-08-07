using LogicLayer.Manager;
using ModelLayer.Utility;
using System.Windows;

namespace FrontLayer {
	public partial class App : Application {

		#region constructor

		public App() {

			// Load the saved Categories from The XML-File
			XMLHandler.Laden(ObjectManager.CategoryList, nameof(ObjectManager.CategoryList));

		}

		#endregion

	}
}
