using LogicLayer.Commands;
using LogicLayer.ViewModels;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class DayPlanViewModel : ViewModelBase {

		#region fields

		private ICommand? _LeftMouseDownCommand;
		private ICommand? _MouseMoveCommand;
		private ICommand? _LeftMouseUpCommand;

		#endregion

		#region properties

		public ICommand LeftMouseDownCommand => _LeftMouseDownCommand ??=
			new RelayCommand(parameter => { });

		public ICommand MouseMoveCommand => _MouseMoveCommand ??=
			new RelayCommand(parameter => { });

		public ICommand LeftMouseUpCommand => _LeftMouseUpCommand ??=
			new RelayCommand(parameter => { });

		#endregion

		#region constructor

		public DayPlanViewModel() {

		}

		#endregion

		#region methods

		#endregion
	}
}
