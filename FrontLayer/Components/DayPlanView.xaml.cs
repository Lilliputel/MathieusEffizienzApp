using ModelLayer.Planning;
using System.Windows;
using System.Windows.Controls;

namespace FrontLayer.Components {

	public partial class DayPlanView : UserControl {

		public DayPlanView() {
			InitializeComponent();
		}

		public DayPlan DayPlan {
			get { return (DayPlan)GetValue(DayPlanProperty); }
			set { SetValue(DayPlanProperty, value); }
		}

		public static readonly DependencyProperty DayPlanProperty =
			DependencyProperty.Register("DayPlan", typeof(DayPlan), typeof(DayPlanView));

		public DataTemplate HeaderTemplate {
			get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
			set { SetValue(HeaderTemplateProperty, value); }
		}

		public static readonly DependencyProperty HeaderTemplateProperty =
			DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(DayPlanView));

		public object Header {
			get { return (object)GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}

		public static readonly DependencyProperty HeaderProperty =
			DependencyProperty.Register("Header", typeof(object), typeof(DayPlanView));

	}
}
