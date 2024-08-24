// Ignore Spelling: dcm Defs

namespace BestChat.IRC.Data.Defs;

[System.ComponentModel.ImmutableObject(true)]
public class UserMode : IMode
{
	#region Constructors & Deconstructors
		internal UserMode(in char chModeChar, in LocalizedTextSystem textDesc, in bool bNotAlwaysAvailable = false, in bool bIsReadOnly = false)
		{
			this.chModeChar = chModeChar;
			this.textDesc = textDesc;
			this.bNotAlwaysAvailable = bNotAlwaysAvailable;
			this.bIsReadOnly = bIsReadOnly;
		}

		internal UserMode(in DTO.UserModeDTO dcmUs)
		{
			chModeChar = dcmUs.Mode;
			textDesc = new(dcmUs.LocalizedDesc, dcmUs.DefaultDesc);
			bNotAlwaysAvailable = dcmUs.NotAlwaysAvailable;
			bIsReadOnly = dcmUs.IsReadOnly;
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
		public readonly char chModeChar;

		public readonly LocalizedTextSystem textDesc;

		public readonly bool bNotAlwaysAvailable;

		public readonly bool bIsReadOnly;
	#endregion

	#region Properties
		public char ModeChar
			=> chModeChar;

		public LocalizedTextSystem Desc
			=> textDesc;

		public bool NotAlwaysAvailable
			=> bNotAlwaysAvailable;

		public bool IsReadOnly
			=> bIsReadOnly;

		public System.Collections.Generic.IReadOnlyDictionary<string, ModeParam>? ParamsByName
			=> null;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}