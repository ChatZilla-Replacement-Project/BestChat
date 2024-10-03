using System.Linq;

namespace BestChat.IRC.Data.Prefs
{
public class GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
	public GlobalAltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
		base(mgrParent, "Alternate Nicks", PrefsRsrcs.strGlobalAltNicksTitle, PrefsRsrcs
			.strGlobalAltNicksDesc)
		=> entries = new(this, "Entries", PrefsRsrcs
			.strGlobalAltNicksTitle, PrefsRsrcs.strGlobalAltNicksDesc, []);

	public GlobalAltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO<GlobalDtoType>
		.GlobalDTO.OneAltNickDTO[]? dto):
		base(mgrParent, "Alternate Nicks", PrefsRsrcs.strGlobalAltNicksTitle, PrefsRsrcs
			.strGlobalAltNicksDesc)
		=> entries = new(
			this,
			"Entries",
			PrefsRsrcs.strGlobalAltNicksTitle,
			PrefsRsrcs.strGlobalAltNicksDesc,
			[],
			dto?.Select(danickCur
				=> new OneAltNick(danickCur, this)
			) ?? []
		);
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	public interface IReadOnlyOneAltNick
	{
		string NickToUse
		{
			get;
		}
	}

	public class OneAltNick : Platform.DataAndExt.Obj<OneAltNick>, IReadOnlyOneAltNick, Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.
		IKeyChanged<OneAltNick, string>
	{
		#region Constructors & Deconstructors
		public OneAltNick(in string strNickToUse, in GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> parent)
		{
			this.strNickToUse = strNickToUse;

			this.parent = parent;
		}

		public OneAltNick(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO dto, in
			GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> parent) :
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

		public event Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<OneAltNick, string>.DKeyChanged? evtKeyChanged;
		#endregion

		#region Constants
		#endregion

		#region Helper Types
		#endregion

		#region Members
		public readonly GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> parent;

		private string strNickToUse;
		#endregion

		#region Properties
		public string NickToUse
		{
			get => strNickToUse;

			set
			{
				if(strNickToUse != value)
				{
					string strOldNickToUse = strNickToUse;

					strNickToUse = value;

					MakeDirty();

					FireCtntsChanged(strOldNickToUse);
				}
			}
		}
		#endregion

		#region Methods
		private void FireCtntsChanged(string strOldNickToUse)
		{
			FirePropChanged(nameof(NickToUse));

			evtNickToUseChanged?.Invoke(this, strOldNickToUse, strNickToUse);
			evtKeyChanged?.Invoke(this, strOldNickToUse, strNickToUse);
		}

		public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO ToDTO()
			=> new(guid, strNickToUse);
		#endregion

		#region Event Handlers
		#endregion
	}
	#endregion

	#region Members
	private readonly Platform.DataAndExt.Prefs.ReorderableListItem<OneAltNick> entries;
	#endregion

	#region Properties
	public Platform.DataAndExt.Prefs.ReorderableListItem<OneAltNick> Entries
		=> entries;
	#endregion

	#region Methods
	public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO[]? ToDTO()
		=> entries.Select(aliasCur
			=> aliasCur.ToDTO()
		).ToArray();
	#endregion

	#region Event Handlers
	#endregion
}
}