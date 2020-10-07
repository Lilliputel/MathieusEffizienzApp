using ModelLayer.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FrontLayer.WPF.Components {
	public partial class GanttElement : UserControl {

		#region properties

		public Goal Goal {
			get { return (Goal)GetValue(GoalProperty); }
			set { SetValue(GoalProperty, value); }
		}
		public static readonly DependencyProperty GoalProperty =
			DependencyProperty.Register("Goal", typeof(Goal), typeof(GanttElement), new FrameworkPropertyMetadata( new Goal(), FrameworkPropertyMetadataOptions.AffectsRender ));

		public DateTime MainStart {
			get { return (DateTime)GetValue(MainStartProperty); }
			set { SetValue(MainStartProperty, value); }
		}
		public static readonly DependencyProperty MainStartProperty =
			DependencyProperty.Register("MainStart", typeof(DateTime), typeof(GanttElement));

		public DateTime MainEnd {
			get { return (DateTime)GetValue(MainEndProperty); }
			set { SetValue(MainEndProperty, value); }
		}
		public static readonly DependencyProperty MainEndProperty =
			DependencyProperty.Register("MainEnd", typeof(DateTime), typeof(GanttElement));

		public double TimeLineWidth {
			get { return (double)GetValue(TimeLineWidthProperty); }
			set { SetValue(TimeLineWidthProperty, value); }
		}
		public static readonly DependencyProperty TimeLineWidthProperty =
			DependencyProperty.Register("TimeLineWidth", typeof(double), typeof(GanttElement), new PropertyMetadata(0.0));

		public double TextWidth {
			get { return (double)GetValue(TextWidthProperty); }
			set { SetValue(TextWidthProperty, value); }
		}
		public static readonly DependencyProperty TextWidthProperty =
			DependencyProperty.Register("TextWidth", typeof(double), typeof(GanttElement), new PropertyMetadata(0.0));

		#endregion

		#region constructor
		public GanttElement() {
			InitializeComponent();
		}
		#endregion
	}
}
