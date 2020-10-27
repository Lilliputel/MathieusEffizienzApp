using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {
	public class ProportionalBar : ContentControl {

		#region public properties
		public Orientation Orientation {
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(ProportionalBar), new PropertyMetadata(Orientation.Vertical));
		public double OffsetFactor {
			get { return (double)GetValue(OffsetFactorProperty); }
			set { SetValue(OffsetFactorProperty, value); }
		}
		public static readonly DependencyProperty OffsetFactorProperty =
			DependencyProperty.Register(nameof(OffsetFactor), typeof(double), typeof(ProportionalBar), new PropertyMetadata(0.0));
		public double SizeFactor {
			get { return (double)GetValue(SizeFactorProperty); }
			set { SetValue(SizeFactorProperty, value); }
		}
		public static readonly DependencyProperty SizeFactorProperty =
			DependencyProperty.Register(nameof(SizeFactor), typeof(double), typeof(ProportionalBar), new PropertyMetadata(0.0));
		public double TotalSize {
			get { return (double)GetValue(TotalSizeProperty); }
			set { SetValue(TotalSizeProperty, value); }
		}
		public static readonly DependencyProperty TotalSizeProperty =
			DependencyProperty.Register(nameof(TotalSize), typeof(double), typeof(ProportionalBar), new PropertyMetadata(0.0));
		public CornerRadius CornerRadius {
			get { return (CornerRadius)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
		public static readonly DependencyProperty CornerRadiusProperty =
			DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ProportionalBar), new PropertyMetadata(new CornerRadius(0)));
		#endregion

		#region initializer
		static ProportionalBar() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ProportionalBar), new FrameworkPropertyMetadata(typeof(ProportionalBar)));
		}
		#endregion

	}
}
