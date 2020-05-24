using Effizienz.Classes;
using Effizienz.Interfaces;
using Effizienz.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Effizienz.Views {

	public partial class ViewAufgabe : UserControl, IParsable {

		public ViewAufgabe() {
			InitializeComponent();

			ComboBox_Kategorie.ItemsSource = ListContainer.KategorienListe.Liste;
			ComboBox_Projekt.ItemsSource = ListContainer.ProjektListe.Liste;
		}

		~ViewAufgabe() { }

		private void Button_SpeichernSchliessen_Click( object sender, RoutedEventArgs e ) {
			if( Parse() )
				Wipe();
		}

		public bool Parse() {
			Kategorie selectedKategorie = (Kategorie)ComboBox_Kategorie.SelectedItem;
			Projekt selectedProjekt = (Projekt)ComboBox_Projekt.SelectedItem;
			try {
				ListContainer.AufgabenListe.AddMember(
					new Aufgabe(
						TextBox_Titel.Text,
						TextBox_Beschreibung.Text,
						selectedProjekt.ID,
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
			this.ComboBox_Kategorie.SelectedIndex = -1;
			this.ComboBox_Projekt.SelectedIndex = -1;
			this.DatePicker_EndDatum.Text = string.Empty;
		}

	}
}
