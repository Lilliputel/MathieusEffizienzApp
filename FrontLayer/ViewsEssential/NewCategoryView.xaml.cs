using LogicLayer.Utility;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class NewCategoryView : UserControl {

		public NewCategoryView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.NewCategory);
		}

		~NewCategoryView() { }
	}
}
