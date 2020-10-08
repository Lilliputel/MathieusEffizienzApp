using ModelLayer.Utility;
using System;
using System.Drawing;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	[Serializable]
	public class Identification : ObservableObject {

		#region private fields
		private Guid _Guid;
		private string _Title = "New Category";
		private string _Description = "This is a Category without description!";
		private Color _Color;
		#endregion

		#region public properties
		[XmlAttribute(nameof(Guid))]
		public Guid Guid {
			get { return _Guid; }
			set {
				if( this._Guid == value )
					return;
				_Guid = value;
				OnIdentificationChanged();
			}
		}
		[XmlAttribute(nameof(Title))]
		public string Title {
			get { return _Title; }
			set {
				if( this._Title == value )
					return;
				_Title = value;
				OnIdentificationChanged();
			}
		}
		[XmlAttribute(nameof(Description))]
		public string Description {
			get {
				return _Description;
			}
			set {
				if( this._Description == value )
					return;
				_Description = value;
				OnIdentificationChanged();
			}
		}
		[XmlElement(nameof(Color))]
		public Color Color {
			get {
				return _Color;
			}
			set {
				if( this._Color == value )
					return;
				_Color = value;
				OnIdentificationChanged();
			}
		}
		#endregion

		#region public events
		public event EventHandler? IdentificationChanged;
		#endregion

		#region constructor
		public Identification( string? title, string? description, Color color ) {
			this.Guid = Guid.NewGuid();

			this.Title = title ?? this.Title;
			this.Description = description ?? this.Description;
			this.Color = color;
		}

		public Identification() { }
		~Identification() { }
		#endregion

		#region methods
		private void OnIdentificationChanged()
			=> this.IdentificationChanged?.Invoke(this, null);
		#endregion

	}
}
