using LogicLayer.BaseViewModels;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace FrontLayer.WPF.Components {

	public partial class MainViewButton : ToggleButton {
		public MainViewButton()
			=> InitializeComponent();
		public Style IconStyle {
			get => (Style)GetValue( IconStyleProperty );
			set => SetValue( IconStyleProperty, value );
		}
		public static readonly DependencyProperty IconStyleProperty =
			DependencyProperty.Register( nameof( IconStyle ), typeof( Style ), typeof( MainViewButton ), new PropertyMetadata( new Style( typeof( VectorIcon ) ) ) );

		public Type ViewModel {
			get => (Type)GetValue( ViewModelProperty );
			set => SetValue( ViewModelProperty, value );
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register( nameof( ViewModel ), typeof( Type ), typeof( MainViewButton ), new PropertyMetadata( typeof( ViewModelBase ) ) );

	}
}
