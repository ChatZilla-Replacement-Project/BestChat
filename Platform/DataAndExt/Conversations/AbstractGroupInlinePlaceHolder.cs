// Ignore Spelling: Ctnts Inline

namespace BestChat.Platform.DataAndExt.Conversations;

public abstract class AbstractGroupInlinePlaceHolder : IInlinePlaceHolder, System
	.ComponentModel.INotifyPropertyChanged
{
	public AbstractGroupInlinePlaceHolder(in System.Collections.Generic.IEnumerable<IInlinePlaceHolder> ieCtnts)
		=> llistCtnts = new System.Collections.Generic.List<IInlinePlaceHolder>(ieCtnts);

	/// <inheritdoc/>
	public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

	private System.Collections.Generic.List<IInlinePlaceHolder> llistCtnts;

	public System.Collections.Generic.IEnumerable<IInlinePlaceHolder> Ctnts
	{
		get => llistCtnts;

		protected set
		{
			llistCtnts = new(value);

			PropertyChanged?.Invoke(this, new(nameof(Ctnts)));
		}
	}

	public string AsText => string.Join("", llistCtnts);
}