using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace EffizienzControls.Extensions {
#pragma warning disable IDE1006 // Naming Styles
	public abstract class MarkedUpTemplateSelector : MarkupExtension {

		#region private selector class
		private sealed class _PrivateSelector : DataTemplateSelector {
			private readonly Func<object, DependencyObject, DataTemplate?> _Func;
			public _PrivateSelector( Func<object, DependencyObject, DataTemplate?> selectTemplate )
				=> _Func = selectTemplate;
			public override DataTemplate? SelectTemplate( object item, DependencyObject container )
				=> _Func( item, container );
		}
		#endregion

		#region markupextension-Methods
		public override object ProvideValue( IServiceProvider serviceProvider )
			=> new _PrivateSelector( SelectTemplate );
		#endregion

		#region SelectorMethod
		public abstract DataTemplate? SelectTemplate( object item, DependencyObject container );
		#endregion
#pragma warning restore IDE1006 // Naming Styles
	}
}
