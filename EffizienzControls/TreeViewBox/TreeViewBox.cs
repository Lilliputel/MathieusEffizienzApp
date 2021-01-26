using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {
	public class TreeViewBox : ComboBox {
		static TreeViewBox()
			=> DefaultStyleKeyProperty.OverrideMetadata( typeof( TreeViewBox ), new FrameworkPropertyMetadata( typeof( TreeViewBox ) ) );


	}
}
