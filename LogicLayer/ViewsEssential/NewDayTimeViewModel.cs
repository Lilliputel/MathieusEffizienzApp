using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewDayTimeViewModel : ViewModelBase {

		#region private fields
		private ICommand? _SaveGoalCommand;
		#endregion

		#region public properties
		public ObservableCollection<Category> CategoryList { get; private set; }
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
				 ( obj ) =>
					 SelectedCategory is Category && StartTime != EndTime
				 ,
				 ( ex ) => {
					 if( ex is ArgumentException )
						 this.Warning = $"{ex.Message}";
					 else
						 AlertManager.InputInkorrekt(ex.Message);
				 });

		#endregion

		#region constructors
		public NewDayTimeViewModel( ObservableCollection<Category> categoryList )
			=> CategoryList = categoryList;
		#endregion

		#region private helper methods
		private Task AddToWeekPlan()
			=> ObjectManager.WeekPlan.AddItemToDayAsync(this.DayOfWeek, new PlanItem(new DoubleTime(this.StartTime, this.EndTime), this.SelectedCategory!.ID.Guid, this.SelectedCategory.ID.Color, this.SelectedCategory.ID.Title));
		#endregion

	}
}
