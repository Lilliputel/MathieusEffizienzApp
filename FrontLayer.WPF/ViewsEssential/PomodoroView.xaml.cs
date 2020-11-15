using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.Pomodoro, true )]
	public partial class PomodoroView : UserControl {

		#region constructor

		public PomodoroView() {
			InitializeComponent();
		}

		~PomodoroView() { }

		#endregion

	}
}
