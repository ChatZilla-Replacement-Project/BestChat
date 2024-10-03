using System.Linq;

namespace BestChat.IRC.Data.Prefs
{
public class GlobalAliasesPrefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
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
				=> aliasEntry.evtNameChanged += mapAliasHandlers[evth] = (in GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> aliasSender, in string strVal,
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

	public GlobalAliasesPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.IrcDTO<GlobalDtoType>
		.GlobalDTO.OneAliasDTO[]? dto, in string? strName = null, in string? strLocalizedName = null, in string?
		strLocalizedDesc = null) :
		base(mgrParent, strName ?? "Aliases", strLocalizedName ?? PrefsRsrcs.strGlobalAliasesTitle,
			strLocalizedDesc ?? PrefsRsrcs.strGlobalAliasesDesc)
		=> entries = new(
			this,
			"Entries",
			PrefsRsrcs.strGlobalAliasesTitle,
			PrefsRsrcs.strGlobalAliasesDesc,
			[],
			dto?.Select(daliasCur
				=> new GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>(daliasCur, this)) ?? [],
			aliasCur
				=> aliasCur.Name,
			(aliasEntry,
					evth)
				=> aliasEntry.evtNameChanged += mapAliasHandlers[evth] = (in GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> aliasSender, in string strVal,
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
	public interface IReadOnlyOneAlias
	{
		string Name
		{
			get;
		}

		string Cmd
		{
			get;
		}

		System.Guid GUID
		{
			get;
		}
	}
	#endregion

	#region Members
	private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>> entries;

	private readonly
		System.Collections.Generic.Dictionary<System.Action<string, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>>, Platform.DataAndExt.Obj<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>>
			.DFieldChanged<string>> mapAliasHandlers = [];
	#endregion

	#region Properties
	public Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>> Entries
		=> entries;
	#endregion

	#region Methods
	public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO[]? ToDTO()
		=> entries.Values.Select(aliasCur
			=> aliasCur.ToDTO()).ToArray();
	#endregion

	#region Event Handlers
	#endregion
}
}