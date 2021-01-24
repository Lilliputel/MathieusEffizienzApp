using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace EffizienzControls.Extensions {

	public abstract class MarkedupMultiValueConverter<T> : MarkupExtension, IMultiValueConverter where T : class, new() {

		#region private fields
		private static T _Converter = new T();
		#endregion

		#region markupextension-Methods
		public override object ProvideValue( IServiceProvider serviceProvider )
			=> _Converter;
		#endregion

		#region converter-Methods
		public abstract object Convert( object[] values, Type targetType, object parameter, CultureInfo culture );
		public virtual object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException( $"ConvertBack was not overridden in the Markedup MultiValueConverter of {typeof( T ).Name}" );
		#endregion

	}

}
