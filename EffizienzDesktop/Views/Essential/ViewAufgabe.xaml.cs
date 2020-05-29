using Effizienz.Classes;
using Effizienz.Interfaces;
using Effizienz.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewAufgabe : UserControl, IParsable {

		public ViewAufgabe() {
			InitializeComponent();

			ComboBox_Kategorie.ItemsSource = ( Application.Current as App ).KategorienListe;
			ComboBox_Projekt.ItemsSource = from Kategorie in ( Application.Current as App ).KategorienListe
										   select Kategorie.Projekte into ProjektListe
										   from Projekt in ProjektListe
										   select Projekt;

			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Aufgabe);
		}

		~ViewAufgabe() { }

		private void Button_SpeichernSchliessen_Click( object sender, RoutedEventArgs e ) {
			if( Parse() )
				Wipe();
		}

		public bool Parse() {
			
				if( ComboBox_Projekt.SelectedIndex > -1 ) {
					Guid selectedProjektID = (ComboBox_Projekt.SelectedItem as Projekt).ID;


				(( from Kat in ( Application.Current as App ).KategorienListe
				  from Proj in Kat.Projekte
				  where Proj.ID == selectedProjektID
				  select Proj )
				  .First()).AddAufgabe(
					new Aufgabe(
						TextBox_Titel.Text,
						TextBox_Beschreibung.Text,
						selectedProjektID,
						(DateTime)DatePicker_EndDatum.SelectedDate));

				return true;
				}
				else if( ComboBox_Kategorie.SelectedIndex > -1 ) {
				Guid selectedKategorieID = (ComboBox_Kategorie.SelectedItem as Kategorie).ID;

					( Application.Current as App ).KategorienListe.Where(
						k => k.ID == selectedKategorieID).First().AddAufgabe(
					new Aufgabe(
						TextBox_Titel.Text,
						TextBox_Beschreibung.Text,
						selectedKategorieID,
						(DateTime)DatePicker_EndDatum.SelectedDate));

				return true;
				}
			return false;
		}
		public void Wipe() {
			TextBox_Titel.Clear();
			TextBox_Beschreibung.Clear();
			this.ComboBox_Kategorie.SelectedIndex = -1;
			this.ComboBox_Projekt.SelectedIndex = -1;
			this.DatePicker_EndDatum.Text = string.Empty;
		}

	}
}
