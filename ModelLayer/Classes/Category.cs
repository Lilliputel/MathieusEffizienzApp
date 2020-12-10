using ModelLayer.Extensions;
using ModelLayer.Interfaces;
using ModelLayer.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
#if XML
using System.Xml.Serialization;
#elif SQLite
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace ModelLayer.Classes {
	public class Category : ObservableObject, IAccountableParent<Goal>, IPlannedWork {

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
		public UserText UserText { get; set; }
#if XML
		[XmlArray( nameof( Children ) )]
#endif
		public ObservableCollection<Goal> Children { get; }
			= new ObservableCollection<Goal>();
#if XML
		[XmlArray( nameof( WorkHours ) ), AlsoNotifyFor( nameof( Time ) )]
#endif
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
		[XmlArray( nameof( WorkPlan ) )]
#endif
		public ObservableCollection<DoubleTime> WorkPlan { get; }
			= new ObservableCollection<DoubleTime>();
#if XML
		[XmlAttribute( nameof( Archived ) )]
#endif
		public bool Archived { get; set; } = false;
		#endregion

		#region constructor
		public Category( UserText userText, bool archived = false ) {
			UserText = userText;
			Archived = archived;
		}
		public Category() { }
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

	}
}
