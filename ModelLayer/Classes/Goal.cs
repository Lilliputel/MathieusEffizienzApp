using ModelLayer.Enums;
using ModelLayer.Extensions;
using ModelLayer.Utility;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	[Serializable]
	public class Goal : ObservableObject {

		#region public properties
		[XmlElement(nameof(ID))]
		public Identification ID { get; set; }
		[XmlElement(nameof(Plan))]
		public DateSpan Plan { get; set; }
		[XmlAttribute(nameof(State))]
		public EnumState State { get; set; }

		[XmlArray(nameof(Children))]
		public Children Children { get; set; }
		[XmlArray(nameof(WorkHours))]
		[AlsoNotifyFor(nameof(Time))]
		public TimeContainer WorkHours { get; set; }
		[XmlIgnore]
		public TimeSpan Time {
			get {
				var placeholder = TimeSpan.Zero;
				foreach( var item in WorkHours )
					placeholder += item.Time;
				return placeholder;
			}
		}
		#endregion

		#region public events
		public event PlanEventHandler? PlanChanged;
		#endregion

		#region Constructors
		public Goal( Identification id, DateSpan plan, EnumState state = EnumState.ToDo ) : this() {
			this.ID = id;
			this.Plan = plan;
			this.Plan.PropertyChanged += ( sender, e ) => PlanChanged?.Invoke(this.Plan);
			this.State = state;
		}
		public Goal() {
			this.Children = new Children(this.ID);
			this.Children.CollectionChanged += ( sender, e ) => {
				if( e.Action == NotifyCollectionChangedAction.Add )
					if( e.NewItems is ICollection<Goal> col )
						new List<Goal>(col).ForEach(goal => goal.PlanChanged += Child_PlanChanged);
			};
			this.WorkHours = new TimeContainer();
		}
		~Goal() {
			this.Plan.PropertyChanged -= ( sender, e ) => PlanChanged?.Invoke(this.Plan);
			new List<Goal>(Children).ForEach(goal => goal.PlanChanged -= Child_PlanChanged);
		}
		#endregion

		#region public methods
		public TimeSpan GetTotalTimeOnDate( DateTime date ) {
			var placeholder = new TimeSpan();
			foreach( var goal in Children )
				placeholder += goal.GetTotalTimeOnDate(date.Date);
			placeholder += WorkHours.Time;
			return placeholder;
		}
		#endregion

		#region private helper methods
		private void Child_PlanChanged( DateSpan plan ) {
			if( plan.Start < this.Plan.Start )
				this.Plan.Start = plan.Start;
			if( plan.End > this.Plan.End )
				this.Plan.End = plan.End;
		}
		#endregion

	}
}
