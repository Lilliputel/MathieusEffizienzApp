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

		#region public methods
		public TimeSpan GetTotalTimeOnDate( DateTime date ) {
			var placeholder = TimeSpan.Zero;
			new List<Category>(Children).ForEach(Child =>
				placeholder += ( Child ).GetTotalTimeOnDate(date)
				);
			placeholder += ( this as IAccountable ).GetTimeOnDate(date);
			return placeholder;
		}
		public ICollection<DateTime> GetTotalWorkedDates() {
			var placeholder = new List<DateTime>();
			new List<Category>(Children).ForEach(Child =>
				placeholder.AddUniqueRange(Child.GetTotalWorkedDates())
				);
			new List<WorkItem>(WorkHours).ForEach(workItem =>
				placeholder.AddUnique(workItem.Date));
			return placeholder;
		}
		#endregion
	}
}
