using System.Windows.Controls;
using UiLayer.Utility;

namespace UiLayer.Views {

	public partial class KategorieView : UserControl {

		public KategorieView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Kategorie);
		}

		~KategorieView() { }
	}
}
