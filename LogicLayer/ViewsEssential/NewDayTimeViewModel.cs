using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Planning;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewDayTimeViewModel : ViewModelBase {

		#region fields

		private ICommand? _SaveGoalCommand;

		#endregion

		#region properties

		public ObservableCollection<Category> CategoryList
			=> ObjectManager.CategoryList;
		public Category? SelectedCategory { get; set; }

		public DayOfWeek DayOfWeek { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }

		public DoubleTime? Overlapping { get; set; }

		public ICommand SaveGoalCommand => _SaveGoalCommand ??=
			 new AsyncRelayCommand(GetIfPossible, ( ex ) => {
				 AlertManager.InputInkorrekt(ex.Message);
			 });

		#endregion

		#region constructors

		#endregion

		#region methods

#warning System.AggregateException thrown

		private async Task GetIfPossible() {
			await Task.Run(() => Overlapping = ObjectManager.WeekPlan.AddItemToDayAsync(DayOfWeek, new PlanItem(new DoubleTime(StartTime, EndTime), SelectedCategory!.ID, SelectedCategory.Color, SelectedCategory.Title)).Result);
		}

		#endregion

	}
}
