// Ignore Spelling: pnet dpnetwork Defs

namespace BestChat.IRC.Data.Defs;

[System.ComponentModel.ImmutableObject(true)]
public class PredefinedNet : Net
{
	#region Constructors & Deconstructors
		public PredefinedNet()
		{
		}

		public PredefinedNet(in DTO.PredefinedNetDTO dpnetworkUs) :
			base(dpnetworkUs)
		{
			foreach(DTO.ChanModeDTO dcmCur in dpnetworkUs.ChanModeList)
				mapChanModesByModeChar[dcmCur.Mode] = new(dcmCur);
			foreach(DTO.UserModeDTO dumCur in dpnetworkUs.UserModeList)
				mapUserModesByModeChar[dumCur.Mode] = new(dumCur);
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
		private readonly System.Collections.Generic.SortedDictionary<char, ChanMode> mapChanModesByModeChar =
			[];

		private readonly System.Collections.Generic.SortedDictionary<char, UserMode> mapUserModesByModeChar =
			[];
	#endregion

	#region Properties
		public override System.Collections.Generic.IReadOnlyDictionary<char, ChanMode> ChanModesByModeChar
			=> mapChanModesByModeChar;

		public override System.Collections.Generic.IReadOnlyDictionary<char, UserMode> UserModesByModeChar
			=> mapUserModesByModeChar;

		public override bool HasPredefinition
			=> true;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}