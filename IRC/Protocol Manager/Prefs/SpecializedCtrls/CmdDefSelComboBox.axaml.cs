namespace BestChat.IRC.ProtocolMgr.Prefs.SpecializedCtrls;

public partial class CmdDefSelComboBox : Avalonia.Controls.ComboBox
{
	public CmdDefSelComboBox()
		=> InitializeComponent();

	public event System.Action<CmdDefSelComboBox, Platform.DataAndExt.Cmd.CmdDef?>? evtSelValChanged;

	protected override void OnInitialized()
	{
		base.OnInitialized();

		SelectionChanged += (_, _)
			=> evtSelValChanged?.Invoke(this, SelVal);

		ItemsSource = Platform.DataAndExt.Cmd.CmdDef.AllInstancesByName.Values;
	}

	public Platform.DataAndExt.Cmd.CmdDef? SelVal
	{
		get => (Platform.DataAndExt.Cmd.CmdDef?)SelectedItem;

		set => SelectedItem = value;
	}
}