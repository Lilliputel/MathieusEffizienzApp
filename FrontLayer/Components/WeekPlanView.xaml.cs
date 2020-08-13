using ModelLayer.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrontLayer.Components {
	/// <summary>
	/// Interaction logic for WeekPlanView.xaml
	/// </summary>
	public partial class WeekPlanView : UserControl {

		public WeekPlanView() {
			InitializeComponent();
		}

		public SolidColorBrush Color {
			get { return (SolidColorBrush)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		public static readonly DependencyProperty ColorProperty =
			DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(WeekPlanView));

		public WeekPlan WeekPlan {
			get { return (WeekPlan)GetValue(WeekPlanProperty); }
			set { SetValue(WeekPlanProperty, value); }
		}

		public static readonly DependencyProperty WeekPlanProperty =
			DependencyProperty.Register("WeekPlan", typeof(WeekPlan), typeof(WeekPlanView), new PropertyMetadata( new WeekPlan() ));

	}
}
