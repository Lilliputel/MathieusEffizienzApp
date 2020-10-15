using ModelLayer.Utility;

namespace LogicLayer.ViewModels {
	public class ViewModelBase : ObservableObject {

		public ViewModelBase() {
			InitializeCommands();
		}

		protected virtual void InitializeCommands() {

		}
	}
}
