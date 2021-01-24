using System;
using System.Windows.Markup;

namespace EffizienzControls.Extensions {
	public class EnumBindingSourceExtension : MarkupExtension {

		private Type? _EnumType;
		public Type? EnumType {
			get => _EnumType;
			set {
				if( value != _EnumType ) {
					if( value is { } ) {
						Type enumType = Nullable.GetUnderlyingType( value ) ?? value;
						if( enumType.IsEnum is false )
							throw new ArgumentException( "Type must be for an Enum." );
						_EnumType = value;
					}
				}
			}
		}

		public EnumBindingSourceExtension() { }

		public EnumBindingSourceExtension( Type enumType ) {
			EnumType = enumType;
		}

		public override object ProvideValue( IServiceProvider serviceProvider ) {
			if( _EnumType is null )
				throw new InvalidOperationException( "The EnumType must be specified." );

			Type actualEnumType = Nullable.GetUnderlyingType( _EnumType ) ?? _EnumType;
			Array enumValues = Enum.GetValues( actualEnumType );

			if( actualEnumType == _EnumType )
				return enumValues;

			var tempArray = Array.CreateInstance( actualEnumType, enumValues.Length + 1 );
			enumValues.CopyTo( tempArray, 1 );
			return tempArray;
		}

	}
}
