using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {
	public class ProportionalBar : ContentControl {

		#region public properties
		public double TotalSize {
			get => (double) GetValue( TotalSizeProperty );
			set => SetValue( TotalSizeProperty, value );
		}
		public static readonly DependencyProperty TotalSizeProperty =
			DependencyProperty.Register( nameof( TotalSize ), typeof( double ), typeof( ProportionalBar ), new PropertyMetadata( double.NaN ) );

		#region Layout
		public Dock Dock {
			get => (Dock) GetValue( DockProperty );
			set => SetValue( DockProperty, value );
		}
		public static readonly DependencyProperty DockProperty =
			DependencyProperty.Register( nameof( Dock ), typeof( Dock ), typeof( ProportionalBar ), new PropertyMetadata( Dock.Left ) );
		public Orientation ContentOrientation {
			get => (Orientation) GetValue( ContentOrientationProperty );
			set => SetValue( ContentOrientationProperty, value );
		}
		public static readonly DependencyProperty ContentOrientationProperty =
			DependencyProperty.Register( nameof( ContentOrientation ), typeof( Orientation ), typeof( ProportionalBar ), new PropertyMetadata( Orientation.Horizontal ) );
		#endregion

		#region Value Settings
		public double MaxValue {
			get => (double) GetValue( MaxValueProperty );
			set => SetValue( MaxValueProperty, value );
		}
		public static readonly DependencyProperty MaxValueProperty =
			DependencyProperty.Register( nameof( MaxValue ), typeof( double ), typeof( ProportionalBar ), new PropertyMetadata( double.NaN ) );
		public double MinValue {
			get => (double) GetValue( MinValueProperty );
			set => SetValue( MinValueProperty, value );
		}
		public static readonly DependencyProperty MinValueProperty =
			DependencyProperty.Register( nameof( MinValue ), typeof( double ), typeof( ProportionalBar ), new PropertyMetadata( double.NaN ) );
		public double SizeValue {
			get => (double) GetValue( SizeValueProperty );
			set => SetValue( SizeValueProperty, value );
		}
		public static readonly DependencyProperty SizeValueProperty =
			DependencyProperty.Register( nameof( SizeValue ), typeof( double ), typeof( ProportionalBar ), new PropertyMetadata( double.NaN ) );
		public double OffsetValue {
			get => (double) GetValue( OffsetValueProperty );
			set => SetValue( OffsetValueProperty, value );
		}
		public static readonly DependencyProperty OffsetValueProperty =
			DependencyProperty.Register( nameof( OffsetValue ), typeof( double ), typeof( ProportionalBar ), new PropertyMetadata( double.NaN ) );
		#endregion

		#region Factor Settings
		public double SizeFactor {
			get => (double) GetValue( SizeFactorProperty );
			set => SetValue( SizeFactorProperty, value );
		}
		public static readonly DependencyProperty SizeFactorProperty =
			DependencyProperty.Register( nameof( SizeFactor ), typeof( double ), typeof( ProportionalBar ), new PropertyMetadata( double.NaN ) );
		public double OffsetFactor {
			get => (double) GetValue( OffsetFactorProperty );
			set => SetValue( OffsetFactorProperty, value );
		}
		public static readonly DependencyProperty OffsetFactorProperty =
			DependencyProperty.Register( nameof( OffsetFactor ), typeof( double ), typeof( ProportionalBar ), new PropertyMetadata( double.NaN ) );
		public double BarWidth {
			get => (double) GetValue( BarWidthProperty );
			set => SetValue( BarWidthProperty, value );
		}
		public static readonly DependencyProperty BarWidthProperty =
			DependencyProperty.Register( nameof( BarWidth ), typeof( double ), typeof( ProportionalBar ), new PropertyMetadata( double.NaN ) );
		public double BarMinWidth {
			get => (double) GetValue( BarMinWidthProperty );
			set => SetValue( BarMinWidthProperty, value );
		}
		public static readonly DependencyProperty BarMinWidthProperty =
			DependencyProperty.Register( nameof( BarMinWidth ), typeof( double ), typeof( ProportionalBar ), new PropertyMetadata( 1.0 ) );
		#endregion

		#region Bar Styling
		public Style BarStyle {
			get => (Style) GetValue( BarStyleProperty );
			set => SetValue( BarStyleProperty, value );
		}
		public static readonly DependencyProperty BarStyleProperty =
			DependencyProperty.Register( nameof( BarStyle ), typeof( Style ), typeof( ProportionalBar ), new PropertyMetadata( new Style( typeof( Border ) ) ) );
		#endregion

		#endregion

		#region initializer
		static ProportionalBar() {
			DefaultStyleKeyProperty.OverrideMetadata( typeof( ProportionalBar ), new FrameworkPropertyMetadata( typeof( ProportionalBar ) ) );
		}
		#endregion

	}
}
