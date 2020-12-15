using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace ModelLayer.Extensions {

	public class EnumDescriptionTypeConverter : EnumConverter {

		public EnumDescriptionTypeConverter( Type type ) : base( type ) { }

		public override object ConvertTo( ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType ) {
			if( destinationType == typeof( string ) ) {
				if( value is { } ) {
					FieldInfo? fi = value.GetType().GetField( value.ToString() ?? "" );
					if( fi is { } ) {
						var attributes = fi.GetCustomAttributes( typeof( DescriptionAttribute ), false ) as DescriptionAttribute[];
						return ((attributes?.Length ?? 0) > 0 && (string.IsNullOrEmpty( attributes?[0]?.Description ?? "" ) is false))
							? attributes![0]!.Description
							: value?.ToString() ?? "";
					}
				}
				return string.Empty;
			}
			return base.ConvertTo( context, culture, value, destinationType );
		}

	}

}
