using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class GlobalAliasesPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAliasesPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string? strName = null, in
			string? strLocalizedName = null, in string? strLocalizedDesc = null) :
			base(mgrParent, strName ?? "Aliases", strLocalizedName ?? PrefsRsrcs.strGlobalAliasesTitle,
				strLocalizedDesc ?? PrefsRsrcs.strGlobalAliasesDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalAliasesTitle,
				PrefsRsrcs.strGlobalAliasesDesc,
				[],
				aliasCur
					=> aliasCur.Name,
				(aliasEntry,
						evth)
					=> aliasEntry.evtNameChanged += mapAliasHandlers[evth] = (in GlobalAliasesOneAlias aliasSender, in string strVal,
							in string _)
						=> evth(strVal, aliasEntry),
				(aliasEntry,
						evth)
					=>
						{
							aliasEntry.evtNameChanged -= mapAliasHandlers[evth];

							mapAliasHandlers.Remove(evth);
						}
			);

		public GlobalAliasesPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.GlobalAliasesOneAliasDTO[]? dto, in
				string? strName = null, in string? strLocalizedName = null, in string? strLocalizedDesc = null) :
			base(mgrParent, strName ?? "Aliases", strLocalizedName ?? PrefsRsrcs.strGlobalAliasesTitle,
				strLocalizedDesc ?? PrefsRsrcs.strGlobalAliasesDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalAliasesTitle,
				PrefsRsrcs.strGlobalAliasesDesc,
				[],
				dto?.Select(daliasCur
					=> new GlobalAliasesOneAlias(daliasCur)) ?? [],
				aliasCur
					=> aliasCur.Name,
				(aliasEntry,
						evth)
					=> aliasEntry.evtNameChanged += mapAliasHandlers[evth] = (in GlobalAliasesOneAlias aliasSender, in string strVal,
							in string _)
						=> evth(strVal, aliasEntry),
				(aliasEntry,
						evth)
					=>
				{
					aliasEntry.evtNameChanged -= mapAliasHandlers[evth];

					mapAliasHandlers.Remove(evth);
				}
			);
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
		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalAliasesOneAlias> entries;

		private readonly
			System.Collections.Generic.Dictionary<System.Action<string, GlobalAliasesOneAlias>, Platform.DataAndExt
				.Obj<GlobalAliasesOneAlias>.DFieldChanged<string>> mapAliasHandlers = [];
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalAliasesOneAlias> Entries
			=> entries;
	#endregion

	#region Methods
		public DTO.GlobalAliasesOneAliasDTO[]? ToDTO()
			=> entries.Values.Select(aliasCur
				=> aliasCur.ToDTO()).ToArray();
	#endregion

	#region Event Handlers
	#endregion
}