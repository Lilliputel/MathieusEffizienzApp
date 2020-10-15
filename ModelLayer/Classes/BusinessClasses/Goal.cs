using ModelLayer.Enums;
using ModelLayer.Extensions;
using ModelLayer.Interfaces;
using ModelLayer.Utility;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace ModelLayer.Classes {
	[Serializable]
	public class Goal : ObservableObject, IAccountableParent<Goal>, ICompleteable {

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

		[XmlElement(nameof(Plan))]
		public DateSpan Plan { get; set; }
		[XmlAttribute(nameof(State))]
		public StateEnum State { get; set; }
		#endregion

		#region public events
		public event PlanEventHandler? PlanChanged;
		#endregion

		#region Constructors
		public Goal( UserText userText, DateSpan plan, StateEnum state = StateEnum.ToDo ) : this() {
			UserText = userText;
			Plan = plan;
			Plan.PropertyChanged += ( sender, e ) => PlanChanged?.Invoke(Plan);
			State = state;
		}
		public Goal() {
			Children.CollectionChanged += ( sender, e ) => {
				if( e.Action == NotifyCollectionChangedAction.Add )
					if( e.NewItems is ICollection<Goal> col )
						new List<Goal>(col).ForEach(goal =>
							goal.PlanChanged += Child_PlanChanged);
			};
		}
		~Goal() {
			Plan.PropertyChanged -= ( sender, e ) => PlanChanged?.Invoke(Plan);
			new List<Goal>(Children).ForEach(goal => goal.PlanChanged -= Child_PlanChanged);
		}
		#endregion

		#region private helper methods
		private void Child_PlanChanged( DateSpan plan ) {
			if( plan.Start < Plan.Start )
				Plan.Start = plan.Start;
			if( plan.End > Plan.End )
				Plan.End = plan.End;
		}
		#endregion

	}
}
