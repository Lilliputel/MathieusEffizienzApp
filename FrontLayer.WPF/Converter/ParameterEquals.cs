using EffizienzControls.Extensions;
using LogicLayer.BaseViewModels;
using ModelLayer.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converter {

	[ValueConversion( typeof( ViewModelEnum ), typeof( bool ) )]
	public class ParameterEquals : MarkedupValueConverter<ParameterEquals> {
		public override object? Convert( object? value, Type targetType, object parameter, CultureInfo culture )
			=> value is ViewModelBase viewModel && viewModel.GetType()?.Name is string vmName
				&& parameter is string str && string.IsNullOrWhiteSpace( str ) is false
				? vmName.Remove( vmName.Length - "ViewModel".Length ) == str
				: false;


		public override object? ConvertBack( object? value, Type targetType, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
