using Effizienz.Views;
using System.Windows;

namespace Effizienz.Windows {

	public partial class MainWindow : Window {

		public MainWindow() {
			InitializeComponent();
			DataContext = new ViewModelMain();
		}
	}
}
