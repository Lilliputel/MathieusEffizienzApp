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
#if SQLite
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
#endif

namespace ModelLayer.Classes {
	public class Goal : ObservableObject, IAccountableParent<Goal>, ICompleteable {

		#region private fields
		private DateSpan _Plan;
		#endregion

		#region public properties
		[Required, Key]
		public int Id { get; set; }
#if SQLite
		[ForeignKey( nameof( UserText ) )]
		[Required( AllowEmptyStrings = false )]
		public string UserTextId { get; set; }
#endif
		[Required]
		public UserText UserText { get; set; }

#if SQLite
		[ForeignKey( nameof( ParentCategory ) )]
		public int? ParentCategoryId { get; set; }
		public Category? ParentCategory { get; set; }

		[ForeignKey( nameof( ParentGoal ) )]
		public int? ParentGoalId { get; set; }
		public Goal? ParentGoal { get; set; }
#endif
		public ObservableCollection<Goal> Children { get; }
			= new ObservableCollection<Goal>();

		[AlsoNotifyFor( nameof( Time ) )]
		public ObservableCollection<WorkItem> WorkHours { get; }
			= new ObservableCollection<WorkItem>();
#if SQLite
		[NotMapped]
#endif
		public TimeSpan Time
			=> WorkHours.Aggregate( TimeSpan.Zero, ( sum, item ) => sum += item.Time );

#if SQLite
		[ForeignKey( nameof( Plan ) )]
		[Required]
		public int PlanId { get; set; }
#endif
		public DateSpan Plan {
			get => _Plan;
			set {
				if( value == _Plan )
					return;
				if( _Plan is DateSpan )
					_Plan.PropertyChanged -= RaisePlanChanged;
				_Plan = value;
				_Plan.PropertyChanged += RaisePlanChanged;
			}
		}
#if SQLite
		[Column( TypeName = "TEXT" )]
#endif
		public StateEnum State { get; set; }
		#endregion

		#region public events
		public event EventHandler<DateSpan>? PlanChanged;
		public event EventHandler? WorkHoursChanged;
		#endregion

		#region Constructors
		public Goal( UserText userText, DateSpan plan, StateEnum state = StateEnum.ToDo ) : this() {
			UserText = userText;
			Plan = plan;
			State = state;
		}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		private Goal() {
			WorkHours.CollectionChanged += ( sender, e ) => RaiseWorkHoursChanged( this, EventArgs.Empty );
			Children.CollectionChanged += ( sender, e ) => {
				if( e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace ) {
					if( e.NewItems is ICollection<Goal> col )
						new List<Goal>( col ).ForEach( goal => {
							goal.PlanChanged += OnChildPlanChanged;
							goal.WorkHoursChanged += OnChildTimeChanged;
						} );
				}
				if( e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace ) {
					if( e.OldItems is ICollection<Goal> col )
						new List<Goal>( col ).ForEach( goal => {
							goal.PlanChanged -= OnChildPlanChanged;
							goal.WorkHoursChanged -= OnChildTimeChanged;
						} );
				}
			};
		}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		~Goal() {
			new List<Goal>( Children ).ForEach( goal => goal.PlanChanged -= OnChildPlanChanged );
		}
		#endregion

		#region public methods
		public TimeSpan GetTotalTimeOnDate( DateTime date ) {
			TimeSpan placeholder = TimeSpan.Zero;
			new List<Goal>( Children ).ForEach( child =>
				   placeholder += child.GetTotalTimeOnDate( date )
				);
			placeholder += (this as IAccountable).GetTimeOnDate( date );
			return placeholder;
		}
		public ICollection<DateTime> GetTotalWorkedDates() {
			var placeholder = new List<DateTime>();
			new List<Goal>( Children ).ForEach( child =>
				   placeholder.AddUniqueRange( child.GetTotalWorkedDates() )
				);
			new List<WorkItem>( WorkHours ).ForEach( workItem =>
				   placeholder.AddUnique( workItem.Date ) );
			return placeholder;
		}
		#endregion

		#region private helper methods
		private void RaisePlanChanged( object? sender, EventArgs e )
			=> PlanChanged?.Invoke( this, Plan );
		private void OnChildPlanChanged( object? sender, DateSpan plan ) {
			if( plan.Start < Plan.Start )
				Plan.Start = plan.Start;
			if( plan.End > Plan.End )
				Plan.End = plan.End;
		}
		private void RaiseWorkHoursChanged( object? sender, EventArgs e )
			=> WorkHoursChanged?.Invoke( sender, e );
		private void OnChildTimeChanged( object? sender, EventArgs e )
			=> RaiseWorkHoursChanged( sender, e );
		#endregion

	}
}
