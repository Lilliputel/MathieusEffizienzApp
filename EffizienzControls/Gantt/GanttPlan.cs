using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {

	public class GanttPlan : Control {

		#region properties

		public Category Category {
			get { return (Category)GetValue(CategoryProperty); }
			set {
				SetValue(CategoryProperty, value);
				this.SetStartAndEnd();
				this.SetPlanDuration();
			}
		}
		public static readonly DependencyProperty CategoryProperty =
			DependencyProperty.Register(nameof(Category), typeof(Category), typeof(GanttPlan), new PropertyMetadata(new Category()));
		public DateTime MainStart {
			get { return (DateTime)GetValue(MainStartProperty); }
			private set { SetValue(MainStartProperty, value); }
		}
		public static readonly DependencyProperty MainStartProperty =
			DependencyProperty.Register(nameof(MainStart), typeof(DateTime), typeof(GanttPlan), new PropertyMetadata(DateTime.Today.AddDays(1) ));
		public DateTime MainEnd {
			get { return (DateTime)GetValue(MainEndProperty); }
			private set { SetValue(MainEndProperty, value); }
		}
		public static readonly DependencyProperty MainEndProperty =
			DependencyProperty.Register(nameof(MainEnd), typeof(DateTime), typeof(GanttPlan), new PropertyMetadata(DateTime.Today.AddDays(10)));
		public ObservableCollection<DateTime> PlanDuration {
			get { return (ObservableCollection<DateTime>)GetValue(PlanDurationProperty); }
			private set { SetValue(PlanDurationProperty, value); }
		}
		public static readonly DependencyProperty PlanDurationProperty =
			DependencyProperty.Register(nameof(PlanDuration), typeof(ObservableCollection<DateTime>), typeof(GanttPlan), new PropertyMetadata( new ObservableCollection<DateTime>() ));
		public double NameWidth {
			get { return (double)GetValue(NameWidthProperty); }
			set {
				SetValue(NameWidthProperty, value);
				this.SetStartAndEnd();
				this.SetPlanDuration();
			}
		}
		public static readonly DependencyProperty NameWidthProperty =
			DependencyProperty.Register(nameof(NameWidth), typeof(double), typeof(GanttPlan), new PropertyMetadata(200.0));
		public double NameMargin {
			get { return (double)GetValue(NameMarginProperty); }
			set {
				SetValue(NameMarginProperty, value);
				this.SetStartAndEnd();
				this.SetPlanDuration();
			}
		}
		public static readonly DependencyProperty NameMarginProperty =
			DependencyProperty.Register(nameof(NameMargin), typeof(double), typeof(GanttPlan), new PropertyMetadata(220.0));

		#endregion

		#region initializer
		static GanttPlan() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(GanttPlan), new FrameworkPropertyMetadata(typeof(GanttPlan)));
		}
		#endregion

		#region private Helperfunctions
		private void SetStartAndEnd() {
			var NewDates = GetStartAndEnd(this.Category.Children);
			if( NewDates.start is DateTime start && start <= this.MainStart )
				this.MainStart = start;
			if( NewDates.end is DateTime end && end <= this.MainEnd )
				this.MainEnd = end;
		}
		private (DateTime? start, DateTime? end) GetStartAndEnd( Collection<Goal> goals ) {
			DateTime? start = null;
			DateTime? end = null;
			foreach( var goal in goals ) {
				start = goal.Plan.Start;
				end = goal.Plan.End;
				if( goal.Children.IsParent is true ) {
					var subPlan = GetStartAndEnd(goal.Children);
					if( subPlan.start <= start )
						start = subPlan.start;
					if( subPlan.end >= end )
						end = subPlan.end;
				}
			}
			return (start, end);
		}
		private void SetPlanDuration() {
			var placeholder = new ObservableCollection<DateTime>();
			for( DateTime day = this.MainStart; day <= this.MainEnd; day.AddDays(1.0) ) {
				placeholder.Add(day.Date);
			}
			this.PlanDuration = placeholder;
		}
		#endregion

	}

}