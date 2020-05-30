using Effizienz.Classes;
using Effizienz.Interfaces;
using Effizienz.Utility;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewProjekt : UserControl, IParsable {

		public ViewProjekt() {
			InitializeComponent();

			ComboBox_Kategorie.ItemsSource = (Application.Current as App).KategorienListe;
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Projekt);
		}

		~ViewProjekt() { }

		private void Button_SpeichernSchliessen_Click( object sender, RoutedEventArgs e ) {
			if( Parse() )
				Wipe();
		}

		public bool Parse() {
			try {
				Guid selectedKategorieID = (ComboBox_Kategorie.SelectedItem as Kategorie).ID;

				( Application.Current as App ).KategorienListe.Where(
					k => k.ID == selectedKategorieID).First().Projekte.Add(
					new Projekt(
						TextBox_Titel.Text,
						selectedKategorieID,
						(DateTime)DatePicker_EndDatum.SelectedDate) { 
						Beschreibung = TextBox_Beschreibung.Text
					});
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
