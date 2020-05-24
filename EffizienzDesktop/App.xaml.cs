using EffizienzNeu.Classes;
using EffizienzNeu.Interfaces;
using EffizienzNeu.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EffizienzNeu {

	public partial class App : Application {

		public static CultureInfo zeitformat = new CultureInfo("ch-DE");

		private ResourceDictionary themeDark;
		private ResourceDictionary themeBright;
		private string themeDirectory = "/EffizienzNeu;component/Themes/";

		public App() {
			themeDark = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute ) };
			themeBright = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeBright.xaml", UriKind.RelativeOrAbsolute) };			

			//Laden(ListContainer.KategorienListe, nameof(ListContainer.KategorienListe));
			//Laden(ListContainer.ProjektListe, nameof(ListContainer.ProjektListe));
			//Laden(ListContainer.AufgabenListe, nameof(ListContainer.AufgabenListe));
		}

		public void SetTheme( bool _DarkMode ) {
			Resources.MergedDictionaries[0].MergedDictionaries.Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add(	_DarkMode ? themeDark : themeBright );
		}

		private void Laden<T>( ListeIIdentifizierbar<T> _inputListe, string _listenName ) where T : IIdentifizierbar {
			List<T> neueListe;
			XMLHandler.Laden(out neueListe, _listenName);
			foreach( T item in neueListe ) {
				_inputListe.AddMember(item);
			}
		}
		public void Speichern<T>( ListeIIdentifizierbar<T> _liste, string _listenName ) where T : IIdentifizierbar {
			XMLHandler.Speichern( new List<T>(_liste.Liste), _listenName);
			MessageBoxDisplayer.ListeGespeichert(_listenName);
		}

	}
}
