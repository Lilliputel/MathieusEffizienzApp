using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {
	public class BarChart : Control {

		#region public properties
		public Orientation Orientation {
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(BarChart), new PropertyMetadata(Orientation.Vertical));
		public double TotalSize {
			get { return (double)GetValue(TotalSizeProperty); }
			set { SetValue(TotalSizeProperty, value); }
		}
		public static readonly DependencyProperty TotalSizeProperty =
			DependencyProperty.Register(nameof(TotalSize), typeof(double), typeof(BarChart), new PropertyMetadata(0.0));
		#endregion

		#region initializer
		static BarChart() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(BarChart), new FrameworkPropertyMetadata(typeof(BarChart)));
		}
		#endregion

	}
}
