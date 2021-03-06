﻿using EffizienzControls.Extensions;
using System;
using System.Globalization;

namespace EffizienzControls.Converters {

	public class CompareViewModel : MarkedupMultiValueConverter<CompareViewModel> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
			if( (values[0]?.GetType()?.Name is string vmName) )
				if( values[1] is string str && string.IsNullOrWhiteSpace( str ) is false )
					return vmName == str;
				else
					throw new ArgumentException( "The second value must be a ViewModelEnumValue!", nameof( values ) );
			else if( values[0] is null || values[1] is null )
				return false;
			else
				throw new ArgumentException( "The first value must be a ViewModel!", nameof( values ) );
		}
	}
}
