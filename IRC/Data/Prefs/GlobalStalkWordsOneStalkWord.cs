namespace BestChat.IRC.Data.Prefs;

public class GlobalStalkWordsOneStalkWord : Platform.DataAndExt
		.Obj<GlobalStalkWordsOneStalkWord>, IReadOnlyOneStalkWord, IKeyChanged<GlobalStalkWordsOneStalkWord, string>
{
	#region Constructors & Deconstructors
		public GlobalStalkWordsOneStalkWord(in string strCtnts, in IStalkWordsPrefs cmgrParent, in System.Guid guid =
				default) :
			base(guid)
		{
			this.cmgrParent  = cmgrParent;

			this.strCtnts = strCtnts;
		}

		public GlobalStalkWordsOneStalkWord(in DTO.GlobalStalkWordsOneStalkWordDTO dto, in IStalkWordsPrefs cmgrParent)
			: base(dto.GUID)
		{
			this.cmgrParent  = cmgrParent;

			strCtnts = dto.Ctnts;
		}
		#endregion

	#region Delegates
	#endregion

	#region Events
		public event DFieldChanged<string>? evtCtntsChanged;

		public event IKeyChanged<GlobalStalkWordsOneStalkWord, string>.DKeyChanged? evtKeyChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly IStalkWordsPrefs cmgrParent;

		private string strCtnts;
	#endregion

	#region Properties
		public IStalkWordsPrefs Parent
			=> cmgrParent;

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

		public GlobalStalkWordsOneStalkWordEditable MakeEditable()
			=> new(this);

		public void SaveFrom(GlobalStalkWordsOneStalkWordEditable esw)
			=> Ctnts = esw.Ctnts;

		public DTO.GlobalStalkWordsOneStalkWordDTO ToDTO()
			=> new(
				guid,
				strCtnts
			);
	#endregion

	#region Event Handlers
	#endregion
}