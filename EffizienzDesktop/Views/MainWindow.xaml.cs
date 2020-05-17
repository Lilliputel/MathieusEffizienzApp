using EffizienzNeu.Classes;
using EffizienzNeu.Interfaces;
using EffizienzNeu.Utility;
using EffizienzNeu.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EffizienzNeu.Windows {

	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		public MainWindow() {
			InitializeComponent();
			DataContext = new ViewModelMain();
		}

		private void ToggleTheme( object sender, RoutedEventArgs e ) {
			ToggleButton toggleButton = (ToggleButton)e.Source;
			bool isChecked = (bool)toggleButton.IsChecked;
			toggleButton.Content = isChecked ? "Bright!" : "Dark!";
			( (App)Application.Current ).SetTheme(isChecked);
		}
	}
}
