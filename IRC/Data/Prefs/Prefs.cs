// Ignore Spelling: Prefs evt Dcc dto Ip Ctnts istep Chans

using System.Linq;

namespace BestChat.IRC.Data.Prefs;

using Platform.DataAndExt.Ext;

public abstract class Prefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : Prefs<GlobalPrefsType, GlobalDtoType>.GlobalPrefs
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
		protected Prefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) : base(mgrParent, "IRC",
			PrefsRsrcs.strIrcRootTitle, PrefsRsrcs.strIrcRootDesc)
		{
			instance = this;

			listNetworks = [];
		}

		protected Prefs(Platform.DataAndExt.Prefs.AbstractChildMgr mgrParent, DTO.IrcDTO<GlobalDtoType> dto) :
			base(mgrParent, "IRC", PrefsRsrcs.strIrcRootTitle, PrefsRsrcs.strIrcRootDesc)
		{
			instance = this;

			listNetworks = dto.Networks is null
				? []
				: [.. dto.Networks.Select(netdtoCur => new NetworkPrefs(this,
					netdtoCur))];
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public abstract class GlobalPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
				public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
					base(mgrParent, "Global", PrefsRsrcs.strGlobalTitle, PrefsRsrcs.strGlobalDesc)
				{
					autoPerform = new(this);
					dcc = new(this);
					conn = new(this);
					aliases = new(this);
					altNicks = new(this);
					stalkWords = new(this);
				}

				public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, GlobalDtoType dto)
					: base(mgrParent, "Global", PrefsRsrcs.strGlobalTitle, PrefsRsrcs.strGlobalDesc)
				{
					autoPerform = new(this, dto.AutoPerform);
					dcc = new(this, dto.DCC);
					conn = new(this, dto.Conn);
					aliases = new(this, dto.Aliases);
					altNicks = new(this, dto.AltNicks);
					stalkWords = new(this, dto.StalkWords);
				}
			#endregion

			#region Delegates
			#endregion

			#region Events
			#endregion

			#region Constants
			#endregion

			#region Helper Types
				public class AliasesPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public AliasesPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
							base(mgrParent, "Aliases", PrefsRsrcs.strGlobalAliasesTitle, PrefsRsrcs
								.strGlobalAliasesDesc)
							=> entries = new(this, "Entries", PrefsRsrcs
								.strGlobalAliasesTitle, PrefsRsrcs.strGlobalAliasesDesc, [], aliasCur
								=> aliasCur.Name);

						public AliasesPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.IrcDTO<GlobalDtoType>
								.GlobalDTO.OneAliasDTO[]? dto) :
							base(mgrParent, "Aliases", PrefsRsrcs.strGlobalAliasesTitle, PrefsRsrcs
								.strGlobalAliasesDesc)
							=> entries = new(this, "Entries", PrefsRsrcs
								.strGlobalAliasesTitle, PrefsRsrcs.strGlobalAliasesDesc, [], dto?
								.Select(daliasCur => new OneAlias(daliasCur, this)) ?? [],
								aliasCur => aliasCur.Name);
					#endregion

					#region Delegates
					#endregion

					#region Events
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public interface IReadOnlyOneAlias
						{
							string Name
							{
								get;
							}

							string Cmd
							{
								get;
							}

							System.Guid GUID
							{
								get;
							}
						}

						public class OneAlias : Platform.DataAndExt.Obj<OneAlias>, IReadOnlyOneAlias
						{
							#region Constructors & Deconstructors
								public OneAlias(in string strName, in string strCmd, in AliasesPrefs parent)
								{
									this.strName = strName;
									this.strCmd = strCmd;

									this.parent = parent;
								}

								public OneAlias(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO dto, in AliasesPrefs parent) :
									base(dto.GUID)
								{
									strName = dto.Name;
									strCmd = dto.Cmd;

									this.parent = parent;
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public event DFieldChanged<string>? evtNameChanged;

								public event DFieldChanged<string>? evtCmdChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								public readonly AliasesPrefs parent;

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

								public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO ToDTO()
									=> new(guid, strName, strCmd);
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneAlias> entries;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneAlias> Entries
							=> entries;
					#endregion

					#region Methods
						public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO[]? ToDTO()
							=> entries.Values.Select(aliasCur => aliasCur.ToDTO()).ToArray();
					#endregion

					#region Event Handlers
					#endregion
				}

				public class AutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public AutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
							: base(mgrParent, "Auto-perform", PrefsRsrcs.strGlobalAutoPerformTitle,
									PrefsRsrcs.strGlobalAutoPerformDesc)
						{
							whenStartingBestChat = new(this, "When Starting Best Chat", PrefsRsrcs
								.strGlobalAutoPerformWhenStartingBestChatTitle, PrefsRsrcs
								.strGlobalAutoPerformWhenStartingBestChatDesc);
							whenJoiningNet = new(this, "When Joining a Network", PrefsRsrcs
								.strGlobalAutoPerformWhenJoiningNetTitle, PrefsRsrcs
								.strGlobalAutoPerformWhenJoiningNetDesc);
							whenJoiningChan = new(this, "When Joining a Channel", PrefsRsrcs
								.strGlobalAutoPerformWhenJoiningChanTitle, PrefsRsrcs
								.strGlobalAutoPerformWhenJoiningChanDesc);
							whenOpeningUserChat = new(this, "When Opening a User Chat", PrefsRsrcs
								.strGlobalAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
								.strGlobalAutoPerformWhenOpeningUserChatDesc);
						}

						public AutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO
							.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO dto) :
							base(mgrParent, "Auto-perform", PrefsRsrcs.strGlobalAutoPerformTitle, PrefsRsrcs
								.strGlobalAutoPerformDesc)
						{
							whenStartingBestChat = new(this, "When Starting Best Chat", PrefsRsrcs
								.strGlobalAutoPerformWhenStartingBestChatTitle, PrefsRsrcs
								.strGlobalAutoPerformWhenStartingBestChatDesc, dto.WhenStartingBestChat);
							whenJoiningNet = new(this, "When Joining a Network", PrefsRsrcs
								.strGlobalAutoPerformWhenJoiningNetTitle, PrefsRsrcs
								.strGlobalAutoPerformWhenJoiningNetTitle, dto.WhenJoiningNet);
							whenJoiningChan = new(this, "When Joining a Channel", PrefsRsrcs
								.strGlobalAutoPerformWhenJoiningChanTitle, PrefsRsrcs
								.strGlobalAutoPerformWhenJoiningChanDesc, dto.WhenJoiningChan);
							whenOpeningUserChat = new(this, "When Opening a User Chat", PrefsRsrcs
								.strGlobalAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
								.strGlobalAutoPerformWhenOpeningUserChatDesc, dto.WhenOpeningUserChat);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class OnEvtPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public OnEvtPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string
										strName, in string strLocalizedName, in string strLocalizedDesc) :
									base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
									=> steps = new(this, "Steps", strLocalizedName, strLocalizedDesc, []);

								public OnEvtPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string
										strName, in string strLocalizedName, in string strLocalizedDesc, in DTO
										.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO[]? dto) :
									base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
									=> steps = new(this, "Steps", strLocalizedName, strLocalizedDesc,
										[], dto?.Select(dstep
											=> new OneStep(dstep, this)) ??
											[]);
							#endregion

							#region Delegates
							#endregion

							#region Events
							#endregion

							#region Constants
							#endregion

							#region Helper Types
								public interface IReadOnlyOneStep
								{
									System.Guid GUID
									{
										get;
									}

									string WhatToDo
									{
										get;
									}
								}

								public class OneStep : Platform.DataAndExt.Obj<OneStep>, IReadOnlyOneStep
								{
									#region Constructors & Deconstructors
										public OneStep(in string strWhatToDo, in OnEvtPrefs parent)
										{
											this.strWhatToDo = strWhatToDo;

											this.parent = parent;
										}

										public OneStep(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO dto, in
												OnEvtPrefs parent, in System.Guid guid = default) :
											base(guid)
										{
											strWhatToDo = dto.WhatToDo;

											this.parent = parent;
										}
									#endregion

									#region Delegates
									#endregion

									#region Events
										public event DFieldChanged<string>? evtWhatToDoChanged;
									#endregion

									#region Constants
									#endregion

									#region Helper Types
									#endregion

									#region Members
										public readonly OnEvtPrefs parent;

										private string strWhatToDo;
									#endregion

									#region Properties
										public string WhatToDo
										{
											get => strWhatToDo;

											set
											{
												if(strWhatToDo != value)
												{
													string strOldWhatToDo = strWhatToDo;

													strWhatToDo = value;

													MakeDirty();

													FireWhatToDoChanged(strOldWhatToDo);
												}
											}
										}
									#endregion

									#region Methods
										private void FireWhatToDoChanged(in string strOldWhatToDo)
										{
											FirePropChanged(nameof(strWhatToDo));

											evtWhatToDoChanged?.Invoke(this, strOldWhatToDo, strWhatToDo);
										}

										public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO ToDTO()
											=> new(guid, strWhatToDo);
									#endregion

									#region Event Handlers
									#endregion
								}
							#endregion

							#region Members
								private readonly Platform.DataAndExt.Prefs.ReorderableListItem<OneStep> steps;
							#endregion

							#region Properties
								public Platform.DataAndExt.Prefs.ReorderableListItem<OneStep> Steps
									=> steps;
							#endregion

							#region Methods
								public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO[]? ToDTO()
									=> steps.Select(aliasCur
										=> aliasCur.ToDTO()).ToArray();
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
						public OnEvtPrefs? WhenStartingBestChat
							=> whenStartingBestChat;

						public OnEvtPrefs WhenJoiningNet
							=> whenJoiningNet;

						public OnEvtPrefs WhenJoiningChan
							=> whenJoiningChan;

						public OnEvtPrefs WhenOpeningUserChat
							=> whenOpeningUserChat;
					#endregion

					#region Methods
						public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO ToDTO()
							=> new(whenStartingBestChat.ToDTO(), whenJoiningNet.ToDTO(), whenJoiningChan
								.ToDTO(), whenOpeningUserChat.ToDTO());
					#endregion

					#region Event Handlers
					#endregion
				}

				public class AltNicksPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public AltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
							base(mgrParent, "Alternate Nicks", PrefsRsrcs.strGlobalAltNicksTitle, PrefsRsrcs
								.strGlobalAltNicksDesc)
							=> entries = new(this, "Entries", PrefsRsrcs
								.strGlobalAltNicksTitle, PrefsRsrcs.strGlobalAltNicksDesc, []);

						public AltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO<GlobalDtoType>
								.GlobalDTO.OneAltNickDTO[]? dto): 
							base(mgrParent, "Alternate Nicks", PrefsRsrcs.strGlobalAltNicksTitle, PrefsRsrcs
								.strGlobalAltNicksDesc)
							=> entries = new(this, "Entries", PrefsRsrcs
								.strGlobalAltNicksTitle, PrefsRsrcs.strGlobalAltNicksDesc, [], dto?
								.Select(danickCur
									=> new OneAltNick(danickCur, this))
								?? []);
					#endregion

					#region Delegates
					#endregion

					#region Events
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public interface IReadOnlyOneAltNick
						{
							string NickToUse
							{
								get;
							}
						}

						public class OneAltNick : Platform.DataAndExt.Obj<OneAltNick>, IReadOnlyOneAltNick
						{
							#region Constructors & Deconstructors
								public OneAltNick(in string strNickToUse, in AltNicksPrefs parent)
								{
									this.strNickToUse = strNickToUse;

									this.parent = parent;
								}

								public OneAltNick(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO dto, in
									AltNicksPrefs parent) :
									base(dto.GUID)
								{
									strNickToUse = dto.NickToUse;

									this.parent = parent;
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public event DFieldChanged<string>? evtNickToUseChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								public readonly AltNicksPrefs parent;

								private string strNickToUse;
							#endregion

							#region Properties
								public string NickToUse
								{
									get => strNickToUse;

									set
									{
										if(strNickToUse != value)
										{
											string strOldNickToUse = strNickToUse;

											strNickToUse = value;

											MakeDirty();

											FireCtntsChanged(strOldNickToUse);
										}
									}
								}
							#endregion

							#region Methods
								private void FireCtntsChanged(string strOldNickToUse)
								{
									FirePropChanged(nameof(NickToUse));

									evtNickToUseChanged?.Invoke(this, strOldNickToUse, strNickToUse);
								}

								public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO ToDTO()
									=> new(guid, strNickToUse);
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						private readonly Platform.DataAndExt.Prefs.ReorderableListItem<OneAltNick> entries;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.ReorderableListItem<OneAltNick> Entries
							=> entries;
					#endregion

					#region Methods
						public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO[]? ToDTO()
							=> entries.Select(aliasCur => aliasCur.ToDTO()).ToArray();
					#endregion

					#region Event Handlers
					#endregion
				}

				public class StalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public StalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
							base(mgrParent, "Stalk Words", PrefsRsrcs.strGlobalStalkWordsTitle, PrefsRsrcs
								.strGlobalStalkWordsDesc)
							=> entries = new(this, "Entries", PrefsRsrcs
								.strGlobalStalkWordsTitle, PrefsRsrcs.strGlobalStalkWordsDesc, [],
								val
									=> val.Ctnts);

						public StalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO<GlobalDtoType>
								.GlobalDTO.OneStalkWordDTO[]? dto)
							: base(mgrParent, "Stalk Words", PrefsRsrcs.strGlobalStalkWordsTitle, PrefsRsrcs
								.strGlobalStalkWordsDesc)
							=> entries = new(this, "Entries", PrefsRsrcs
								.strGlobalStalkWordsTitle, PrefsRsrcs.strGlobalStalkWordsDesc, [], dto?
								.Select(dsto
									=> new OneStalkWord(dsto, this))
								?? [], val
									=> val.Ctnts);
					#endregion

					#region Delegates
					#endregion

					#region Events
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public interface IReadOnlyOneStalkWord
						{
							System.Guid GUID
							{
								get;
							}

							string Ctnts
							{
								get;
							}
						}

						public class OneStalkWord : Platform.DataAndExt.Obj<OneStalkWord>, IReadOnlyOneStalkWord
						{
							#region Constructors & Deconstructors
								public OneStalkWord(in string strCtnts, in StalkWordsPrefs parent)
								{
									this.strCtnts = strCtnts;

									this.parent = parent;
								}

								public OneStalkWord(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO dto, in StalkWordsPrefs
										parent) :
									base(dto.GUID)
								{
									strCtnts = dto.Ctnts;

									this.parent = parent;
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public event DFieldChanged<string>? evtCtntsChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								public readonly StalkWordsPrefs parent;

								private string strCtnts;
							#endregion

							#region Properties
								public string Ctnts
								{
									get => strCtnts;

									set
									{
										if(strCtnts != value)
										{
											string strOldCtnts = strCtnts;

											strCtnts = value;

											MakeDirty();

											FireCtntsChanged(strOldCtnts);
										}
									}
								}
							#endregion

							#region Methods
								private void FireCtntsChanged(string strOldCtnts)
								{
									FirePropChanged(nameof(Ctnts));

									evtCtntsChanged?.Invoke(this, strOldCtnts, strCtnts);
								}

								public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO ToDTO()
									=> new(guid, strCtnts);
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneStalkWord> entries;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneStalkWord> Entries
							=> entries;
					#endregion

					#region Methods
						public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO[]? ToDTO()
							=> entries.Values.Select(swCur => swCur.ToDTO()).ToArray();
					#endregion

					#region Event Handlers
					#endregion
				}

				public class DccPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public DccPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
							: base(mgrParent, "DCC (Direct Client Chat)", PrefsRsrcs.strGlobalDccTitle,
								PrefsRsrcs.strGlobalDccTitle)
						{
							enabled = new(this, "Enabled", PrefsRsrcs.strGlobalDccEnabledTitle,
								PrefsRsrcs.strGlobalDccEnabledDesc, false);
							getIpFromServer = new(this, "Get IP From Server", PrefsRsrcs
								.strGlobalDccGetIpFromServerTitle, PrefsRsrcs.strGlobalDccGetIpFromServerDesc,
								false);
							downloadsFolder = new(this, "Downloads Folder", PrefsRsrcs
								.strGlobalDccDownloadsFolderTitle, PrefsRsrcs.strGlobalDccDownloadsFolderDesc,
								null);
							llistPorts = [];
						}

						public DccPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO<GlobalDtoType>
								.GlobalDTO.DccDTO dto) :
							base(mgrParent, "DCC (Direct Client Chat)", PrefsRsrcs.strGlobalDccTitle,
								PrefsRsrcs.strGlobalDccTitle)
						{
							enabled = new(this, "Enabled", PrefsRsrcs.strGlobalDccEnabledTitle,
								PrefsRsrcs.strGlobalDccEnabledDesc, dto.Enabled);
							getIpFromServer = new(this, "Get IP From Server", PrefsRsrcs
								.strGlobalDccGetIpFromServerTitle, PrefsRsrcs.strGlobalDccGetIpFromServerDesc, dto
								.GetLocalIpFromServer ?? false);
							downloadsFolder = new(this, "Downloads Folder", PrefsRsrcs
								.strGlobalDccDownloadsFolderTitle, PrefsRsrcs.strGlobalDccDownloadsFolderDesc, dto
								.DownloadsFolder);
							llistPorts = dto.Ports == null
								? []
								: new(dto.Ports);
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
						public virtual DTO.IrcDTO<GlobalDtoType>.GlobalDTO.DccDTO ToDTO()
							=> new(
								enabled.CurVal,
								getIpFromServer.CurVal,
								downloadsFolder.CurVal,
								[.. llistPorts]
							);
					#endregion

					#region Event Handlers
					#endregion
				}

				public class ConnPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public ConnPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
							base(mgrParent,"Connection", PrefsRsrcs.strGlobalConnTitle, PrefsRsrcs
								.strGlobalConnDesc)
						{
							enableIndent = new(this, "Enable Ident", PrefsRsrcs
								.strGlobalConnEnableIdentTitle, PrefsRsrcs.strGlobalConnEnableIdentDesc, true);

							autoReconnect = new(this, "Auto Reconnect", PrefsRsrcs
								.strGlobalConnAutoReconnectTitle, PrefsRsrcs.strGlobalConnAutoReconnectDesc,
								true);

							rejoinAfterKick = new(this, "Rejoin After Kick",
								PrefsRsrcs.strGlobalConnRejoinAfterKickTitle, PrefsRsrcs
								.strGlobalConnRejoinAfterKickDesc, true);

							characterEncoding = new(this, "Character Encoding", PrefsRsrcs
								.strGlobalConnCharEncodingTitle, PrefsRsrcs.strGlobalConnCharEncodingDesc,
								"UTF-8");

							unlimitedAttempts = new(this, "Unlimited Reconnection Attempts",
								PrefsRsrcs.strGlobalConnUnlimitedAttemptsTitle, PrefsRsrcs
								.strGlobalConnUnlimitedAttemptsDesc, true);

							maxAttempts = new(this, "Maximum Attempts to Reconnect", PrefsRsrcs
								.strGlobalConnMaxAttemptsTitle, PrefsRsrcs.strGlobalConnMaxAttemptsDesc, 1,
								iMinVal: 1);

							defQuitMsg = new(this, "Default Quit message", PrefsRsrcs
								.strGlobalConnDefQuitMsgTitle, PrefsRsrcs.strGlobalConnDefQuitMsgDesc, PrefsRsrcs
								.strDefQuitMsg);
						}

						internal ConnPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.IrcDTO<GlobalDtoType>
								.GlobalDTO.ConnDTO dto) :
							base(mgrParent, "Connection", PrefsRsrcs.strGlobalConnTitle, PrefsRsrcs
								.strGlobalConnDesc)
						{
							enableIndent = new(this, "Enable Ident", PrefsRsrcs
								.strGlobalConnEnableIdentTitle, PrefsRsrcs.strGlobalConnEnableIdentDesc, true, dto
								.IsIdentEnabled);

							autoReconnect = new(this, "Auto Reconnect", PrefsRsrcs
								.strGlobalConnAutoReconnectTitle, PrefsRsrcs.strGlobalConnAutoReconnectDesc, true,
								dto.IsAutoReconnectEnabled);

							rejoinAfterKick = new(this, "Rejoin After Kick", PrefsRsrcs
								.strGlobalConnRejoinAfterKickTitle, PrefsRsrcs.strGlobalConnRejoinAfterKickDesc,
								true, dto.IsRejoinAfterKickEnabled);

							characterEncoding = new(this, "Character Encoding", PrefsRsrcs
								.strGlobalConnCharEncodingTitle, PrefsRsrcs.strGlobalConnCharEncodingDesc,
								"UTF-8", dto.CharEncoding);

							unlimitedAttempts = new(this, "Unlimited Reconnection Attempts",
								PrefsRsrcs.strGlobalConnUnlimitedAttemptsTitle, PrefsRsrcs
								.strGlobalConnUnlimitedAttemptsDesc, true, dto.IsUnlimitedAttemptsOn);

							maxAttempts = new(this, "Maximum Attempts to Reconnect", PrefsRsrcs
								.strGlobalConnMaxAttemptsTitle, PrefsRsrcs.strGlobalConnMaxAttemptsDesc, 1,
								iMinVal: 1, iCurVal: dto.MaxAttempts);

							defQuitMsg = new(this, "Default Quit message", PrefsRsrcs
								.strGlobalConnDefQuitMsgTitle, PrefsRsrcs.strGlobalConnDefQuitMsgDesc, PrefsRsrcs
								.strDefQuitMsg, dto.DefQuitMsg ?? PrefsRsrcs.strDefQuitMsg);
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
						public readonly Platform.DataAndExt.Prefs.Item<bool> enableIndent;
						public readonly Platform.DataAndExt.Prefs.Item<bool> autoReconnect;
						public readonly Platform.DataAndExt.Prefs.Item<bool> rejoinAfterKick;
						public readonly Platform.DataAndExt.Prefs.Item<string> characterEncoding;
						public readonly Platform.DataAndExt.Prefs.Item<bool> unlimitedAttempts;
						public readonly Platform.DataAndExt.Prefs.IntItem maxAttempts;
						public readonly Platform.DataAndExt.Prefs.Item<string> defQuitMsg;
						// TODO: Add proxy once we know what we're doing with that.
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.Item<bool> EnableIdent
							=> enableIndent;

						public Platform.DataAndExt.Prefs.Item<bool> AutoReconnect
							=> autoReconnect;

						public Platform.DataAndExt.Prefs.Item<bool> RejoinAfterKick
							=> rejoinAfterKick;

						public Platform.DataAndExt.Prefs.Item<string> CharEncoding
							=> characterEncoding;

						public Platform.DataAndExt.Prefs.Item<bool> UnlimitedAttempts
							=> unlimitedAttempts;

						public Platform.DataAndExt.Prefs.IntItem MaxAttempts
							=> maxAttempts;

						public Platform.DataAndExt.Prefs.Item<string> DefQuitMsg
							=> defQuitMsg;
					#endregion

					#region Methods
						public virtual DTO.IrcDTO<GlobalDtoType>.GlobalDTO.ConnDTO ToDTO()
							=> new(enableIndent.CurVal, autoReconnect.CurVal, rejoinAfterKick.CurVal,
								characterEncoding.CurVal, unlimitedAttempts.CurVal, maxAttempts.CurVal, defQuitMsg
								.CurVal);
					#endregion

					#region Event Handlers
					#endregion
				}
			#endregion

			#region Members
				private readonly AutoPerformPrefs autoPerform;

				private readonly DccPrefs dcc;

				private readonly ConnPrefs conn;

				private readonly AliasesPrefs aliases;

				private readonly AltNicksPrefs altNicks;

				private readonly StalkWordsPrefs stalkWords;
			#endregion

			#region Properties
				public AutoPerformPrefs AutoPerform
					=> autoPerform;

				public DccPrefs DCC
					=> dcc;

				public ConnPrefs Conn
					=> conn;

				public AliasesPrefs Aliases
					=> aliases;

				public AltNicksPrefs AltNicks
					=> altNicks;

				public StalkWordsPrefs StalkWords
					=> stalkWords;
			#endregion

			#region Methods
				public abstract GlobalDtoType ToDTO();
			#endregion

			#region Event Handlers
			#endregion
		}

		public class NetworkPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
				public NetworkPrefs(Prefs<GlobalPrefsType, GlobalDtoType> mgrParent, Defs.Net netOwner) :
					base(mgrParent, "IRC", PrefsRsrcs.strNetTitle, PrefsRsrcs.strNetTitle)
				{
					OwnerNet = netOwner;

					timeStamps = new(this);
					dcc = new(this);
					autoPeform = new(this);
					conn = new(this);
					aliases = new(this, instance!.Global.Aliases);
					altNicks = new(this, instance!.Global.AltNicks);
					notifyWhenOnline = new(this);
					stalkWords = new(this, instance!.Global.StalkWords);
					knownChans = new(this);
				}

				public NetworkPrefs(Prefs<GlobalPrefsType, GlobalDtoType> mgrParent, DTO.IrcDTO<GlobalDtoType>
						.NetworkDTO dto) :
					base(mgrParent, "IRC", PrefsRsrcs.strNetTitle, PrefsRsrcs.strNetTitle)
				{
					OwnerNet = (Defs.Net?)Defs.Net.AllInstancesByGUID[dto.OwnerNet] ?? throw new System.
						Exception($"While loading IRC preferences, found preferences for {dto.OwnerNet}, " +
						"but we can't find that network.");

					timeStamps = new(this, dto.TimeStamps);
					dcc = new(this, dto.DCC);
					autoPeform = new(this, dto.AutoPerform);
					conn = new(this, dto.Conn);
					aliases = new(this, dto.Aliases, instance!.Global.Aliases);
					altNicks = new(this, dto.AltNicks, instance!.Global.AltNicks);
					notifyWhenOnline = new(this, dto.NotifyWhenOnline);
					stalkWords = new(this, dto.StalkWords, instance!.Global.StalkWords);
					knownChans = new(this, dto.KnownChans);
				}
			#endregion

			#region Delegates
			#endregion

			#region Events
			#endregion

			#region Constants
			#endregion

			#region Helper Types
				public class InheritedItemEnabledStatus<InheritedType>(InheritedType inheritedItem, bool bStatus =
						true)
					: Platform.DataAndExt.Obj<InheritedItemEnabledStatus<InheritedType>>
				{
					public InheritedType InheritedItem => inheritedItem;

					public bool Status
					{
						get => bStatus;

						set
						{
							if(bStatus != value)
							{
								bStatus = value;

								MakeDirty();

								FireStatusChanged();
							}
						}
					}

					public event DFieldChanged<bool>? evtStatusChanged;

					private void FireStatusChanged()
					{
						FirePropChanged(nameof(Status));

						evtStatusChanged?.Invoke(this, !bStatus, bStatus);
					}
				}

				public class TimeStampPrefs : Platform.DataAndExt.Prefs.PrefsBase.GlobalPrefs.TimeStampPrefs
				{
					#region Constructors & Deconstructors
						public TimeStampPrefs(NetworkPrefs mgrParent) :
							base(mgrParent)
							=> @override = new(mgrParent, "Override", PrefsRsrcs
								.strNetTimeStampOverrideNetTitle, PrefsRsrcs.strNetTimeStampOverrideNetDesc,
								false);

						public TimeStampPrefs(NetworkPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.TimeStampDTO dto) :
							base(mgrParent, dto)
							=> @override = new(mgrParent, "Override", PrefsRsrcs
								.strNetTimeStampOverrideNetTitle, PrefsRsrcs.strNetTimeStampOverrideNetDesc,
								false, dto.Override);
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
						private readonly Platform.DataAndExt.Prefs.Item<bool> @override;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.Item<bool> Override
							=> @override;
					#endregion

					#region Methods
						public override DTO.IrcDTO<GlobalDtoType>.NetworkDTO.TimeStampDTO ToDTO()
							=> new(@override.CurVal, Show.CurVal, Fmt.CurVal);
					#endregion

					#region Event Handlers
					#endregion
				}

				public class DccPrefs : GlobalPrefs.DccPrefs
				{
					#region Constructors & Deconstructors
						public DccPrefs(NetworkPrefs mgrParent) :
							base(mgrParent)
							=> @override = new(mgrParent, "Override", PrefsRsrcs
								.strNetDccOverrideTitle, PrefsRsrcs.strNetDccOverrideDesc, false);

						public DccPrefs(NetworkPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.DccDTO dto) :
							base(mgrParent, dto)
							=> @override = new(mgrParent, "Override", PrefsRsrcs
								.strNetDccOverrideTitle, PrefsRsrcs.strNetDccOverrideDesc, false, dto.Override);
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
						private readonly Platform.DataAndExt.Prefs.Item<bool> @override;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.Item<bool> Override
							=> @override;
					#endregion

					#region Methods
						public override DTO.IrcDTO<GlobalDtoType>.NetworkDTO.DccDTO ToDTO()
							=> new(@override.CurVal, Enabled.CurVal, GetIpFromServer.CurVal, DownloadsFolder.CurVal,
								[.. Ports]);
					#endregion

					#region Event Handlers
					#endregion
				}

				public class AutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public AutoPerformPrefs(NetworkPrefs mgrParent) :
							base(mgrParent, "Auto-perform", PrefsRsrcs.strNetAutoPerformTitle, PrefsRsrcs
								.strNetAutoPerformDesc)
						{
							this.mgrParent = mgrParent;

							whenJoiningNet = new(this, "When joining this network", PrefsRsrcs
								.strNetAutoPerformWhenJoiningNetTitle, PrefsRsrcs.strNetAutoPerformWhenJoiningNetDesc,
								instance!.Global.AutoPerform.WhenJoiningNet);
							whenJoiningChan = new(this, "When joining any channel on this network",
								PrefsRsrcs.strNetAutoPerformWhenJoiningChanTitle, PrefsRsrcs
								.strNetAutoPerformWhenJoiningChanDesc, instance!.Global.AutoPerform.WhenJoiningChan);
							whenOpeningUserChat = new(this, "When opening chat with any user on this" +
								" network", PrefsRsrcs.strNetAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
								.strNetAutoPerformWhenOpeningUserChatDesc, instance!.Global.AutoPerform
								.WhenOpeningUserChat);
						}

						public AutoPerformPrefs(NetworkPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.AutoPerformDTO dto)
							: base(mgrParent, "Auto-perform", PrefsRsrcs.strNetAutoPerformTitle, PrefsRsrcs
								.strNetAutoPerformDesc)
						{
							this.mgrParent = mgrParent;

							whenJoiningNet = new(this, "When joining this network", PrefsRsrcs
								.strNetAutoPerformWhenJoiningNetTitle, PrefsRsrcs.strNetAutoPerformWhenJoiningNetDesc,
								dto.WhenJoiningNet, instance!.Global.AutoPerform.WhenJoiningNet);
							whenJoiningChan = new(this, "When joining any channel on this network",
								PrefsRsrcs.strNetAutoPerformWhenJoiningChanTitle, PrefsRsrcs
								.strNetAutoPerformWhenJoiningChanDesc, dto.WhenJoiningChan, instance!.Global
								.AutoPerform.WhenJoiningChan);
							whenOpeningUserChat = new(this, "When opening chat with any user on this" +
								" network", PrefsRsrcs.strNetAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
								.strNetAutoPerformWhenOpeningUserChatDesc, dto.WhenOpeningUserChat, instance!.Global
								.AutoPerform.WhenOpeningUserChat);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class OnEvtPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public OnEvtPrefs(AutoPerformPrefs mgrParent, in string strName,
										in string strLocalizedName, in string strLocalizedDesc, GlobalPrefs
										.AutoPerformPrefs.OnEvtPrefs inheritedSettings) :
									base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
								{
									this.mgrParent = mgrParent;
									this.inheritedSettings = inheritedSettings;

									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs
										.OnEvtPrefs.OneStep>> enabledInheritedSteps = 
											from stepCur in inheritedSettings.Steps
											select new InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.OneStep>(stepCur);
									mapAllInheritanceOverrides = new(this, "Steps"
										+ " that are disabled", PrefsRsrcs.strNetAutoPerformOnEvtDisabledStepsTitle, PrefsRsrcs
										.strNetAutoPerformOnEvtDisabledStepsDesc, enabledInheritedSteps, KeyObtainer)
									{
										DefTester = TestCurValForDef,
										ResetToDefMethod = ResetCurValToDef,
									};

									additionalSteps = new(this, "Steps", strLocalizedName,
										strLocalizedDesc, []);
								}

								public OnEvtPrefs(AutoPerformPrefs mgrParent, in string strName,
										in string strLocalizedName, in string strLocalizedDesc, DTO.IrcDTO<GlobalDtoType>.NetworkDTO
										.AutoPerformDTO.OnEvtDTO dto, GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
										inheritedSettings) :
									base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
								{
									this.mgrParent = mgrParent;
									this.inheritedSettings = inheritedSettings;


									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs
										.OnEvtPrefs.OneStep>> defEnabledInheritedSteps =
											from stepCur in inheritedSettings.Steps
											select new InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.OneStep>(stepCur);
									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs
										.OnEvtPrefs.OneStep>> curEnabledInheritedSteps =
											from stepCur in inheritedSettings.Steps
											select new InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.OneStep>(stepCur, !dto?.DisabledInheritedSteps?.Contains(stepCur.guid) ?? false);
									mapAllInheritanceOverrides = new(this, "Steps"
										+ " that are disabled", PrefsRsrcs.strNetAutoPerformOnEvtDisabledStepsTitle, PrefsRsrcs
										.strNetAutoPerformOnEvtDisabledStepsDesc, defEnabledInheritedSteps, curEnabledInheritedSteps,
										KeyObtainer)
									{
										DefTester = TestCurValForDef,
										ResetToDefMethod = ResetCurValToDef,
									};

									additionalSteps = new(this, "Steps", strLocalizedName,
										strLocalizedDesc, [], dto.AddedSteps == null
											? []
											: dto.AddedSteps.Select(dstepCur => new OneStep(dstepCur)));
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
							#endregion

							#region Constants
							#endregion

							#region Helper Types
								public class OneStep : Platform.DataAndExt.Obj<OneStep>, GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
									.IReadOnlyOneStep
								{
									#region Constructors & Deconstructors
										public OneStep(in string strWhatToDo)
											=> this.strWhatToDo = strWhatToDo;

										public OneStep(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO dto) :
											base(dto.GUID)
											=> strWhatToDo = dto.WhatToDo;
									#endregion

									#region Delegates
									#endregion

									#region Events
										public event DFieldChanged<string>? evtWhatToDoChanged;
									#endregion

									#region Constants
									#endregion

									#region Helper Types
									#endregion

									#region Members
										private string strWhatToDo;
									#endregion

									#region Properties
										public string WhatToDo
										{
											get => strWhatToDo;

											set
											{
												if(strWhatToDo != value)
												{
													string strOldWhatToDo = strWhatToDo;

													strWhatToDo = value;

													MakeDirty();

													FireWhatToDoChanged(strOldWhatToDo);
												}
											}
										}
									#endregion

									#region Methods
										private void FireWhatToDoChanged(in string strOldWhatToDo)
										{
											FirePropChanged(nameof(strWhatToDo));

											evtWhatToDoChanged?.Invoke(this, strOldWhatToDo, strWhatToDo);
										}

										public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO ToDTO()
											=> new(guid, strWhatToDo);
									#endregion

									#region Event Handlers
									#endregion
								}
							#endregion

							#region Members
								public new readonly AutoPerformPrefs mgrParent; 

								public readonly GlobalPrefs.AutoPerformPrefs.OnEvtPrefs inheritedSettings;


								private readonly Platform.DataAndExt.Prefs.MappedListItem<string,
									InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs.OneStep>>
									mapAllInheritanceOverrides;

								private readonly Platform.DataAndExt.Prefs.ReorderableListItem<OneStep> additionalSteps;
							#endregion

							#region Properties
								public Platform.DataAndExt.Prefs.MappedListItem<string, InheritedItemEnabledStatus<GlobalPrefs
									.AutoPerformPrefs.OnEvtPrefs.OneStep>> AllInheritanceOverrides
										=> mapAllInheritanceOverrides;


								public Platform.DataAndExt.Prefs.ReorderableListItem<OneStep> AdditionalSteps
									=> additionalSteps;
							#endregion

							#region Methods
								public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.AutoPerformDTO.OnEvtDTO ToDTO()
									=> new(
										(
											from InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs.OneStep>
												itemCur in mapAllInheritanceOverrides.Values
											where !itemCur.Status
											select itemCur.InheritedItem.guid
										).ToArray(),
										additionalSteps.Select(
											stepCur
												=> new DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO(stepCur.guid, stepCur.WhatToDo)
											).ToArray()
									);

								private static string KeyObtainer(InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs
									.OnEvtPrefs.OneStep> item)
									=> item.InheritedItem.WhatToDo;

								private bool TestCurValForDef(System.Collections.Generic
									.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs.OneStep>>
									entries)
								{
									foreach(InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs.OneStep>
											entryCur in mapAllInheritanceOverrides.Def)
										if(!mapAllInheritanceOverrides[entryCur.InheritedItem.WhatToDo].Status)
											return false;

									return false;
								}

								private void ResetCurValToDef()
								{
									foreach(InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs.OneStep> entryCur in
											mapAllInheritanceOverrides.Def)
										entryCur.Status = true;
								}
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						public new readonly NetworkPrefs mgrParent;

						private readonly OnEvtPrefs whenJoiningNet;

						private readonly OnEvtPrefs whenJoiningChan;

						private readonly OnEvtPrefs whenOpeningUserChat;
					#endregion

					#region Properties
						public OnEvtPrefs WhenJoiningNet
							=> whenJoiningNet;

						public OnEvtPrefs WhenJoiningChan
							=> whenJoiningChan;

						public OnEvtPrefs WhenOpeningUserChat
							=> whenOpeningUserChat;
					#endregion

					#region Methods
						public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.AutoPerformDTO ToDTO()
							=> new(whenJoiningNet.ToDTO(), whenJoiningChan.ToDTO(), whenOpeningUserChat
								.ToDTO());
					#endregion

					#region Event Handlers
					#endregion
				}

				public class ConnPrefs : GlobalPrefs.ConnPrefs
				{
					#region Constructors & Deconstructors
						public ConnPrefs(NetworkPrefs mgrParent) :
							base(mgrParent)
							=> @override = new(mgrParent, "Override", PrefsRsrcs
								.strNetConnOverrideTitle, PrefsRsrcs.strNetConnOverrideDesc, false);

						public ConnPrefs(NetworkPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.ConnDTO dto) :
							base(mgrParent)
							=> @override = new(mgrParent, "Override", PrefsRsrcs
								.strNetConnOverrideTitle, PrefsRsrcs.strNetConnOverrideDesc, false, dto.Override);
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
						private readonly Platform.DataAndExt.Prefs.Item<bool> @override;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.Item<bool> Override
							=> @override;
					#endregion

					#region Methods
						public override DTO.IrcDTO<GlobalDtoType>.NetworkDTO.ConnDTO ToDTO()
							=> new(@override.CurVal, EnableIdent.CurVal, AutoReconnect.CurVal, RejoinAfterKick.CurVal,
								CharEncoding.CurVal, UnlimitedAttempts.CurVal, MaxAttempts.CurVal, DefQuitMsg.CurVal);
					#endregion

					#region Event Handlers
					#endregion
				}

				public class AliasesPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public AliasesPrefs(NetworkPrefs mgrParent, GlobalPrefs.AliasesPrefs inheritedSettings) :
							base(mgrParent, "Alias overrides for this network", PrefsRsrcs.strNetAliasTitle, PrefsRsrcs.strNetAliasDesc)
						{
							this.mgrParent = mgrParent;
							this.inheritedSettings = inheritedSettings;

							System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>>
								defEnabledAliases = inheritedSettings.Entries.Values.Select(aliasCur
									=> new InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>(aliasCur)
								);
							mapAllInheritanceOverridesByName = new(this, "Which "
								+ "Inherited Aliases are Enabled", PrefsRsrcs.strNetAliasesInheritedTitle, PrefsRsrcs
								.strNetAliasesInheritedDesc, defEnabledAliases, KeyObtainer);

							addedAliases = new(this, "Additional Aliases", PrefsRsrcs
								.strNetAliasesAdditionalTitle, PrefsRsrcs.strNetAliasesAdditionalDesc, [],
								KeyObtainer);
						}

						public AliasesPrefs(NetworkPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.AliasesDTO dto, GlobalPrefs
								.AliasesPrefs inheritedSettings) :
							base(mgrParent, "Alias overrides for this network", PrefsRsrcs.strNetAliasTitle, PrefsRsrcs
								.strNetAliasDesc)
						{
							this.mgrParent = mgrParent;
							this.inheritedSettings = inheritedSettings;


							System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>>
								defEnabledAliases = inheritedSettings.Entries.Values.Select(aliasCur
									=> new InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>(aliasCur)
								);
							System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>>
								enabledAliases = inheritedSettings.Entries.Values.Select(aliasCur
									=> new InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>(aliasCur, dto?
										.DisabledInheritedAliases?.Contains(aliasCur.guid) ?? false)
								);
							mapAllInheritanceOverridesByName = new(this, "Which "
								+ "Inherited Aliases are Enabled", PrefsRsrcs.strNetAliasesInheritedTitle, PrefsRsrcs
								.strNetAliasesInheritedDesc, defEnabledAliases, enabledAliases, KeyObtainer);

							addedAliases = new(this, "Additional Aliases", PrefsRsrcs
								.strNetAliasesAdditionalTitle, PrefsRsrcs.strNetAliasesAdditionalDesc, [], dto?
								.AddedAliases?.Select(daliasCur
									=> new OneAlias(daliasCur, this)) ?? [],
								KeyObtainer);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class OneAlias : Platform.DataAndExt.Obj<OneAlias>, GlobalPrefs.AliasesPrefs.IReadOnlyOneAlias
						{
							#region Constructors & Deconstructors
								public OneAlias(in string strName, in string strCmd, in AliasesPrefs parent)
								{
									this.strName = strName;
									this.strCmd = strCmd;

									this.parent = parent;
								}

								public OneAlias(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO dto, in AliasesPrefs parent)
								{
									strName = dto.Name;
									strCmd = dto.Cmd;

									this.parent = parent;
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public event DFieldChanged<string>? evtNameChanged;

								public event DFieldChanged<string>? evtCmdChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								public readonly AliasesPrefs parent;

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

								public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO ToDTO()
									=> new(guid, strName, strCmd);
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						public new readonly NetworkPrefs mgrParent;

						public readonly GlobalPrefs.AliasesPrefs inheritedSettings;


						private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string,
							InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>> mapAllInheritanceOverridesByName;

						private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneAlias> addedAliases;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.MappedObjListItem<string, InheritedItemEnabledStatus<GlobalPrefs
								.AliasesPrefs.OneAlias>> AllInheritanceOverridesByName
							=> mapAllInheritanceOverridesByName;

						public Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneAlias> AddedAliases
							=> addedAliases;
					#endregion

					#region Methods
						public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.AliasesDTO ToDTO()
							=> new(
								(
									from InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias> ialiasCur in
										mapAllInheritanceOverridesByName.Values
									where !ialiasCur.Status
									select ialiasCur.InheritedItem.guid
								)
								.ToArray(),
								addedAliases.Values.Select(
									aliasCur
										=> new DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO(aliasCur.guid, aliasCur.Name, aliasCur.Cmd)
									).ToArray()
							);

						private string KeyObtainer(InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias> status)
							=> status.InheritedItem.Name;

						private string KeyObtainer(OneAlias aliasToLookUpKeyFor)
							=> aliasToLookUpKeyFor.Name;
					#endregion

					#region Event Handlers
					#endregion
				}

				public class AltNicksPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public AltNicksPrefs(NetworkPrefs mgrParent, in GlobalPrefs.AltNicksPrefs
								inheritedSettings) :
							base(mgrParent, "Alternate Nicks", PrefsRsrcs.strNetAltNicksTitle, PrefsRsrcs
								.strNetAltNicksDesc)
						{
							this.mgrParent = mgrParent;
							this.inheritedSettings = inheritedSettings;

							System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AltNicksPrefs
								.OneAltNick>> defInheritedEntries = inheritedSettings.Entries.Select(ianick
									=> new InheritedItemEnabledStatus<GlobalPrefs.AltNicksPrefs.OneAltNick>(ianick));
							mapAllInheritanceOverridesByNick = new(this,
								"Alternate nicks from global settings", PrefsRsrcs.strNetAltNicksInheritedTitle,
								PrefsRsrcs.strNetAltNicksInheritedDesc, defInheritedEntries, KeyObtainer);

							additionalAltNicks = new(this, "Lists more alternate nicks specific to " +
								"this network", PrefsRsrcs.strNetAltNicksAdditionalTitle, PrefsRsrcs.strNetAltNicksAdditionalDesc,
								[]);
						}

						public AltNicksPrefs(NetworkPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.AltNicksDTO dto, in GlobalPrefs
								.AltNicksPrefs inheritedSettings) :
							base(mgrParent, "Alternate Nicks", PrefsRsrcs.strNetAltNicksTitle, PrefsRsrcs
								.strNetAltNicksDesc)
						{
							this.mgrParent = mgrParent;
							this.inheritedSettings = inheritedSettings;


							System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AltNicksPrefs
								.OneAltNick>> defInheritedEntries = inheritedSettings.Entries.Select(ianickCur
									=> new InheritedItemEnabledStatus<GlobalPrefs.AltNicksPrefs.OneAltNick>(ianickCur));
							System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AltNicksPrefs
								.OneAltNick>> enabledAltNicks = inheritedSettings.Entries.Select(ianickCur
									=> new InheritedItemEnabledStatus<GlobalPrefs.AltNicksPrefs.OneAltNick>(ianickCur, dto?
										.DisabledInheritedNicks?.Contains(ianickCur.guid) ?? false)
								);
							mapAllInheritanceOverridesByNick = new(this,
								"Alternate nicks from global settings", PrefsRsrcs.strNetAltNicksInheritedTitle,
								PrefsRsrcs.strNetAltNicksInheritedDesc, defInheritedEntries, enabledAltNicks, KeyObtainer);

							additionalAltNicks = new(this, "Lists more alternate nicks specific to " +
								"this network", PrefsRsrcs.strNetAltNicksAdditionalTitle, PrefsRsrcs.strNetAltNicksAdditionalDesc,
								dto?.AddedNicks?.Select(danickCur
									=> new OneAltNick(danickCur, this))
								?? []);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class OneAltNick : Platform.DataAndExt.Obj<OneAltNick>, GlobalPrefs.AltNicksPrefs
							.IReadOnlyOneAltNick
						{
							#region Constructors & Deconstructors
								public OneAltNick(in string strNickToUse, in AltNicksPrefs parent)
								{
									this.strNickToUse = strNickToUse;

									this.parent = parent;
								}

								public OneAltNick(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO dto, in AltNicksPrefs parent) :
									base(dto.GUID)
								{
									strNickToUse = dto.NickToUse;

									this.parent = parent;
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public event DFieldChanged<string>? evtNickToUseChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								public readonly AltNicksPrefs parent;

								private string strNickToUse;
							#endregion

							#region Properties
								public string NickToUse
								{
									get => strNickToUse;

									protected set
									{
										if(strNickToUse != value)
										{
											string strOldNickToUse = strNickToUse;

											strNickToUse = value;

											FireNickToUseChanged(strOldNickToUse);

											MakeDirty();
										}
									}
								}
							#endregion

							#region Methods
								private void FireNickToUseChanged(in string strOldName)
								{
									FirePropChanged(nameof(NickToUse));

									evtNickToUseChanged?.Invoke(this, strOldName, strNickToUse);
								}

								public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO ToDTO()
									=> new(guid, strNickToUse);
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						public new readonly NetworkPrefs mgrParent;

						public readonly GlobalPrefs.AltNicksPrefs inheritedSettings;


						private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string,
							InheritedItemEnabledStatus<GlobalPrefs.AltNicksPrefs.OneAltNick>> mapAllInheritanceOverridesByNick;

						private readonly Platform.DataAndExt.Prefs.ReorderableObjListItem<OneAltNick> additionalAltNicks;
					#endregion

					#region Properties
						public System.Collections.Generic.IReadOnlyDictionary<string, InheritedItemEnabledStatus<GlobalPrefs
							.AltNicksPrefs.OneAltNick>> AllInheritanceOverridesByNick
								=> mapAllInheritanceOverridesByNick;

						public Platform.DataAndExt.Prefs.ReorderableObjListItem<OneAltNick> Entries
							=> additionalAltNicks;
					#endregion

					#region Methods
						public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.AltNicksDTO ToDTO()
							=> new(
								(
									from InheritedItemEnabledStatus<GlobalPrefs.AltNicksPrefs.OneAltNick> entryCur in
										mapAllInheritanceOverridesByNick.Values
									where !entryCur.Status
									select entryCur.InheritedItem.guid
								).ToArray(),
								mapAllInheritanceOverridesByNick.Values.Select(ianickCur
										=> new DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO(ianickCur.guid, ianickCur.InheritedItem.NickToUse)
									).ToArray()
							);

						private string KeyObtainer(InheritedItemEnabledStatus<GlobalPrefs.AltNicksPrefs.OneAltNick> status)
							=> status.InheritedItem.NickToUse;
					#endregion

					#region Event Handlers
					#endregion
				}

				public class StalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public StalkWordsPrefs(NetworkPrefs mgrParent, GlobalPrefs.StalkWordsPrefs
								inheritedSettings) :
							base(mgrParent, "Stalk words", PrefsRsrcs.strNetStalkWordsTitle, PrefsRsrcs
								.strNetStalkWordsDesc)
						{
							this.mgrParent = mgrParent;
							this.inheritedSettings = inheritedSettings;

							System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
								.OneStalkWord>> defInheritedEntries = inheritedSettings.Entries.Values
									.Select(iswCur
										=> new InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.OneStalkWord>(iswCur));
							mapAllInheritanceOverridesByCtnts = new(this,
								"Stalk words from global settings", PrefsRsrcs.strNetStalkWordsInheritedTitle,
								PrefsRsrcs.strNetStalkWordsInheritedDesc, defInheritedEntries, KeyObtainer);

							addedStalkWords = new(this, "Lists more stalk words specific to " +
								"this network", PrefsRsrcs.strNetStalkWordsAdditionalTitle, PrefsRsrcs
								.strNetStalkWordsAdditionalDesc, [], KeyObtainer);
						}

						public StalkWordsPrefs(NetworkPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.StalkWordsDTO dto,
								GlobalPrefs.StalkWordsPrefs inheritedSettings) :
							base(mgrParent, "Stalk words", PrefsRsrcs.strNetStalkWordsTitle, PrefsRsrcs
								.strNetStalkWordsDesc)
						{
							this.mgrParent = mgrParent;
							this.inheritedSettings = inheritedSettings;


							System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
								.OneStalkWord>> defInheritedEntries = inheritedSettings.Entries.Values
									.Select(iswCur
										=> new InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.OneStalkWord>(iswCur));
							System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
								.OneStalkWord>> enabledAltNicks = inheritedSettings.Entries.Values.Select(iswCur
									=> new InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.OneStalkWord>(iswCur, dto?
										.DisabledInheritedStalkWords?.Contains(iswCur.guid) ?? false)
								);
							mapAllInheritanceOverridesByCtnts = new(this,
								"Stalk words from global settings", PrefsRsrcs.strNetStalkWordsInheritedTitle,
								PrefsRsrcs.strNetStalkWordsInheritedDesc, defInheritedEntries, enabledAltNicks, KeyObtainer);

							addedStalkWords = new(this, "Lists more stalk words specific to " +
								"this network", PrefsRsrcs.strNetStalkWordsAdditionalTitle, PrefsRsrcs
								.strNetStalkWordsAdditionalDesc, [], dto?.AddedStalkWords?
								.Select(dswCur
									=> new OneStalkWord(dswCur, this))
								?? [], KeyObtainer);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class OneStalkWord : Platform.DataAndExt.Obj<OneStalkWord>, GlobalPrefs.StalkWordsPrefs
							.IReadOnlyOneStalkWord
						{
							#region Constructors & Deconstructors
								public OneStalkWord(in string strCtnts, in StalkWordsPrefs parent)
								{
									this.strCtnts = strCtnts;

									this.parent = parent;
								}

								public OneStalkWord(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO dto, in StalkWordsPrefs
									parent)
								{
									strCtnts = dto.Ctnts;

									this.parent = parent;
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public event DFieldChanged<string>? evtCtntsChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								public readonly StalkWordsPrefs parent;

								private string strCtnts;
							#endregion

							#region Properties
								public string Ctnts
								{
									get => strCtnts;

									set
									{
										if(strCtnts != value)
										{
											string strOldCtnts = strCtnts;

											strCtnts = value;

											MakeDirty();

											FireCtntsChanged(strOldCtnts);
										}
									}
								}
							#endregion

							#region Methods
								private void FireCtntsChanged(string strOldCtnts)
								{
									FirePropChanged(nameof(Ctnts));

									evtCtntsChanged?.Invoke(this, strOldCtnts, strCtnts);
								}

								public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO ToDTO()
									=> new(guid, strCtnts);
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						public new readonly NetworkPrefs mgrParent;

						public readonly GlobalPrefs.StalkWordsPrefs inheritedSettings;


						private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string,
							InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.OneStalkWord>>
							mapAllInheritanceOverridesByCtnts;

						private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneStalkWord> addedStalkWords;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.MappedObjListItem<string, InheritedItemEnabledStatus<GlobalPrefs
							.StalkWordsPrefs.OneStalkWord>> AllInheritanceOverridesByCtnts
								=> mapAllInheritanceOverridesByCtnts;

						public Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneStalkWord> AddedStalkWords
							=> addedStalkWords;
					#endregion

					#region Methods
						public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.StalkWordsDTO ToDTO()
							=> new(
								(
									from InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.OneStalkWord> iswCur in
										mapAllInheritanceOverridesByCtnts.Values
									where !iswCur.Status
									select iswCur.InheritedItem.guid
								).ToArray(),
								addedStalkWords.Values.Select(swCur => new DTO.IrcDTO<GlobalDtoType>.GlobalDTO
									.OneStalkWordDTO(swCur.guid, swCur.Ctnts)).ToArray()
							);

						private string KeyObtainer(InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.OneStalkWord> iswCur)
							=> iswCur.InheritedItem.Ctnts;

						private string KeyObtainer(OneStalkWord swCur)
							=> swCur.Ctnts;
					#endregion

					#region Event Handlers
					#endregion
				}

				public class NotifyWhenOnlinePrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public NotifyWhenOnlinePrefs(in NetworkPrefs mgrParent) :
							base(mgrParent, "Notify When Online", PrefsRsrcs.strNetNotifyWhenOnlineTitle,
								PrefsRsrcs.strNetNotifyWhenOnlineDesc)
							=> entries = new(this, "Notify when online", PrefsRsrcs
								.strNetNotifyTitle, PrefsRsrcs.strNetNotifyDesc, [], KeyObtainer);

						public NotifyWhenOnlinePrefs(in NetworkPrefs mgrParent, in string[]? dto) :
							base(mgrParent, "Notify When Online", PrefsRsrcs.strNetNotifyWhenOnlineTitle,
								PrefsRsrcs.strNetNotifyWhenOnlineDesc)
							=> entries = new(this, "Notify when online", PrefsRsrcs
								.strNetNotifyTitle, PrefsRsrcs.strNetNotifyDesc, [], dto ?? [], KeyObtainer);
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
						private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, string> entries;
					#endregion

					#region Properties
						public Platform.DataAndExt.Prefs.MappedSortedListItem<string, string> Entries
							=> entries;
					#endregion

					#region Methods
						public string[]? ToDTO()
							=> [.. entries.Values];

						private string KeyObtainer(string strVal)
							=> strVal;
					#endregion

					#region Event Handlers
					#endregion
				}

				public class ChanPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public ChanPrefs(ChanListPrefs mgrParent, Chan chanOwner) :
							base(mgrParent, "Preferences for one channel", PrefsRsrcs.strNetChanTitle,
								PrefsRsrcs.strNetChanDesc)
						{
							OwnerChan = chanOwner;

							timeStamps = new(this);
							aliases = new(this, mgrParent.mgrParent.aliases);
							autoPeform = new(this, mgrParent.mgrParent.autoPeform);
							stalkWords = new(this, mgrParent.mgrParent.stalkWords);
						}

						public ChanPrefs(ChanListPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.ChanDTO dto) :
							base(mgrParent, "Preferences for one channel", PrefsRsrcs.strNetChanTitle,
								PrefsRsrcs.strNetChanDesc)
						{
							if(!Chan.AllChanByName.ContainsKey(dto.OwnerChan))
								throw new System.InvalidProgramException("We have a preference for a channel we can't find " +
									"in the network definition.  Did something load out of order?");
							OwnerChan = Chan.AllChanByName[dto.OwnerChan];

							timeStamps = new(this, dto.TimeStamps);
							aliases = new(this, dto.Aliases, mgrParent.mgrParent.aliases);
							autoPeform = new(this, dto.AutoPerform, mgrParent.mgrParent.autoPeform);
							stalkWords = new(this, dto.StalkWords, mgrParent.mgrParent.stalkWords);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class InheritedItemEnabledStatus<InheritedType>(InheritedType inheritedItem, bool
								bStatus, InheritedItemEnabledStatus<InheritedType>.InheritedFromTypes ifSrc, string
								strDescOfInheritedType, Defs.Net net)
							: Platform.DataAndExt.Obj<InheritedItemEnabledStatus<InheritedType>>
						{
							public InheritedType InheritedItem => inheritedItem;

							public bool Status
							{
								get => bStatus;

								set
								{
									if(bStatus != value)
									{
										bStatus = value;

										MakeDirty();

										FireStatusChanged();
									}
								}
							}

							public event DFieldChanged<bool>? evtStatusChanged;

							private void FireStatusChanged()
							{
								FirePropChanged(nameof(Status));

								evtStatusChanged?.Invoke(this, !bStatus, bStatus);
							}

							public enum InheritedFromTypes : byte
							{
								network,
								global,
							}

							public InheritedFromTypes InheritedFromSrc => ifSrc;

							public string InheritedFromAsText => ifSrc switch
							{
								InheritedFromTypes.network
									=> PrefsRsrcs.strNetChanInheritedFromNetText,
								InheritedFromTypes.global
									=> PrefsRsrcs.strNetChanInheritedFromGlobalText,
								_
									=> throw new Platform.DataAndExt.Exceptions
									.UnknownOrInvalidEnumException<InheritedFromTypes>(ifSrc, "While returning a textual " +
										"description of the source of this inherited item"),
							};

							public string InheritedFromAsTextDesc => ifSrc switch
							{
								InheritedFromTypes.network
									=> PrefsRsrcs.strNetChanInheritedFromNetDesc.Fmt(strDescOfInheritedType, net.Name),
								InheritedFromTypes.global
									=> PrefsRsrcs.strNetChanInheritedFromGlobalText.Fmt(strDescOfInheritedType),
								_
									=> throw new Platform.DataAndExt.Exceptions
									.UnknownOrInvalidEnumException<InheritedFromTypes>(ifSrc, "While returning a textual " +
										"description of the source of this inherited item"),
							};
						}

						public class TimeStampPrefs : Platform.DataAndExt.Prefs.PrefsBase.GlobalPrefs
							.TimeStampPrefs
						{
							#region Constructors & Deconstructors
								public TimeStampPrefs(ChanPrefs mgrParent) :
									base(mgrParent)
									=> @override = new(mgrParent, "Override", PrefsRsrcs
										.strNetTimeStampOverrideNetTitle, PrefsRsrcs.strNetTimeStampOverrideNetDesc,
										false);

								public TimeStampPrefs(ChanPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.TimeStampDTO dto) :
									base(mgrParent)
									=> @override = new(mgrParent, "Override", PrefsRsrcs
										.strNetTimeStampOverrideNetTitle, PrefsRsrcs.strNetTimeStampOverrideNetDesc,
										false, dto.Override);
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
								private readonly Platform.DataAndExt.Prefs.Item<bool> @override;
							#endregion

							#region Properties
								public Platform.DataAndExt.Prefs.Item<bool> Override
									=> @override;
							#endregion

							#region Methods
								public override DTO.IrcDTO<GlobalDtoType>.NetworkDTO.TimeStampDTO ToDTO()
									=> new(@override.CurVal, Show.CurVal, Fmt.CurVal);
							#endregion

							#region Event Handlers
							#endregion
						}

						public class AliasesPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public AliasesPrefs(ChanPrefs mgrParent, NetworkPrefs.AliasesPrefs inheritedSettings)
									: base(mgrParent, "Alias overrides for this network", PrefsRsrcs
										.strNetAliasTitle, PrefsRsrcs.strNetAliasDesc)
								{
									this.inheritedSettings = inheritedSettings;

									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias>> defEnabledAliases = (System.Collections.Generic
										.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias>>)inheritedSettings.AllInheritanceOverridesByName.Values
										.Select(ialias
											=> new InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>(ialias.InheritedItem,
												ialias.Status, InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>
												.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent
												.OwnerNet)
										);
									defEnabledAliases = defEnabledAliases.Concat((System.Collections.Generic
										.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias>>)inheritedSettings.AddedAliases.Values.Select(ialiasCur
											=> new InheritedItemEnabledStatus<NetworkPrefs.AliasesPrefs.OneAlias>(ialiasCur, true,
												InheritedItemEnabledStatus<NetworkPrefs.AliasesPrefs.OneAlias>.InheritedFromTypes.network,
												PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
									mapAllInheritanceOverridesByName =
										new(this, "Inherited aliases",
										PrefsRsrcs.strNetChanAliasInheritedTitle, PrefsRsrcs.strNetChanAliasInheritedDesc,
										defEnabledAliases, KeyObtainer, true);

									addedAliases = new(this, "Additional Aliases", PrefsRsrcs
										.strNetChanAliasesAdditionalTitle, PrefsRsrcs.strNetChanAliasesAdditionalDesc,
										[], KeyObtainer, true);
								}

								public AliasesPrefs(ChanPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.AliasesDTO dto, NetworkPrefs
									.AliasesPrefs inheritedSettings) :
									base(mgrParent, "Alias overrides for this network", PrefsRsrcs
										.strNetAliasTitle, PrefsRsrcs.strNetAliasDesc)
								{
									this.inheritedSettings = inheritedSettings;


									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias>> defEnabledInheritedAliases = (System.Collections.Generic
										.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias>>)inheritedSettings.AllInheritanceOverridesByName.Values
										.Select(ialias
											=> new InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>(ialias.InheritedItem,
												ialias.Status, InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>
												.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent
												.OwnerNet)
										);
									defEnabledInheritedAliases = defEnabledInheritedAliases.Concat((System.Collections.Generic
										.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias>>)inheritedSettings.AddedAliases.Values.Select(ialiasCur
											=> new InheritedItemEnabledStatus<NetworkPrefs.AliasesPrefs.OneAlias>(ialiasCur, true,
												InheritedItemEnabledStatus<NetworkPrefs.AliasesPrefs.OneAlias>.InheritedFromTypes.network,
												PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias>> enabledInheritedAliases = (System.Collections.Generic
										.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias>>)inheritedSettings.AllInheritanceOverridesByName.Values
										.Select(ialias
											=> new InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>(ialias.InheritedItem,
												!dto?.DisabledInheritedAliases?.Contains(ialias.InheritedItem.guid) ?? ialias.Status,
												InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.OneAlias>.InheritedFromTypes.global,
												PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)
										);
									enabledInheritedAliases = enabledInheritedAliases.Concat((System.Collections.Generic
										.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias>>)inheritedSettings.AddedAliases.Values.Select(ialiasCur
											=> new InheritedItemEnabledStatus<NetworkPrefs.AliasesPrefs.OneAlias>(ialiasCur, !dto?
												.DisabledInheritedAliases?.Contains(ialiasCur.guid) ?? true,
												InheritedItemEnabledStatus<NetworkPrefs.AliasesPrefs.OneAlias>.InheritedFromTypes.network,
												PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
									mapAllInheritanceOverridesByName =
										new(this, "Inherited aliases",
										PrefsRsrcs.strNetChanAliasInheritedTitle, PrefsRsrcs.strNetChanAliasInheritedDesc,
										defEnabledInheritedAliases, enabledInheritedAliases, KeyObtainer, true);

									addedAliases = new(this, "Additional Aliases", PrefsRsrcs
										.strNetChanAliasesAdditionalTitle, PrefsRsrcs.strNetChanAliasesAdditionalDesc,
										[], dto?.AddedAliases?.Select(daliasCur
											=> new OneAlias(daliasCur, this)
										) ?? [], KeyObtainer, true);
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
							#endregion

							#region Constants
							#endregion

							#region Helper Types
								public class OneAlias : Platform.DataAndExt.Obj<OneAlias>
								{
									#region Constructors & Deconstructors
										public OneAlias(in string strName, in string strCmd, in AliasesPrefs parent)
										{
											this.strName = strName;
											this.strCmd = strCmd;

											this.parent = parent;
										}

										public OneAlias(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO dto, in AliasesPrefs parent)
										{
											strName = dto.Name;
											strCmd = dto.Cmd;

											this.parent = parent;
										}
									#endregion

									#region Delegates
									#endregion

									#region Events
										public event DFieldChanged<string>? evtNameChanged;

										public event DFieldChanged<string>? evtCmdChanged;
									#endregion

									#region Constants
									#endregion

									#region Helper Types
									#endregion

									#region Members
										public readonly AliasesPrefs parent;

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

										public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO ToDTO()
											=> new(guid, strName, strCmd);
									#endregion

									#region Event Handlers
									#endregion
								}
							#endregion

							#region Members
								public readonly NetworkPrefs.AliasesPrefs inheritedSettings;


								private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string,
									InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.IReadOnlyOneAlias>>
									mapAllInheritanceOverridesByName;

								private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, OneAlias> addedAliases;
							#endregion

							#region Properties
								public Platform.DataAndExt.Prefs.MappedObjListItem<string,
									InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.IReadOnlyOneAlias>>
									AllInheritanceOverridesByName
										=> mapAllInheritanceOverridesByName;


								public Platform.DataAndExt.Prefs.MappedObjListItem<string, OneAlias> AddedAliases
									=> addedAliases;
							#endregion

							#region Methods
								public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.AliasesDTO ToDTO()
									=> new(
										(
											from InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs.IReadOnlyOneAlias> ialiasCur in
												mapAllInheritanceOverridesByName.Values
											where !ialiasCur.Status
											select ialiasCur.InheritedItem.GUID
										).ToArray(),
										addedAliases.Values.Select(aliasCur => new DTO.IrcDTO<GlobalDtoType>.GlobalDTO
											.OneAliasDTO(aliasCur.guid, aliasCur.Name, aliasCur.Cmd)).ToArray()
									);

								private static string KeyObtainer(InheritedItemEnabledStatus<GlobalPrefs.AliasesPrefs
										.IReadOnlyOneAlias> aliasCur)
									=> aliasCur.InheritedItem.Name;

								private static string KeyObtainer(OneAlias aliasCur)
									=> aliasCur.Name;
							#endregion

							#region Event Handlers
							#endregion
						}

						public class AutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public AutoPerformPrefs(ChanPrefs mgrParent, NetworkPrefs.AutoPerformPrefs inheritedSettings) :
									base(mgrParent, "Steps to take when joining this channel", PrefsRsrcs
										.strNetChanAutoPerformTitle, PrefsRsrcs.strNetChanAutoPerformDesc)
								{
									this.inheritedSettings = inheritedSettings;

									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs
										.OnEvtPrefs.IReadOnlyOneStep>> defEnabledAliases = inheritedSettings.WhenJoiningChan
										.AllInheritanceOverrides.Values.Select(istep
											=> new InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.IReadOnlyOneStep>(istep.InheritedItem, istep.Status,
												InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs.IReadOnlyOneStep>
												.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent
												.OwnerNet)
										);
									defEnabledAliases = defEnabledAliases.Concat(inheritedSettings.WhenJoiningChan.AdditionalSteps
										.Select(istepCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.IReadOnlyOneStep>(istepCur, true, InheritedItemEnabledStatus<GlobalPrefs
												.AutoPerformPrefs.OnEvtPrefs.IReadOnlyOneStep>.InheritedFromTypes.network, PrefsRsrcs
												.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
									mapAllInheritanceOverrides = new(this,
										"Inherited aliases", PrefsRsrcs.strNetChanAliasInheritedTitle, PrefsRsrcs
										.strNetChanAliasInheritedDesc, defEnabledAliases, KeyObtainer);

									addedSteps = new(this, "Additional Steps to run when joining this channel",
										PrefsRsrcs.strNetChanAutoPerformAddedTitle, PrefsRsrcs.strNetChanAutoPerformAddedDesc,
										[]);
								}

								public AutoPerformPrefs(ChanPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.ChanDTO.AutoPerformDTO dto,
										NetworkPrefs.AutoPerformPrefs inheritedSettings) :
									base(mgrParent, "Steps to take when joining this channel", PrefsRsrcs
										.strNetChanAutoPerformTitle, PrefsRsrcs.strNetChanAutoPerformDesc)
								{
									this.inheritedSettings = inheritedSettings;

									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs
										.OnEvtPrefs.IReadOnlyOneStep>> defEnabledAliases = inheritedSettings.WhenJoiningChan
										.AllInheritanceOverrides.Values.Select(istep
											=> new InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.IReadOnlyOneStep>(istep.InheritedItem, istep.Status,
												InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs.IReadOnlyOneStep>
												.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent
												.OwnerNet)
										);
									defEnabledAliases = defEnabledAliases.Concat(inheritedSettings.WhenJoiningChan.AdditionalSteps
										.Select(istepCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.IReadOnlyOneStep>(istepCur, true, InheritedItemEnabledStatus<GlobalPrefs
												.AutoPerformPrefs.OnEvtPrefs.IReadOnlyOneStep>.InheritedFromTypes.network, PrefsRsrcs
												.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs
										.OnEvtPrefs.IReadOnlyOneStep>> enabledInheritedSteps = inheritedSettings.WhenJoiningChan
										.AllInheritanceOverrides.Values.Select(istepCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.IReadOnlyOneStep>(istepCur.InheritedItem, !dto?.DisabledInheritedSteps?.Contains(istepCur
												.InheritedItem.guid) ?? istepCur.Status, InheritedItemEnabledStatus<GlobalPrefs
												.AutoPerformPrefs.OnEvtPrefs.IReadOnlyOneStep>.InheritedFromTypes.global,
												PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)
										);
									enabledInheritedSteps = enabledInheritedSteps.Concat(inheritedSettings.WhenJoiningChan
										.AdditionalSteps.Select(istepCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.IReadOnlyOneStep>(istepCur, !dto?.DisabledInheritedSteps?.Contains(istepCur.guid) ?? true,
												InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
												.IReadOnlyOneStep>.InheritedFromTypes.network,
												PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
									mapAllInheritanceOverrides = new(this,
										"Inherited aliases", PrefsRsrcs.strNetChanAliasInheritedTitle, PrefsRsrcs
										.strNetChanAliasInheritedDesc, enabledInheritedSteps, defEnabledAliases, KeyObtainer);

									addedSteps = new(this, "Additional Steps to run when joining this channel",
										PrefsRsrcs.strNetChanAutoPerformAddedTitle, PrefsRsrcs.strNetChanAutoPerformAddedDesc,
										[], dto?.AddedSteps?.Select(dstepCur => new NetworkPrefs
										.AutoPerformPrefs.OnEvtPrefs.OneStep(dstepCur)) ?? []);
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
								public readonly NetworkPrefs.AutoPerformPrefs inheritedSettings;


								private readonly Platform.DataAndExt.Prefs.MappedListItem<string,
									InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs.IReadOnlyOneStep>>
									mapAllInheritanceOverrides;

								private readonly Platform.DataAndExt.Prefs.ReorderableListItem<NetworkPrefs.AutoPerformPrefs
									.OnEvtPrefs.OneStep> addedSteps;
							#endregion

							#region Properties
								public Platform.DataAndExt.Prefs.MappedListItem<string, InheritedItemEnabledStatus<GlobalPrefs
									.AutoPerformPrefs.OnEvtPrefs.IReadOnlyOneStep>> AllInheritanceOverrides
										=> mapAllInheritanceOverrides;

								public Platform.DataAndExt.Prefs.ReorderableListItem<NetworkPrefs.AutoPerformPrefs.OnEvtPrefs
									.OneStep> AddedSteps
										=> addedSteps;
							#endregion

							#region Methods
								public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.ChanDTO.AutoPerformDTO ToDTO()
									=> new(
										(
											from InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs.IReadOnlyOneStep>
												istepCur in mapAllInheritanceOverrides.Values
											where !istepCur.Status
											select istepCur.InheritedItem.GUID
										).ToArray(),
										(
											from NetworkPrefs.AutoPerformPrefs.OnEvtPrefs.OneStep stepCur in addedSteps
											select new DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO(stepCur.GUID, stepCur.WhatToDo)
										).ToArray()
									);

								public static string KeyObtainer(InheritedItemEnabledStatus<GlobalPrefs.AutoPerformPrefs.OnEvtPrefs
										.IReadOnlyOneStep> istepCur)
									=> istepCur.InheritedItem.WhatToDo;
							#endregion

							#region Event Handlers
							#endregion
						}

						public class StalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public StalkWordsPrefs(ChanPrefs mgrParent, NetworkPrefs.StalkWordsPrefs inheritedSettings) :
									base(mgrParent, "Stalk words for this channel", PrefsRsrcs
										.strNetChanStalkWordsInheritedTitle, PrefsRsrcs.strNetChanStalkWordsInheritedDesc)
								{
									this.inheritedSettings = inheritedSettings;

									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
										.IReadOnlyOneStalkWord>> defEnabledAliases = inheritedSettings.AllInheritanceOverridesByCtnts.Values
										.Select(iswCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
												.IReadOnlyOneStalkWord>(iswCur.InheritedItem, iswCur.Status,
												InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>
												.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent
												.OwnerNet)
										);
									defEnabledAliases = defEnabledAliases.Concat(inheritedSettings.AddedStalkWords.Values
										.Select(iswCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>(iswCur,
												true, InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>
												.InheritedFromTypes.network, PrefsRsrcs.strAliasText, inheritedSettings.mgrParent
												.OwnerNet)));
									mapAllInheritanceOverridesByName =
										new(this, "Inherited " +
										"aliases", PrefsRsrcs.strNetChanAliasInheritedTitle, PrefsRsrcs.strNetChanAliasInheritedDesc,
										defEnabledAliases, KeyObtainer, true);

									addedStalkWords = new(this, "Additional Stalk Words",
										PrefsRsrcs.strNetChanStalkWordsAddedTitle, PrefsRsrcs.strNetChanStalkWordsAddedDesc,
										[], KeyObtainer, true);
								}

								public StalkWordsPrefs(ChanPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.StalkWordsDTO dto, NetworkPrefs
										.StalkWordsPrefs inheritedSettings) :
									base(mgrParent, "Stalk words for this channel", PrefsRsrcs
										.strNetChanStalkWordsInheritedTitle, PrefsRsrcs.strNetChanStalkWordsInheritedDesc)
								{
									this.inheritedSettings = inheritedSettings;

									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
										.IReadOnlyOneStalkWord>> defEnabledStalkWords = inheritedSettings.AllInheritanceOverridesByCtnts.Values
										.Select(iswCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
												.IReadOnlyOneStalkWord>(iswCur.InheritedItem, iswCur.Status,
												InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>
												.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent
												.OwnerNet)
										);
									defEnabledStalkWords = defEnabledStalkWords.Concat(inheritedSettings.AddedStalkWords.Values
										.Select(iswCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>(iswCur,
												true, InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>
												.InheritedFromTypes.network, PrefsRsrcs.strAliasText, inheritedSettings.mgrParent
												.OwnerNet)));
									System.Collections.Generic.IEnumerable<InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
										.IReadOnlyOneStalkWord>> enabledInheritedStalkWords = inheritedSettings
										.AllInheritanceOverridesByCtnts.Values.Select(iswCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>(iswCur
												.InheritedItem, !dto?.DisabledInheritedStalkWords?.Contains(iswCur.InheritedItem.guid) ??
												iswCur.Status, InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
												.IReadOnlyOneStalkWord>.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText,
												inheritedSettings.mgrParent.OwnerNet)
										);
									enabledInheritedStalkWords = enabledInheritedStalkWords.Concat(inheritedSettings.AddedStalkWords
										.Values.Select(iswCur
											=> new InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>(iswCur,
												!dto?.DisabledInheritedStalkWords?.Contains(iswCur.guid) ?? true,
												InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>
												.InheritedFromTypes.network, PrefsRsrcs.strAliasText, inheritedSettings.mgrParent
												.OwnerNet)));
									mapAllInheritanceOverridesByName =
										new(this, "Inherited " +
										"aliases", PrefsRsrcs.strNetChanAliasInheritedTitle, PrefsRsrcs.strNetChanAliasInheritedDesc,
										defEnabledStalkWords, enabledInheritedStalkWords, KeyObtainer, true);

									addedStalkWords = new(this, "Additional Stalk Words",
										PrefsRsrcs.strNetChanStalkWordsAddedTitle, PrefsRsrcs.strNetChanStalkWordsAddedDesc,
										[], KeyObtainer, true);
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
								public readonly NetworkPrefs.StalkWordsPrefs inheritedSettings;


								private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string,
									InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs.IReadOnlyOneStalkWord>>
									mapAllInheritanceOverridesByName;

								private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, NetworkPrefs.StalkWordsPrefs
									.OneStalkWord> addedStalkWords;
							#endregion

							#region Properties
								public Platform.DataAndExt.Prefs.MappedObjListItem<string, InheritedItemEnabledStatus<GlobalPrefs
									.StalkWordsPrefs.IReadOnlyOneStalkWord>> AllInheritanceOverridesByName
										=> mapAllInheritanceOverridesByName;


								public Platform.DataAndExt.Prefs.MappedObjListItem<string, NetworkPrefs.StalkWordsPrefs
									.OneStalkWord> AddedStalkWords
										=> addedStalkWords;
							#endregion

							#region Methods
								public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.StalkWordsDTO ToDTO()
									=> new(
										(from iswCur in mapAllInheritanceOverridesByName.Values
										where !iswCur.Status
										select iswCur.InheritedItem.GUID).ToArray()
									);

								private static string KeyObtainer(InheritedItemEnabledStatus<GlobalPrefs.StalkWordsPrefs
										.IReadOnlyOneStalkWord> iswCur)
									=> iswCur.InheritedItem.Ctnts;

								private static string KeyObtainer(NetworkPrefs.StalkWordsPrefs.OneStalkWord swCur)
									=> swCur.Ctnts;
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						private readonly TimeStampPrefs timeStamps;

						private readonly AliasesPrefs aliases;

						private readonly AutoPerformPrefs autoPeform;

						private readonly StalkWordsPrefs stalkWords;
					#endregion

					#region Properties
						public Chan OwnerChan
						{
							get;

							private init;
						}


						public TimeStampPrefs TimeStamps
							=> timeStamps;

						public AliasesPrefs Aliases
							=> aliases;

						public AutoPerformPrefs AutoPerform
							=> autoPeform;

						public StalkWordsPrefs StalkWords
							=> stalkWords;

						public override bool CanBeRemoved
							=> true;
					#endregion

					#region Methods
						public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.ChanDTO ToDTO()
							=> new(OwnerChan.Name, timeStamps.ToDTO(), aliases.ToDTO(), autoPeform.ToDTO(), stalkWords
								.ToDTO());
					#endregion

					#region Event Handlers
					#endregion
				}

				public class ChanListPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public ChanListPrefs(NetworkPrefs mgrParent) :
							base(mgrParent, "Known Channels", PrefsRsrcs.strNetKnownChanTitle, PrefsRsrcs
								.strNetKnownChanDesc)
							=> this.mgrParent = mgrParent;

						public ChanListPrefs(NetworkPrefs mgrParent, DTO.IrcDTO<GlobalDtoType>.NetworkDTO.ChanDTO[]? dto) :
							base(mgrParent, "Known Channels", PrefsRsrcs.strNetKnownChanTitle, PrefsRsrcs
								.strNetKnownChanDesc)
						{
							this.mgrParent = mgrParent;

							if(dto != null)
								foreach(DTO.IrcDTO<GlobalDtoType>.NetworkDTO.ChanDTO dchanCur in dto)
									RegisterChan(Chan.AllChanByName[dchanCur.OwnerChan]);
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
						public new readonly NetworkPrefs mgrParent;

						public System.Collections.Generic.Dictionary<Chan, ChanPrefs> mapAllChanPrefsByChan =
							[];
					#endregion

					#region Properties
					#endregion

					#region Methods
						public void RegisterChan(in Chan chanToBeRegistered)
						{
							ChanPrefs pchanNew = new(this, chanToBeRegistered);

							Add(pchanNew);

							mapAllChanPrefsByChan[chanToBeRegistered] = pchanNew;
						}

						public void RemovePrefsForChan(in Chan chanToRemovePrefsFor)
						{
							RemoveChildMgr(mapAllChanPrefsByChan[chanToRemovePrefsFor]);

							mapAllChanPrefsByChan.Remove(chanToRemovePrefsFor);
						}

						public DTO.IrcDTO<GlobalDtoType>.NetworkDTO.ChanDTO[]? ToDTO()
							=> mapAllChanPrefsByChan.Values.IsEmpty()
								? []
								: mapAllChanPrefsByChan.Values.Select(pchanCur
										=> pchanCur.ToDTO()
									).ToArray();
					#endregion

					#region Event Handlers
					#endregion
				}
			#endregion

			#region Members
				private readonly TimeStampPrefs timeStamps;

				private readonly DccPrefs dcc;

				private readonly AutoPerformPrefs autoPeform;

				private readonly ConnPrefs conn;

				private readonly AliasesPrefs aliases;

				private readonly AltNicksPrefs altNicks;

				private readonly StalkWordsPrefs stalkWords;

				private readonly NotifyWhenOnlinePrefs notifyWhenOnline;

				private readonly ChanListPrefs knownChans;
			#endregion

			#region Properties
				public Defs.Net OwnerNet
				{
					get;

					private init;
				}


				public TimeStampPrefs TimeStamps
					=> timeStamps;

				public DccPrefs DCC
					=> dcc;

				public AutoPerformPrefs AutoPerform
					=> autoPeform;

				public ConnPrefs Conn
					=> conn;

				public AliasesPrefs Aliases
					=> aliases;

				public AltNicksPrefs AltNicks
					=> altNicks;

				public StalkWordsPrefs StalkWords
					=> stalkWords;

				public NotifyWhenOnlinePrefs NotifyWhenOnline
					=> notifyWhenOnline;

				public ChanListPrefs KnownChans
					=> knownChans;

				public override bool CanBeRemoved
					=> true;
			#endregion

			#region Methods
				public DTO.IrcDTO<GlobalDtoType>.NetworkDTO ToDTO()
					=> new(OwnerNet.guid, timeStamps.ToDTO(), dcc.ToDTO(), autoPeform.ToDTO(), conn
						.ToDTO(), aliases.ToDTO(), altNicks.ToDTO(), stalkWords.ToDTO(), notifyWhenOnline
						.ToDTO(), knownChans.ToDTO());
			#endregion

			#region Event Handlers
			#endregion
		}
	#endregion

	#region Members
		private static Prefs<GlobalPrefsType, GlobalDtoType>? instance = null;

		private readonly System.Collections.Generic.List<NetworkPrefs> listNetworks;
	#endregion

	#region Properties
		public abstract GlobalPrefsType Global
		{
			get;
		}

		public System.Collections.Generic.IReadOnlyList<NetworkPrefs> Networks
			=> listNetworks;
	#endregion

	#region Methods
		public abstract DTO.IrcDTO<GlobalDtoType> ToDTO();
	#endregion

	#region Event Handlers
	#endregion
}