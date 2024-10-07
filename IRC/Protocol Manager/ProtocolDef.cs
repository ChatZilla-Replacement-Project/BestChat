// Ignore Spelling: Ctrl gvc Prefs

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

		Prefs.IrcPrefs.InitInstance(Platform.UI.Desktop.Prefs.RootPrefs.Instance);
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
				[typeof(Data.Prefs.GlobalAliasesPrefs)] = cmgrToCreatePageFor
					=> new Prefs.Pages.GlobalAliasesPage()
					{
						Ctxt = (Data.Prefs.GlobalAliasesPrefs)cmgrToCreatePageFor,
					},
				[typeof(Data.Prefs.GlobalAltNicksPrefs)] = cmgrToCreatePageFor
					=> new Prefs.Pages.GlobalAltNicksPage()
					{
						Ctxt = (Data.Prefs.GlobalAltNicksPrefs)cmgrToCreatePageFor,
					},
				[typeof(Data.Prefs.ChanAutoPerformPrefs)] = cmgrToCreatePageFor
					=> new Prefs.Pages.GlobalAutoPerformOnEvtPage()
					{
						Ctxt = (Data.Prefs.GlobalAutoPerformOnEvtPrefs)cmgrToCreatePageFor,
					},
				[typeof(Data.Prefs.GlobalConnPrefs)] = cmgrToCreatePageFor
					=> new Prefs.Pages.GlobalConnPage()
					{
						Ctxt = (Data.Prefs.GlobalConnPrefs)cmgrToCreatePageFor,
					},
				[typeof(Data.Prefs.GlobalDccPrefs)] = cmgrToCreatePageFor
				=> new Prefs.Pages.GlobalDccPage()
					{
						Ctxt = (Data.Prefs.GlobalDccPrefs)cmgrToCreatePageFor,
					},
				[typeof(Data.Prefs.GlobalStalkWordsPrefs)] = cmrToCreatePageFor
					=> new Prefs.Pages.GlobalStalkWordsPage()
					{
						Ctnts = (Data.Prefs.GlobalStalkWordsPrefs)cmrToCreatePageFor,
					},
				[typeof(Data.Prefs.NetAltNicksPrefs)] = cmgrToCreatePageFor
					=> new Prefs.Pages.NetAltNicksPage()
					{
						Ctxt = (Data.Prefs.NetAltNicksPrefs)cmgrToCreatePageFor,
					},
				[typeof(Data.Prefs.NetAliasesPrefs)] = cmgrToCreatePageFor
					=> new Prefs.Pages.NetAliasesPage()
					{
						Ctxt = (Data.Prefs.NetAliasesPrefs)cmgrToCreatePageFor,
					},
				[typeof(Data.Prefs.NetAutoPerformOnEvtPrefs)] = cmgrToCreatePageFor
					=> new Prefs.Pages.NetAutoPerformOnEvtPage()
					{
						Ctxt = (Data.Prefs.NetAutoPerformOnEvtPrefs)cmgrToCreatePageFor,
					},
				[typeof(Data.Prefs.NetConnPrefs)] = cmgrToCreatePageFor
					=> new Prefs.Pages.NetConnPage()
					{
						Ctxt = (Data.Prefs.NetConnPrefs)cmgrToCreatePageFor,
					},
				[typeof(Data.Prefs.NetNotifyWhenOnlinePrefs)] = cmgrToCreatePageFor
					=> new Prefs.Pages.NetNotifyWhenOnlinePage()
					{
						Ctxt = (Data.Prefs.NetNotifyWhenOnlinePrefs)cmgrToCreatePageFor,
					},
			};

	public Platform.DataAndExt.Prefs.AbstractChildMgr? LoadAllPrefsData(in System.IO.StreamReader sr, in Platform
		.DataAndExt.Prefs.AbstractMgr mgrYourParent)
	{
		// TODO: Load the data
		return Prefs.IrcPrefs.InitInstance(mgrYourParent);
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