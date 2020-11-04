using ModelLayer.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {

	public class DayPlanView : Control {

		#region properties
		public DayPlan DayPlan {
			get => (DayPlan) GetValue( DayPlanProperty );
			set => SetValue( DayPlanProperty, value );
		}
		public static readonly DependencyProperty DayPlanProperty =
			DependencyProperty.Register( nameof( DayPlan ), typeof( DayPlan ), typeof( DayPlanView ), new PropertyMetadata( new DayPlan() ) );
		public DayOfWeek Day {
			get => (DayOfWeek) GetValue( DayProperty );
			set => SetValue( DayProperty, value );
		}
		public static readonly DependencyProperty DayProperty =
			DependencyProperty.Register( nameof( Day ), typeof( DayOfWeek ), typeof( DayPlanView ), new PropertyMetadata( DayOfWeek.Monday ) );
		#endregion

		#region initializer
		static DayPlanView() {
			DefaultStyleKeyProperty.OverrideMetadata( typeof( DayPlanView ), new FrameworkPropertyMetadata( typeof( DayPlanView ) ) );
		}
		#endregion

	}

}
