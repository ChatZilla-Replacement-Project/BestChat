namespace BestChat.IRC.Data.Prefs;

public abstract class GlobalPrefs<GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalDtoType : DTO.GlobalDTO
{
	#region Constructors & Deconstructors
		public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent, "Global", PrefsRsrcs.strGlobalTitle, PrefsRsrcs.strGlobalDesc)
		{
			autoPerform = new(this);
			dcc = new(this);
			conn = new(this);
			aliases = new(this);
			altNicks = new(this);
			stalkWords = new(this);
		}

		public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, GlobalDtoType dto)
			: base(mgrParent, "Global", PrefsRsrcs.strGlobalTitle, PrefsRsrcs.strGlobalDesc)
		{
			autoPerform = new(this, dto.AutoPerform);
			dcc = new(this, dto.DCC);
			conn = new(this, dto.Conn);
			aliases = new(this, dto.Aliases);
			altNicks = new(this, dto.AltNicks);
			stalkWords = new(this, dto.StalkWords);
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
		private readonly GlobalAutoPerformPrefs autoPerform;

		private readonly GlobalDccPrefs dcc;

		private readonly GlobalConnPrefs conn;

		private readonly GlobalAliasesPrefs aliases;

		private readonly GlobalAltNicksPrefs altNicks;

		private readonly GlobalStalkWordsPrefs stalkWords;
	#endregion

	#region Properties
		public GlobalAutoPerformPrefs AutoPerform
			=> autoPerform;

		public GlobalDccPrefs DCC
			=> dcc;

		public GlobalConnPrefs Conn
			=> conn;

		public GlobalAliasesPrefs Aliases
			=> aliases;

		public GlobalAltNicksPrefs AltNicks
			=> altNicks;

		public GlobalStalkWordsPrefs StalkWords
			=> stalkWords;
	#endregion

	#region Methods
		public abstract GlobalDtoType ToDTO();
	#endregion

	#region Event Handlers
	#endregion
}