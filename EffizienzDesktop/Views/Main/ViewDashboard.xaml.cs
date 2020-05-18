using EffizienzNeu.Classes;
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
			( (App)Application.Current ).Speichern(ListContainer.KategorienListe, nameof(ListContainer.KategorienListe));
		}
		private void Button_Projekte_Speichern_Click( object sender, RoutedEventArgs e ) {
			( (App)Application.Current ).Speichern(ListContainer.ProjektListe, nameof(ListContainer.ProjektListe));
		}
		private void Button_Aufgaben_Speichern_Click( object sender, RoutedEventArgs e ) {
			( (App)Application.Current ).Speichern(ListContainer.AufgabenListe, nameof(ListContainer.AufgabenListe));
		}

		public bool Parse() => throw new NotImplementedException();
		public void Wipe() => throw new NotImplementedException();

		

	}
}
