using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace EffizienzControls.Extensions {

	public abstract class MarkedupValueConverter<T> : MarkupExtension, IValueConverter where T : class, new() {

		#region private fields

		private static T _Converter = new T();

		#endregion

		#region markupextension-Methods

		public override object ProvideValue( IServiceProvider serviceProvider )
			=> _Converter;

		#endregion

		#region converter-Methods

		public abstract object Convert( object value, Type targetType, object parameter, CultureInfo culture );
		public abstract object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture );

		#endregion

	}

}
