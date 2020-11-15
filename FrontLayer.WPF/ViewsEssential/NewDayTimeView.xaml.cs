using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.NewTime, true )]
	public partial class NewDayTimeView : UserControl {

		public NewDayTimeView() {
			InitializeComponent();
		}

	}
}
