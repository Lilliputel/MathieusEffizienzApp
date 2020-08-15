using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class NewDayTimeView : UserControl {

		public NewDayTimeView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.NewDayTime);
		}

	}
}
