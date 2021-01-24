using EffizienzControls.Extensions;
using System;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( object[] ), typeof( bool ) )]
	public class ElementsToFalse : MarkedupMultiValueConverter<ElementsToFalse> {

		public override object Convert( object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture ) {
			foreach( object item in values ) {
				if( item is string s && s == string.Empty )
					return false;
				if( item is bool b && b is false )
					return false;
				if( item is null )
					return false;
			}
			return true;
		}

	}
}
