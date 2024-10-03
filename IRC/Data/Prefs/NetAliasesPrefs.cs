using System.Linq;

namespace BestChat.IRC.Data.Prefs
{
public class NetAliasesPrefs : GlobalAliasesPrefs
{
	#region Constructors & Deconstructors
	public NetAliasesPrefs(in NetPrefsBase mgrParent, in GlobalAliasesPrefs inheritedSettings, in string? strName = null, in string? strLocalizedName = null, in string?
		strLocalizedDesc = null) :
		base(mgrParent, strName ?? "Alias overrides for this network", strLocalizedName ?? PrefsRsrcs
			.strNetAliasTitle, strLocalizedDesc ?? PrefsRsrcs.strNetAliasDesc)
	{
		this.mgrParent = mgrParent;
		this.inheritedSettings = inheritedSettings;

		System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalAliasesOneAlias,
			IReadOnlyOneAlias>> defEnabledAliases = inheritedSettings.Entries.Values
			.Select(aliasCur
				=> new NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(aliasCur)
			);
		mapAllInheritanceOverridesByName = new(
			this,
			"Which Inherited Aliases are Enabled",
			PrefsRsrcs.strNetAliasesInheritedTitle,
			PrefsRsrcs.strNetAliasesInheritedDesc,
			defEnabledAliases,
			KeyObtainer,
			(inherited, evth)
				=> inherited.evtKeyOfInheritedItemChanged += mapOverrideHandlers[evth] = (in
						NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> inherited, in string strOldName, in string _)
					=> evth(strOldName, inherited),
			(inherited,
					evth)
				=>
			{
				inherited.evtKeyOfInheritedItemChanged -= mapOverrideHandlers[evth];

				mapOverrideHandlers.Remove(evth);
			}
		);

		addedAliases = new(
			this,
			"Additional Aliases",
			PrefsRsrcs.strNetAliasesAdditionalTitle,
			PrefsRsrcs.strNetAliasesAdditionalDesc,
			[],
			KeyObtainer,
			(aliasEntry,
					evth)
				=> aliasEntry.evtNameChanged += mapAddedAliasesHandlers[evth] = (in GlobalAliasesOneAlias aliasEntry, in string
						strOldName, in string _)
					=> evth(strOldName, aliasEntry),
			(aliasEntry,
					evth)
				=>
			{
				aliasEntry.evtNameChanged -= mapAddedAliasesHandlers[evth];

				mapAddedAliasesHandlers.Remove(evth);
			}
		);
	}

	public NetAliasesPrefs(in NetPrefsBase mgrParent, DTO.NetAliasesDTO dto, in GlobalAliasesPrefs inheritedSettings, in string? strName = null, in string?
		strLocalizedName = null, in string? strLocalizedDesc = null) :
		base(mgrParent, strName ?? "Alias overrides for this network", strLocalizedName ?? PrefsRsrcs
			.strNetAliasTitle, strLocalizedDesc ?? PrefsRsrcs.strNetAliasDesc)
	{
		this.mgrParent = mgrParent;
		this.inheritedSettings = inheritedSettings;


		System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalAliasesOneAlias,
			IReadOnlyOneAlias>> defEnabledAliases = inheritedSettings.Entries.Values
			.Select(aliasCur
				=> new NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(aliasCur)
			);
		System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalAliasesOneAlias,
			IReadOnlyOneAlias>> enabledAliases = inheritedSettings.Entries.Values
			.Select(aliasCur
				=> new NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(aliasCur, dto?
					.DisabledInheritedAliases?.Contains(aliasCur.guid) ?? false)
			);
		mapAllInheritanceOverridesByName = new(
			this,
			"Which Inherited Aliases are Enabled",
			PrefsRsrcs.strNetAliasesInheritedTitle,
			PrefsRsrcs.strNetAliasesInheritedDesc,
			defEnabledAliases,
			enabledAliases,
			KeyObtainer,
			(inherited, evth)
				=> inherited.evtKeyOfInheritedItemChanged += mapOverrideHandlers[evth] = (in
						NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> inherited, in string strOldName, in string _)
					=> evth(strOldName, inherited),
			(inherited,
					evth)
				=>
			{
				inherited.evtKeyOfInheritedItemChanged -= mapOverrideHandlers[evth];

				mapOverrideHandlers.Remove(evth);
			}
		);

		addedAliases = new(
			this,
			"Additional Aliases",
			PrefsRsrcs.strNetAliasesAdditionalTitle,
			PrefsRsrcs.strNetAliasesAdditionalDesc,
			[],
			dto?.AddedAliases?.Select(daliasCur
				=> new GlobalAliasesOneAlias(daliasCur)
			)
			?? [],
			KeyObtainer,
			(aliasEntry,
					evth)
				=> aliasEntry.evtNameChanged += mapAddedAliasesHandlers[evth] = (
						in GlobalAliasesOneAlias aliasEntry, in string
							strOldName, in string _)
					=> evth(strOldName, aliasEntry),
			(aliasEntry,
					evth)
				=>
			{
				aliasEntry.evtNameChanged -= mapAddedAliasesHandlers[evth];

				mapAddedAliasesHandlers.Remove(evth);
			}
		);
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
	public new readonly NetPrefsBase mgrParent;

	public readonly GlobalAliasesPrefs inheritedSettings;


	private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>> mapAllInheritanceOverridesByName;

	private readonly System.Collections.Generic.Dictionary<System.Action<string,
		NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>>, NetInheritedItemEnabledStatus<GlobalAliasesOneAlias,IReadOnlyOneAlias>.DFieldChanged<string>> mapOverrideHandlers = [];

	private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalAliasesOneAlias> addedAliases;

	private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalAliasesOneAlias>, GlobalAliasesOneAlias.DFieldChanged<string>> mapAddedAliasesHandlers = [];
	#endregion

	#region Properties
	public Platform.DataAndExt.Prefs.MappedObjListItem<string, NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>> AllInheritanceOverridesByName
		=> mapAllInheritanceOverridesByName;

	public Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalAliasesOneAlias> AddedAliases
		=> addedAliases;
	#endregion

	#region Methods
	public new DTO.NetAliasesDTO ToDTO()
		=> new(
			(
				from NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> ialiasCur
					in mapAllInheritanceOverridesByName.Values
				where !ialiasCur.Status
				select ialiasCur.inheritedItem.guid
			)
			.ToArray(),
			addedAliases.Values.Select(
				aliasCur
					=> new DTO.GlobalAliasesOneAliasDTO(aliasCur.guid, aliasCur.Name, aliasCur.Cmd)
			).ToArray()
		);

	private static string KeyObtainer(NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> status)
		=> status.InheritedItem.Name;

	private static string KeyObtainer(GlobalAliasesOneAlias aliasToLookUpKeyFor)
		=> aliasToLookUpKeyFor.Name;
	#endregion

	#region Event Handlers
	#endregion
}
}