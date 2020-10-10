using ModelLayer.Classes;
using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {

	public class WeekPlanView : Control {

		#region properties
		public WeekPlan WeekPlan {
			get { return (WeekPlan)GetValue(WeekPlanProperty); }
			set { SetValue(WeekPlanProperty, value); }
		}
		public static readonly DependencyProperty WeekPlanProperty =
			DependencyProperty.Register(nameof(WeekPlan), typeof(WeekPlan), typeof(WeekPlanView), new PropertyMetadata( new WeekPlan() ));
		#endregion

		#region initializer
		static WeekPlanView() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(WeekPlanView), new FrameworkPropertyMetadata(typeof(WeekPlanView)));
		}
		#endregion
	}

}
