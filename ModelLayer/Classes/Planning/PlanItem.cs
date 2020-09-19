using ModelLayer.Utility;
using System;
using System.Drawing;
using System.Xml.Serialization;

namespace ModelLayer.Planning {
	/// <summary>
	/// Enthält eine StartZeit und eine EndZeit
	/// </summary>
	public class PlanItem : ObservableObject {

		#region properties

		[XmlElement("Time")]
		public DoubleTime Time { get; set; }
		[XmlAttribute("ID")]
		public Guid ID { get; set; }
		[XmlElement("Color")]
		public Color Color { get; set; }
		[XmlAttribute("Title")]
		public string Title { get; set; } = "Unknown PlanItem";

		#endregion

		#region constructors

		public PlanItem() {
			this.Time = new DoubleTime();
		}

		public PlanItem( DoubleTime time, Guid id, Color color, string title ) {
			this.Time = time;
			this.ID = id;
			this.Color = color;
			this.Title = title;
		}

		#endregion

		#region methods

		#endregion

	}
}
