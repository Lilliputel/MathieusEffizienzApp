using ModelLayer.Utility;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
#if XML
using System.Xml.Serialization;
#elif SQLite
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace ModelLayer.Classes {
	public class UserText : ObservableObject {

		#region public properties
#if SQLite
		[Key, Required( AllowEmptyStrings = false )]
#endif
		[MaxLength( 128 )]
		public string Title { get; set; }
#if XML
		[XmlAttribute( nameof( Description ) )]
#elif SQLite
		[Required( AllowEmptyStrings = true )]
#endif
		[MaxLength( 128 )]
		public string Description { get; set; } = "This is an object without description!";
#if XML
		[XmlElement( nameof( Color ) )] 
#elif SQLite
		[Column( TypeName = "TEXT" )]
#endif
		[Required]
		public Color Color { get; set; }
		#endregion

		#region constructor
		public UserText( string title, string? description, Color color ) {
			Title = title;
			Description = description ?? Description;
			Color = color;
		}
#if XML
		public UserText() { }
#endif
		#endregion

	}
}
