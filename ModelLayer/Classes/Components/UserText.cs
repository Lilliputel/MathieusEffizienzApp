using ModelLayer.Utility;
using System;
using System.Drawing;
using System.Xml.Serialization;

namespace ModelLayer.Classes {
	[Serializable]
	public class UserText : ObservableObject {

		#region public properties
		public string Title { get; set; }
		[XmlAttribute( nameof( Description ) )]
		public string Description { get; set; } = "This is an object without description!";
		[XmlElement( nameof( Color ) )]
		public Color Color { get; set; }
		#endregion

		#region constructor
		public UserText( string title, string? description, Color color ) {
			Title = title;
			Description = description ?? Description;
			Color = color;
		}
		public UserText() { }
		~UserText() { }
		#endregion

	}
}
