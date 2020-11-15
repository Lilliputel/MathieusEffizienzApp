using ModelLayer.Attributes;
using ModelLayer.Enums;
using System.Windows.Controls;

namespace FrontLayer.WPF.Views {

	[View( ViewModelEnum.Plan, true )]
	public partial class PlanView : UserControl {

		public PlanView() {
			InitializeComponent();
		}

		~PlanView() { }
	}
}
