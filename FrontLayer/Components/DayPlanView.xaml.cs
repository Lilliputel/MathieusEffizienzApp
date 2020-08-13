using ModelLayer.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrontLayer.Components {

	public partial class DayPlanView : UserControl {

		public DayPlanView() {
			InitializeComponent();
		}

		public SolidColorBrush Color {
			get { return (SolidColorBrush)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		public static readonly DependencyProperty ColorProperty =
			DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(DayPlanView), new PropertyMetadata(new SolidColorBrush(Colors.AliceBlue) ));


		public object Header {
			get { return (object)GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}

		public static readonly DependencyProperty HeaderProperty =
			DependencyProperty.Register("Header", typeof(object), typeof(DayPlanView), new PropertyMetadata(null));


		public DataTemplate HeaderTemplate {
			get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
			set { SetValue(HeaderTemplateProperty, value); }
		}

		public static readonly DependencyProperty HeaderTemplateProperty =
			DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(DayPlanView), new PropertyMetadata( null ));


		public DayPlan DayPlan {
			get { return (DayPlan)GetValue(DayPlanProperty); }
			set { SetValue(DayPlanProperty, value); }
		}

		public static readonly DependencyProperty DayPlanProperty =
			DependencyProperty.Register("DayPlan", typeof(DayPlan), typeof(DayPlanView), new PropertyMetadata( null ));








	}
}
