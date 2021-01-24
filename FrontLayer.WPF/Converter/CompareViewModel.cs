using EffizienzControls.Extensions;
using LogicLayer.BaseViewModels;
using System;
using System.Globalization;

namespace FrontLayer.WPF.Converter {

	public class CompareViewModel : MarkedupMultiValueConverter<CompareViewModel> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
			=> values[0] is ViewModelBase viewModel && viewModel.GetType()?.Name is string vmName
				&& values[1] is string str && string.IsNullOrWhiteSpace( str ) is false
				&& vmName.Remove( vmName.Length - "ViewModel".Length ) == str;

	}
}
