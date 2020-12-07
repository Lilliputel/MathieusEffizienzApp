using ModelLayer.Enums;
using ModelLayer.Extensions;
using ModelLayer.Interfaces;
using ModelLayer.Utility;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
#if XML
using System.Xml.Serialization; 
#elif SQLite
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace ModelLayer.Classes {
	public class Goal : ObservableObject, IAccountableParent<Goal>, ICompleteable {

		#region public properties
		[Required, Key]
		public int Id { get; set; }
#if XML
		[XmlElement( nameof( UserText ) )] 
#elif SQLite
		[ForeignKey( nameof( UserText ) )]
		[Required( AllowEmptyStrings = false )]
		public string UserTextId { get; set; }
#endif
		[Required]
		public UserText UserText { get; set; }

#if XML
		[XmlArray( nameof( Children ) )] 
#elif SQLite
		[ForeignKey( nameof( ParentCategory ) )]
		public int ParentCategoryId { get; set; }
		public Category ParentCategory { get; set; }

		[ForeignKey( nameof( ParentGoal ) )]
		public int ParentGoalId { get; set; }
		public Goal ParentGoal { get; set; }
#endif
		public ObservableCollection<Goal> Children { get; }
			= new ObservableCollection<Goal>();
#if XML
		[XmlArray( nameof( WorkHours ) )] 
#endif
		[AlsoNotifyFor( nameof( Time ) )]
		public ObservableCollection<WorkItem> WorkHours { get; }
			= new ObservableCollection<WorkItem>();
#if XML
		[XmlIgnore] 
#elif SQLite
		[NotMapped]
#endif
		public TimeSpan Time {
			get {
				TimeSpan placeholder = TimeSpan.Zero;
				new List<WorkItem>( WorkHours ).ForEach( item => placeholder += item.Time );
				return placeholder;
			}
		}

#if XML
		[XmlElement( nameof( Plan ) )] 
#elif SQLite
		[ForeignKey( nameof( Plan ) )]
		[Required]
		public int PlanId { get; set; }
#endif
		public DateSpan Plan { get; set; }
#if XML
		[XmlAttribute( nameof( State ) )] 
#elif SQLite
		[Column( TypeName = "TEXT" )]
#endif
		public StateEnum State { get; set; }
		#endregion

		#region public events
		public event PlanEventHandler? PlanChanged;
		#endregion

		#region Constructors
		public Goal( UserText userText, DateSpan plan, StateEnum state = StateEnum.ToDo ) : this() {
			UserText = userText;
			Plan = plan;
			Plan.PropertyChanged += ( sender, e ) => PlanChanged?.Invoke( Plan );
			State = state;
		}
		public Goal() {
			Children.CollectionChanged += ( sender, e ) => {
				if( e.Action == NotifyCollectionChangedAction.Add )
					if( e.NewItems is ICollection<Goal> col )
						new List<Goal>( col ).ForEach( goal =>
							   goal.PlanChanged += Child_PlanChanged );
			};
		}
		~Goal() {
			Plan.PropertyChanged -= ( sender, e ) => PlanChanged?.Invoke( Plan );
			new List<Goal>( Children ).ForEach( goal => goal.PlanChanged -= Child_PlanChanged );
		}
		#endregion

		#region public methods
		public TimeSpan GetTotalTimeOnDate( DateTime date ) {
			TimeSpan placeholder = TimeSpan.Zero;
			new List<Goal>( Children ).ForEach( Child =>
				   placeholder += (Child).GetTotalTimeOnDate( date )
				);
			placeholder += (this as IAccountable).GetTimeOnDate( date );
			return placeholder;
		}
		public ICollection<DateTime> GetTotalWorkedDates() {
			var placeholder = new List<DateTime>();
			new List<Goal>( Children ).ForEach( Child =>
				   placeholder.AddUniqueRange( Child.GetTotalWorkedDates() )
				);
			new List<WorkItem>( WorkHours ).ForEach( workItem =>
				   placeholder.AddUnique( workItem.Date ) );
			return placeholder;
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
