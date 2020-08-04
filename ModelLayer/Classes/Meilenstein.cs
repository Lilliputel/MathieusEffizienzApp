using ModelLayer.Interfaces;
using ModelLayer.Utility;
using ModelLayer.ValueTypes;
using System;

namespace ModelLayer.Classes {
	public class Meilenstein : ObservableObject, IEindeutig, IChild, IBearbeitbar {

		#region fields

		private string titel;

		private Guid parentID;
		private EnumStatus status;
		private ZeitSpanne planung;
		private TimeSpan zeit;

		#endregion

		#region properties

		public Guid ID { get; }
		public string Titel {
			get {
				return titel;
			}
			set {
				titel = value;
				OnPropertyChanged(nameof(Titel));
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
		public ZeitSpanne Planung {
			get {
				return planung;
			}
			set {
				planung = value;
				OnPropertyChanged(nameof(Planung));
			}
		}
		public TimeSpan Zeit {
			get {
				return zeit;
			}
			set {
				zeit = value;
				OnPropertyChanged(nameof(Zeit));
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
			this.Planung = new ZeitSpanne(_StartDatum, _EndDatum);
		}
		#endregion

		#region methods

		#endregion

	}
}
