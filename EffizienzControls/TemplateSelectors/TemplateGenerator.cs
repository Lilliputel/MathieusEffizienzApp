using System;
using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls.TemplateSelectors {

	//TODO i have to understand this because it is just copy paste
	public static class TemplateGenerator {
		private sealed class _TemplateGeneratorControl : ContentControl {
			internal static readonly DependencyProperty FactoryProperty =
				DependencyProperty.Register( "Factory", typeof( Func<object> ), typeof( _TemplateGeneratorControl ), new PropertyMetadata( null, _FactoryChanged ) );
			private static void _FactoryChanged( DependencyObject instance, DependencyPropertyChangedEventArgs args )
				=> (instance as _TemplateGeneratorControl).Content = (args.NewValue as Func<object>)?.Invoke();
		}

		public static DataTemplate CreateDataTemplate( Func<object> factory ) {
			if( factory is null )
				throw new ArgumentNullException( "factory" );

			var frameworkElementFactory = new FrameworkElementFactory( typeof( _TemplateGeneratorControl ) );
			frameworkElementFactory.SetValue( _TemplateGeneratorControl.FactoryProperty, factory );

			return new DataTemplate( typeof( DependencyObject ) ) {
				VisualTree = frameworkElementFactory
			};
		}

		public static ControlTemplate CreateControlTemplate( Type controlType, Func<object> factory ) {
			if( controlType is null )
				throw new ArgumentNullException( "controlType" );
			if( factory is null )
				throw new ArgumentNullException( "factory" );

			var frameworkElementFactory = new FrameworkElementFactory( typeof( _TemplateGeneratorControl ) );
			frameworkElementFactory.SetValue( _TemplateGeneratorControl.FactoryProperty, factory );

			return new ControlTemplate( controlType ) {
				VisualTree = frameworkElementFactory
			};
		}
	}
}
