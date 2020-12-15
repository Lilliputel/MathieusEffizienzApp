using EffizienzControls.Extensions;
using ModelLayer.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converter {

	[ValueConversion( typeof( ViewModelEnum ), typeof( bool ) )]
	public class ParameterEquals : MarkedupValueConverter<ParameterEquals> {
		public override object? Convert( object? value, Type targetType, object parameter, CultureInfo culture )
			=> value is null ? false : (ViewModelEnum)value == Enum.Parse<ViewModelEnum>( (string)parameter );
		public override object? ConvertBack( object? value, Type targetType, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
