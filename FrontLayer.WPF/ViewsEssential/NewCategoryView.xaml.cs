using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.NewCategory, true )]
	public partial class NewCategoryView : UserControl {

		public NewCategoryView() {
			InitializeComponent();
		}

		~NewCategoryView() { }
	}
}
