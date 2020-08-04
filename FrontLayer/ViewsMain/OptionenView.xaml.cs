using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class OptionenView : UserControl {

		public OptionenView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Optionen);
		}

		~OptionenView() { }

	}
}
