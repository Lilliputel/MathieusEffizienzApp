using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrontLayer.WPF.Components {
	public partial class VectorIcon : UserControl {

		public VectorIcon() {
			InitializeComponent();
		}
		public double PathHeight {
			get => (double) GetValue( PathHeightProperty );
			set => SetValue( PathHeightProperty, value );
		}
		public static readonly DependencyProperty PathHeightProperty =
			DependencyProperty.Register( nameof( PathHeight ), typeof( double ), typeof( VectorIcon ), new PropertyMetadata( 24.0 ) );
		public double PathWidth {
			get => (double) GetValue( PathWidthProperty );
			set => SetValue( PathWidthProperty, value );
		}
		public static readonly DependencyProperty PathWidthProperty =
			DependencyProperty.Register( nameof( PathWidth ), typeof( double ), typeof( VectorIcon ), new PropertyMetadata( 24.0 ) );
		public Geometry Geometry {
			get => (Geometry) GetValue( GeometryProperty );
			set => SetValue( GeometryProperty, value );
		}
		public static readonly DependencyProperty GeometryProperty =
			DependencyProperty.Register( nameof( Geometry ), typeof( Geometry ), typeof( VectorIcon ), new PropertyMetadata( null ) );
	}
}
