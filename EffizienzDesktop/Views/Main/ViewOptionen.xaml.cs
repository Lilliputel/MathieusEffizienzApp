using EffizienzNeu.Interfaces;
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

	public partial class ViewOptionen : UserControl, IParsable {
		
		public ViewOptionen() {
			InitializeComponent();
		}

		~ViewOptionen() { }

		private void ToggleTheme( object sender, RoutedEventArgs e ) {

			ToggleButton toggleButton = (ToggleButton)e.Source;
			bool isChecked = (bool)toggleButton.IsChecked;
			toggleButton.Content = isChecked ? "Bright!" : "Dark!";
			( (App)Application.Current ).SetTheme(isChecked);

		}
		public bool Parse() => throw new NotImplementedException();
		public void Wipe() => throw new NotImplementedException();

	}
}
