using EffizienzNeu.Classes;
using EffizienzNeu.Enums;
using EffizienzNeu.Interfaces;
using EffizienzNeu.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EffizienzNeu.Views {

	public partial class ViewDashboard : UserControl, IParsable {

		public ViewDashboard() {
			InitializeComponent();

			this.ListView_Kategorien.ItemsSource = ListContainer.KategorienListe.Liste;
			this.ListView_Projekte.ItemsSource = ListContainer.ProjektListe.Liste;
			this.ListView_Aufgaben.ItemsSource = ListContainer.AufgabenListe.Liste;

		}

		~ViewDashboard() { }
		
		private void Button_Kategorien_Speichern_Click( object sender, RoutedEventArgs e ) {
			Speichern(ListContainer.KategorienListe, nameof(ListContainer.KategorienListe));
		}
		private void Button_Projekte_Speichern_Click( object sender, RoutedEventArgs e ) {
			Speichern(ListContainer.ProjektListe, nameof(ListContainer.ProjektListe));
		}
		private void Button_Aufgaben_Speichern_Click( object sender, RoutedEventArgs e ) {
			Speichern(ListContainer.AufgabenListe, nameof(ListContainer.AufgabenListe));
		}

		private void Button_Kategorien_Laden_Click( object sender, RoutedEventArgs e ) {
			Laden( ListContainer.KategorienListe, nameof(ListContainer.KategorienListe));
		}
		private void Button_Projekte_Laden_Click( object sender, RoutedEventArgs e ) {
			Laden( ListContainer.ProjektListe, nameof(ListContainer.ProjektListe));
		}
		private void Button_Aufgaben_Laden_Click( object sender, RoutedEventArgs e ) {
			Laden( ListContainer.AufgabenListe, nameof(ListContainer.AufgabenListe));
		}

		public bool Parse() => throw new NotImplementedException();
		public void Wipe() => throw new NotImplementedException();

		private void Laden<T>( ListeIIdentifizierbar<T> _inputListe, string _listenName ) where T : IIdentifizierbar {
			ObservableCollection<T> neueListe = new ObservableCollection<T>();
			XMLHandler.Laden( out neueListe, _listenName);
			foreach( T item in neueListe ) {
				_inputListe.AddMember(item);
			}
			MessageBoxDisplayer.ListeGeladen(_listenName);
		}
		private void Speichern<T>( ListeIIdentifizierbar<T> _liste, string _listenName ) where T : IIdentifizierbar {
			XMLHandler.Speichern(_liste.Liste, _listenName);
			MessageBoxDisplayer.ListeGespeichert(_listenName);
		}

	}
}
