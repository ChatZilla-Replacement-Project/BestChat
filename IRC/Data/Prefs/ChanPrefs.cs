namespace BestChat.IRC.Data.Prefs;

public class ChanPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public ChanPrefs(NetChanListPrefs mgrParent, Chan chanOwner) :
			base(mgrParent, "Preferences for one channel", PrefsRsrcs.strNetChanTitle, PrefsRsrcs.strNetChanDesc)
		{
			OwnerChan = chanOwner;

			aliases = new(this, mgrParent.mgrParent.Aliases);
			autoPerform = new(this, mgrParent.mgrParent.AutoPerform);
			stalkWords = new(this, mgrParent.mgrParent.StalkWords);
		}

		public ChanPrefs(NetChanListPrefs mgrParent, DTO.ChanDTO dto) :
			base(mgrParent, "Preferences for one channel", PrefsRsrcs.strNetChanTitle, PrefsRsrcs.strNetChanDesc)
		{
			if(!Chan.AllChanByName.ContainsKey(dto.OwnerChan))
				throw new System.InvalidProgramException("We have a preference for a channel we can't find " +
					"in the network definition.  Did something load out of order?");
			OwnerChan = Chan.AllChanByName[dto.OwnerChan];

			aliases = new(this, dto.Aliases, mgrParent.mgrParent.Aliases);
			autoPerform = new(this, dto.AutoPerform, mgrParent.mgrParent.AutoPerform);
			stalkWords = new(this, dto.StalkWords, mgrParent.mgrParent.StalkWords);
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
		private readonly ChanAliasesPrefs aliases;

		private readonly ChanAutoPerformPrefs autoPerform;

		private readonly ChanStalkWordsPrefs stalkWords;
	#endregion

	#region Properties
		public Chan OwnerChan
		{
			get;
		}


		public ChanAliasesPrefs Aliases
			=> aliases;

		public ChanAutoPerformPrefs AutoPerform
			=> autoPerform;

		public ChanStalkWordsPrefs StalkWords
			=> stalkWords;

		public override bool CanBeRemoved
			=> true;
	#endregion

	#region Methods
	public DTO.ChanDTO ToDTO()
		=> new(
			OwnerChan.Name,
			aliases.ToDTO(),
			autoPerform.ToDTO(),
			stalkWords.ToDTO()
		);
	#endregion

	#region Event Handlers
	#endregion
}