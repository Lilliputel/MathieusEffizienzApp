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

		public App() {
			themeDark = new ResourceDictionary() { Source = new Uri("/EffizienzNeu;component/Themes/ThemeDark.xaml", UriKind.RelativeOrAbsolute ) };
			themeBright = new ResourceDictionary() { Source = new Uri("/EffizienzNeu;component/Themes/ThemeBright.xaml", UriKind.RelativeOrAbsolute) };
		}

		public void SetTheme( bool _DarkMode ) {
			ThemeDictionary.MergedDictionaries.Clear();
			switch( _DarkMode ) {
			case true:
				ThemeDictionary.MergedDictionaries.Add( themeDark );
				break;
			case false:
				ThemeDictionary.MergedDictionaries.Add( themeBright);
				break;
			}
		}

	}
}
