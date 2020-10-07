using ModelLayer.Classes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FrontLayer.WPF.Components {
	public partial class GanttPlan : UserControl {

		#region properties
		public ObservableCollection<Goal> Goals {
			get { return (ObservableCollection<Goal>)GetValue(GoalsProperty); }
			set { SetValue(GoalsProperty, value); }
		}
		public static readonly DependencyProperty GoalsProperty =
			DependencyProperty.Register("Goals", typeof(ObservableCollection<Goal>), typeof(GanttPlan), new PropertyMetadata(new ObservableCollection<Goal>()));


		#endregion
		#region constructor
		public GanttPlan() {
			InitializeComponent();
		}
		#endregion
	}
}
