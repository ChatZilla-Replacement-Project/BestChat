using System.Linq;
using BestChat.IRC.Data.Prefs.DTO;
using BestChat.Platform.DataAndExt.Ext;

namespace BestChat.IRC.Data.Prefs
{
public class NetPrefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : GlobalDTO
{
	#region Constructors & Deconstructors
	public NetPrefs(Prefs<GlobalPrefsType, GlobalDtoType> mgrParent, Defs.Net netOwner) :
		base(mgrParent, "IRC", PrefsRsrcs.strNetTitle, PrefsRsrcs.strNetTitle)
	{
		OwnerNet = netOwner;

		timeStamps = new(this);
		dcc = new(this);
		autoPeform = new(this);
		conn = new(this);
		aliases = new(this, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.Aliases);
		altNicks = new(this, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.AltNicks);
		notifyWhenOnline = new(this);
		stalkWords = new(this, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.StalkWords);
		knownChans = new(this);
	}

	public NetPrefs(Prefs<GlobalPrefsType, GlobalDtoType> mgrParent, NetDTO dto) :
		base(mgrParent, "IRC", PrefsRsrcs.strNetTitle, PrefsRsrcs.strNetTitle)
	{
		OwnerNet = (Defs.Net?)Defs.Net.AllInstancesByGUID[dto.OwnerNet] ?? throw new System.
			Exception($"While loading IRC preferences, found preferences for {dto.OwnerNet}, " +
				"but we can't find that network.");

		timeStamps = new(this, dto.TimeStamps);
		dcc = new(this, dto.DCC);
		autoPeform = new(this, dto.AutoPerform);
		conn = new(this, dto.Conn);
		aliases = new(this, dto.Aliases, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.Aliases);
		altNicks = new(this, dto.AltNicks, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.AltNicks);
		notifyWhenOnline = new(this, dto.NotifyWhenOnline);
		stalkWords = new(this, dto.StalkWords, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.StalkWords);
		knownChans = new(this, dto.KnownChans);
	}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	public interface IKeyChanged<InheritedType, KeyType>
		where InheritedType : IKeyChanged<InheritedType, string>
	{
		delegate void DKeyChanged(in InheritedType whatChanged, in KeyType oldKey, in KeyType newKey);

		event DKeyChanged evtKeyChanged;
	}

	public class InheritedItemEnabledStatus<InheritedType, ReadOnlyInterfaceType> : Platform.DataAndExt
		.Obj<InheritedItemEnabledStatus<InheritedType, ReadOnlyInterfaceType>>
		where InheritedType : IKeyChanged<InheritedType, string>, ReadOnlyInterfaceType
	{
		public InheritedItemEnabledStatus(InheritedType inheritedItem, bool bStatus = true)
		{
			this.inheritedItem = inheritedItem;
			this.bStatus = bStatus;

			this.inheritedItem.evtKeyChanged += (in InheritedType changed, in string strOldKey, in string strNewKey) =>
				evtKeyOfInheritedItemChanged?.Invoke(this, strOldKey, strNewKey);
		}

		public readonly InheritedType inheritedItem;
		private bool bStatus;

		public ReadOnlyInterfaceType InheritedItem => inheritedItem;

		public bool Status
		{
			get => bStatus;

			set
			{
				if(bStatus != value)
				{
					bStatus = value;

					MakeDirty();

					FireStatusChanged();
				}
			}
		}

		public event DFieldChanged<bool>? evtStatusChanged;

		public event DFieldChanged<string>? evtKeyOfInheritedItemChanged;

		private void FireStatusChanged()
		{
			FirePropChanged(nameof(Status));

			evtStatusChanged?.Invoke(this, !bStatus, bStatus);
		}

		public static implicit operator ReadOnlyInterfaceType(InheritedItemEnabledStatus<InheritedType,
			ReadOnlyInterfaceType> val)
			=> val.InheritedItem;
	}

	public class TimeStampPrefs : Platform.DataAndExt.Prefs.GlobalAppearanceTimeStampPrefs
	{
		#region Constructors & Deconstructors
		public TimeStampPrefs(NetPrefs mgrParent) :
			base(mgrParent)
			=> @override = new(mgrParent, "Override", PrefsRsrcs.strNetTimeStampOverrideNetTitle, PrefsRsrcs
				.strNetTimeStampOverrideNetDesc,false);

		public TimeStampPrefs(NetPrefs mgrParent, NetTimeStampDTO dto) :
			base(mgrParent, dto)
			=> @override = new(mgrParent, "Override", PrefsRsrcs.strNetTimeStampOverrideNetTitle, PrefsRsrcs
				.strNetTimeStampOverrideNetDesc, false, dto.Override);
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
		public override NetTimeStampDTO ToDTO()
			=> new(@override.CurVal, Show.CurVal, Fmt.CurVal);
		#endregion

		#region Event Handlers
		#endregion
	}

	public class DccPrefs : GlobalDccPrefs<GlobalPrefsType, GlobalDtoType>
	{
		#region Constructors & Deconstructors
		public DccPrefs(NetPrefs mgrParent) :
			base(mgrParent)
			=> @override = new(mgrParent, "Override", PrefsRsrcs
				.strNetDccOverrideTitle, PrefsRsrcs.strNetDccOverrideDesc, false);

		public DccPrefs(NetPrefs mgrParent, NetDccDTO dto) :
			base(mgrParent, dto)
			=> @override = new(mgrParent, "Override", PrefsRsrcs
				.strNetDccOverrideTitle, PrefsRsrcs.strNetDccOverrideDesc, false, dto.Override);
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
		public override NetDccDTO ToDTO()
			=> new(@override.CurVal, Enabled.CurVal, GetIpFromServer.CurVal, DownloadsFolder.CurVal,
				[.. Ports]);
		#endregion

		#region Event Handlers
		#endregion
	}

	public class AutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public AutoPerformPrefs(NetPrefs mgrParent) :
			base(mgrParent, "Auto-perform", PrefsRsrcs.strNetAutoPerformTitle, PrefsRsrcs
				.strNetAutoPerformDesc)
		{
			this.mgrParent = mgrParent;

			whenJoiningNet = new(this, "When joining this network", PrefsRsrcs
				.strNetAutoPerformWhenJoiningNetTitle, PrefsRsrcs.strNetAutoPerformWhenJoiningNetDesc, Prefs<GlobalPrefsType, GlobalDtoType>.instance!.Global.AutoPerform.WhenJoiningNet);
			whenJoiningChan = new(this, "When joining any channel on this network",
				PrefsRsrcs.strNetAutoPerformWhenJoiningChanTitle, PrefsRsrcs
					.strNetAutoPerformWhenJoiningChanDesc, Prefs<GlobalPrefsType, GlobalDtoType>.instance!.Global.AutoPerform.WhenJoiningChan);
			whenOpeningUserChat = new(this, "When opening chat with any user on this" +
				" network", PrefsRsrcs.strNetAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
					.strNetAutoPerformWhenOpeningUserChatDesc, Prefs<GlobalPrefsType, GlobalDtoType>.instance!.Global.AutoPerform
					.WhenOpeningUserChat);
		}

		public AutoPerformPrefs(NetPrefs mgrParent, NetAutoPerformDTO dto)
			: base(mgrParent, "Auto-perform", PrefsRsrcs.strNetAutoPerformTitle, PrefsRsrcs
				.strNetAutoPerformDesc)
		{
			this.mgrParent = mgrParent;

			whenJoiningNet = new(this, "When joining this network", PrefsRsrcs
					.strNetAutoPerformWhenJoiningNetTitle, PrefsRsrcs.strNetAutoPerformWhenJoiningNetDesc,
				dto.WhenJoiningNet, Prefs<GlobalPrefsType, GlobalDtoType>.instance!.Global.AutoPerform.WhenJoiningNet);
			whenJoiningChan = new(this, "When joining any channel on this network",
				PrefsRsrcs.strNetAutoPerformWhenJoiningChanTitle, PrefsRsrcs
					.strNetAutoPerformWhenJoiningChanDesc, dto.WhenJoiningChan, Prefs<GlobalPrefsType, GlobalDtoType>.instance!.Global
					.AutoPerform.WhenJoiningChan);
			whenOpeningUserChat = new(this, "When opening chat with any user on this" +
				" network", PrefsRsrcs.strNetAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
					.strNetAutoPerformWhenOpeningUserChatDesc, dto.WhenOpeningUserChat, Prefs<GlobalPrefsType, GlobalDtoType>.instance!.Global
					.AutoPerform.WhenOpeningUserChat);
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
			public OnEvtPrefs(AutoPerformPrefs mgrParent, in string strName, in string strLocalizedName, in string
				strLocalizedDesc, GlobalAutoPeformOnEvtPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings) :
				base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
			{
				this.mgrParent = mgrParent;
				this.inheritedSettings = inheritedSettings;

				System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>> enabledInheritedSteps =
					from stepCur in inheritedSettings.Steps
					select new InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>(stepCur);
				mapAllInheritanceOverrides = new(
					this,
					"Steps that are disabled",
					PrefsRsrcs.strNetAutoPerformOnEvtDisabledStepsTitle,
					PrefsRsrcs.strNetAutoPerformOnEvtDisabledStepsDesc,
					enabledInheritedSteps,
					KeyObtainer,
					(overrideEntry,
							evth) =>
						overrideEntry.inheritedItem.evtKeyChanged += mapInheritedStepsHandlers[evth] = (in GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType> stepEntry, in string strOldWhatToDo, in string _)
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

			public OnEvtPrefs(AutoPerformPrefs mgrParent, in string strName,
				in string strLocalizedName, in string strLocalizedDesc, NetAutoPeformOnEvtDTO dto, GlobalAutoPeformOnEvtPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings) :
				base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
			{
				this.mgrParent = mgrParent;
				this.inheritedSettings = inheritedSettings;


				System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>>
					defEnabledInheritedSteps =
						from stepCur in
							inheritedSettings.Steps
						select new InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>(stepCur);
				System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>> curEnabledInheritedSteps =
					from stepCur in
						inheritedSettings.Steps
					select new InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>(stepCur, !dto?
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
								in GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType> stepEntry, in string strOldWhatToDo, in string _)
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
							=> new GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>(dstepCur)));
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
			public new readonly AutoPerformPrefs mgrParent;

			public readonly GlobalAutoPeformOnEvtPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings;


			private readonly Platform.DataAndExt.Prefs.MappedListItem<string,
				InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>> mapAllInheritanceOverrides;

			private readonly Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>> additionalSteps;

			private readonly System.Collections.Generic.Dictionary<System.Action<string,
				InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>>, IKeyChanged<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, string>.DKeyChanged> mapInheritedStepsHandlers = [];
			#endregion

			#region Properties
			public Platform.DataAndExt.Prefs.MappedListItem<string, InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>>
				AllInheritanceOverrides
				=> mapAllInheritanceOverrides;


			public Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>>
				AdditionalSteps
				=> additionalSteps;
			#endregion

			#region Methods
			public NetAutoPeformOnEvtDTO ToDTO()
				=> new(
					(
						from InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>
							itemCur in mapAllInheritanceOverrides.Values
						where !itemCur.Status
						select itemCur.inheritedItem.guid
					).ToArray(),
					additionalSteps.Select(
						stepCur
							=> new GlobalAutoPerformOneStepDTO(stepCur.guid, stepCur.WhatToDo)
					).ToArray()
				);

			private static string KeyObtainer(InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep> item)
				=> item.InheritedItem.WhatToDo;

			private bool TestCurValForDef(System.Collections.Generic
				.IEnumerable<InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>> entries)
				=> mapAllInheritanceOverrides.Def
					.Any(entryCur
						=> !mapAllInheritanceOverrides[entryCur.InheritedItem.WhatToDo].Status) && false;

			private void ResetCurValToDef()
			{
				foreach(InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep> entryCur in mapAllInheritanceOverrides.Def)
					entryCur.Status = true;
			}
			#endregion

			#region Event Handlers
			#endregion
		}
		#endregion

		#region Members
		public new readonly NetPrefs mgrParent;

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
		public NetAutoPerformDTO ToDTO()
			=> new(
				whenJoiningNet.ToDTO(),
				whenJoiningChan.ToDTO(),
				whenOpeningUserChat.ToDTO()
			);
		#endregion

		#region Event Handlers
		#endregion
	}

	public class ConnPrefs : GlobalConnPrefs<GlobalPrefsType, GlobalDtoType>
	{
		#region Constructors & Deconstructors
		public ConnPrefs(NetPrefs mgrParent) :
			base(mgrParent)
			=> @override = new(mgrParent, "Override", PrefsRsrcs
				.strNetConnOverrideTitle, PrefsRsrcs.strNetConnOverrideDesc, false);

		public ConnPrefs(NetPrefs mgrParent, NetConnDTO dto) :
			base(mgrParent)
			=> @override = new(mgrParent, "Override", PrefsRsrcs
				.strNetConnOverrideTitle, PrefsRsrcs.strNetConnOverrideDesc, false, dto.Override);
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
		public override NetConnDTO ToDTO()
			=> new(
				@override.CurVal,
				EnableIdent.CurVal,
				AutoReconnect.CurVal,
				RejoinAfterKick.CurVal,
				CharEncoding.CurVal,
				UnlimitedAttempts.CurVal,
				MaxAttempts.CurVal,
				DefQuitMsg.CurVal
			);
		#endregion

		#region Event Handlers
		#endregion
	}

	public class AliasesPrefs : GlobalAliasesPrefs<GlobalPrefsType, GlobalDtoType>
	{
		#region Constructors & Deconstructors
		public AliasesPrefs(in NetPrefs mgrParent, in GlobalAliasesPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings, in string? strName = null, in string? strLocalizedName = null, in string?
			strLocalizedDesc = null) :
			base(mgrParent, strName ?? "Alias overrides for this network", strLocalizedName ?? PrefsRsrcs
				.strNetAliasTitle, strLocalizedDesc ?? PrefsRsrcs.strNetAliasDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>,
				IReadOnlyOneAlias>> defEnabledAliases = inheritedSettings.Entries.Values
				.Select(aliasCur
					=> new InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>(aliasCur)
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
							InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias> inherited, in string strOldName, in string _)
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
					=> aliasEntry.evtNameChanged += mapAddedAliasesHandlers[evth] = (in GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> aliasEntry, in string
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

		public AliasesPrefs(in NetPrefs mgrParent, NetAliasesDTO dto, in GlobalAliasesPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings, in string? strName = null, in string?
			strLocalizedName = null, in string? strLocalizedDesc = null) :
			base(mgrParent, strName ?? "Alias overrides for this network", strLocalizedName ?? PrefsRsrcs
				.strNetAliasTitle, strLocalizedDesc ?? PrefsRsrcs.strNetAliasDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;


			System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>,
				IReadOnlyOneAlias>> defEnabledAliases = inheritedSettings.Entries.Values
				.Select(aliasCur
					=> new InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>(aliasCur)
				);
			System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>,
				IReadOnlyOneAlias>> enabledAliases = inheritedSettings.Entries.Values
				.Select(aliasCur
					=> new InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>(aliasCur, dto?
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
							InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias> inherited, in string strOldName, in string _)
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
					=> new GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>(daliasCur, this))
				?? [],
				KeyObtainer,
				(aliasEntry,
						evth)
					=> aliasEntry.evtNameChanged += mapAddedAliasesHandlers[evth] = (
							in GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> aliasEntry, in string
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
		public new readonly NetPrefs mgrParent;

		public readonly GlobalAliasesPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings;


		private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>> mapAllInheritanceOverridesByName;

		private readonly System.Collections.Generic.Dictionary<System.Action<string,
			InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>>, InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>,IReadOnlyOneAlias>.DFieldChanged<string>> mapOverrideHandlers = [];

		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>> addedAliases;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>>, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>.DFieldChanged<string>> mapAddedAliasesHandlers = [];
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.MappedObjListItem<string, InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>> AllInheritanceOverridesByName
			=> mapAllInheritanceOverridesByName;

		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>> AddedAliases
			=> addedAliases;
		#endregion

		#region Methods
		public new NetAliasesDTO ToDTO()
			=> new(
				(
					from InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias> ialiasCur
						in mapAllInheritanceOverridesByName.Values
					where !ialiasCur.Status
					select ialiasCur.inheritedItem.guid
				)
				.ToArray(),
				addedAliases.Values.Select(
					aliasCur
						=> new GlobalAliasesOneAliasDTO(aliasCur.guid, aliasCur.Name, aliasCur.Cmd)
				).ToArray()
			);

		private string KeyObtainer(InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias> status)
			=> status.InheritedItem.Name;

		private string KeyObtainer(GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> aliasToLookUpKeyFor)
			=> aliasToLookUpKeyFor.Name;
		#endregion

		#region Event Handlers
		#endregion
	}

	public class AltNicksPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public AltNicksPrefs(NetPrefs mgrParent, in GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings) :
			base(mgrParent, "Alternate Nicks", PrefsRsrcs.strNetAltNicksTitle, PrefsRsrcs.strNetAltNicksDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>,
				IReadOnlyOneAltNick>> defInheritedEntries = inheritedSettings.Entries
				.Select(ianick
					=> new InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick>(ianick));
			mapAllInheritanceOverridesByNick = new(
				this,
				"Alternate nicks from global settings",
				PrefsRsrcs.strNetAltNicksInheritedTitle,
				PrefsRsrcs.strNetAltNicksInheritedDesc,
				defInheritedEntries,
				KeyObtainer,
				(inherited,
						evth)
					=> inherited.evtKeyOfInheritedItemChanged += mapOverrideHandlers[evth] = (in
							InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick> _, in string strOldCtnts, in string _)
						=> evth(strOldCtnts, inherited),
				(inherited,
						evth)
					=>
				{
					inherited.evtKeyOfInheritedItemChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			additionalAltNicks = new(this, "Lists more alternate nicks specific to " +
				"this network", PrefsRsrcs.strNetAltNicksAdditionalTitle, PrefsRsrcs.strNetAltNicksAdditionalDesc,
				[]);
		}

		public AltNicksPrefs(NetPrefs mgrParent, NetAltNicksDTO dto, in GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings) :
			base(mgrParent, "Alternate Nicks", PrefsRsrcs.strNetAltNicksTitle, PrefsRsrcs
				.strNetAltNicksDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;


			System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick>> defInheritedEntries = inheritedSettings
				.Entries.Select(ianickCur
					=> new InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick>(ianickCur));
			System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick>> enabledAltNicks = inheritedSettings.Entries
				.Select(ianickCur
					=> new InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick>(ianickCur, dto?.DisabledInheritedNicks?.Contains(ianickCur.guid)
						?? false)
				);
			mapAllInheritanceOverridesByNick = new(
				this,
				"Alternate nicks from global settings",
				PrefsRsrcs.strNetAltNicksInheritedTitle,
				PrefsRsrcs.strNetAltNicksInheritedDesc,
				defInheritedEntries,
				enabledAltNicks,
				KeyObtainer,
				(
						inherited,
						evth)
					=> inherited.evtKeyOfInheritedItemChanged += mapOverrideHandlers[evth] = (in
							InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick> _, in string strOldCtnts, in string _)
						=> evth(strOldCtnts, inherited),
				(
						inherited,
						evth)
					=>
				{
					inherited.evtKeyOfInheritedItemChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			additionalAltNicks = new(this, "Lists more alternate nicks specific to " +
				"this network", PrefsRsrcs.strNetAltNicksAdditionalTitle, PrefsRsrcs.strNetAltNicksAdditionalDesc,
				dto?.AddedNicks?.Select(danickCur
					=> new OneAltNick(danickCur, this))
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
		public class OneAltNick : Platform.DataAndExt.Obj<OneAltNick>, IReadOnlyOneAltNick
		{
			#region Constructors & Deconstructors
			public OneAltNick(in string strNickToUse, in AltNicksPrefs parent)
			{
				this.strNickToUse = strNickToUse;

				this.parent = parent;
			}

			public OneAltNick(in GlobalOneAltNickDTO dto, in AltNicksPrefs parent) :
				base(dto.GUID)
			{
				strNickToUse = dto.NickToUse;

				this.parent = parent;
			}
			#endregion

			#region Delegates
			#endregion

			#region Events
			public event DFieldChanged<string>? evtNickToUseChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
			#endregion

			#region Members
			public readonly AltNicksPrefs parent;

			private string strNickToUse;
			#endregion

			#region Properties
			public string NickToUse
			{
				get => strNickToUse;

				protected set
				{
					if(strNickToUse != value)
					{
						string strOldNickToUse = strNickToUse;

						strNickToUse = value;

						FireNickToUseChanged(strOldNickToUse);

						MakeDirty();
					}
				}
			}
			#endregion

			#region Methods
			private void FireNickToUseChanged(in string strOldName)
			{
				FirePropChanged(nameof(NickToUse));

				evtNickToUseChanged?.Invoke(this, strOldName, strNickToUse);
			}

			public GlobalOneAltNickDTO ToDTO()
				=> new(guid, strNickToUse);
			#endregion

			#region Event Handlers
			#endregion
		}
		#endregion

		#region Members
		public new readonly NetPrefs mgrParent;

		public readonly GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings;


		private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string,
			InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick>> mapAllInheritanceOverridesByNick;

		private readonly System.Collections.Generic.Dictionary<System.Action<string,
			InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick>>, InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick>.DFieldChanged<string>> mapOverrideHandlers = [];

		private readonly Platform.DataAndExt.Prefs.ReorderableObjListItem<OneAltNick> additionalAltNicks;
		#endregion

		#region Properties
		public System.Collections.Generic.IReadOnlyDictionary<string, InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick>> AllInheritanceOverridesByNick
			=> mapAllInheritanceOverridesByNick;

		public Platform.DataAndExt.Prefs.ReorderableObjListItem<OneAltNick> Entries
			=> additionalAltNicks;
		#endregion

		#region Methods
		public NetAltNicksDTO ToDTO()
			=> new(
				(
					from InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAltNick> entryCur
						in mapAllInheritanceOverridesByNick.Values
					where !entryCur.Status
					select entryCur.inheritedItem.guid
				).ToArray(),
				mapAllInheritanceOverridesByNick.Values.Select(ianickCur
					=> new GlobalOneAltNickDTO(ianickCur.guid, ianickCur.InheritedItem.NickToUse)
				).ToArray()
			);

		private static string KeyObtainer(InheritedItemEnabledStatus<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>,
			IReadOnlyOneAltNick> status)
			=> status.InheritedItem.NickToUse;
		#endregion

		#region Event Handlers
		#endregion
	}

	public class StalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public StalkWordsPrefs(NetPrefs mgrParent, GlobalStalkWordsPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings) :
			base(mgrParent, "Stalk words", PrefsRsrcs.strNetStalkWordsTitle, PrefsRsrcs
				.strNetStalkWordsDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>> defInheritedEntries =
				inheritedSettings.Entries.Values.Select(iswCur
					=> new InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>(iswCur));
			mapAllInheritanceOverridesByCtnts = new(
				this,
				"Stalk words from global settings",
				PrefsRsrcs.strNetStalkWordsInheritedTitle,
				PrefsRsrcs.strNetStalkWordsInheritedDesc,
				defInheritedEntries,
				KeyObtainer,
				(inherited,
						evth)
					=> inherited.evtKeyOfInheritedItemChanged += mapOverrideHandlers[evth] = (in
							InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord> inherited, in string strOldCtnts, in string _)
						=> evth(strOldCtnts, inherited),
				(inherited,
						evth)
					=>
				{
					inherited.evtKeyOfInheritedItemChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			addedStalkWords = new(
				this,
				"Lists more stalk words specific to this network",
				PrefsRsrcs.strNetStalkWordsAdditionalTitle,
				PrefsRsrcs.strNetStalkWordsAdditionalDesc,
				[],
				KeyObtainer,
				(swEntry,
						evth)
					=> swEntry.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType> swEntry, in string strNewCtnts, in string _)
						=> evth(strNewCtnts, swEntry),
				(swEntry,
						evth)
					=>
				{
					swEntry.evtCtntsChanged -= mapAddedStalkWordHandlers[evth];

					mapAddedStalkWordHandlers.Remove(evth);
				}
			);
		}

		public StalkWordsPrefs(NetPrefs mgrParent, NetStalkWordsDTO dto,
			GlobalStalkWordsPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings) :
			base(mgrParent, "Stalk words", PrefsRsrcs.strNetStalkWordsTitle, PrefsRsrcs
				.strNetStalkWordsDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;


			System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>> defInheritedEntries =
				inheritedSettings.Entries.Values.Select(iswCur
					=> new InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>(iswCur));
			System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>> enabledAltNicks = inheritedSettings
				.Entries.Values.Select(iswCur
					=> new InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>(iswCur, dto?.DisabledInheritedStalkWords?
						.Contains(iswCur.guid) ?? false)
				);
			mapAllInheritanceOverridesByCtnts = new(
				this,
				"Stalk words from global settings",
				PrefsRsrcs.strNetStalkWordsInheritedTitle,
				PrefsRsrcs.strNetStalkWordsInheritedDesc,
				defInheritedEntries,
				enabledAltNicks,
				KeyObtainer,
				(
						inherited,
						evth)
					=> inherited.evtKeyOfInheritedItemChanged += mapOverrideHandlers[evth] = (in
							InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord> inherited, in string strOldCtnts, in string _)
						=> evth(strOldCtnts, inherited),
				(inherited, evth)
					=>
				{
					inherited.evtKeyOfInheritedItemChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			addedStalkWords = new(
				this,
				"Lists more stalk words specific to this network",
				PrefsRsrcs.strNetStalkWordsAdditionalTitle,
				PrefsRsrcs.strNetStalkWordsAdditionalDesc,
				[],
				dto?.AddedStalkWords?.Select(dswCur
					=> new GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>(dswCur)
				) ?? [],
				KeyObtainer,
				(
						swEntry,
						evth)
					=> swEntry.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType> swEntry, in string strNewCtnts, in string _)
						=> evth(strNewCtnts, swEntry),
				(swEntry,
						evth)
					=>
				{
					swEntry.evtCtntsChanged -= mapAddedStalkWordHandlers[evth];

					mapAddedStalkWordHandlers.Remove(evth);
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
		public new readonly NetPrefs mgrParent;

		public readonly GlobalStalkWordsPrefs<GlobalPrefsType, GlobalDtoType> inheritedSettings;


		private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string,
			InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>> mapAllInheritanceOverridesByCtnts;

		private readonly System.Collections.Generic.Dictionary<System.Action<string,
			InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>>, InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>.DFieldChanged<string>> mapOverrideHandlers = [];

		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>> addedStalkWords;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>>, GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>.DFieldChanged<string>> mapAddedStalkWordHandlers
			= [];
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.MappedObjListItem<string, InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>> AllInheritanceOverridesByCtnts
			=> mapAllInheritanceOverridesByCtnts;

		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>>
			AddedStalkWords
			=> addedStalkWords;
		#endregion

		#region Methods
		public NetStalkWordsDTO ToDTO()
			=> new(
				(
					from InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord> iswCur
						in mapAllInheritanceOverridesByCtnts.Values
					where !iswCur.Status
					select iswCur.inheritedItem.guid
				).ToArray(),
				addedStalkWords.Values.Select(swCur
					=> new GlobalStalkWordsOneStalkWordDTO(swCur.guid, swCur.Ctnts)
				).ToArray()
			);

		private string KeyObtainer(InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord> iswCur)
			=> iswCur.InheritedItem.Ctnts;

		private string KeyObtainer(GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType> swCur)
			=> swCur.Ctnts;
		#endregion

		#region Event Handlers
		#endregion
	}

	public class NotifyWhenOnlinePrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public NotifyWhenOnlinePrefs(in NetPrefs mgrParent) :
			base(mgrParent, "Notify When Online", PrefsRsrcs.strNetNotifyWhenOnlineTitle,
				PrefsRsrcs.strNetNotifyWhenOnlineDesc)
			=> entries = new(
				this,
				"Notify when online",
				PrefsRsrcs.strNetNotifyTitle,
				PrefsRsrcs.strNetNotifyDesc,
				[],
				KeyObtainer,
				(_, _)
					=>
				{
				},
				(_, _)
					=>
				{
				}
			);

		public NotifyWhenOnlinePrefs(in NetPrefs mgrParent, in string[]? dto) :
			base(mgrParent, "Notify When Online", PrefsRsrcs.strNetNotifyWhenOnlineTitle,
				PrefsRsrcs.strNetNotifyWhenOnlineDesc)
			=> entries = new(
				this,
				"Notify when online",
				PrefsRsrcs.strNetNotifyTitle,
				PrefsRsrcs.strNetNotifyDesc,
				[],
				dto ?? [],
				KeyObtainer,
				(_, _)
					=>
				{
				},
				(_, _)
					=>
				{
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
		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, string> entries;
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, string> Entries
			=> entries;
		#endregion

		#region Methods
		public string[]? ToDTO()
			=> [.. entries.Values, ];

		private static string KeyObtainer(string strVal)
			=> strVal;
		#endregion

		#region Event Handlers
		#endregion
	}

	public class ChanPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public ChanPrefs(ChanListPrefs mgrParent, Chan chanOwner) :
			base(mgrParent, "Preferences for one channel", PrefsRsrcs.strNetChanTitle,
				PrefsRsrcs.strNetChanDesc)
		{
			OwnerChan = chanOwner;

			timeStamps = new(this);
			aliases = new(this, mgrParent.mgrParent.aliases);
			autoPeform = new(this, mgrParent.mgrParent.autoPeform);
			stalkWords = new(this, mgrParent.mgrParent.stalkWords);
		}

		public ChanPrefs(ChanListPrefs mgrParent, ChanDTO dto) :
			base(mgrParent, "Preferences for one channel", PrefsRsrcs.strNetChanTitle,
				PrefsRsrcs.strNetChanDesc)
		{
			if(!Chan.AllChanByName.ContainsKey(dto.OwnerChan))
				throw new System.InvalidProgramException("We have a preference for a channel we can't find " +
					"in the network definition.  Did something load out of order?");
			OwnerChan = Chan.AllChanByName[dto.OwnerChan];

			timeStamps = new(this, dto.TimeStamps);
			aliases = new(this, dto.Aliases, mgrParent.mgrParent.aliases);
			autoPeform = new(this, dto.AutoPerform, mgrParent.mgrParent.autoPeform);
			stalkWords = new(this, dto.StalkWords, mgrParent.mgrParent.stalkWords);
		}
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
		#endregion

		#region Helper Types
		public class InheritedItemEnabledStatus<InheritedType, ReadOnlyInterfaceType>(InheritedType inheritedItem,
			bool bStatus, InheritedItemEnabledStatus<InheritedType, ReadOnlyInterfaceType>.InheritedFromTypes ifSrc,
			string strDescOfInheritedType, Defs.Net net)
			: NetPrefs.InheritedItemEnabledStatus<InheritedType, ReadOnlyInterfaceType>(inheritedItem, bStatus)
			where InheritedType : ReadOnlyInterfaceType, IKeyChanged<InheritedType, string>
		{
			public enum InheritedFromTypes : byte
			{
				network,
				global,
			}

			public InheritedFromTypes InheritedFromSrc => ifSrc;

			public string InheritedFromAsText => ifSrc switch
			{
				InheritedFromTypes.network
					=> PrefsRsrcs.strNetChanInheritedFromNetText,
				InheritedFromTypes.global
					=> PrefsRsrcs.strNetChanInheritedFromGlobalText,
				var _
					=> throw new Platform.DataAndExt.Exceptions
						.UnknownOrInvalidEnumException<InheritedFromTypes>(ifSrc, "While returning a textual " +
							"description of the source of this inherited item"),
			};

			public string InheritedFromAsTextDesc => ifSrc switch
			{
				InheritedFromTypes.network
					=> PrefsRsrcs.strNetChanInheritedFromNetDesc.Fmt(strDescOfInheritedType, net.Name),
				InheritedFromTypes.global
					=> PrefsRsrcs.strNetChanInheritedFromGlobalText.Fmt(strDescOfInheritedType),
				var _
					=> throw new Platform.DataAndExt.Exceptions
						.UnknownOrInvalidEnumException<InheritedFromTypes>(ifSrc, "While returning a textual " +
							"description of the source of this inherited item"),
			};
		}

		public class TimeStampPrefs : Platform.DataAndExt.Prefs.GlobalAppearanceTimeStampPrefs
		{
			#region Constructors & Deconstructors
			public TimeStampPrefs(ChanPrefs mgrParent) :
				base(mgrParent)
				=> @override = new(mgrParent, "Override", PrefsRsrcs
						.strNetTimeStampOverrideNetTitle, PrefsRsrcs.strNetTimeStampOverrideNetDesc,
					false);

			public TimeStampPrefs(ChanPrefs mgrParent, NetTimeStampDTO dto) :
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
			public override NetTimeStampDTO ToDTO()
				=> new(@override.CurVal, Show.CurVal, Fmt.CurVal);
			#endregion

			#region Event Handlers
			#endregion
		}

		public class AliasesPrefs : GlobalAliasesPrefs<GlobalPrefsType, GlobalDtoType>
		{
			#region Constructors & Deconstructors
			public AliasesPrefs(ChanPrefs mgrParent, NetPrefs.AliasesPrefs inheritedSettings)
				: base(mgrParent, "Alias overrides for this network", PrefsRsrcs .strNetAliasTitle,
					PrefsRsrcs.strNetAliasDesc)
			{
				this.inheritedSettings = inheritedSettings;

				System.Collections.Generic.List<InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>>
					listDefEnabledAliases = new(inheritedSettings.AllInheritanceOverridesByName.Count +
						inheritedSettings.AddedAliases.Count);
				listDefEnabledAliases.AddRange(inheritedSettings.AllInheritanceOverridesByName.Values
					.Select(ialiasCur
						=> new InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>(ialiasCur.inheritedItem, ialiasCur
							.Status,InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>.InheritedFromTypes
							.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)));
				listDefEnabledAliases.AddRange(inheritedSettings.AddedAliases.Values.Select(aliasCur
					=> new InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>(aliasCur, true,
						InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>.InheritedFromTypes.network,
						PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
				foreach(InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias> ialiasCur in listDefEnabledAliases)
					ialiasCur.evtDirtyChanged += (
							in NetPrefs.InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias> ialiasSender,
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
								in GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> inheritedAlias,
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
								in GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> aliasSender, in string
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

			public AliasesPrefs(ChanPrefs mgrParent, NetAliasesDTO dto, NetPrefs
				.AliasesPrefs inheritedSettings) :
				base(mgrParent, "Alias overrides for this network", PrefsRsrcs
					.strNetAliasTitle, PrefsRsrcs.strNetAliasDesc)
			{
				this.inheritedSettings = inheritedSettings;


				System.Collections.Generic.List<InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>,
					IReadOnlyOneAlias>> listDefEnabledAliases = new(inheritedSettings
					.AllInheritanceOverridesByName.Count + inheritedSettings.AddedAliases.Count);
				listDefEnabledAliases.AddRange(inheritedSettings.AllInheritanceOverridesByName.Values
					.Select(ialiasCur
						=> new InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>(ialiasCur.inheritedItem, ialiasCur.Status,
							InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText,
							inheritedSettings.mgrParent.OwnerNet)));
				listDefEnabledAliases.AddRange(inheritedSettings.AddedAliases.Values.Select(aliasCur
					=> new InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>(aliasCur, true, InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>.InheritedFromTypes
						.network, PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
				System.Collections.Generic.List<InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>,
					IReadOnlyOneAlias>> listEnabledInheritedAliases = [.. inheritedSettings
					.AllInheritanceOverridesByName.Values.Select(ialias
						=> new InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>(ialias.inheritedItem, !dto?.DisabledInheritedAliases?
								.Contains(ialias.inheritedItem.guid) ?? ialias.Status,
							InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings
								.mgrParent.OwnerNet)), ];
				listEnabledInheritedAliases.AddRange(inheritedSettings.AddedAliases.Values.Select(ialiasCur
					=> new InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>(ialiasCur, !dto?.DisabledInheritedAliases?.Contains(ialiasCur.guid)
						?? true, InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>.InheritedFromTypes.network, PrefsRsrcs.strAliasText,
						inheritedSettings.mgrParent.OwnerNet)));
				foreach(InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias> ialiasCur in listDefEnabledAliases)
					ialiasCur.evtDirtyChanged += (in NetPrefs.InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>
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
						=> inherited.inheritedItem.evtNameChanged += mapOverrideHandlers[evth] = (in GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> inheritedAlias,
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
						=> new GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>(daliasCur, this)
					) ?? [],
					KeyObtainer,
					(inherited,
							evth)
						=> inherited.evtNameChanged += mapAddedAliasesHandlers[evth] = (in GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> aliasSender, in string
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
			public readonly NetPrefs.AliasesPrefs inheritedSettings;


			private readonly Platform.DataAndExt.Prefs.MappedListItem<string, InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>,
				IReadOnlyOneAlias>> mapAllInheritanceOverridesByName;

			private readonly System.Collections.Generic.Dictionary<System.Action<string,
					InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias>>, Platform.DataAndExt.Obj<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>>
				.DFieldChanged<string>> mapOverrideHandlers = [];

			private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>> addedAliases;

			private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>>, Platform.DataAndExt.Obj<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>>.DFieldChanged<string>> mapAddedAliasesHandlers = [];
			#endregion

			#region Properties
			public Platform.DataAndExt.Prefs.MappedListItem<string, InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>,
				IReadOnlyOneAlias>> AllInheritanceOverridesByName
				=> mapAllInheritanceOverridesByName;


			public Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>> AddedAliases
				=> addedAliases;
			#endregion

			#region Methods
			public new NetAliasesDTO ToDTO()
				=> new(
					(
						from InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias> ialiasCur in
							mapAllInheritanceOverridesByName.Values
						where !ialiasCur.Status
						select ialiasCur.InheritedItem.GUID
					).ToArray(),
					addedAliases.Values.Select(aliasCur
						=> new GlobalAliasesOneAliasDTO(aliasCur.guid, aliasCur.Name, aliasCur.Cmd)
					).ToArray());

			private static string KeyObtainer(InheritedItemEnabledStatus<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneAlias> aliasCur)
				=> aliasCur.InheritedItem.Name;

			private static string KeyObtainer(GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> aliasCur)
				=> aliasCur.Name;
			#endregion

			#region Event Handlers
			#endregion
		}

		public class AutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
			public AutoPerformPrefs(ChanPrefs mgrParent, NetPrefs.AutoPerformPrefs inheritedSettings) :
				base(mgrParent, "Steps to take when joining this channel", PrefsRsrcs
					.strNetChanAutoPerformTitle, PrefsRsrcs.strNetChanAutoPerformDesc)
			{
				this.inheritedSettings = inheritedSettings;

				System.Collections.Generic.List<InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>>
					listDefEnabledAliases = new(this.inheritedSettings.WhenJoiningChan.AllInheritanceOverrides
						.Count + inheritedSettings.WhenJoiningChan.AdditionalSteps.Count);
				listDefEnabledAliases.AddRange(inheritedSettings.WhenJoiningChan.AllInheritanceOverrides.Values.Select(istep
					=> new InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>(istep.inheritedItem, istep.Status,
						InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>.InheritedFromTypes.global, PrefsRsrcs
							.strOneStepText, inheritedSettings.mgrParent.OwnerNet)
				));
				listDefEnabledAliases.AddRange(inheritedSettings.WhenJoiningChan.AdditionalSteps
					.Select(istepCur
						=> new InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>(istepCur, true, InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>.InheritedFromTypes.network, PrefsRsrcs
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
						=> inherited.inheritedItem.evtWhatToDoChanged += mapOverrideHandlers[evth] = (in GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType> _, in string strNewVal, in string _)
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
				NetPrefs.AutoPerformPrefs inheritedSettings) :
				base(mgrParent, "Steps to take when joining this channel", PrefsRsrcs
					.strNetChanAutoPerformTitle, PrefsRsrcs.strNetChanAutoPerformDesc)
			{
				this.inheritedSettings = inheritedSettings;

				System.Collections.Generic.List<InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>>
					listDefEnabledAliases = new(
						this.inheritedSettings.WhenJoiningChan.AllInheritanceOverrides.Count + inheritedSettings
							.WhenJoiningChan.AdditionalSteps.Count);
				listDefEnabledAliases.AddRange(inheritedSettings.WhenJoiningChan.AllInheritanceOverrides
					.Values.Select(istep
						=> new InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>(istep.inheritedItem, istep.Status,
							InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>.InheritedFromTypes.global, PrefsRsrcs
								.strOneStepText, inheritedSettings.mgrParent.OwnerNet)
					));
				listDefEnabledAliases.AddRange(inheritedSettings.WhenJoiningChan.AdditionalSteps.Select(
					istepCur
						=> new InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>(istepCur, true,
							InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>,
								IReadOnlyOneStep>.InheritedFromTypes.network,
							PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
				System.Collections.Generic.List<InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>> listEnabledInheritedSteps =
					inheritedSettings.WhenJoiningChan.AllInheritanceOverrides.Values.Select(istepCur
						=> new InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>(istepCur.inheritedItem, !dto?.DisabledInheritedSteps?.Contains(istepCur
								.inheritedItem.guid) ?? istepCur.Status, InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>.InheritedFromTypes.global,
							PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)
					).ToList();
				foreach(InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep> istepCur in listEnabledInheritedSteps)
					istepCur.evtDirtyChanged += (in NetPrefs.InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep> _, in bool
							bNowDirty)
						=>
					{
						if(bNowDirty)
							MakeDirty();
					};
				listEnabledInheritedSteps.AddRange(inheritedSettings.WhenJoiningChan.AdditionalSteps.Select(istepCur
					=> new InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>(istepCur, !dto?.DisabledInheritedSteps?
						.Contains(istepCur.guid) ?? true, InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>
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
						=> inherited.inheritedItem.evtWhatToDoChanged += mapOverrideHandlers[evth] = (in GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType> _, in string strNewVal, in string _)
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
						=> new GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>(dstepCur))
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
			public readonly NetPrefs.AutoPerformPrefs inheritedSettings;


			private readonly Platform.DataAndExt.Prefs.MappedListItem<string,
				InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>> mapAllInheritanceOverrides;

			private readonly System.Collections.Generic.Dictionary<System.Action<string,
				InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>>, GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>.DFieldChanged<string>> mapOverrideHandlers = [];

			private readonly Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>> addedSteps;

			private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>>, GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>.DFieldChanged<string>> mapAddedAliasesHandlers = [];
			#endregion

			#region Properties
			public Platform.DataAndExt.Prefs.MappedListItem<string, InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep>>
				AllInheritanceOverrides
				=> mapAllInheritanceOverrides;

			public Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>> AddedSteps
				=> addedSteps;
			#endregion

			#region Methods
			public ChanAutoPerformDTO ToDTO()
				=> new(
					(
						from InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep> istepCur in mapAllInheritanceOverrides.Values
						where !istepCur.Status
						select istepCur.InheritedItem.GUID
					).ToArray(),
					(
						from GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType> stepCur in addedSteps
						select new GlobalAutoPerformOneStepDTO(stepCur.GUID, stepCur.WhatToDo)
					).ToArray()
				);

			private static string KeyObtainer(InheritedItemEnabledStatus<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStep> istepCur)
				=> istepCur.InheritedItem.WhatToDo;
			#endregion

			#region Event Handlers
			#endregion
		}

		public class StalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
			public StalkWordsPrefs(ChanPrefs mgrParent, NetPrefs.StalkWordsPrefs inheritedSettings) :
				base(mgrParent, "Stalk words for this channel", PrefsRsrcs
					.strNetChanStalkWordsInheritedTitle, PrefsRsrcs.strNetChanStalkWordsInheritedDesc)
			{
				this.inheritedSettings = inheritedSettings;

				System.Collections.Generic.List<InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>,
					IReadOnlyOneStalkWord>> listDefEnabledAliases = inheritedSettings
					.AllInheritanceOverridesByCtnts.Values.Select(iswCur
						=> new InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>(iswCur.inheritedItem, iswCur.Status,
							InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>
								.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent
								.OwnerNet)
					).ToList();
				listDefEnabledAliases.AddRange(inheritedSettings.AddedStalkWords.Values.Select(iswCur
					=> new InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>(iswCur, true,
						InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>.InheritedFromTypes.network, PrefsRsrcs.strAliasText,
						inheritedSettings.mgrParent.OwnerNet)));
				mapAllInheritanceOverridesByName = new(
					this,
					"Inherited aliases",
					PrefsRsrcs.strNetChanAutoPerformInheritedTitle,
					PrefsRsrcs.strNetChanAutoPerformInheritedDesc,
					listDefEnabledAliases,
					KeyObtainer,
					(inherited, evth)
						=> inherited.inheritedItem.evtCtntsChanged += mapOverrideHandlers[evth] = (in GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType> _, in string strOldVal, in string _)
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
						=> inherited.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType> _, in string strOldVal, in string _)
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

			public StalkWordsPrefs(ChanPrefs mgrParent, NetStalkWordsDTO dto, NetPrefs
				.StalkWordsPrefs inheritedSettings) :
				base(mgrParent, "Stalk words for this channel", PrefsRsrcs
					.strNetChanStalkWordsInheritedTitle, PrefsRsrcs.strNetChanStalkWordsInheritedDesc)
			{
				this.inheritedSettings = inheritedSettings;

				System.Collections.Generic.List<InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>,
					IReadOnlyOneStalkWord>> listDefEnabledStalkWords = inheritedSettings
					.AllInheritanceOverridesByCtnts.Values.Select(iswCur
						=> new InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>(iswCur.inheritedItem, iswCur.Status,
							InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>.InheritedFromTypes.global, PrefsRsrcs
								.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)
					).ToList();
				listDefEnabledStalkWords.AddRange(inheritedSettings.AddedStalkWords.Values.Select(iswCur
					=> new InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>(iswCur, true,
						InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>.InheritedFromTypes.network, PrefsRsrcs.strAliasText,
						inheritedSettings.mgrParent.OwnerNet)));
				System.Collections.Generic.List<InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>,
					IReadOnlyOneStalkWord>> listEnabledInheritedStalkWords =
					inheritedSettings.AllInheritanceOverridesByCtnts.Values.Select(iswCur
						=> new InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>(iswCur.inheritedItem, !dto?
								.DisabledInheritedStalkWords?.Contains(iswCur.inheritedItem.guid) ?? iswCur.Status,
							InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText,
							inheritedSettings.mgrParent.OwnerNet)
					).ToList();
				foreach(InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord> ialiasCur in listEnabledInheritedStalkWords)
					ialiasCur.evtDirtyChanged += (in NetPrefs.InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord> _, in bool bNowDirty)
						=>
					{
						if(bNowDirty)
							MakeDirty();
					};
				listEnabledInheritedStalkWords.AddRange(inheritedSettings.AddedStalkWords.Values
					.Select(iswCur
						=> new InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>(iswCur, !dto?.DisabledInheritedStalkWords?
								.Contains(iswCur.guid) ?? true, InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>.InheritedFromTypes.network,
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
						=> inherited.inheritedItem.evtCtntsChanged += mapOverrideHandlers[evth] = (in GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType> _, in string strOldVal, in string _)
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
						=> inherited.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType> _, in string strOldVal, in string _)
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
			public readonly NetPrefs.StalkWordsPrefs inheritedSettings;


			private readonly Platform.DataAndExt.Prefs.MappedListItem<string, InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>>
				mapAllInheritanceOverridesByName;

			private readonly System.Collections.Generic.Dictionary<System.Action<string,
					InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>>, GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>.DFieldChanged<string>>
				mapOverrideHandlers = [];

			private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>> addedStalkWords;

			private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>>, GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>.DFieldChanged<string>>
				mapAddedStalkWordHandlers = [];
			#endregion

			#region Properties
			public Platform.DataAndExt.Prefs.MappedListItem<string, InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>, IReadOnlyOneStalkWord>>
				AllInheritanceOverridesByName
				=> mapAllInheritanceOverridesByName;


			public Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>>
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

			private static string KeyObtainer(InheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType>,
				IReadOnlyOneStalkWord> iswCur)
				=> iswCur.InheritedItem.Ctnts;

			private static string KeyObtainer(GlobalStalkWordsOneStalkWord<GlobalPrefsType, GlobalDtoType> swCur)
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

	public class ChanListPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public ChanListPrefs(NetPrefs mgrParent) :
			base(mgrParent, "Known Channels", PrefsRsrcs.strNetKnownChanTitle, PrefsRsrcs
				.strNetKnownChanDesc)
			=> this.mgrParent = mgrParent;

		public ChanListPrefs(NetPrefs mgrParent, ChanDTO[]? dto) :
			base(mgrParent, "Known Channels", PrefsRsrcs.strNetKnownChanTitle, PrefsRsrcs
				.strNetKnownChanDesc)
		{
			this.mgrParent = mgrParent;

			if(dto != null)
				foreach(ChanDTO dchanCur in dto)
					RegisterChan(Chan.AllChanByName[dchanCur.OwnerChan]);
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
		public new readonly NetPrefs mgrParent;

		public System.Collections.Generic.Dictionary<Chan, ChanPrefs> mapAllChanPrefsByChan = [];
		#endregion

		#region Properties
		#endregion

		#region Methods
		public void RegisterChan(in Chan chanToBeRegistered)
		{
			ChanPrefs pchanNew = new(this, chanToBeRegistered);

			Add(pchanNew);

			mapAllChanPrefsByChan[chanToBeRegistered] = pchanNew;
		}

		public void RemovePrefsForChan(in Chan chanToRemovePrefsFor)
		{
			RemoveChildMgr(mapAllChanPrefsByChan[chanToRemovePrefsFor]);

			mapAllChanPrefsByChan.Remove(chanToRemovePrefsFor);
		}

		public ChanDTO[]? ToDTO()
			=> mapAllChanPrefsByChan.Values.IsEmpty()
				? []
				: mapAllChanPrefsByChan.Values.Select(pchanCur
					=> pchanCur.ToDTO()
				).ToArray();
		#endregion

		#region Event Handlers
		#endregion
	}
	#endregion

	#region Members
	private readonly TimeStampPrefs timeStamps;

	private readonly DccPrefs dcc;

	private readonly AutoPerformPrefs autoPeform;

	private readonly ConnPrefs conn;

	private readonly AliasesPrefs aliases;

	private readonly AltNicksPrefs altNicks;

	private readonly StalkWordsPrefs stalkWords;

	private readonly NotifyWhenOnlinePrefs notifyWhenOnline;

	private readonly ChanListPrefs knownChans;
	#endregion

	#region Properties
	public Defs.Net OwnerNet
	{
		get;

		private init;
	}


	public TimeStampPrefs TimeStamps
		=> timeStamps;

	public DccPrefs DCC
		=> dcc;

	public AutoPerformPrefs AutoPerform
		=> autoPeform;

	public ConnPrefs Conn
		=> conn;

	public AliasesPrefs Aliases
		=> aliases;

	public AltNicksPrefs AltNicks
		=> altNicks;

	public StalkWordsPrefs StalkWords
		=> stalkWords;

	public NotifyWhenOnlinePrefs NotifyWhenOnline
		=> notifyWhenOnline;

	public ChanListPrefs KnownChans
		=> knownChans;

	public override bool CanBeRemoved
		=> true;
	#endregion

	#region Methods
	public NetDTO ToDTO()
		=> new(OwnerNet.guid, timeStamps.ToDTO(), dcc.ToDTO(), autoPeform.ToDTO(), conn
			.ToDTO(), aliases.ToDTO(), altNicks.ToDTO(), stalkWords.ToDTO(), notifyWhenOnline
			.ToDTO(), knownChans.ToDTO());
	#endregion

	#region Event Handlers
	#endregion
}
}