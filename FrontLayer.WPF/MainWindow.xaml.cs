using LogicLayer.ViewModels;
using System.Windows;

namespace FrontLayer.WPF.Windows {

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
