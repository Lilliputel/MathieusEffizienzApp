using LogicLayer.BaseViewModels;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace FrontLayer.WPF.Components {
	public partial class EssentialViewButton : ToggleButton {
		public EssentialViewButton()
				=> InitializeComponent();

		public Type ViewModel {
			get => (Type)GetValue( ViewModelProperty );
			set => SetValue( ViewModelProperty, value );
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register( nameof( ViewModel ), typeof( Type ), typeof( EssentialViewButton ), new PropertyMetadata( typeof( ViewModelBase ) ) );

	}
}
