using System.Windows;
using System.Windows.Controls;

namespace Effizienz.DataTemplateSelectors {
	public class DTSelector_String : DataTemplateSelector {

		public DataTemplate TextBlock { get; set; }

		public override DataTemplate SelectTemplate( object item, DependencyObject container ) {
			
			if( item is string )
				return TextBlock;

			return null;

		}
	}
}
