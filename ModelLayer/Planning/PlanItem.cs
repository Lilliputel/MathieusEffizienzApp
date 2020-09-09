using ModelLayer.Utility;
using System;
using System.Xml.Serialization;

namespace ModelLayer.Planning {
	/// <summary>
	/// Enthält eine StartZeit und eine EndZeit
	/// </summary>
	public class PlanItem : ObservableObject {

		#region fields

		private DayTime _Time;
		private Guid _ID;
		private string _ColorHex;
		private string _Title;

		#endregion

		#region properties

		[XmlElement("Time")]
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
		[XmlAttribute("ID")]
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
		[XmlElement("Color")]
		public string ColorHex {
			get {
				return _ColorHex;
			}
			set {
				if( value == _ColorHex )
					return;
				_ColorHex = value;
				OnPropertyChanged(nameof(ColorHex));
			}
		}
		[XmlAttribute("Title")]
		public string Title {
			get {
				return _Title;
			}
			set {
				if( value == _Title )
					return;
				_Title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

		#endregion

		#region constructors

		public PlanItem() { }

		public PlanItem( DayTime time, Guid id, string colorHex, string title ) {
			this._Time = time;
			this._ID = id;
			this._ColorHex = colorHex;
			this._Title = title;
		}

		#endregion

		#region methods

		#endregion

	}
}
