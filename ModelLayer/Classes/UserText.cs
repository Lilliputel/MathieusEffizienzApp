using ModelLayer.Utility;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
#if SQLite
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
#if SQLite
		[Required( AllowEmptyStrings = true )]
#endif
		[MaxLength( 128 )]
		public string Description { get; set; } = "This is an object without description!";
#if SQLite
		[Column( TypeName = "TEXT" ), MaxLength( 128 )]
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
		#endregion

	}
}
