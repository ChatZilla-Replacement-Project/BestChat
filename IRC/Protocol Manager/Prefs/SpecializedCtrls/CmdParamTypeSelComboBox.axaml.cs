namespace BestChat.IRC.ProtocolMgr.Prefs.SpecializedCtrls;

public partial class CmdParamSelComboBox : Avalonia.Controls.ComboBox
{
	public CmdParamSelComboBox()
		=> InitializeComponent();

	public event System.Action<CmdParamSelComboBox, Platform.DataAndExt.Cmd.CmdDef?>? evtSelValChanged;

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