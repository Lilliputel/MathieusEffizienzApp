using FrontLayer.Views;
using System.Windows;

namespace FrontLayer.Windows {

	public partial class MainWindow : Window {

		public MainWindow() {
			InitializeComponent();
			DataContext = new ViewModelMain();
		}

		~MainWindow() {
			MessageBox.Show("Hello");
		}
	}
}
