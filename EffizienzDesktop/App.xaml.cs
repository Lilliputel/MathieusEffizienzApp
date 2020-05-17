using EffizienzNeu.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EffizienzNeu {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		public static CultureInfo zeitformat = new CultureInfo("ch-DE");

		public ResourceDictionary ThemeDictionary {
			get {
				return Resources.MergedDictionaries[0];
			}
		}
		private ResourceDictionary themeDark;
		private ResourceDictionary themeBright;
		private string themeDirectory = "/EffizienzNeu;component/Themes/";

		public App() {
			themeDark = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute ) };
			themeBright = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeBright.xaml", UriKind.RelativeOrAbsolute) };
		}

		public void SetTheme( bool _DarkMode ) {
			ThemeDictionary.MergedDictionaries.Clear();
			ThemeDictionary.MergedDictionaries.Add(	_DarkMode ? themeDark : themeBright );
		}

	}
}
