using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class KategorieView : UserControl {

		public KategorieView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Kategorie);
		}

		~KategorieView() { }
	}
}
