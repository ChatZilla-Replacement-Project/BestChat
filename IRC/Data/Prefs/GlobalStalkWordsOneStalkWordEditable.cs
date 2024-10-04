namespace BestChat.IRC.Data.Prefs;

public class GlobalStalkWordsOneStalkWordEditable : GlobalStalkWordsOneStalkWord, System.ComponentModel
	.INotifyDataErrorInfo
{
	internal GlobalStalkWordsOneStalkWordEditable(GlobalStalkWordsOneStalkWord swOriginal) : base(swOriginal.Ctnts,
		swOriginal.cmgrParent)
	{
		this.swOriginal = swOriginal;
	}

	public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;

	public readonly GlobalStalkWordsOneStalkWord swOriginal;

	public GlobalStalkWordsOneStalkWord Original
		=> swOriginal;

	public new string Ctnts
	{
		get => base.Ctnts;

		set
		{
			if(base.Ctnts != value)
			{
				base.Ctnts = value;


			}
		}
	}

	public bool WereChangesMade
	{
		get;

		private set;
	}

	public bool HasErrors
		=> base.Ctnts == "" || swOriginal.cmgrParent.Entries.ContainsKey(base.Ctnts) && swOriginal.cmgrParent.Entries[base
			.Ctnts] != this;

	public bool IsValid
		=> !HasErrors;

	public System.Collections.IEnumerable GetErrors(string? strWhichPropToGetErrorsFor)
		=> base.Ctnts == ""
			? (new[]
				{
					PrefsRsrcs.strStalkWordBlank,
				})
			: swOriginal.cmgrParent.Entries.ContainsKey(base.Ctnts) && swOriginal.cmgrParent.Entries[base.Ctnts] != this
				? (new[]
					{
						PrefsRsrcs.strStalkWordDuplicate
					})
				: (System.Collections.IEnumerable)System.Array.Empty<string>();

	public void Save()
		=> swOriginal.SaveFrom(this);
}