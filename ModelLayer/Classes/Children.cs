using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	[Serializable]
	public class Children : ObservableCollection<Goal> {

		#region public properties
		[XmlIgnore]
		[AlsoNotifyFor(nameof(IsChild))]
		public Identification ParentID { get; set; }
		[XmlIgnore]
		public bool IsChild
			=> ParentID is Identification;
		[XmlIgnore]
		public bool IsParent
			=> this.Count > 0;
		[XmlIgnore]
		[AlsoNotifyFor(nameof(IsParent))]
		public new int Count { get; set; }
		#endregion

		#region constructors
		public Children( Identification parentID ) : this() {
			this.ParentID = parentID;
		}
		public Children() {
			if( this.ParentID is Identification ) {
				this.ParentID.PropertyChanged += ( sender, e ) => UpdateIdOnChildren();
				this.ParentID.IdentificationChanged += ( sender, e ) => UpdateIdOnChildren();
			}
		}
		~Children() {
			this.ParentID.PropertyChanged -= ( sender, e ) => UpdateIdOnChildren();
			this.ParentID.IdentificationChanged -= ( sender, e ) => UpdateIdOnChildren();
		}
		#endregion

		#region public methods
		public new void Add( Goal child ) {
			child.Children.ParentID = this.ParentID;
			base.Add(child);
		}
		public Goal? GetChild( Guid ID ) {
			Goal? placeholder;
			foreach( Goal child in this ) {
				placeholder = child.Children.GetChild(ID);
				if( placeholder is { ID: Identification phID } && ID == phID.Guid ) {
					return placeholder;
				}
			}
			return null;
		}
		#endregion

		#region private helper methods
		private void UpdateIdOnChildren() {
			foreach( var child in this ) {
				child.Children.ParentID = this.ParentID;
			}
		}
		#endregion

	}
}
