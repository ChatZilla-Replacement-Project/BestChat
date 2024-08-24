// Ignore Spelling: Prefs evt

namespace BestChat.IRC.Data.Prefs;

public abstract class Prefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		protected Prefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) : base(mgrParent, "IRC", Rsrcs.strIRCRootTitle, Rsrcs
			.strIrcRootDesc)
		{
		}

		protected Prefs(Platform.DataAndExt.Prefs.AbstractChildMgr mgrParent, DTO.IrcDTO dto) : base(mgrParent, "IRC", Rsrcs.strIRCRootTitle,
			Rsrcs.strIrcRootDesc)
		{
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public abstract class GlobalPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
				public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
					base(mgrParent, "Global", Rsrcs.strGlobalTitle, Rsrcs.strGlobalDesc)
				{
					aliases = new(this);
					autoPerform = new(this);
					altNicks = new(this);
					stalkWords = new(this);
					dcc = new(this);
				}

				public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.IrcDTO.GlobalDTO dto) :
					base(mgrParent, "Global", Rsrcs.strGlobalTitle, Rsrcs.strGlobalDesc)
				{
					aliases = new(this, dto.Aliases);
					autoPerform = new(this, dto.AutoPerform);
					altNicks = new(this, dto.AltNicks);
					stalkWords = new(this, dto.StalkWords);
					dcc = new(this, dto.DCC);
				}
			#endregion

			#region Delegates
			#endregion

			#region Events
				public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
				public class AliasesPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public AliasesPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
							base(mgrParent, "Aliases", Rsrcs.strGlobalAliasesTitle, Rsrcs.strGlobalAliasesDesc)
								=> mapEntriesSortedByName = [];

						public AliasesPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.IrcDTO.GlobalDTO.AliasDTO[]? dto) :
							base(mgrParent, "Aliases", Rsrcs.strGlobalAliasesTitle, Rsrcs.strGlobalAliasesDesc)
						{
							mapEntriesSortedByName = [];
							if(dto != null)
								foreach(DTO.IrcDTO.GlobalDTO.AliasDTO daliasCur in dto)
								{
									Alias aliasCur = new(daliasCur);
									mapEntriesSortedByName[aliasCur.Name] = aliasCur;

									aliasCur.evtDirtyChanged += OnChildAliasDirtyChanged;
									aliasCur.evtNameChanged += OnChildAliasNameChanged;
								}
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
						public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class Alias : Platform.DataAndExt.Obj<Alias>
						{
							#region Constructors & Deconstructors
								public Alias(in string strName, in string strCmd)
								{
									this.strName = strName;
									this.strCmd = strCmd;
								}

								public Alias(in DTO.IrcDTO.GlobalDTO.AliasDTO dto)
								{
									strName = dto.Name;
									strCmd = dto.Cmd;
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

								public event DFieldChanged<string> evtNameChanged;

								public event DFieldChanged<string> evtCmdChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								private string strName;

								private string strCmd;
							#endregion

							#region Properties
								public string Name
								{
									get => strName;

									set
									{
										if(strName != value)
										{
											string strOldName = strName;

											strName = value;

											FireNameChanged(strOldName);

											MakeDirty();
										}
									}
								}

								public string Cmd
								{
									get => strCmd;

									set
									{
										if(strCmd != value)
										{
											string strOldCmd = strCmd;

											strCmd = value;

											FireCmdChanged(strOldCmd);

											MakeDirty();
										}
									}
								}
							#endregion

							#region Methods
								private void FirePropChanged(in string strPropName)
									=> PropertyChanged?.Invoke(this, new(strPropName));

								private void FireNameChanged(in string strOldName)
								{
									FirePropChanged(nameof(Name));

									evtNameChanged?.Invoke(this, strOldName, strName);
								}

								private void FireCmdChanged(in string strOldCmd)
								{
									FirePropChanged(nameof(Cmd));

									evtCmdChanged?.Invoke(this, strOldCmd, strCmd);
								}
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
					private readonly System.Collections.Generic.SortedDictionary<string, Alias> mapEntriesSortedByName;
					#endregion

					#region Properties
						public System.Collections.Generic.IReadOnlyCollection<Alias> Entries
							=> mapEntriesSortedByName.Values;
					#endregion

					#region Methods
					#endregion

					#region Event Handlers
						private void OnChildAliasNameChanged(in Alias aliasSender, in string strOldVal, in string strNewVal)
						{
							mapEntriesSortedByName.Remove(strOldVal);
							mapEntriesSortedByName[strNewVal] = aliasSender;
						}

						private void OnChildAliasDirtyChanged(in Alias aliasSender, in bool bIsNowDirty)
							=> MakeDirty();
					#endregion
				}

				public class AutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public AutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
							base(mgrParent, "Auto-perform", Rsrcs.strGlobalAutoPerformTitle, Rsrcs.strGlobalAutoPerformDesc)
						{
							whenStartingBestChat = new(this, "When Starting Best Chat", Rsrcs
								.strGlobalAutoPerformWhenStartingBestChatTitle, Rsrcs.strGlobalAutoPerformWhenStartingBestChatDesc);
							whenJoiningNet = new(this, "When Joining a Network", Rsrcs.strGlobalAutoPerformWhenJoiningNetTitle, Rsrcs
								.strGlobalAutoPerformWhenJoiningNetDesc);
							whenJoiningChan = new(this, "When Joining a Channel", Rsrcs.strGlobalAutoPerformWhenJoiningChanTitle, Rsrcs
								.strGlobalAutoPerformWhenJoiningChanDesc);
							whenOpeningUserChat = new(this, "When Opening a User Chat", Rsrcs.strGlobalAutoPerformWhenOpeningUserChatTitle,
								Rsrcs.strGlobalAutoPerformWhenOpeningUserChatDesc);
						}

						public AutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.IrcDTO.GlobalDTO.AutoPerformDTO dto) :
							base(mgrParent, "Auto-perform", Rsrcs.strGlobalAutoPerformTitle, Rsrcs.strGlobalAutoPerformDesc)
						{
							whenStartingBestChat = new(this, "When Starting Best Chat", Rsrcs.strGlobalAutoPerformWhenStartingBestChatTitle, Rsrcs
								.strGlobalAutoPerformWhenStartingBestChatDesc, dto.WhenStartingBestChat);
							whenJoiningNet = new(this, "When Joining a Network", Rsrcs.strGlobalAutoPerformWhenJoiningNetTitle, Rsrcs
								.strGlobalAutoPerformWhenJoiningNetTitle, dto.WhenJoiningNet);
							whenJoiningChan = new(this, "When Joining a Channel", Rsrcs.strGlobalAutoPerformWhenJoiningChanTitle, Rsrcs
								.strGlobalAutoPerformWhenJoiningChanDesc, dto.WhenJoiningChan);
							whenOpeningUserChat = new(this, "When Opening a User Chat", Rsrcs.strGlobalAutoPerformWhenOpeningUserChatTitle, Rsrcs
								.strGlobalAutoPerformWhenOpeningUserChatDesc, dto.WhenOpeningUserChat);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
						public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class OnEvtPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public OnEvtPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string strName, in string strLocalizedName, in
										string strLocalizedDesc) :
									base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
									=> llistEntries = [];

								public OnEvtPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string strName, in string strLocalizedName, in
										string strLocalizedDesc, in string[]? dto) :
									base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
									=> llistEntries = dto == null ? [] : new(dto);
							#endregion

							#region Delegates
							#endregion

							#region Events
								public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								private readonly System.Collections.Generic.LinkedList<string> llistEntries;
							#endregion

							#region Properties
								public System.Collections.Generic.IReadOnlyCollection<string> Entries
									=> llistEntries;
							#endregion

							#region Methods
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						private readonly OnEvtPrefs whenStartingBestChat;

						private readonly OnEvtPrefs whenJoiningNet;

						private readonly OnEvtPrefs whenJoiningChan;

						private readonly OnEvtPrefs whenOpeningUserChat;
					#endregion

					#region Properties
						public OnEvtPrefs WhenStartingBestChat
							=> whenStartingBestChat;

						public OnEvtPrefs WhenJoiningNet
							=> whenJoiningNet;

						public OnEvtPrefs WhenJoiningChan
							=> whenJoiningChan;

						public OnEvtPrefs WhenOpeningUserChat
							=> whenOpeningUserChat;
					#endregion

					#region Methods
					#endregion

					#region Event Handlers
					#endregion
				}

				public class AltNicksPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public AltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
							base(mgrParent, "Alternate Nicks", Rsrcs.strGlobalAltNicksTitle, Rsrcs.strGlobalAltNicksDesc)
							=> llistEntries = [];

						public AltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string[]? dto) : 
							base(mgrParent, "Alternate Nicks", Rsrcs.strGlobalAltNicksTitle, Rsrcs.strGlobalAltNicksDesc)
							=> llistEntries = dto == null
								? []
								: new(dto);
					#endregion

					#region Delegates
					#endregion

					#region Events
						public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
					#endregion

					#region Constants
					#endregion

					#region Helper Types
					#endregion

					#region Members
						private readonly System.Collections.Generic.LinkedList<string> llistEntries;
					#endregion

					#region Properties
						public System.Collections.Generic.IReadOnlyCollection<string> Entries
							=> llistEntries;
					#endregion

					#region Methods
					#endregion

					#region Event Handlers
					#endregion
				}

				public class StalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public StalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
							base(mgrParent, "Stalk Words", Rsrcs.strGlobalStalkWordsTitle, Rsrcs.strGlobalStalkWordsDesc)
							=> llistEntries = [];

						public StalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string[]? dto)
							: base(mgrParent, "Stalk Words", Rsrcs.strGlobalStalkWordsTitle, Rsrcs.strGlobalStalkWordsDesc)
							=> llistEntries = dto == null
								? []
								: new(dto);
					#endregion

					#region Delegates
					#endregion

					#region Events
						public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
					#endregion

					#region Constants
					#endregion

					#region Helper Types
					#endregion

					#region Members
						private readonly System.Collections.Generic.LinkedList<string> llistEntries;
					#endregion

					#region Properties
						public System.Collections.Generic.IReadOnlyCollection<string> Entries
							=> llistEntries;
					#endregion

					#region Methods
					#endregion

					#region Event Handlers
					#endregion
				}

				public class DccPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public DccPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
							: base(mgrParent, "DCC (Direct Client Chat)", Rsrcs.strGlobalDccTitle, Rsrcs.strGlobalDccTitle)
						{
							enabled = new(this, "Enabled", Rsrcs.strGlobalDccEnabledTitle, Rsrcs.strGlobalDccEnabledDesc, false);
							getIpFromServer = new(this, "Get IP From Server", Rsrcs.strGlobalDccGetIpFromServerTitle, Rsrcs
								.strGlobalDccGetIpFromServerDesc, false);
							downloadsFolder = new(this, "Downloads Folder", Rsrcs.strGlobalDccDownloadsFolderTitle, Rsrcs
								.strGlobalDccDownloadsFolderDesc, null);
							llistPorts = [];
						}

						public DccPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO.GlobalDTO.DccDTO dto) :
							base(mgrParent, "DCC (Direct Client Chat)", Rsrcs.strGlobalDccTitle, Rsrcs.strGlobalDccTitle)
						{
							enabled = new(this, "Enabled", Rsrcs.strGlobalDccEnabledTitle, Rsrcs.strGlobalDccEnabledDesc, dto.Enabled);
							getIpFromServer = new(this, "Get IP From Server", Rsrcs.strGlobalDccGetIpFromServerTitle, Rsrcs
								.strGlobalDccGetIpFromServerDesc, dto.GetLocalIpFromServer ?? false);
							downloadsFolder = new(this, "Downloads Folder", Rsrcs.strGlobalDccDownloadsFolderTitle, Rsrcs
								.strGlobalDccDownloadsFolderDesc, dto.DownloadsFolder == null ? null : new(dto.DownloadsFolder));
							llistPorts = dto.Ports == null ? [] : new(dto.Ports);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
						public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
					#endregion

					#region Constants
					#endregion

					#region Helper Types
					#endregion

					#region Members
						private readonly Platform.DataAndExt.Prefs.Item<bool> enabled;

						private readonly Platform.DataAndExt.Prefs.Item<bool> getIpFromServer;

						private readonly Platform.DataAndExt.Prefs.Item<System.IO.DirectoryInfo?> downloadsFolder;

						private readonly System.Collections.Generic.LinkedList<int> llistPorts;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.Item<bool> Enabled
							=> enabled;

						public Platform.DataAndExt.Prefs.Item<bool> GetIpFromServer
							=> getIpFromServer;

						public Platform.DataAndExt.Prefs.Item<System.IO.DirectoryInfo?> DownloadsFolder
							=> downloadsFolder;

						public System.Collections.Generic.IReadOnlyCollection<int> Ports
							=> llistPorts;
					#endregion

					#region Methods
					#endregion

					#region Event Handlers
					#endregion
				}
			#endregion

			#region Members
				private readonly AliasesPrefs aliases;

				private readonly AutoPerformPrefs autoPerform;

				private readonly AltNicksPrefs altNicks;

				private readonly StalkWordsPrefs stalkWords;

				private readonly DccPrefs dcc;
			#endregion

			#region Properties
				public AliasesPrefs Aliases
					=> aliases;

				public AutoPerformPrefs AutoPerform
					=> autoPerform;

				public AltNicksPrefs AltNicks
					=> altNicks;

				public StalkWordsPrefs StalkWords
					=> stalkWords;

				public DccPrefs DCC
					=> dcc;
			#endregion

			#region Methods
			#endregion

			#region Event Handlers
			#endregion
		}

		public class NetworkPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
				public NetworkPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
					base(mgrParent, "IRC", Rsrcs.strGlobalAliasesTitle, Rsrcs.strGlobalAliasesDesc)
				{
				}
			#endregion

			#region Delegates
			#endregion

			#region Events
				public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
			#endregion

			#region Members
			#endregion

			#region Properties
			#endregion

			#region Methods
			#endregion

			#region Event Handlers
			#endregion
		}
	#endregion

	#region Members
	#endregion

	#region Properties
		public abstract GlobalPrefs Global
		{
			get;
		}
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}