using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class StatistikView : UserControl {

		public StatistikView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Statistik);
		}

		~StatistikView() { }
	}
}
