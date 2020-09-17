using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class StatisticsView : UserControl {

		public StatisticsView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.Statistics;
		}

		~StatisticsView() { }
	}
}
