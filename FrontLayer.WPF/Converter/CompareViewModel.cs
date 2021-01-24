using EffizienzControls.Extensions;
using LogicLayer.BaseViewModels;
using System;
using System.Globalization;

namespace FrontLayer.WPF.Converter {

	public class CompareViewModel : MarkedupMultiValueConverter<CompareViewModel> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
			if( values[0] is ViewModelBase viewModel && viewModel.GetType()?.Name is string vmName )
				if( values[1] is string str && string.IsNullOrWhiteSpace( str ) is false )
					return vmName.Remove( vmName.Length - "ViewModel".Length ) == str;
				else
					throw new ArgumentException( "The second value must be a ViewModelEnumValue!", nameof( values ) );
			else
				throw new ArgumentException( "The first value must be a ViewModel!", nameof( values ) );
		}
	}
}
