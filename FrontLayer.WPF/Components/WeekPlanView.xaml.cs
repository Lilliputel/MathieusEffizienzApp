using ModelLayer.Planning;
using System.Windows;
using System.Windows.Controls;

namespace FrontLayer.WPF.Components {
	/// <summary>
	/// Interaction logic for WeekPlanView.xaml
	/// </summary>
	public partial class WeekPlanView : UserControl {

		public WeekPlanView() {
			InitializeComponent();
		}

		public WeekPlan WeekPlan {
			get { return (WeekPlan)GetValue(WeekPlanProperty); }
			set { SetValue(WeekPlanProperty, value); }
		}

		public static readonly DependencyProperty WeekPlanProperty =
			DependencyProperty.Register("WeekPlan", typeof(WeekPlan), typeof(WeekPlanView), new PropertyMetadata( new WeekPlan() ));

	}
}
