using System.Windows.Controls;
using UiLayer.Utility;

namespace UiLayer.Views {

	public partial class StatistikView : UserControl {

		public StatistikView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Statistik);
		}

		~StatistikView() { }
	}
}
