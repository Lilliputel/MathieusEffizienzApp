using System.Windows;
using System.Windows.Controls;

namespace FrontLayer.WPF.TemplateSelectors {
	public class ClearTemplateSelector : DataTemplateSelector {
		public DataTemplate? ClearTemplate => null;
		public DataTemplate? ItemTemplate { get; set; }
		public override DataTemplate? SelectTemplate( object? item, DependencyObject container )
			=> (item is null) ? ClearTemplate : ItemTemplate;
	}
}
