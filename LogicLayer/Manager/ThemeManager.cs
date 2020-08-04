using System;
using System.Windows;

namespace LogicLayer.Manager {
	public static class ThemeManager {
		#region fields

		private static string themeDirectory = "/FrontLayer;component/Themes/";
		private static ResourceDictionary themeDark;
		private static ResourceDictionary themeLight;
		private static bool DarkMode;

		#endregion

		#region constructor

		static ThemeManager() {

			// Initialize the Themes
			themeDark = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute) };
			themeLight = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeLight.xaml", UriKind.RelativeOrAbsolute) };
			DarkMode = false;
		}

		#endregion

		#region methods

		//public static void SwitchTheme() {
		//	DarkMode = !DarkMode;
		//	SetDarkMode(DarkMode);
		//}

		//private static void SetDarkMode( bool _DarkMode ) {

		//	Resources.MergedDictionaries[0].MergedDictionaries.Clear();
		//	Resources.MergedDictionaries[0].MergedDictionaries.Add(_DarkMode ? themeDark : themeLight);
		//}

		//public static bool GetDarkMode() {
		//	return Resources.MergedDictionaries[0].MergedDictionaries.Contains(this.themeDark);
		//}

		//public static void Speichern() {
		//	XMLHandler.Speichern(ObjectManager.KategorienListe, nameof(ObjectManager.KategorienListe));
		//	MessageBoxDisplayer.ListeGespeichert(nameof(ObjectManager.KategorienListe));
		//}

		#endregion
	}
}
