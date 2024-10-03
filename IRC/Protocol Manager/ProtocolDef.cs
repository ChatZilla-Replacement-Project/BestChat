// Ignore Spelling: Ctrl gvc Prefs

using BestChat.IRC.ProtocolMgr.Prefs;
using BestChat.IRC.ProtocolMgr.Prefs.Pages;

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
		miFileIrcNetMgr.SetCurrentValue(Avalonia.Controls.ToolTip.TipProperty, Rsrcs
			.strFileNetworkMgrMenuItemToolTip);

		miFileIrcBncMgr = new()
		{
			Header = Rsrcs.strFileBncMgrMenuItemTitle,
		};
		miFileIrcBncMgr.Click += OnFileIrcBncMgrClicked;
		miFileIrcBncMgr.SetCurrentValue(Avalonia.Controls.ToolTip.TipProperty, Rsrcs
			.strFileBncMgrMenuItemToolTip);

		miFileIrc = new()
		{
			Name = Rsrcs.strFileMenuItemTitle,
			Items =
			{
				miFileIrcNetMgr,
				miFileIrcBncMgr,
			},
		};
		miFileIrc.SetCurrentValue(Avalonia.Controls.ToolTip.TipProperty, Rsrcs.strFileMenuItemToolTip);

		IrcPrefs.InitInstance(Platform.UI.Desktop.Prefs.RootPrefs.Instance);

		Platform.UI.Desktop.Prefs.VisualPrefsTreeData.RegisterDataEditorCtrlType(
			typeof(Data.Prefs.GlobalAliasesPrefs),
			mgrNew => new GlobalAliasesPage()
			{
				Ctxt = (Data.Prefs.GlobalAliasesPrefs)mgrNew,
			});
		Platform.UI.Desktop.Prefs.VisualPrefsTreeData.RegisterDataEditorCtrlType(
			typeof(Data.Prefs.GlobalAliasesPrefs),
			mgrNew => new GlobalAltNicksPage()
			{
				Ctxt = (Data.Prefs.GlobalAltNicksPrefs)mgrNew,
			});
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

	public Platform.DataAndExt.Conversations.IGroupViewOrConversation? TopLevelViewGroupOrConversation
		=> throw new System.NotImplementedException();

	public bool GuiRecommended
		=> false;

	public Platform.UI.Desktop.AbstractVisualConversationCtrl MakeConversationCtrl(Platform.DataAndExt.Conversations
			.IGroupViewOrConversation gvcWhatWeNeedCtrlFor)
		=> throw new System.NotImplementedException();

	public Platform.DataAndExt.Prefs.AbstractChildMgr? TopLevelPrefsMgr
		=> Prefs.IrcPrefs.Instance;

	public System.Collections.Generic.IReadOnlyDictionary<System.Type, System.Func<Platform.DataAndExt.Prefs
			.AbstractMgr, Platform.UI.Desktop.Prefs.VisualPrefsTabCtrl>>
		PrefCtrlMap
			=> new System.Collections.Generic.Dictionary<System.Type, System.Func<Platform.DataAndExt.Prefs
				.AbstractMgr, Platform.UI.Desktop.Prefs.VisualPrefsTabCtrl>>()
			{
				// TODO: Add the entries
			};

	public Platform.DataAndExt.Prefs.AbstractChildMgr? LoadAllPrefsData(in System.IO.StreamReader stream, in
		Platform.DataAndExt.Prefs.AbstractMgr mgrYourParent)
	{
		// TODO: Load the data
		return IrcPrefs.InitInstance(mgrYourParent);
	}

	public Avalonia.Controls.MenuItem? FileMenuItem
		=> miFileIrc;

	private readonly Avalonia.Controls.MenuItem miFileIrc;

	private readonly Avalonia.Controls.MenuItem miFileIrcNetMgr;

	private readonly Avalonia.Controls.MenuItem miFileIrcBncMgr;

	private readonly Views.Desktop.NetMgrDlg wndNetMgr = new();

	private readonly Views.Desktop.BncMgrDlg wndBncMgr = new();

	public readonly Avalonia.Controls.Window wndMain;

	private void OnFileIrcNetMgrClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		=> wndNetMgr.Show(wndMain);

	private void OnFileIrcBncMgrClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		=> wndBncMgr.Show(wndMain);
}