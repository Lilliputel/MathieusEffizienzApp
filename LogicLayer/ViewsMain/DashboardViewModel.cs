using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.ObjectModel;
namespace LogicLayer.Views {
	public class DashboardViewModel : ViewModelBase {

		#region private fields

		#endregion

		#region public properties
		public ObservableCollection<Category> Categories { get; private set; }
		#endregion

		#region constructor
		public DashboardViewModel( ObservableCollection<Category> categories )
			=> Categories = categories;
		#endregion

		#region methods

		#endregion

	}
}
