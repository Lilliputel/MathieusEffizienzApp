using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class NewDayTimeView : UserControl {

		public NewDayTimeView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.NewDayTime;
		}

	}
}
