namespace BestChat.Platform.DataAndExt.Conversations;

[System.ComponentModel.ImmutableObject(true)]
public class AbstractEventType<EnumTypeAssociatedWithThisEventType>
	where EnumTypeAssociatedWithThisEventType : struct, System.Enum
{
	#region Constructors & Deconstructors
		protected AbstractEventType(in EnumTypeAssociatedWithThisEventType val, in string strDescOfVal)
		{
			this.val = val;
			this.strDescOfVal = strDescOfVal;

			mapAllInstancesByVal[val] = this;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly EnumTypeAssociatedWithThisEventType val;

		public readonly string strDescOfVal;

		private static readonly System.Collections.Generic.SortedDictionary<EnumTypeAssociatedWithThisEventType,
			AbstractEventType<EnumTypeAssociatedWithThisEventType>> mapAllInstancesByVal =
			[];
	#endregion

	#region Properties
		public EnumTypeAssociatedWithThisEventType Val
			=> val;

		public string DescOfVal
			=> strDescOfVal;
	#endregion

	#region Methods
	#endregion

	#region Operators
		public static implicit operator EnumTypeAssociatedWithThisEventType(AbstractEventType<EnumTypeAssociatedWithThisEventType> convertThis)
			=> convertThis.val;

		public static implicit operator AbstractEventType<EnumTypeAssociatedWithThisEventType>(EnumTypeAssociatedWithThisEventType convertThis)
			=> !mapAllInstancesByVal.ContainsKey(convertThis) ? throw new Exceptions
				.UnknownOrInvalidEnumException<EnumTypeAssociatedWithThisEventType>(convertThis, "While looking up the AbstractEventType for the " +
				"value") : mapAllInstancesByVal[convertThis];
	#endregion

	#region Event Handlers
	#endregion
}