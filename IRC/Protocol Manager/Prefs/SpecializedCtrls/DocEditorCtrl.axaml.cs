namespace BestChat.IRC.ProtocolMgr.Prefs.SpecializedCtrls;

public partial class DocEditorCtrl : Avalonia.Controls.UserControl
{
	public DocEditorCtrl()
		=> InitializeComponent();

	public static readonly Avalonia.Interactivity.RoutedEvent<CtntChangedEventArgs> evtCtntChangedEvent = Avalonia
		.Interactivity.RoutedEvent.Register<CtntChangedEventArgs>(nameof(evtCtntChanged), Avalonia
		.Interactivity.RoutingStrategies.Direct, typeof(string));

	public new event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

	public event System.EventHandler<Avalonia.Interactivity.RoutedEventArgs>? evtCtntChanged;

	public static readonly Avalonia.DirectProperty<DocEditorCtrl, string> CtntProperty = Avalonia.AvaloniaProperty
		.RegisterDirect<DocEditorCtrl, string>(
			nameof(Ctnt),
			ctrlSender
				=> ctrlSender.Ctnt,
			(ctrlSender, strNewCtnt)
				=> ctrlSender.Ctnt = strNewCtnt
		);

	public class CtntChangedEventArgs
	(
		string strOldCtnt,
		string strNewCtnt,
		object? objSrc = null
	) : Avalonia.Interactivity.RoutedEventArgs(evtCtntChangedEvent, objSrc)
	{
		string OldCtnt
			=> strOldCtnt;

		string NewCtnt
			=> strNewCtnt;
	}

	private string strCtnt = "";

	public string Ctnt
	{
		get => strCtnt;

		set
		{
			if(strCtnt != value)
			{
				string strOldCtnt = strCtnt;

				strCtnt = value;

				PropertyChanged?.Invoke(this, new(nameof(Ctnt)));
				RaiseEvent(new CtntChangedEventArgs(strOldCtnt, strCtnt, this));
			}
		}
	}
}