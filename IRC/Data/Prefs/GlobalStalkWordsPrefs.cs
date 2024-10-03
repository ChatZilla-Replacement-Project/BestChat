using System.Linq;
using BestChat.IRC.Data.Prefs.DTO;

namespace BestChat.IRC.Data.Prefs
{
public class GlobalStalkWordsPrefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
	public GlobalStalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
		base(mgrParent, "Stalk Words", PrefsRsrcs.strGlobalStalkWordsTitle, PrefsRsrcs
			.strGlobalStalkWordsDesc)
		=> entries = new(
			this,
			"Entries",
			PrefsRsrcs.strGlobalStalkWordsTitle,
			PrefsRsrcs.strGlobalStalkWordsDesc,
			[],
			val
				=> val.Ctnts,
			(swEntry,
					evth)
				=> swEntry.evtCtntsChanged += mapStalkWordHandlers[evth] = (in OneStalkWord swEntry, in string
						strOldCtnts, in string _)
					=> evth(strOldCtnts, swEntry),
			(swEntry,
					evth)
				=>
			{
				swEntry.evtCtntsChanged -= mapStalkWordHandlers[evth];

				mapStalkWordHandlers.Remove(evth);
			}
		);

	public GlobalStalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO<GlobalDtoType>
		.GlobalDTO.OneStalkWordDTO[]? dto)
		: base(mgrParent, "Stalk Words", PrefsRsrcs.strGlobalStalkWordsTitle, PrefsRsrcs
			.strGlobalStalkWordsDesc)
		=> entries = new(
			this,
			"Entries",
			PrefsRsrcs.strGlobalStalkWordsTitle,
			PrefsRsrcs.strGlobalStalkWordsDesc,
			[],
			dto?.Select(dswCUr
				=> new OneStalkWord(dswCUr))
			?? [],
			val
				=> val.Ctnts,
			(swEntry,
					evth)
				=> swEntry.evtCtntsChanged += mapStalkWordHandlers[evth] = (in OneStalkWord swEntry, in string
						strOldCtnts, in string _)
					=> evth(strOldCtnts, swEntry),
			(swEntry,
					evth)
				=>
			{
				swEntry.evtCtntsChanged -= mapStalkWordHandlers[evth];

				mapStalkWordHandlers.Remove(evth);
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
	public interface IReadOnlyOneStalkWord
	{
		System.Guid GUID
		{
			get;
		}

		string Ctnts
		{
			get;
		}
	}

	public class OneStalkWord : Platform.DataAndExt.Obj<OneStalkWord>, IReadOnlyOneStalkWord, Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs
		.IKeyChanged<OneStalkWord, string>
	{
		#region Constructors & Deconstructors
		public OneStalkWord(in string strCtnts, System.Guid guid = default) :
			base(guid)
			=> this.strCtnts = strCtnts;

		public OneStalkWord(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO dto) :
			base(dto.GUID)
			=> strCtnts = dto.Ctnts;
		#endregion

		#region Delegates
		#endregion

		#region Events
		public event DFieldChanged<string>? evtCtntsChanged;

		public event Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<OneStalkWord, string>.DKeyChanged? evtKeyChanged;
		#endregion

		#region Constants
		#endregion

		#region Helper Types
		#endregion

		#region Members
		private string strCtnts;
		#endregion

		#region Properties
		public string Ctnts
		{
			get => strCtnts;

			set
			{
				if(strCtnts != value)
				{
					string strOldCtnts = strCtnts;

					strCtnts = value;

					MakeDirty();

					FireCtntsChanged(strOldCtnts);
				}
			}
		}
		#endregion

		#region Methods
		private void FireCtntsChanged(string strOldCtnts)
		{
			FirePropChanged(nameof(Ctnts));

			evtCtntsChanged?.Invoke(this, strOldCtnts, strCtnts);
			evtKeyChanged?.Invoke(this, strOldCtnts, strCtnts);
		}

		public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO ToDTO()
			=> new(
				guid,
				strCtnts
			);
		#endregion

		#region Event Handlers
		#endregion
	}
	#endregion

	#region Members
	private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneStalkWord> entries;

	private readonly System.Collections.Generic.Dictionary<System.Action<string, OneStalkWord>, Platform.DataAndExt.Obj<OneStalkWord>.DFieldChanged<string>> mapStalkWordHandlers = [];
	#endregion

	#region Properties
	public Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneStalkWord> Entries
		=> entries;
	#endregion

	#region Methods
	public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO[]? ToDTO()
		=> entries.Values.Select<, IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO>(swCur =>
	swCur.ToDTO()
	).ToArray();
	#endregion

	#region Event Handlers
	#endregion
}
}