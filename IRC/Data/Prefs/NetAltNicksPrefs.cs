using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class NetAltNicksPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr, IAltNickPrefs
{
	#region Constructors & Deconstructors
		public NetAltNicksPrefs(NetPrefsBase mgrParent, in GlobalAltNicksPrefs inheritedSettings) :
			base(mgrParent, "Alternate Nicks", PrefsRsrcs.strNetAltNicksTitle, PrefsRsrcs.strNetAltNicksDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick,
				IReadOnlyOneAltNick>> defInheritedEntries = inheritedSettings.Entries
				.Select(ianick
					=> new NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick>(ianick));
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
							NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick> _, in string strOldCtnts, in string _)
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

		public NetAltNicksPrefs(NetPrefsBase mgrParent, DTO.NetAltNicksDTO dto, in GlobalAltNicksPrefs inheritedSettings) :
			base(mgrParent, "Alternate Nicks", PrefsRsrcs.strNetAltNicksTitle, PrefsRsrcs
				.strNetAltNicksDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;


			System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick>> defInheritedEntries = inheritedSettings
				.Entries.Select(ianickCur
					=> new NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick>(ianickCur));
			System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick>> enabledAltNicks = inheritedSettings.Entries
				.Select(ianickCur
					=> new NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick>(ianickCur, dto?.DisabledInheritedNicks?.Contains(ianickCur.guid)
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
							NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick> _, in string strOldCtnts, in string _)
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
					=> new GlobalAltNicksOneAltNick(danickCur, this))
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
		public new readonly NetPrefsBase mgrParent;

		public readonly GlobalAltNicksPrefs inheritedSettings;


		private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string,
			NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick>> mapAllInheritanceOverridesByNick;

		private readonly System.Collections.Generic.Dictionary<System.Action<string,
			NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick>>,
			NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick>.DFieldChanged<string>>
			mapOverrideHandlers = [];

		private readonly Platform.DataAndExt.Prefs.ReorderableObjListItem<GlobalAltNicksOneAltNick> additionalAltNicks;
	#endregion

	#region Properties
		public System.Collections.Generic.IReadOnlyDictionary<string,
			NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick>> AllInheritanceOverridesByNick
				=> mapAllInheritanceOverridesByNick;

		public Platform.DataAndExt.Prefs.ReorderableObjListItem<GlobalAltNicksOneAltNick> Entries
			=> additionalAltNicks;
	#endregion

	#region Methods
		public DTO.NetAltNicksDTO ToDTO()
			=> new(
				(
					from NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick, IReadOnlyOneAltNick> entryCur
						in mapAllInheritanceOverridesByNick.Values
					where !entryCur.Status
					select entryCur.inheritedItem.guid
				).ToArray(),
				mapAllInheritanceOverridesByNick.Values.Select(ianickCur
					=> new DTO.GlobalOneAltNickDTO(ianickCur.guid, ianickCur.InheritedItem.NickToUse)
				).ToArray()
			);

		private static string KeyObtainer(NetInheritedItemEnabledStatus<GlobalAltNicksOneAltNick,
			IReadOnlyOneAltNick> status)
			=> status.InheritedItem.NickToUse;

		public void ResetInherited()
		{
			mapAllInheritanceOverridesByNick.Clear();

			MakeDirty();
		}
	#endregion

	#region Event Handlers
	#endregion
}