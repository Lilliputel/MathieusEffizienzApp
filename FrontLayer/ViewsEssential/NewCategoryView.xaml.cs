using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.Views {

	public partial class NewCategoryView : UserControl {

		public NewCategoryView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.NewCategory);
		}

		~NewCategoryView() { }
	}
}
