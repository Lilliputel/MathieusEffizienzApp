using LogicLayer.Manager;
using LogicLayer.Views;
using ModelLayer.Planning;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FrontLayer.WPF.Components {

	public partial class DayPlanView : HeaderedItemsControl {

		public DayPlanView() {
			InitializeComponent();
		}

		private new void MouseLeftButtonDown( object sender, MouseButtonEventArgs e ) {
			double y = e.GetPosition((IInputElement)sender).Y;
			DayOfWeek day = Enum.Parse<DayOfWeek>((sender as FrameworkElement)!.Tag.ToString()!);

#warning I should define the new ViewModel via the event not directly setting it here

			if( e.OriginalSource is Border border ) {
				Debug.WriteLine("The origin is the Border!");
				if( border.DataContext is PlanItem planItem ) {
					Debug.WriteLine($"=> {ObjectManager.GetCategory(planItem.ID)?.Title ?? "Unknown Category"}");
					NewDayTimeViewModel viewModel = ViewModelManager.NewDayTime;
					//NewDayTimeViewModel viewModel = new NewDayTimeViewModel();
					viewModel.DayOfWeek = day;
					var timespans = planItem.Time.GetTimeSpans();
					viewModel.StartTime = timespans.Start;
					viewModel.EndTime = timespans.End;
					viewModel.SelectedCategory = ObjectManager.GetCategory(planItem.ID);
					viewModel.Warning = null;
					ViewModelManager.OnEssentialViewModelChanged(viewModel, planItem);
				}
			}
			else if( e.OriginalSource is Grid grid ) {
				Debug.WriteLine("The origin is the Grid!");


				double test = grid.RowDefinitions[1].ActualHeight;
			}
		}

		private new void MouseMove( object sender, MouseEventArgs e ) {

		}

		private new void MouseLeftButtonUp( object sender, MouseButtonEventArgs e ) {

		}

	}
}
