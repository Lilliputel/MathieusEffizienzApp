using System;
using System.Windows;

namespace LogicLayer.Manager {
	public static class ThemeManager {

		#region fields

		private static string themeDirectory = "/FrontLayer;component/Themes/";
		private static ResourceDictionary _themeDark;
		private static ResourceDictionary _themeLight;
		private static bool _darkMode;

		#endregion

		#region properties

		public static ResourceDictionary SelectedTheme { get; set; }
		public static bool DarkMode {
			get {
				return _darkMode;
			}
			set {
				_darkMode = value;
				SetTheme();
			}
		}

		#endregion

		#region constructor

		static ThemeManager() {
			_themeDark = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute) };
			_themeLight = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeLight.xaml", UriKind.RelativeOrAbsolute) };

			SelectedTheme = new ResourceDictionary();
			DarkMode = true;

		}

		#endregion

		#region methods

		public static void SwitchTheme() {
			DarkMode = !DarkMode;
			SetTheme();
		}

		private static void SetTheme() {
			SelectedTheme.Clear();
			SelectedTheme.MergedDictionaries.Add(DarkMode ? _themeDark : _themeLight);
		}

		#endregion
	}
}
