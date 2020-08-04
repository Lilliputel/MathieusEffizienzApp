using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Data;

namespace LogicLayer.Views {
	public class PlanungViewModel : ViewModelBase {

		#region properties

		public DataTable DTStundenplan { get; set; }

		public Stundenplan Stundenplan { get; set; }

		#endregion

		#region constructor

		public PlanungViewModel() {
			Stundenplan = new Stundenplan();
			CreateDataTable();
		}

		#endregion

		#region methods

		private void CreateDataTable() {
			DTStundenplan = new DataTable();
			DTStundenplan.Columns.Add("Zeit");
			foreach( var Tage in Stundenplan.Plan.Keys ) {
				DTStundenplan.Columns.Add(Tage.ToString());

			}
			for( int stunde = 0; stunde < 24; stunde++ ) {
				for( int minute = 0; minute <= 45; minute += 30 ) {
					DTStundenplan.Rows.Add(
						DTStundenplan.NewRow()["Zeit"]
						= $"{stunde.ToString("00")}:{minute.ToString("00")}");
				}
			}
		}

		#endregion

	}
}
