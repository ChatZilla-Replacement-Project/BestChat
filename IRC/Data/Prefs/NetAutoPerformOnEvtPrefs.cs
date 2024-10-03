﻿using System.Linq;

namespace BestChat.IRC.Data.Prefs
{
public class NetAutoPerformOnEvtPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
	public NetAutoPerformOnEvtPrefs(NetAutoPerformPrefs mgrParent, in string strName, in string strLocalizedName, in string
		strLocalizedDesc, GlobalAutoPeformOnEvtPrefs inheritedSettings) :
		base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
	{
		this.mgrParent = mgrParent;
		this.inheritedSettings = inheritedSettings;

		System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>> enabledInheritedSteps =
			from stepCur in inheritedSettings.Steps
			select new NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>(stepCur);
		mapAllInheritanceOverrides = new(
			this,
			"Steps that are disabled",
			PrefsRsrcs.strNetAutoPerformOnEvtDisabledStepsTitle,
			PrefsRsrcs.strNetAutoPerformOnEvtDisabledStepsDesc,
			enabledInheritedSteps,
			KeyObtainer,
			(overrideEntry,
					evth) =>
				overrideEntry.inheritedItem.evtKeyChanged += mapInheritedStepsHandlers[evth] = (in GlobalAutoPerformOneStep stepEntry, in string strOldWhatToDo, in string _)
					=> evth(strOldWhatToDo, overrideEntry),
			(overrideEntry, evth)
				=>
			{
				overrideEntry.inheritedItem.evtKeyChanged -= mapInheritedStepsHandlers[evth];

				mapInheritedStepsHandlers.Remove(evth);
			}
		)
		{
			DefTester = TestCurValForDef,
			ResetToDefMethod = ResetCurValToDef,
		};

		additionalSteps = new(this, "Steps", strLocalizedName, strLocalizedDesc, []);
	}

	public NetAutoPerformOnEvtPrefs(NetAutoPerformPrefs mgrParent, in string strName,
		in string strLocalizedName, in string strLocalizedDesc, DTO.NetAutoPeformOnEvtDTO dto, GlobalAutoPeformOnEvtPrefs inheritedSettings) :
		base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
	{
		this.mgrParent = mgrParent;
		this.inheritedSettings = inheritedSettings;


		System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>>
			defEnabledInheritedSteps =
				from stepCur in
					inheritedSettings.Steps
				select new NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>(stepCur);
		System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>> curEnabledInheritedSteps =
			from stepCur in
				inheritedSettings.Steps
			select new NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>(stepCur, !dto?
				.DisabledInheritedSteps?.Contains(stepCur.guid) ?? false);
		mapAllInheritanceOverrides = new(
			this,
			"Steps that are disabled",
			PrefsRsrcs.strNetAutoPerformOnEvtDisabledStepsTitle,
			PrefsRsrcs.strNetAutoPerformOnEvtDisabledStepsDesc,
			defEnabledInheritedSteps,
			curEnabledInheritedSteps,
			KeyObtainer,
			(
					overrideEntry,
					evth) =>
				overrideEntry.inheritedItem.evtKeyChanged += mapInheritedStepsHandlers[evth] = (
						in GlobalAutoPerformOneStep stepEntry, in string strOldWhatToDo, in string _)
					=> evth(strOldWhatToDo, overrideEntry),
			(overrideEntry, evth)
				=>
			{
				overrideEntry.inheritedItem.evtKeyChanged -= mapInheritedStepsHandlers[evth];

				mapInheritedStepsHandlers.Remove(evth);
			}
		)
		{
			DefTester = TestCurValForDef,
			ResetToDefMethod = ResetCurValToDef,
		};

		additionalSteps = new(this, "Steps", strLocalizedName, strLocalizedDesc, [], dto
			.AddedSteps == null
			? []
			: dto.AddedSteps.Select(
				dstepCur
					=> new GlobalAutoPerformOneStep(dstepCur)));
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
	public new readonly NetAutoPerformPrefs mgrParent;

	public readonly GlobalAutoPeformOnEvtPrefs inheritedSettings;


	private readonly Platform.DataAndExt.Prefs.MappedListItem<string,
		NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>> mapAllInheritanceOverrides;

	private readonly Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep> additionalSteps;

	private readonly System.Collections.Generic.Dictionary<System.Action<string,
		NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>>, IKeyChanged<GlobalAutoPerformOneStep, string>.DKeyChanged> mapInheritedStepsHandlers = [];
	#endregion

	#region Properties
	public Platform.DataAndExt.Prefs.MappedListItem<string, NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>>
		AllInheritanceOverrides
		=> mapAllInheritanceOverrides;


	public Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep>
		AdditionalSteps
		=> additionalSteps;
	#endregion

	#region Methods
	public DTO.NetAutoPeformOnEvtDTO ToDTO()
		=> new(
			(
				from NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>
					itemCur in mapAllInheritanceOverrides.Values
				where !itemCur.Status
				select itemCur.inheritedItem.guid
			).ToArray(),
			additionalSteps.Select(
				stepCur
					=> new DTO.GlobalAutoPerformOneStepDTO(stepCur.guid, stepCur.WhatToDo)
			).ToArray()
		);

	private static string KeyObtainer(NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep> item)
		=> item.InheritedItem.WhatToDo;

	private bool TestCurValForDef(System.Collections.Generic
		.IEnumerable<NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep>> entries)
		=> mapAllInheritanceOverrides.Def
			.Any(entryCur
				=> !mapAllInheritanceOverrides[entryCur.InheritedItem.WhatToDo].Status) && false;

	private void ResetCurValToDef()
	{
		foreach(NetInheritedItemEnabledStatus<GlobalAutoPerformOneStep, IReadOnlyOneStep> entryCur in mapAllInheritanceOverrides.Def)
			entryCur.Status = true;
	}
	#endregion

	#region Event Handlers
	#endregion
}
}