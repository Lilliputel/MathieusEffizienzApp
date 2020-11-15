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
		public abstract object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture );
		#endregion

	}

}
