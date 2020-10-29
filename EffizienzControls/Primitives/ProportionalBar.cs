using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {
	public class ProportionalBar : ContentControl {

		#region public properties
		public double TotalSize {
			get { return (double)GetValue(TotalSizeProperty); }
			set { SetValue(TotalSizeProperty, value); }
		}
		public static readonly DependencyProperty TotalSizeProperty =
			DependencyProperty.Register(nameof(TotalSize), typeof(double), typeof(ProportionalBar), new PropertyMetadata(double.NaN));

		#region Layout
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
		#endregion

#warning i have to implement this!
		#region Value Setting
		public double MaxValue {
			get { return (double)GetValue(MaxValueProperty); }
			set { SetValue(MaxValueProperty, value); }
		}
		public static readonly DependencyProperty MaxValueProperty =
			DependencyProperty.Register(nameof(MaxValue), typeof(double), typeof(ProportionalBar), new PropertyMetadata(double.NaN));
		public double MinValue {
			get { return (double)GetValue(MinValueProperty); }
			set { SetValue(MinValueProperty, value); }
		}
		public static readonly DependencyProperty MinValueProperty =
			DependencyProperty.Register(nameof(MinValue), typeof(double), typeof(ProportionalBar), new PropertyMetadata(double.NaN));
		public double SizeValue {
			get { return (double)GetValue(SizeValueProperty); }
			set { SetValue(SizeValueProperty, value); }
		}
		public static readonly DependencyProperty SizeValueProperty =
			DependencyProperty.Register(nameof(SizeValue), typeof(double), typeof(ProportionalBar), new PropertyMetadata(double.NaN));
		public double OffsetValue {
			get { return (double)GetValue(OffsetValueProperty); }
			set { SetValue(OffsetValueProperty, value); }
		}
		public static readonly DependencyProperty OffsetValueProperty =
			DependencyProperty.Register(nameof(OffsetValue), typeof(double), typeof(ProportionalBar), new PropertyMetadata(double.NaN));
		#endregion

		#region Bar Definition
		public double SizeFactor {
			get { return (double)GetValue(SizeFactorProperty); }
			set { SetValue(SizeFactorProperty, value); }
		}
		public static readonly DependencyProperty SizeFactorProperty =
			DependencyProperty.Register(nameof(SizeFactor), typeof(double), typeof(ProportionalBar), new PropertyMetadata(double.NaN));
		public double OffsetFactor {
			get { return (double)GetValue(OffsetFactorProperty); }
			set { SetValue(OffsetFactorProperty, value); }
		}
		public static readonly DependencyProperty OffsetFactorProperty =
			DependencyProperty.Register(nameof(OffsetFactor), typeof(double), typeof(ProportionalBar), new PropertyMetadata(double.NaN));
		public double BarWidth {
			get { return (double)GetValue(BarWidthProperty); }
			set { SetValue(BarWidthProperty, value); }
		}
		public static readonly DependencyProperty BarWidthProperty =
			DependencyProperty.Register(nameof(BarWidth), typeof(double), typeof(ProportionalBar), new PropertyMetadata(double.NaN));
		public double BarMinWidth {
			get { return (double)GetValue(BarMinWidthProperty); }
			set { SetValue(BarMinWidthProperty, value); }
		}
		public static readonly DependencyProperty BarMinWidthProperty =
			DependencyProperty.Register(nameof(BarMinWidth), typeof(double), typeof(ProportionalBar), new PropertyMetadata(1.0));
		#endregion

		#region Bar Styling
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

		#endregion

		#region initializer
		static ProportionalBar() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ProportionalBar), new FrameworkPropertyMetadata(typeof(ProportionalBar)));
		}
		#endregion

	}
}
