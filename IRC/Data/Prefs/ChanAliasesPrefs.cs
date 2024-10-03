using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class ChanAliasesPrefs : GlobalAliasesPrefs
{
	#region Constructors & Deconstructors
		public ChanAliasesPrefs(ChanPrefs mgrParent, NetAliasesPrefs inheritedSettings)
			: base(mgrParent, "Alias overrides for this network", PrefsRsrcs .strNetAliasTitle, PrefsRsrcs.strNetAliasDesc)
		{
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>>
				listDefEnabledAliases = new(inheritedSettings.AllInheritanceOverridesByName.Count + inheritedSettings
				.AddedAliases.Count);
			listDefEnabledAliases.AddRange(inheritedSettings.AllInheritanceOverridesByName.Values.Select(ialiasCur
					=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(ialiasCur.inheritedItem,
						ialiasCur.Status,ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>
						.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)));
			listDefEnabledAliases.AddRange(inheritedSettings.AddedAliases.Values.Select(aliasCur
				=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(aliasCur, true,
					ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes.network,
					PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
			foreach(ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> ialiasCur in
					listDefEnabledAliases)
				ialiasCur.evtDirtyChanged += (
						in NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> ialiasSender,
						in bool bNowDirty)
					=>
						{
							if(bNowDirty)
								MakeDirty();
						};
			mapAllInheritanceOverridesByName = new(
				this,
				"Inherited aliases",
				PrefsRsrcs.strNetChanAliasInheritedTitle,
				PrefsRsrcs.strNetChanAliasInheritedDesc,
				listDefEnabledAliases,
				KeyObtainer,
				(
						inherited, evth)
					=> inherited.inheritedItem.evtNameChanged += mapOverrideHandlers[evth] = (in GlobalAliasesOneAlias _, in
							string strOldName, in string _)
						=> evth(strOldName, inherited),
				(inherited, evth)
					=>
						{
							inherited.inheritedItem.evtNameChanged -= mapOverrideHandlers[evth];

							mapOverrideHandlers.Remove(evth);
						}
			);

			addedAliases = new(
				this,
				"Additional Aliases",
				PrefsRsrcs.strNetChanAliasesAdditionalTitle,
				PrefsRsrcs.strNetChanAliasesAdditionalDesc,
				[],
				KeyObtainer,
				(inherited, evth)
					=> inherited.evtNameChanged += mapAddedAliasesHandlers[evth] = (in GlobalAliasesOneAlias aliasSender, in
							string strOldName, in string _)
						=> evth(strOldName, inherited),
				(
						inherited,
						evth)
					=>
				{
					inherited.evtNameChanged -= mapAddedAliasesHandlers[evth];

					mapAddedAliasesHandlers.Remove(evth);
				}
			);
		}

		public ChanAliasesPrefs(ChanPrefs mgrParent, DTO.NetAliasesDTO dto, NetAliasesPrefs inheritedSettings) :
			base(mgrParent, "Alias overrides for this network", PrefsRsrcs.strNetAliasTitle, PrefsRsrcs.strNetAliasDesc)
		{
			this.inheritedSettings = inheritedSettings;


			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>>
				listDefEnabledAliases = new(inheritedSettings.AllInheritanceOverridesByName.Count + inheritedSettings
				.AddedAliases.Count);
			listDefEnabledAliases.AddRange(inheritedSettings.AllInheritanceOverridesByName.Values.Select(ialiasCur
					=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(ialiasCur.inheritedItem,
						ialiasCur.Status, ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>
						.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)));
			listDefEnabledAliases.AddRange(inheritedSettings.AddedAliases.Values.Select(aliasCur
				=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(aliasCur, true,
					ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes.network,
					PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>>
				listEnabledInheritedAliases = [.. inheritedSettings.AllInheritanceOverridesByName.Values.Select(ialias
					=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(ialias.inheritedItem,
						!dto?.DisabledInheritedAliases?.Contains(ialias.inheritedItem.guid) ?? ialias.Status,
						ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes.global,
						PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)), ];
			listEnabledInheritedAliases.AddRange(inheritedSettings.AddedAliases.Values.Select(ialiasCur
				=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(ialiasCur, !dto
					.DisabledInheritedAliases?.Contains(ialiasCur.guid) ?? true,
					ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes.network,
					PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
			foreach(ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> ialiasCur in
					listDefEnabledAliases)
				ialiasCur.evtDirtyChanged += (in NetInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>
						ialiasSender, in bool bNowDirty)
					=>
						{
							if(bNowDirty)
								MakeDirty();
						};
			mapAllInheritanceOverridesByName = new(
				this,
				"Inherited aliases",
				PrefsRsrcs.strNetChanAliasInheritedTitle,
				PrefsRsrcs.strNetChanAliasInheritedDesc,
				listDefEnabledAliases,
				listEnabledInheritedAliases,
				KeyObtainer,
				(inherited,
						evth)
					=> inherited.inheritedItem.evtNameChanged += mapOverrideHandlers[evth] = (in GlobalAliasesOneAlias _, in
							string strOldName, in string _)
						=> evth(strOldName, inherited),
				(inherited, evth)
					=>
						{
							inherited.inheritedItem.evtNameChanged -= mapOverrideHandlers[evth];

							mapOverrideHandlers.Remove(evth);
						}
			);

			addedAliases = new(
				this,
				"Additional Aliases",
				PrefsRsrcs.strNetChanAliasesAdditionalTitle,
				PrefsRsrcs.strNetChanAliasesAdditionalDesc,
				[],
				dto?.AddedAliases?.Select(daliasCur
					=> new GlobalAliasesOneAlias(daliasCur)
				) ?? [],
				KeyObtainer,
				(inherited, evth)
					=> inherited.evtNameChanged += mapAddedAliasesHandlers[evth] = (in GlobalAliasesOneAlias aliasSender, in
							string strOldName, in string _)
						=> evth(strOldName, inherited),
				(inherited, evth)
					=>
						{
							inherited.evtNameChanged -= mapAddedAliasesHandlers[evth];

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
		public readonly NetAliasesPrefs inheritedSettings;


		private readonly Platform.DataAndExt.Prefs.MappedListItem<string,
			ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>> mapAllInheritanceOverridesByName;

		private readonly System.Collections.Generic.Dictionary<System.Action<string,
				ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>>, Platform.DataAndExt
			.Obj<GlobalAliasesOneAlias>.DFieldChanged<string>> mapOverrideHandlers = [];

		private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalAliasesOneAlias> addedAliases;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalAliasesOneAlias>, Platform
			.DataAndExt.Obj<GlobalAliasesOneAlias>.DFieldChanged<string>> mapAddedAliasesHandlers = [];
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.MappedListItem<string, ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias,
			IReadOnlyOneAlias>> AllInheritanceOverridesByName
				=> mapAllInheritanceOverridesByName;

		public Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalAliasesOneAlias> AddedAliases
			=> addedAliases;
	#endregion

	#region Methods
		public new DTO.NetAliasesDTO ToDTO()
			=> new(
				(
					from ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> ialiasCur
						in mapAllInheritanceOverridesByName.Values
					where !ialiasCur.Status
					select ialiasCur.InheritedItem.GUID
				).ToArray(),
				addedAliases.Values.Select(aliasCur
					=> new DTO.GlobalAliasesOneAliasDTO(aliasCur.guid, aliasCur.Name, aliasCur.WhatToRun)
				).ToArray());

		private static string KeyObtainer(ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> aliasCur)
			=> aliasCur.InheritedItem.Name;

		private static string KeyObtainer(GlobalAliasesOneAlias aliasCur)
			=> aliasCur.Name;
	#endregion

	#region Event Handlers
	#endregion
}