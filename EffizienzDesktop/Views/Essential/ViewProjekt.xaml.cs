using Effizienz.Classes;
using Effizienz.Interfaces;
using Effizienz.Utility;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewProjekt : UserControl, IParsable {

		public ViewProjekt() {
			InitializeComponent();

			ComboBox_Kategorie.ItemsSource = ListContainer.KategorienListe;
		}

		~ViewProjekt() { }

		private void Button_SpeichernSchliessen_Click( object sender, RoutedEventArgs e ) {
			if( Parse() )
				Wipe();
		}

		public bool Parse() {
			Kategorie selectedKategorie = (Kategorie)ComboBox_Kategorie.SelectedItem;
			try {
				ListContainer.ProjektListe.Add(
					new Projekt(
						TextBox_Titel.Text,
						TextBox_Beschreibung.Text,
						selectedKategorie.ID,
						(DateTime)DatePicker_EndDatum.SelectedDate));
				return true;
			}
			catch( Exception e ) {
				MessageBoxDisplayer.InputInkorrekt(e.Message);
				return false;
			}
		}
		public void Wipe() {
			TextBox_Titel.Clear();
			TextBox_Beschreibung.Clear();
			ComboBox_Kategorie.SelectedIndex = -1;
			DatePicker_EndDatum.Text = string.Empty;
		}

	}
}
