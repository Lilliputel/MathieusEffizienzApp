using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {
	public class ProportionalBar : ContentControl {

		#region public properties
		public Dock Dock {
			get { return (Dock)GetValue(DockProperty); }
			set { SetValue(DockProperty, value); }
		}
		public static readonly DependencyProperty DockProperty =
			DependencyProperty.Register(nameof(Dock), typeof(Dock), typeof(ProportionalBar), new PropertyMetadata(Dock.Left));
		public Orientation ContentOrientation {
			get { return (Orientation)GetValue(ContentOrientationProperty); }
			set { SetValue(ContentOrientationProperty, value); }
		}
		public static readonly DependencyProperty ContentOrientationProperty =
			DependencyProperty.Register(nameof(ContentOrientation), typeof(Orientation), typeof(ProportionalBar), new PropertyMetadata(Orientation.Horizontal));
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
		public Style BarStyle {
			get { return (Style)GetValue(BarStyleProperty); }
			set { SetValue(BarStyleProperty, value); }
		}
#warning somehow the style does not work
		public static readonly DependencyProperty BarStyleProperty =
			DependencyProperty.Register(nameof(BarStyle), typeof(Style), typeof(ProportionalBar), new PropertyMetadata(new Style( typeof(Border)) ));
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
