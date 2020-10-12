using ModelLayer.Extensions;
using System.ComponentModel;

namespace ModelLayer.Enums {

	/// <summary>
	/// Ein Enum, in dem alle möglichen Arbeitsschritte erfasst sind.
	/// </summary>
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum WorkModeEnum {

		[Description("Stop: keine Arbeit")]
		Stop,
		[Description("Work: am arbeiten")]
		Work,
		[Description("Break: am Pause machen")]
		Break,
		[Description("Delay Work: die Arbeit aufschieben")]
		DelayWork,
		[Description("Delay Break: die Pause aufschieben")]
		DelayBreak

	}
}
