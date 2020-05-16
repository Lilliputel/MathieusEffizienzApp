using EffizienzNeu.Classes;
using EffizienzNeu.Interfaces;
using EffizienzNeu.Utility;
using EffizienzNeu.ViewModels;
using System;
using System.Collections.Generic;
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

		private void Button_Click( object sender, RoutedEventArgs e ) {
			var test = (App)Application.Current;
			test.SetTheme(true);
		}
	}
}
