using DataLayer;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LogicLayer.Views {
	public class PlanViewModel : ViewModelBase {

		#region private fields
		private readonly IRepository _DataService;
		#endregion

		#region public properties
		public ICollectionView CategoryList { get; }
		public WeekPlan WeekPlan { get; }
			= new WeekPlan();
		#endregion

		#region constructor
		public PlanViewModel( IRepository dataService ) {
			_DataService = dataService;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
			var tasklist = _DataService.LoadAll().SelectMany( c => c.WorkPlan ).Select( dt => WeekPlan.AddItemToDayAsync( dt ) );
			Parallel.ForEach( tasklist, t => t.Start() );
		}
		#endregion

	}
}
