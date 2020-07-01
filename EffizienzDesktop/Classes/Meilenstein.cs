using Effizienz.Utility;
using System;

namespace Effizienz.Classes {
	public class Meilenstein : ObservableObject {

		#region fields

		private DateTime startDatum;
		private DateTime endDatum;

		private Guid parentID;
		private EnumStatus status;

		#endregion

		#region properties

		public Guid ID {
			get;
		}

		public DateTime StartDatum {
			get {
				return startDatum;
			}
			set {
				startDatum = value;
				OnPropertyChanged(nameof(StartDatum));
			}
		}
		public DateTime EndDatum {
			get {
				return endDatum;
			}
			set {
				endDatum = value;
				OnPropertyChanged(nameof(EndDatum));
			}
		}

		public Guid ParentID {
			get {
				return parentID;
			}
			set {
				parentID = value;
				OnPropertyChanged(nameof(ParentID));
			}
		}
		public EnumStatus Status {
			get {
				return status;
			}
			set {
				status = value;
				OnPropertyChanged(nameof(Status));
			}
		}

		#endregion

		#region constructor

		public Meilenstein() {

		}

		#endregion

		#region methods

		#endregion

	}
}
