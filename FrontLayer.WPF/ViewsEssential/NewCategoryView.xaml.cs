using LogicLayer.Manager;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	public partial class NewCategoryView : UserControl {

		public NewCategoryView() {
			InitializeComponent();
			this.DataContext = ViewModelManager.GetViewModel(EnumViewModels.NewCategory);
		}

		~NewCategoryView() { }
	}
}
