using ModelLayer.Utility;
using System;
using System.Windows.Media;

namespace ModelLayer.Planning {
	public class PlanItem : ObservableObject {

		#region fields

		private DayTime _Time;
		private Guid _ID;
		private Color _Color;

		#endregion

		#region properties

		public DayTime Time {
			get {
				return this._Time;
			}
			set {
				if( value == _Time )
					return;
				_Time = value;
				OnPropertyChanged(nameof(Time));
			}
		}
		public Guid ID {
			get {
				return this._ID;
			}
			set {
				if( value == _ID )
					return;
				_ID = value;
				OnPropertyChanged(nameof(ID));
			}
		}
		public Color Color {
			get {
				return _Color;
			}
			set {
				if( value == _Color )
					return;
				_Color = value;
				OnPropertyChanged(nameof(Color));
			}
		}

		#endregion

		#region constructors

		public PlanItem( DayTime time, Guid id, Color color ) {
			this._Time = time;
			this._ID = id;
			this._Color = color;
		}

		#endregion

		#region methods

		#endregion

	}
}
