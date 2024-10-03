using System.Linq;
using BestChat.IRC.Data.Prefs.DTO;

namespace BestChat.IRC.Data.Prefs
{
public class ChanPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
	public ChanPrefs(NetChanListPrefs mgrParent, Chan chanOwner) :
		base(mgrParent, "Preferences for one channel", PrefsRsrcs.strNetChanTitle,
			PrefsRsrcs.strNetChanDesc)
	{
		OwnerChan = chanOwner;

		timeStamps = new(this);
		aliases = new(this, mgrParent.mgrParent.Aliases);
		autoPeform = new(this, mgrParent.mgrParent.AutoPerform);
		stalkWords = new(this, mgrParent.mgrParent.StalkWords);
	}

	public ChanPrefs(NetChanListPrefs mgrParent, DTO.ChanDTO dto) :
		base(mgrParent, "Preferences for one channel", PrefsRsrcs.strNetChanTitle,
			PrefsRsrcs.strNetChanDesc)
	{
		if(!Chan.AllChanByName.ContainsKey(dto.OwnerChan))
			throw new System.InvalidProgramException("We have a preference for a channel we can't find " +
				"in the network definition.  Did something load out of order?");
		OwnerChan = Chan.AllChanByName[dto.OwnerChan];

		timeStamps = new(this, dto.TimeStamps);
		aliases = new(this, dto.Aliases, mgrParent.mgrParent.Aliases);
		autoPeform = new(this, dto.AutoPerform, mgrParent.mgrParent.AutoPerform);
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
	public class TimeStampPrefs : Platform.DataAndExt.Prefs.GlobalAppearanceTimeStampPrefs
	{
		#region Constructors & Deconstructors
		public TimeStampPrefs(ChanPrefs mgrParent) :
			base(mgrParent)
			=> @override = new(mgrParent, "Override", PrefsRsrcs
					.strNetTimeStampOverrideNetTitle, PrefsRsrcs.strNetTimeStampOverrideNetDesc,
				false);

		public TimeStampPrefs(ChanPrefs mgrParent, DTO.NetTimeStampDTO dto) :
			base(mgrParent)
			=> @override = new(mgrParent, "Override", PrefsRsrcs
					.strNetTimeStampOverrideNetTitle, PrefsRsrcs.strNetTimeStampOverrideNetDesc,
				false, dto.Override);
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
		private readonly Platform.DataAndExt.Prefs.Item<bool> @override;
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.Item<bool> Override
			=> @override;
		#endregion

		#region Methods
		public override DTO.NetTimeStampDTO ToDTO()
			=> new(@override.CurVal, Show.CurVal, Fmt.CurVal);
		#endregion

		#region Event Handlers
		#endregion
	}

	public class AliasesPrefs : GlobalAliasesPrefs
	{
		#region Constructors & Deconstructors
		public AliasesPrefs(ChanPrefs mgrParent, NetAliasesPrefs inheritedSettings)
			: base(mgrParent, "Alias overrides for this network", PrefsRsrcs .strNetAliasTitle,
				PrefsRsrcs.strNetAliasDesc)
		{
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>>
				listDefEnabledAliases = new(inheritedSettings.AllInheritanceOverridesByName.Count +
					inheritedSettings.AddedAliases.Count);
			listDefEnabledAliases.AddRange(inheritedSettings.AllInheritanceOverridesByName.Values
				.Select(ialiasCur
					=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(ialiasCur.inheritedItem, ialiasCur
						.Status,ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes
						.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)));
			listDefEnabledAliases.AddRange(inheritedSettings.AddedAliases.Values.Select(aliasCur
				=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(aliasCur, true,
					ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes.network,
					PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
			foreach(ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> ialiasCur in listDefEnabledAliases)
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
						inherited,
						evth)
					=> inherited.inheritedItem.evtNameChanged += mapOverrideHandlers[evth] = (
							in GlobalAliasesOneAlias inheritedAlias,
							in string strOldName, in string _)
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
				(
						inherited,
						evth)
					=> inherited.evtNameChanged += mapAddedAliasesHandlers[evth] = (
							in GlobalAliasesOneAlias aliasSender, in string
								strOldName, in string _)
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

		public AliasesPrefs(ChanPrefs mgrParent, NetAliasesDTO dto, NetAliasesPrefs inheritedSettings) :
			base(mgrParent, "Alias overrides for this network", PrefsRsrcs
				.strNetAliasTitle, PrefsRsrcs.strNetAliasDesc)
		{
			this.inheritedSettings = inheritedSettings;


			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>> listDefEnabledAliases = new(inheritedSettings
				.AllInheritanceOverridesByName.Count + inheritedSettings.AddedAliases.Count);
			listDefEnabledAliases.AddRange(inheritedSettings.AllInheritanceOverridesByName.Values
				.Select(ialiasCur
					=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(ialiasCur.inheritedItem, ialiasCur.Status,
						ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText,
						inheritedSettings.mgrParent.OwnerNet)));
			listDefEnabledAliases.AddRange(inheritedSettings.AddedAliases.Values.Select(aliasCur
				=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(aliasCur, true, ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes
					.network, PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>> listEnabledInheritedAliases = [.. inheritedSettings
				.AllInheritanceOverridesByName.Values.Select(ialias
					=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(ialias.inheritedItem, !dto?.DisabledInheritedAliases?
							.Contains(ialias.inheritedItem.guid) ?? ialias.Status,
						ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings
							.mgrParent.OwnerNet)), ];
			listEnabledInheritedAliases.AddRange(inheritedSettings.AddedAliases.Values.Select(ialiasCur
				=> new ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>(ialiasCur, !dto?.DisabledInheritedAliases?.Contains(ialiasCur.guid)
					?? true, ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>.InheritedFromTypes.network, PrefsRsrcs.strAliasText,
					inheritedSettings.mgrParent.OwnerNet)));
			foreach(ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> ialiasCur in listDefEnabledAliases)
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
					=> inherited.inheritedItem.evtNameChanged += mapOverrideHandlers[evth] = (in GlobalAliasesOneAlias inheritedAlias,
							in string strOldName, in string _)
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
				(inherited,
						evth)
					=> inherited.evtNameChanged += mapAddedAliasesHandlers[evth] = (in GlobalAliasesOneAlias aliasSender, in string
							strOldName, in string _)
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


		private readonly Platform.DataAndExt.Prefs.MappedListItem<string, ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>> mapAllInheritanceOverridesByName;

		private readonly System.Collections.Generic.Dictionary<System.Action<string,
				ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>>, Platform.DataAndExt.Obj<GlobalAliasesOneAlias>
			.DFieldChanged<string>> mapOverrideHandlers = [];

		private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalAliasesOneAlias> addedAliases;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalAliasesOneAlias>, Platform.DataAndExt.Obj<GlobalAliasesOneAlias>.DFieldChanged<string>> mapAddedAliasesHandlers = [];
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.MappedListItem<string, ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias>> AllInheritanceOverridesByName
			=> mapAllInheritanceOverridesByName;


		public Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalAliasesOneAlias> AddedAliases
			=> addedAliases;
		#endregion

		#region Methods
		public new NetAliasesDTO ToDTO()
			=> new(
				(
					from ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> ialiasCur in
						mapAllInheritanceOverridesByName.Values
					where !ialiasCur.Status
					select ialiasCur.InheritedItem.GUID
				).ToArray(),
				addedAliases.Values.Select(aliasCur
					=> new GlobalAliasesOneAliasDTO(aliasCur.guid, aliasCur.Name, aliasCur.Cmd)
				).ToArray());

		private static string KeyObtainer(ChanInheritedItemEnabledStatus<GlobalAliasesOneAlias, IReadOnlyOneAlias> aliasCur)
			=> aliasCur.InheritedItem.Name;

		private static string KeyObtainer(GlobalAliasesOneAlias aliasCur)
			=> aliasCur.Name;
		#endregion

		#region Event Handlers
		#endregion
	}

	public class AutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public AutoPerformPrefs(ChanPrefs mgrParent, NetAutoPerformPrefs inheritedSettings) :
			base(mgrParent, "Steps to take when joining this channel", PrefsRsrcs
				.strNetChanAutoPerformTitle, PrefsRsrcs.strNetChanAutoPerformDesc)
		{
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>>
				listDefEnabledAliases = new(this.inheritedSettings.WhenJoiningChan.AllInheritanceOverrides
					.Count + inheritedSettings.WhenJoiningChan.AdditionalSteps.Count);
			listDefEnabledAliases.AddRange(inheritedSettings.WhenJoiningChan.AllInheritanceOverrides.Values.Select(istep
				=> new ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>(istep.inheritedItem, istep.Status,
					ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>.InheritedFromTypes.global, PrefsRsrcs
						.strOneStepText, inheritedSettings.mgrParent.OwnerNet)
			));
			listDefEnabledAliases.AddRange(inheritedSettings.WhenJoiningChan.AdditionalSteps
				.Select(istepCur
					=> new ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>(istepCur, true, ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>.InheritedFromTypes.network, PrefsRsrcs
						.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
			mapAllInheritanceOverrides = new(
				this,
				"Inherited aliases",
				PrefsRsrcs.strNetChanAliasInheritedTitle,
				PrefsRsrcs.strNetChanAliasInheritedDesc,
				listDefEnabledAliases,
				KeyObtainer,
				(inherited,
						evth)
					=> inherited.inheritedItem.evtWhatToDoChanged += mapOverrideHandlers[evth] = (in GlobalAutoPerformOneStep _, in string strNewVal, in string _)
						=> evth(strNewVal, inherited),
				(
						inherited,
						evth)
					=>
				{
					inherited.inheritedItem.evtWhatToDoChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			addedSteps = new(this, "Additional Steps to run when joining this channel",
				PrefsRsrcs.strNetChanAutoPerformAddedTitle, PrefsRsrcs.strNetChanAutoPerformAddedDesc,
				[]);
		}

		public AutoPerformPrefs(ChanPrefs mgrParent, ChanAutoPerformDTO dto,
			NetAutoPerformPrefs inheritedSettings) :
			base(mgrParent, "Steps to take when joining this channel", PrefsRsrcs
				.strNetChanAutoPerformTitle, PrefsRsrcs.strNetChanAutoPerformDesc)
		{
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>>
				listDefEnabledAliases = new(
					this.inheritedSettings.WhenJoiningChan.AllInheritanceOverrides.Count + inheritedSettings
						.WhenJoiningChan.AdditionalSteps.Count);
			listDefEnabledAliases.AddRange(inheritedSettings.WhenJoiningChan.AllInheritanceOverrides
				.Values.Select(istep
					=> new ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>(istep.inheritedItem, istep.Status,
						ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>.InheritedFromTypes.global, PrefsRsrcs
							.strOneStepText, inheritedSettings.mgrParent.OwnerNet)
				));
			listDefEnabledAliases.AddRange(inheritedSettings.WhenJoiningChan.AdditionalSteps.Select(
				istepCur
					=> new ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>(istepCur, true,
						ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>.InheritedFromTypes.network,
						PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>> listEnabledInheritedSteps =
				inheritedSettings.WhenJoiningChan.AllInheritanceOverrides.Values.Select(istepCur
					=> new ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>(istepCur.inheritedItem, !dto?.DisabledInheritedSteps?.Contains(istepCur
							.inheritedItem.guid) ?? istepCur.Status, ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>.InheritedFromTypes.global,
						PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)
				).ToList();
			foreach(ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep> istepCur in listEnabledInheritedSteps)
				istepCur.evtDirtyChanged += (in NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep> _, in bool
						bNowDirty)
					=>
				{
					if(bNowDirty)
						MakeDirty();
				};
			listEnabledInheritedSteps.AddRange(inheritedSettings.WhenJoiningChan.AdditionalSteps.Select(istepCur
				=> new ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>(istepCur, !dto?.DisabledInheritedSteps?
					.Contains(istepCur.guid) ?? true, ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>
					.InheritedFromTypes.network, PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
			mapAllInheritanceOverrides = new(
				this,
				"Inherited aliases",
				PrefsRsrcs.strNetChanAliasInheritedTitle,
				PrefsRsrcs.strNetChanAliasInheritedDesc,
				listEnabledInheritedSteps,
				listDefEnabledAliases,
				KeyObtainer,
				(inherited,
						evth)
					=> inherited.inheritedItem.evtWhatToDoChanged += mapOverrideHandlers[evth] = (in GlobalAutoPerformOneStep _, in string strNewVal, in string _)
						=> evth(strNewVal, inherited),
				(
						inherited,
						evth)
					=>
				{
					inherited.inheritedItem.evtWhatToDoChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			addedSteps = new(this, "Additional Steps to run when joining this channel",
				PrefsRsrcs.strNetChanAutoPerformAddedTitle, PrefsRsrcs.strNetChanAutoPerformAddedDesc,
				[], dto?.AddedSteps?.Select(dstepCur
					=> new GlobalAutoPerformOneStep(dstepCur))
				?? []);
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
		public readonly NetAutoPerformPrefs inheritedSettings;


		private readonly Platform.DataAndExt.Prefs.MappedListItem<string,
			ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>> mapAllInheritanceOverrides;

		private readonly System.Collections.Generic.Dictionary<System.Action<string,
			ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>>, GlobalAutoPerformOneStep.DFieldChanged<string>> mapOverrideHandlers = [];

		private readonly Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep> addedSteps;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalAutoPerformOneStep>, GlobalAutoPerformOneStep.DFieldChanged<string>> mapAddedAliasesHandlers = [];
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.MappedListItem<string, ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>>
			AllInheritanceOverrides
			=> mapAllInheritanceOverrides;

		public Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep> AddedSteps
			=> addedSteps;
		#endregion

		#region Methods
		public ChanAutoPerformDTO ToDTO()
			=> new(
				(
					from ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep> istepCur in mapAllInheritanceOverrides.Values
					where !istepCur.Status
					select istepCur.InheritedItem.GUID
				).ToArray(),
				(
					from GlobalAutoPerformOneStep stepCur in addedSteps
					select new GlobalAutoPerformOneStepDTO(stepCur.GUID, stepCur.WhatToDo)
				).ToArray()
			);

		private static string KeyObtainer(ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep> istepCur)
			=> istepCur.InheritedItem.WhatToDo;
		#endregion

		#region Event Handlers
		#endregion
	}

	public class StalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public StalkWordsPrefs(ChanPrefs mgrParent, NetStalkWordsPrefs inheritedSettings) :
			base(mgrParent, "Stalk words for this channel", PrefsRsrcs
				.strNetChanStalkWordsInheritedTitle, PrefsRsrcs.strNetChanStalkWordsInheritedDesc)
		{
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>> listDefEnabledAliases = inheritedSettings
				.AllInheritanceOverridesByCtnts.Values.Select(iswCur
					=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur.inheritedItem, iswCur.Status,
						ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>
							.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent
							.OwnerNet)
				).ToList();
			listDefEnabledAliases.AddRange(inheritedSettings.AddedStalkWords.Values.Select(iswCur
				=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur, true,
					ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.network, PrefsRsrcs.strAliasText,
					inheritedSettings.mgrParent.OwnerNet)));
			mapAllInheritanceOverridesByName = new(
				this,
				"Inherited aliases",
				PrefsRsrcs.strNetChanAutoPerformInheritedTitle,
				PrefsRsrcs.strNetChanAutoPerformInheritedDesc,
				listDefEnabledAliases,
				KeyObtainer,
				(inherited, evth)
					=> inherited.inheritedItem.evtCtntsChanged += mapOverrideHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string strOldVal, in string _)
						=> evth(strOldVal, inherited),
				(inherited,
						evth)
					=>
				{
					inherited.inheritedItem.evtCtntsChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			addedStalkWords = new(
				this,
				"Additional Stalk Words",
				PrefsRsrcs.strNetChanStalkWordsAddedTitle,
				PrefsRsrcs.strNetChanStalkWordsAddedDesc,
				[],
				KeyObtainer,
				(inherited,
						evth)
					=> inherited.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string strOldVal, in string _)
						=> evth(strOldVal, inherited),
				(inherited,
						evth)
					=>
				{
					inherited.evtCtntsChanged -= mapAddedStalkWordHandlers[evth];

					mapAddedStalkWordHandlers.Remove(evth);
				},
				true
			);
		}

		public StalkWordsPrefs(ChanPrefs mgrParent, NetStalkWordsDTO dto, NetStalkWordsPrefs inheritedSettings) :
			base(mgrParent, "Stalk words for this channel", PrefsRsrcs
				.strNetChanStalkWordsInheritedTitle, PrefsRsrcs.strNetChanStalkWordsInheritedDesc)
		{
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>> listDefEnabledStalkWords = inheritedSettings
				.AllInheritanceOverridesByCtnts.Values.Select(iswCur
					=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur.inheritedItem, iswCur.Status,
						ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.global, PrefsRsrcs
							.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)
				).ToList();
			listDefEnabledStalkWords.AddRange(inheritedSettings.AddedStalkWords.Values.Select(iswCur
				=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur, true,
					ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.network, PrefsRsrcs.strAliasText,
					inheritedSettings.mgrParent.OwnerNet)));
			System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>> listEnabledInheritedStalkWords =
				inheritedSettings.AllInheritanceOverridesByCtnts.Values.Select(iswCur
					=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur.inheritedItem, !dto?
							.DisabledInheritedStalkWords?.Contains(iswCur.inheritedItem.guid) ?? iswCur.Status,
						ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText,
						inheritedSettings.mgrParent.OwnerNet)
				).ToList();
			foreach(ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord> ialiasCur in listEnabledInheritedStalkWords)
				ialiasCur.evtDirtyChanged += (in NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord> _, in bool bNowDirty)
					=>
				{
					if(bNowDirty)
						MakeDirty();
				};
			listEnabledInheritedStalkWords.AddRange(inheritedSettings.AddedStalkWords.Values
				.Select(iswCur
					=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur, !dto?.DisabledInheritedStalkWords?
							.Contains(iswCur.guid) ?? true, ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.network,
						PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
			mapAllInheritanceOverridesByName = new(
				this,
				"Inherited aliases",
				PrefsRsrcs.strNetChanAliasInheritedTitle,
				PrefsRsrcs.strNetChanAliasInheritedDesc,
				listDefEnabledStalkWords,
				listEnabledInheritedStalkWords,
				KeyObtainer,
				(inherited, evth)
					=> inherited.inheritedItem.evtCtntsChanged += mapOverrideHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string strOldVal, in string _)
						=> evth(strOldVal, inherited),
				(inherited,
						evth)
					=>
				{
					inherited.inheritedItem.evtCtntsChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			addedStalkWords = new(
				this,
				"Additional Stalk Words",
				PrefsRsrcs.strNetChanStalkWordsAddedTitle,
				PrefsRsrcs.strNetChanStalkWordsAddedDesc,
				[],
				KeyObtainer,
				(inherited,
						evth)
					=> inherited.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string strOldVal, in string _)
						=> evth(strOldVal, inherited),
				(inherited,
						evth)
					=>
				{
					inherited.evtCtntsChanged -= mapAddedStalkWordHandlers[evth];

					mapAddedStalkWordHandlers.Remove(evth);
				},
				true);
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
		public readonly NetStalkWordsPrefs inheritedSettings;


		private readonly Platform.DataAndExt.Prefs.MappedListItem<string, ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>>
			mapAllInheritanceOverridesByName;

		private readonly System.Collections.Generic.Dictionary<System.Action<string,
				ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>>, GlobalStalkWordsOneStalkWord.DFieldChanged<string>>
			mapOverrideHandlers = [];

		private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalStalkWordsOneStalkWord> addedStalkWords;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalStalkWordsOneStalkWord>, GlobalStalkWordsOneStalkWord.DFieldChanged<string>>
			mapAddedStalkWordHandlers = [];
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.MappedListItem<string, ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>>
			AllInheritanceOverridesByName
			=> mapAllInheritanceOverridesByName;


		public Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalStalkWordsOneStalkWord>
			AddedStalkWords
			=> addedStalkWords;
		#endregion

		#region Methods
		public NetStalkWordsDTO ToDTO()
			=> new(
				(from iswCur in mapAllInheritanceOverridesByName.Values
				where !iswCur.Status
				select iswCur.InheritedItem.GUID).ToArray()
			);

		private static string KeyObtainer(ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord> iswCur)
			=> iswCur.InheritedItem.Ctnts;

		private static string KeyObtainer(GlobalStalkWordsOneStalkWord swCur)
			=> swCur.Ctnts;
		#endregion

		#region Event Handlers
		#endregion
	}
	#endregion

	#region Members
	private readonly TimeStampPrefs timeStamps;

	private readonly AliasesPrefs aliases;

	private readonly AutoPerformPrefs autoPeform;

	private readonly StalkWordsPrefs stalkWords;
	#endregion

	#region Properties
	public Chan OwnerChan
	{
		get;

		private init;
	}


	public TimeStampPrefs TimeStamps
		=> timeStamps;

	public AliasesPrefs Aliases
		=> aliases;

	public AutoPerformPrefs AutoPerform
		=> autoPeform;

	public StalkWordsPrefs StalkWords
		=> stalkWords;

	public override bool CanBeRemoved
		=> true;
	#endregion

	#region Methods
	public ChanDTO ToDTO()
		=> new(OwnerChan.Name, timeStamps.ToDTO(), aliases.ToDTO(), autoPeform.ToDTO(), stalkWords
			.ToDTO());
	#endregion

	#region Event Handlers
	#endregion
}
}