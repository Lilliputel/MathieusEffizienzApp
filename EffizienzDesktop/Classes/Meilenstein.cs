using Effizienz.Interfaces;
using Effizienz.Utility;
using System;

namespace Effizienz.Classes {
	public class Meilenstein : ObservableObject, IGuid, IChild, IStatus, IPlanbar {

		#region fields

		private Guid parentID;
		private EnumStatus status;

		private DateTime startDatum;
		private DateTime endDatum;

		#endregion

		#region properties

		public Guid ID {
			get;
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

		#endregion

		#region constructor

		public Meilenstein() {

		}

		#endregion

		#region methods

		#endregion

	}
}
