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

		[XmlAttribute(nameof(Time))]
		public DoubleTime Time { get; set; }
		[XmlAttribute(nameof(ID))]
		public Guid ID { get; set; }
		[XmlElement(nameof(Color))]
		public Color Color { get; set; }
		[XmlAttribute(nameof(Title))]
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
