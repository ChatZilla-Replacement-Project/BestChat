namespace BestChat.IRC.ProtocolMgr;

public partial class AliasImportFailureDlg : Avalonia.Controls.Window
{
	public AliasImportFailureDlg()
		=> InitializeComponent();

	public int? AliasesSuccessfullyImported
	{
		get;

		set;
	}

	public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>>? Errors
	{
		get;

		set;
	}

	protected override void OnInitialized()
	{
		if(AliasesSuccessfullyImported == null)
			throw new System.InvalidOperationException($"Set {nameof(AliasesSuccessfullyImported)} before opening the " +
				"window.)}");

		if(Errors is null)
			throw new System.InvalidOperationException($"Set {nameof(Errors)} before opening the window.");

		base.OnInitialized();

		DataContext = this;
	}

	private void OnCloseClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
		=> Close();
}