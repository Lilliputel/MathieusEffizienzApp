using System.Windows.Controls;
using FrontLayer.Utility;

namespace FrontLayer.Views {

	public partial class KategorieView : UserControl {

		public KategorieView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Kategorie);
		}

		~KategorieView() { }
	}
}
