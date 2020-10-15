using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewDayTimeViewModel : ViewModelBase {

		#region private fields
		private ICommand? _SaveGoalCommand;
		#endregion

		#region public properties
		public IAccountableParent<Category> CategoryList { get; }
		public Category? SelectedCategory { get; set; }
		public DayOfWeek DayOfWeek { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }

		public string? Warning { get; set; }
		#endregion

		#region public commands
		public ICommand SaveGoalCommand => _SaveGoalCommand ??=
			 new RelayCommandAsync(
				 () => AddToWeekPlan(),
				 obj => SelectedCategory is Category && StartTime != EndTime,
				 ex => {
					 if( ex is ArgumentException )
						 Warning = $"{ex.Message}";
					 else
						 AlertManager.InputInkorrekt(ex.Message);
				 });

		#endregion

		#region constructors
		public NewDayTimeViewModel( IAccountableParent<Category> categoryList )
			=> CategoryList = categoryList;
		#endregion

		#region private helper methods
		private Task AddToWeekPlan()
			=> ObjectManager.WeekPlan.AddItemToDayAsync(DayOfWeek, new PlanItem(new DoubleTime(StartTime, EndTime), SelectedCategory));
		#endregion

	}
}
