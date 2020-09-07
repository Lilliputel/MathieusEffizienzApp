using LogicLayer.Manager;
using LogicLayer.Views;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class PomodoroView : UserControl {

		#region constructor

		public PomodoroView() {
			InitializeComponent();
			this.DataContext = ( ViewModelManager.GetViewModel(EnumViewModels.Pomodoro) as PomodoroViewModel );
		}

		~PomodoroView() { }

		#endregion

	}
}
