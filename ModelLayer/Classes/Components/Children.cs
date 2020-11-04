using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	[Serializable]
	public class Children<T> : ObservableCollection<T> where T : class {

		#region public properties
		[XmlIgnore]
		public bool IsParent
			=> Count > 0;
		[XmlIgnore]
		[AlsoNotifyFor( nameof( IsParent ) )]
		public new int Count { get; set; }
		#endregion
	}
}
