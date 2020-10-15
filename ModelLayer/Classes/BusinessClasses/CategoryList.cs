using ModelLayer.Extensions;
using ModelLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ModelLayer.Classes {
	[Serializable]
	public class CategoryList : IAccountableParent<Category> {
		[XmlArray("Categories")]
		public Children<Category> Children { get; } = new Children<Category>();
		[XmlIgnore]
		public ObservableCollection<WorkItem> WorkHours {
			get {
				var placeholder = new ObservableCollection<WorkItem>();
				new List<Category>(Children).ForEach(category =>
					placeholder.AddRange(category.WorkHours));
				return placeholder;
			}
		}

		[XmlIgnore]
		public TimeSpan Time {
			get {
				var placeholder = TimeSpan.Zero;
				new List<WorkItem>(WorkHours).ForEach(item =>
					placeholder += item.Time);
				return placeholder;
			}
		}
	}
}
