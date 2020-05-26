using Effizienz.Interfaces;
using Effizienz.Utility;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewDashboard : UserControl, IParsable {

		public ViewDashboard() {
			InitializeComponent();

			this.ListView_Kategorien.ItemsSource = ListContainer.KategorienListe;
			this.ListView_Projekte.ItemsSource = ListContainer.ProjektListe;
			this.ListView_Aufgaben.ItemsSource = ListContainer.AufgabenListe;

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
