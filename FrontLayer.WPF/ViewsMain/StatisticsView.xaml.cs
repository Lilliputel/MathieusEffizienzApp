using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.Statistics, true )]
	public partial class StatisticsView : UserControl {

		public StatisticsView() {
			InitializeComponent();
		}

		~StatisticsView() { }
	}
}
