using UiLayer.Utility;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class OptionenView : UserControl {

		public OptionenView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Optionen);
		}

		~OptionenView() { }

	}
}
