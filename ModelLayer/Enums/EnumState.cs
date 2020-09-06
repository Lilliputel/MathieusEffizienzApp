using ModelLayer.Extensions;
using System.ComponentModel;

namespace ModelLayer.Enums {

	/// <summary>
	/// Ein Enum, in dem alle möglichen Arbeitsschritte erfasst sind.
	/// </summary>
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumState {

		[Description("ToDo: Muss erledigt werden!")]
		ToDo,
		[Description("In Bearbeitung!")]
		InProgress,
		[Description("Prio 1: Sofort erledigen!")]
		Priority,
		[Description("Erledigt!")]
		Done,
		[Description("Bugs: Muss überarbeitet werden!")]
		Bugs

	}
}
