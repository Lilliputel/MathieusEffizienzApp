using Effizienz.Interfaces;
using Effizienz.Utility;
using Effizienz.ValueTypes;
using System;

namespace Effizienz.Classes {
	public class Meilenstein : ObservableObject, IGuid, IChild, IStatus, IPlanbar {

		#region fields

		private Guid parentID;
		private EnumStatus status;

		private StructDaten planung;

		#endregion

		#region properties

		public Guid ID { get; }

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
		public StructDaten Planung {
			get {
				return planung;
			}
			set {
				planung = value;
				OnPropertyChanged(nameof(Planung));
			}
		}

		#endregion

		#region constructor

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Meilenstein must have a ParentID and a Planung!
		/// </summary>
		public Meilenstein() {
			this.ID = Guid.NewGuid();
			this.Status = EnumStatus.ToDo;
		}

		public Meilenstein( Guid _ParentID, DateTime _StartDatum, DateTime _EndDatum ) : this() {
			this.ParentID = _ParentID;
			this.Planung = new StructDaten(_StartDatum, _EndDatum);
		}
		#endregion

		#region methods

		#endregion

	}
}
