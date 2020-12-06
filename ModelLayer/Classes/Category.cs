using ModelLayer.Extensions;
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
		[XmlElement( nameof( UserText ) )]
		public UserText UserText { get; set; }

		[XmlArray( nameof( Children ) )]
		public ObservableCollection<Goal> Children { get; } = new ObservableCollection<Goal>();
		[XmlArray( nameof( WorkHours ) ), AlsoNotifyFor( nameof( Time ) )]
		public ObservableCollection<WorkItem> WorkHours { get; } = new ObservableCollection<WorkItem>();
		[XmlIgnore]
		public TimeSpan Time {
			get {
				TimeSpan placeholder = TimeSpan.Zero;
				new List<WorkItem>( WorkHours ).ForEach( item => placeholder += item.Time );
				return placeholder;
			}
		}

		[XmlArray( nameof( WorkPlan ) )]
		public ObservableCollection<(DayOfWeek Day, DoubleTime Time)> WorkPlan { get; } = new ObservableCollection<(DayOfWeek Day, DoubleTime Time)>();

		[XmlAttribute( nameof( Archived ) )]
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
