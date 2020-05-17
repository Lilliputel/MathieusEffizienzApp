using EffizienzNeu.Classes;
using EffizienzNeu.Enums;
using EffizienzNeu.Interfaces;
using EffizienzNeu.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace EffizienzNeu.Views {

	public partial class ViewProjekt : UserControl, IParsable {

		public ViewProjekt() {
			InitializeComponent();
			
			ComboBox_Kategorie.ItemsSource = ListContainer.KategorienListe.Liste;
		}

		~ViewProjekt() { }

		private void Button_SpeichernSchliessen_Click( object sender, RoutedEventArgs e ) {
			if( Parse() )
				Wipe();
		}

		public bool Parse() {
			Kategorie selectedKategorie = (Kategorie)ComboBox_Kategorie.SelectedItem;
			try {
				ListContainer.ProjektListe.AddMember(
					new Projekt(
						TextBox_Titel.Text,
						TextBox_Beschreibung.Text,
						selectedKategorie.ID,
						(DateTime)DatePicker_EndDatum.SelectedDate));
				return true;
			}
			catch( Exception e ) {
				MessageBoxDisplayer.InputInkorrekt( e.Message );
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
