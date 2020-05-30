using Effizienz.Classes;
using Effizienz.Utility;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Effizienz.Views {

	public partial class ViewKategorie : UserControl {

		public ViewKategorie() {
			InitializeComponent();

			var InputListe = from property in typeof(Colors).GetProperties()
							 orderby property.GetValue(null, null).ToString()
							 select property;
			var ItemWeiss = (from item in InputListe
							 where item.Name == "White"
							 select item).First();

			ComboBox_Farbe.ItemsSource = InputListe;
			ComboBox_Farbe.SelectedItem = ItemWeiss;

			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Kategorie);
		}

		~ViewKategorie() { }

		private void Button_SpeichernSchliessen_Click( object sender, RoutedEventArgs e ) {
			if( Parse() )
				Wipe();
		}

		public bool Parse() {
			PropertyInfo item = (PropertyInfo)ComboBox_Farbe.SelectedItem;

			try {
				(Application.Current as App).KategorienListe.Add(
					new Kategorie(
						TextBox_Titel.Text,
						(Color)item.GetValue(null, null)));
				return true;
			}
			catch( Exception e ) {
				MessageBoxDisplayer.InputInkorrekt(e.Message);
				return false;
			}
		}
		public void Wipe() {
			this.ComboBox_Farbe.SelectedItem = ( from property in typeof(Colors).GetProperties() where property.Name == "White" select property ).First();
			// this.ComboBox_Farbe.SelectedIndex = -1;
			this.TextBox_Titel.Clear();
		}
	}
}
