using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class PomodoroView : UserControl {

		#region constructor

		public PomodoroView() {
			InitializeComponent();

#warning i have to feed it a IAccountable
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.Pomodoro);
		}

		~PomodoroView() { }

		#endregion

	}
}
