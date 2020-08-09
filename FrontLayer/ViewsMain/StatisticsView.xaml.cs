using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class StatisticsView : UserControl {

		public StatisticsView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.Statistics);
		}

		~StatisticsView() { }
	}
}
