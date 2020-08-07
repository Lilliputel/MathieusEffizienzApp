using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class StatisticsView : UserControl {

		public StatisticsView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Statistics);
		}

		~StatisticsView() { }
	}
}
