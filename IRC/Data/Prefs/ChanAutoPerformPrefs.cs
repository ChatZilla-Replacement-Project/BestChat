using System.Linq;

namespace BestChat.IRC.Data.Prefs
{
public class ChanAutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
	public ChanAutoPerformPrefs(ChanPrefs mgrParent, NetAutoPerformPrefs inheritedSettings) :
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

	public ChanAutoPerformPrefs(ChanPrefs mgrParent, DTO.ChanAutoPerformDTO dto,
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
	public DTO.ChanAutoPerformDTO ToDTO()
		=> new(
			(
				from ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep> istepCur in mapAllInheritanceOverrides.Values
				where !istepCur.Status
				select istepCur.InheritedItem.GUID
			).ToArray(),
			(
				from GlobalAutoPerformOneStep stepCur in addedSteps
				select new DTO.GlobalAutoPerformOneStepDTO(stepCur.GUID, stepCur.WhatToDo)
			).ToArray()
		);

	private static string KeyObtainer(ChanInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep> istepCur)
		=> istepCur.InheritedItem.WhatToDo;
	#endregion

	#region Event Handlers
	#endregion
}
}