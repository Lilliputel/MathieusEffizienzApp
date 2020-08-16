using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using LogicLayer.Views;
using ModelLayer.Planning;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LogicLayer.ViewsEssential {
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
