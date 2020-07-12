using System;

namespace Effizienz.ValueTypes {
	public struct StructDaten {

		#region fields


		private DateTime _start;
		private DateTime _ende;
		private TimeSpan _zeitSpanne;

		#endregion

		#region properties

		public DateTime Start {
			get { return _start; }
			set {
				_start = value;
				UpdateZeitSpanne();
			}
		}
		public DateTime Ende {
			get { return _ende; }
			set {
				_ende = value;
				UpdateZeitSpanne();
			}
		}

		public TimeSpan ZeitSpanne {
			get {
				return _zeitSpanne;
			}
		}

		#endregion

		#region constructor

		public StructDaten( DateTime start, DateTime ende ) {
			_start = start;
			_ende = ende;
			_zeitSpanne = _ende - _start;
		}

		#endregion

		#region methods

		private void UpdateZeitSpanne() {
			_zeitSpanne = Ende - Start;
		}

		#endregion

	}
}
