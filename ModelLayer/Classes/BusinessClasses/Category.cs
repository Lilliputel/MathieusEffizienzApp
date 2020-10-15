using ModelLayer.Interfaces;
using ModelLayer.Utility;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	[Serializable]
	public class Category : ObservableObject, IAccountableParent<Goal>, IPlannedWork {

		#region public properties
		[XmlElement(nameof(UserText))]
		public UserText UserText { get; set; }

		[XmlArray(nameof(Children))]
		public Children<Goal> Children { get; } = new Children<Goal>();
		[XmlArray(nameof(WorkHours))]
		[AlsoNotifyFor(nameof(Time))]
		public ObservableCollection<WorkItem> WorkHours { get; } = new ObservableCollection<WorkItem>();
		[XmlIgnore]
		public TimeSpan Time {
			get {
				var placeholder = TimeSpan.Zero;
				new List<WorkItem>(WorkHours).ForEach(item => placeholder += item.Time);
				return placeholder;
			}
		}

		[XmlArray(nameof(WorkPlan))]
		public WorkPlan WorkPlan { get; } = new WorkPlan();

		[XmlAttribute(nameof(Archived))]
		public bool Archived { get; set; } = false;
		#endregion

		#region constructor
		public Category( UserText userText, bool archived = false ) {
			UserText = userText;
			Archived = archived;
		}
		public Category() { }
		#endregion

	}
}
