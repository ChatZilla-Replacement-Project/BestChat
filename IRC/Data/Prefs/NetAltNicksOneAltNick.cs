namespace BestChat.IRC.Data.Prefs;

public class NetAltNicksOneAltNick : Platform.DataAndExt.Obj<NetAltNicksOneAltNick>, IReadOnlyOneAltNick
{
	#region Constructors & Deconstructors
		public NetAltNicksOneAltNick(in string strNickToUse)
			=> this.strNickToUse = strNickToUse;

		public NetAltNicksOneAltNick(in DTO.GlobalOneAltNickDTO dto) :
			base(dto.GUID)
			=> strNickToUse = dto.NickToUse;
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

		public DTO.GlobalOneAltNickDTO ToDTO()
			=> new(guid, strNickToUse);
	#endregion

	#region Event Handlers
	#endregion
}