using UiLayer.Views;
using System.Windows;

namespace UiLayer.Windows {

	public partial class MainWindow : Window {

		public MainWindow() {
			InitializeComponent();
			DataContext = new ViewModelMain();
		}
	}
}
