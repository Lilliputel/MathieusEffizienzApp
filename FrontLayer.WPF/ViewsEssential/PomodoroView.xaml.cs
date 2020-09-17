using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class PomodoroView : UserControl {

		#region constructor

#warning i have to feed the viewModel a IAccountable
		public PomodoroView() {
			InitializeComponent();
		}

		~PomodoroView() { }

		#endregion

	}
}
