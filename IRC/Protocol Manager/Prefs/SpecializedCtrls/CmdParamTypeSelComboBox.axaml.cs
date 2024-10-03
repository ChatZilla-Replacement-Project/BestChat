namespace BestChat.IRC.ProtocolMgr.Prefs.SpecializedCtrls;

public partial class CmdParamTypeSelComboBox : Avalonia.Controls.ComboBox
{
	public CmdParamTypeSelComboBox()
		=> InitializeComponent();

	public event System.Action<CmdParamTypeSelComboBox, Platform.DataAndExt.Cmd.ParamTypes.Abstract?>? evtSelValChanged;

	protected override void OnInitialized()
	{
		base.OnInitialized();

		SelectionChanged += (_, _)
			=> evtSelValChanged?.Invoke(this, SelVal);

		ItemsSource = Platform.DataAndExt.Cmd.CmdDef.AllInstancesByName.Values;
	}

	public Platform.DataAndExt.Cmd.ParamTypes.Abstract? SelVal
	{
		get => (Platform.DataAndExt.Cmd.ParamTypes.Abstract?)SelectedItem;

		set => SelectedItem = value;
	}
}