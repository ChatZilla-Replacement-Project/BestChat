// Ignore Spelling: Prefs

namespace BestChat;

public partial class PrefsWnd : Avalonia.Controls.Window
{
	#region Constructors & Deconstructors
		public PrefsWnd()
		{
			InitializeComponent();

			//mapTreeItemToPage = new()
			//{
			//	[tviGeneral] = new GlobalPages.GeneralPage(),
			//	[tviAppearance] = new GlobalPages.AppearancePage(),
			//	[tviPlugins] = new GlobalPages.PluginsPage(),
			//	[tviPluginsExt] = new GlobalPages.PluginsExtPage(),
			//	[tviPluginsExtWhereToLook] = new GlobalPages.PluginsExtWhereToLookPage(),
			//	[tviPluginsExtHowToRunThemGroupedByWhatRunsThem] = new GlobalPages.PluginsExtHowToRunThemGroupedByWhatRunsThemPage(),
			//	[tviPluginsExtHowToRunThemUngrouped] = new GlobalPages.PluginsExtHowToRunThemUngroupedPage(),
			//	[tviPluginsInternal] = new GlobalPages.PluginsInternalPage(),
			//	[tviReopenOnStartup] = new GlobalPages.ReopenOnStartupPage(),
			//	[tviFormatting] = new GlobalPages.FormattingPage(),
			//	[tviOther] = new GlobalPages.LinksPage(),
			//	[tviIRC] = new IRC.Global.View.Prefs.IrcPage(),
			//	[tviIrcGlobal] = new IRC.Global.View.Prefs.IrcGlobalPage(),
			//	[tviIrcGlobalGeneral] = new IRC.Global.View.Prefs.IrcGeneralPage(),
			//	[tviIrcGlobalAliases] = new IRC.Global.View.Prefs.IrcAliasesPage(),
			//	[tviIrcGlobalAutoPerform] = new IRC.Global.View.Prefs.IrcAutoPerformPage(),
			//	[tviIrcGlobalAutoPerformWhenBestChatStarts] = new IRC.Global.View.Prefs.IrcAutoPerformWhenBestChatStartsPage(),
			//	[tviIrcGlobalAutoPerformWhenJoiningNetworks] = new IRC.Global.View.Prefs.IrcAutoPerformWhenJoiningNetworks(),
			//	[tviIrcGlobalAutoPerformWhenJoiningChans] = new IRC.Global.View.Prefs.IrcAutoPerformWhenJoiningChansPage(),
			//	[tviIrcGlobalAutoPerformWhenOpeningUserPM] = new IRC.Global.View.Prefs.IrcAutoPerformWhenOpeningUserPmPage(),
			//	[tviIrcGlobalAltNicks] = new IRC.Global.View.Prefs.IrcAltNicksPage(),
			//	[tviIrcGlobalStalkWords] = new IRC.Global.View.Prefs.IrcStalkWordsPage(),
			//	[tviIrcGlobalDCC] = new IRC.Global.View.Prefs.IrcDccPage(),
			//	[tviIrcGlobalSoundEvts] = new IRC.Global.View.Prefs.IrcSoundEvtPage(),
			//	[tviIrcLibera] = new IRC.Local.View.Prefs.IrcLocalRootPage(),
			//	[tviIrcLiberaNetworkLevel] = new IRC.Local.View.Prefs.NetworkLevel.IrcPage(),
			//	[tviIrcLiberaNetworkLevelGeneral] = new IRC.Local.View.Prefs.NetworkLevel.IrcGeneralPage(),
			//	[tviIrcLiberaNetworkLevelAppearance] = new IRC.Local.View.Prefs.NetworkLevel.IrcAppearancePage(),
			//	[tviIrcLiberaNetworkLevelLists] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsPage(),
			//	[tviIrcLiberaNetworkLevelListsAutoPerform] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsAutoPerform(),
			//	[tviIrcLiberaNetworkLevelListsAutoPerformNetwork] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsAutoPerformNetworkPage(),
			//	[tviIrcLiberaNetworkLevelListsAutoPerformChan] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsAutoPerformChanPage(),
			//	[tviIrcLiberaNetworkLevelListsNickNames] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsNickNames(),
			//	[tviIrcLiberaNetworkLevelListsNotify] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsNotifyPage(),
			//	[tviIrcLiberalNetworkLevelDCC] = new IRC.Local.View.Prefs.NetworkLevel.IrcDccPage(),
			//	[tviIrcLiberaChans] = new IRC.Local.View.Prefs.NetworkLevel.IrcKnownChansPage(),
			//	[tviIrcLiberaSpace] = new IRC.Local.View.Prefs.ChanLevel.IrcLocalPage(),
			//	[tviIrcLiberaSpaceGeneral] = new IRC.Local.View.Prefs.ChanLevel.IrcGeneralPage(),
			//	[tviIrcLiberaSpaceAppearance] = new IRC.Local.View.Prefs.ChanLevel.IrcAppearancePage(),
			//	[tviIrcLiberaSpaceAutoPerform] = new IRC.Local.View.Prefs.ChanLevel.IrcAutoPerformPage(),
			//	[tviIrcLiberaSpaceOffTopic] = new IRC.Local.View.Prefs.ChanLevel.IrcLocalPage(),
			//	[tviIrcLiberaSpaceOffTopicGeneral] = new IRC.Local.View.Prefs.ChanLevel.IrcGeneralPage(),
			//	[tviIrcLiberaSpaceOffTopicAppearance] = new IRC.Local.View.Prefs.ChanLevel.IrcAppearancePage(),
			//	[tviIrcLiberaSpaceOffTopicAutoPerform] = new IRC.Local.View.Prefs.ChanLevel.IrcAutoPerformPage(),
			//	[tviIrcLiberaBestChat] = new IRC.Local.View.Prefs.ChanLevel.IrcLocalPage(),
			//	[tviIrcLiberaBestChatGeneral] = new IRC.Local.View.Prefs.ChanLevel.IrcGeneralPage(),
			//	[tviIrcLiberaBestChatAppearance] = new IRC.Local.View.Prefs.ChanLevel.IrcAppearancePage(),
			//	[tviIrcLiberaBestChatAutoPerform] = new IRC.Local.View.Prefs.ChanLevel.IrcAutoPerformPage(),
			//	[tviIrcOFTC] = new IRC.Local.View.Prefs.IrcLocalRootPage(),
			//	[tviIrcOftcNetworkLevel] = new IRC.Local.View.Prefs.NetworkLevel.IrcPage(),
			//	[tviIrcOftcNetworkLevelGeneral] = new IRC.Local.View.Prefs.NetworkLevel.IrcGeneralPage(),
			//	[tviIrcOftcNetworkLevelAppearance] = new IRC.Local.View.Prefs.NetworkLevel.IrcAppearancePage(),
			//	[tviIrcOftcNetworkLevelLists] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsPage(),
			//	[tviIrcOftcNetworkLevelListsAutoPerform] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsAutoPerform(),
			//	[tviIrcOftcNetworkLevelListsAutoPerformNetwork] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsAutoPerformNetworkPage(),
			//	[tviIrcOftcNetworkLevelListsAutoPerformChan] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsAutoPerformChanPage(),
			//	[tviIrcOftcNetworkLevelListsNickNames] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsNickNames(),
			//	[tviIrcOftcNetworkLevelListsNotify] = new IRC.Local.View.Prefs.NetworkLevel.IrcListsNotifyPage(),
			//	[tviIrcOftcNetworkLevelDCC] = new IRC.Local.View.Prefs.NetworkLevel.IrcDccPage(),
			//	[tviIrcOftcChans] = new IRC.Local.View.Prefs.NetworkLevel.IrcKnownChansPage(),
			//	[tviIrcOftcOSM] = new IRC.Local.View.Prefs.ChanLevel.IrcLocalPage(),
			//	[tviIrcOftcOsmGeneral] = new IRC.Local.View.Prefs.ChanLevel.IrcGeneralPage(),
			//	[tviIrcOftcOsmAppearance] = new IRC.Local.View.Prefs.ChanLevel.IrcAppearancePage(),
			//	[tviIrcOftcOsmAutoPerform] = new IRC.Local.View.Prefs.ChanLevel.IrcAutoPerformPage(),
			//};

			treeMain.SelectedItem = treeMain.SelectedItems[0];
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private readonly System.Collections.Generic.Dictionary<Avalonia.Controls.TreeViewItem, Avalonia.Controls.UserControl>
			mapTreeItemToPage;
	#endregion

	#region Properties
		//private Avalonia.Controls.UserControl CurPage => mapTreeItemToPage[treeMain.SelectedItem];
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
		//private	void OnTreeSelChanged(object objSender,	Avalonia.Controls.SelectionChangedEventArgs	e) =>	PropertyChanged?.Invoke(this,	new
		//	(nameof(CurPage)));
	#endregion
}