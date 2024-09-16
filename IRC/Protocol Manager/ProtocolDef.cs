// Ignore Spelling: Ctrl gvc

[assembly: BestChat.Platform.DataAndExt.Attr.ProtocolAssemblyInfo("IRC", "ChatZilla Replacement " +
	"Project", nameof(BestChat.IRC.ProtocolMgr.Rsrcs.strTranslatedProtDesc), "Implements the Internet Relay"
	+ " Chat protocol for Best Chat.  Without this module, Best Chat doesn't support IRC.", typeof(BestChat.IRC
	.ProtocolMgr.ProtocolDef), "0.0.0.1")]

namespace BestChat.IRC.ProtocolMgr;

public class ProtocolDef : Platform.UI.Desktop.ProtocolGuiMgr.IProtocolGuiDef
{
	public static readonly ProtocolDef instance = new();

	public static ProtocolDef Instance
		=> instance;

	private ProtocolDef()
	{
		Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime applifetime = (Avalonia
			.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime?)Avalonia.Application.Current?
			.ApplicationLifetime ?? throw new System.InvalidProgramException("Can't find the main Avalonia" +
			" application lifetime.");
		wndMain = applifetime.MainWindow ?? throw new System.InvalidProgramException("Found the Avalonia " +
			"application lifetime, but it lacks a main window.");

		miFileIrcNetMgr = new()
		{
			Header = Rsrcs.strFileNetworkMgrMenuItemTitle,
		};
		miFileIrcNetMgr.Click += OnFileIrcNetMgrClicked;
		miFileIrcNetMgr.SetCurrentValue(Avalonia.Controls.ToolTip.TipProperty, Rsrcs.strFileNetworkMgrMenuItemToolTip);

		miFileIrcBncMgr = new()
		{
			Header = Rsrcs.strFileBncMgrMenuItemTitle,
		};
		miFileIrcBncMgr.SetCurrentValue(Avalonia.Controls.ToolTip.TipProperty, Rsrcs.strFileBncMgrMenuItemToolTip);

		miFileIrc = new()
		{
			Name = Rsrcs.strFileMenuItemTitle,
			Items =
			{
				miFileIrcNetMgr,
				miFileIrcBncMgr,
			}
		};
		miFileIrc.SetCurrentValue(Avalonia.Controls.ToolTip.TipProperty, Rsrcs.strFileMenuItemToolTip);
	}

	public string Name
		=> "IRC";

	public string LocalizedName
		=> Rsrcs.strTranslatedProtName;

	public string LocalizedDesc
		=> Rsrcs.strTranslatedProtDesc;

	public string Publisher
		=> "ChatZilla Replacement Project";

	public System.Uri Homepage
		=> new("https://github.com/ChatZilla-Replacement-Project");

	public System.Uri PublisherHomepage
		=> new("https://github.com/ChatZilla-Replacement-Project");

	public Platform.DataAndExt.Prefs.AbstractChildMgr? RootPrefForProtocol
		=> throw new System.NotImplementedException();

	public Platform.DataAndExt.Conversations.IGroupViewOrConversation? TopLevelViewGroupOrConversation
		=> throw new System.NotImplementedException();

	public bool GuiRecommended
		=> false;

	public Platform.UI.Desktop.AbstractVisualConversationCtrl MakeConversationCtrl(Platform.DataAndExt.Conversations
			.IGroupViewOrConversation gvcWhatWeNeedCtrlFor)
		=> throw new System.NotImplementedException();

	public void RegisterPrefCtrlMap()
		=> throw new System.NotImplementedException();

	public Avalonia.Controls.MenuItem? FileMenuItem
		=> miFileIrc;

	private readonly Avalonia.Controls.MenuItem miFileIrc;

	private readonly Avalonia.Controls.MenuItem miFileIrcNetMgr;

	private readonly Avalonia.Controls.MenuItem miFileIrcBncMgr;

	private Views.Desktop.NetMgrDlg wndNetMgr = new();

	public readonly Avalonia.Controls.Window wndMain;

	private void OnFileIrcNetMgrClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		=> wndNetMgr.Show(wndMain);
}