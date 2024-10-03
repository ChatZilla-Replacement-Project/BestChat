using BestChat.IRC.Data.Prefs.DTO;

namespace BestChat.IRC.Data.Prefs;

public class GlobalAltNicksOneAltNick : Platform.DataAndExt.Obj<GlobalAltNicksOneAltNick>, IReadOnlyOneAltNick,
	IKeyChanged<GlobalAltNicksOneAltNick, string>
{
	#region Constructors & Deconstructors
		public GlobalAltNicksOneAltNick(in GlobalAltNicksPrefs mgrParent)
		{
			strNickToUse = "";

			this.mgrParent = mgrParent;
		}

		public GlobalAltNicksOneAltNick(in string strNickToUse, in GlobalAltNicksPrefs mgrParent)
		{
			this.strNickToUse = strNickToUse;

			this.mgrParent = mgrParent;
		}

		public GlobalAltNicksOneAltNick(in GlobalOneAltNickDTO dto, in
				GlobalAltNicksPrefs parent) :
			base(dto.GUID)
		{
			strNickToUse = dto.NickToUse;

			this.mgrParent = parent;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event DFieldChanged<string>? evtNickToUseChanged;

		public event IKeyChanged<GlobalAltNicksOneAltNick, string>.DKeyChanged? evtKeyChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly GlobalAltNicksPrefs mgrParent;

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
		public GlobalAltNicksOneAltNickEditable MakeEditable()
			=> new(this);

		private void FireCtntsChanged(string strOldNickToUse)
		{
			FirePropChanged(nameof(NickToUse));

			evtNickToUseChanged?.Invoke(this, strOldNickToUse, strNickToUse);
			evtKeyChanged?.Invoke(this, strOldNickToUse, strNickToUse);
		}

		public void SaveFrom(in GlobalAltNicksOneAltNickEditable enick)
			=> NickToUse = enick.NickToUse;

		public GlobalOneAltNickDTO ToDTO()
			=> new(guid, strNickToUse);
	#endregion

	#region Event Handlers
	#endregion
}