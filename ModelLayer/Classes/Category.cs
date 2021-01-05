using ModelLayer.Extensions;
using ModelLayer.Interfaces;
using ModelLayer.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
#if SQLite
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace ModelLayer.Classes {
	public class Category : ObservableObject, IAccountableParent<Goal>, IPlannedWork {

		#region public properties
		[Required, Key]
		public int Id { get; set; }
#if SQLite

		[ForeignKey( nameof( UserText ) )]
		[Required( AllowEmptyStrings = false )]
		public string UserTextId { get; set; }
#endif
		public UserText UserText { get; set; }
		public ObservableCollection<Goal> Children { get; }
			= new ObservableCollection<Goal>();
		public ObservableCollection<WorkItem> WorkHours { get; }
			= new ObservableCollection<WorkItem>();
#if SQLite
		[NotMapped]
#endif
		public TimeSpan Time {
			get {
				TimeSpan placeholder = TimeSpan.Zero;
				new List<WorkItem>( WorkHours ).ForEach( item => placeholder += item.Time );
				return placeholder;
			}
		}
		public ObservableCollection<DoubleTime> WorkPlan { get; }
			= new ObservableCollection<DoubleTime>();
		public bool Archived { get; set; } = false;
		#endregion

		#region constructor
		public Category( UserText userText, bool archived = false ) {
			UserText = userText;
			Archived = archived;
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

	}
}
