using System.Linq;

namespace BestChat.IRC.Data.Prefs
{
public class NetAutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
	public NetAutoPerformPrefs(NetPrefsBase mgrParent, GlobalAutoPerformPrefs inheritedSettings) :
		base(mgrParent, "Auto-perform", PrefsRsrcs.strNetAutoPerformTitle, PrefsRsrcs
			.strNetAutoPerformDesc)
	{
		this.mgrParent = mgrParent;

		whenJoiningNet = new(this, "When joining this network", PrefsRsrcs
			.strNetAutoPerformWhenJoiningNetTitle, PrefsRsrcs.strNetAutoPerformWhenJoiningNetDesc, inheritedSettings
			.WhenJoiningNet);
		whenJoiningChan = new(this, "When joining any channel on this network",
			PrefsRsrcs.strNetAutoPerformWhenJoiningChanTitle, PrefsRsrcs
				.strNetAutoPerformWhenJoiningChanDesc, inheritedSettings.WhenJoiningChan);
		whenOpeningUserChat = new(this, "When opening chat with any user on this" +
			" network", PrefsRsrcs.strNetAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
				.strNetAutoPerformWhenOpeningUserChatDesc, inheritedSettings.WhenOpeningUserChat);
	}

	public NetAutoPerformPrefs(NetPrefsBase mgrParent, DTO.NetAutoPerformDTO dto, GlobalAutoPerformPrefs inheritedSettings)
		: base(mgrParent, "Auto-perform", PrefsRsrcs.strNetAutoPerformTitle, PrefsRsrcs
			.strNetAutoPerformDesc)
	{
		this.mgrParent = mgrParent;

		whenJoiningNet = new(this, "When joining this network", PrefsRsrcs
				.strNetAutoPerformWhenJoiningNetTitle, PrefsRsrcs.strNetAutoPerformWhenJoiningNetDesc,
			dto.WhenJoiningNet, inheritedSettings.WhenJoiningNet);
		whenJoiningChan = new(this, "When joining any channel on this network",
			PrefsRsrcs.strNetAutoPerformWhenJoiningChanTitle, PrefsRsrcs
				.strNetAutoPerformWhenJoiningChanDesc, dto.WhenJoiningChan, inheritedSettings.WhenJoiningChan);
		whenOpeningUserChat = new(this, "When opening chat with any user on this" +
			" network", PrefsRsrcs.strNetAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
				.strNetAutoPerformWhenOpeningUserChatDesc, dto.WhenOpeningUserChat, inheritedSettings.WhenOpeningUserChat);
	}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	public class OnEvtPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public OnEvtPrefs(NetAutoPerformPrefs mgrParent, in string strName, in string strLocalizedName, in string
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

		public OnEvtPrefs(NetAutoPerformPrefs mgrParent, in string strName,
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
	#endregion

	#region Members
	public new readonly NetPrefsBase mgrParent;

	private readonly OnEvtPrefs whenJoiningNet;

	private readonly OnEvtPrefs whenJoiningChan;

	private readonly OnEvtPrefs whenOpeningUserChat;
	#endregion

	#region Properties
	public OnEvtPrefs WhenJoiningNet
		=> whenJoiningNet;

	public OnEvtPrefs WhenJoiningChan
		=> whenJoiningChan;

	public OnEvtPrefs WhenOpeningUserChat
		=> whenOpeningUserChat;
	#endregion

	#region Methods
	public DTO.NetAutoPerformDTO ToDTO()
		=> new(
			whenJoiningNet.ToDTO(),
			whenJoiningChan.ToDTO(),
			whenOpeningUserChat.ToDTO()
		);
	#endregion

	#region Event Handlers
	#endregion
}
}