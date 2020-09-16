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

		private Category? _SelectedCategory;

		private DayOfWeek _DayOfWeek;
		private TimeSpan _StartTime;
		private TimeSpan _EndTime;

		private DoubleTime? _Overlapping;

		private ICommand? _SaveGoalCommand;

		#endregion

		#region properties

		public ObservableCollection<Category> CategoryList
			=> ObjectManager.CategoryList;
		public Category? SelectedCategory {
			get {
				return _SelectedCategory;
			}
			set {
				if( value == _SelectedCategory )
					return;
				_SelectedCategory = value;
				OnPropertyChanged(nameof(SelectedCategory));
			}
		}

		public DayOfWeek DayOfWeek {
			get {
				return _DayOfWeek;
			}
			set {
				if( value == _DayOfWeek )
					return;
				_DayOfWeek = value;
				OnPropertyChanged(nameof(DayOfWeek));
			}
		}
		public TimeSpan StartTime {
			get {
				return _StartTime;
			}
			set {
				if( value == _StartTime )
					return;
				_StartTime = value;
				OnPropertyChanged(nameof(StartTime));
			}
		}
		public TimeSpan EndTime {
			get {
				return _EndTime;
			}
			set {
				if( value == _EndTime )
					return;
				_EndTime = value;
				OnPropertyChanged(nameof(EndTime));
			}
		}

		public DoubleTime? Overlapping {
			get {
				return _Overlapping;
			}
			set {
				if( value == _Overlapping )
					return;
				_Overlapping = value;
				OnPropertyChanged(nameof(Overlapping));
			}
		}

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
