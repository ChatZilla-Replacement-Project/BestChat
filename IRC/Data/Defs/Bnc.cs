// Ignore Spelling: bnc Defs evt Ssl ebnc

using System.Linq;

namespace BestChat.IRC.Data.Defs;

using Platform.DataAndExt.Ext;

public partial class BNC : Platform.DataAndExt.Obj<BNC>, IDataDef<BNC>, System.ComponentModel.INotifyDataErrorInfo
{
	#region Constructors & Deconstructors
		public BNC()
		{
			bIsPredefined = false;

			strsetAllowedNets = [];
			strsetProhibitedNets = [];
			ussetPorts = [];
			ussetSslPorts = [];
		}

		public BNC(in BNC bncCopyThis)
		{
			bIsPredefined = bncCopyThis.IsPredefined;
			strName = bncCopyThis.Name;
			uriHomePage = bncCopyThis.HomePage;
			strHomeNetwork = bncCopyThis.HomeNet;
			strHomeChan = bncCopyThis.HomeChan;
			strOwnBot = bncCopyThis.OwnBot;
			strsetAllowedNets = [.. bncCopyThis.AllowedNets];
			strsetProhibitedNets = [.. bncCopyThis.ProhibitedNets];
			foreach(ServerInfo serverCur in bncCopyThis.AllServersByName.Values)
			{
				ServerInfo serverCopy = new(serverCur);

				servermapServersByName[serverCopy.Name] = serverCopy;

				serverCopy.evtDirtyChanged += OnServerDirtyChanged;
			}
			ussetPorts = [.. bncCopyThis.AllPorts];
			ussetSslPorts = [.. bncCopyThis.AllSslPorts];
			uiMaxNetworksPerBouncerInstance = bncCopyThis.MaxNetworksPerBouncerInstance;
		}

		public BNC(in DTO.BncDTO dto)
		{
			bIsPredefined = dto is not DTO.UserBncDTO;
			strName = dto.Name;
			uriHomePage = dto.HomePage;
			strHomeNetwork = dto.HomeNet;
			strHomeChan = dto.HomeChan;
			strOwnBot = dto.OwnBot;
			strsetAllowedNets = [.. dto.AllowedNets];
			strsetProhibitedNets = [.. dto.ProhibitedNets];
			foreach(DTO.BncDTO.ServerDTO dserverCur in dto.Servers)
			{
				ServerInfo serverCopy = new(this, dserverCur);

				servermapServersByName[serverCopy.Name] = serverCopy;

				serverCopy.evtDirtyChanged += OnServerDirtyChanged;
			}
			ussetPorts = [.. dto.Ports];
			ussetSslPorts = [.. dto.SslPorts];
		}

		public BNC(in DTO.UserBncDTO dubnc)
			: this((DTO.BncDTO)dubnc)
		{
			foreach(DTO.UserBncDTO.InstanceDTO dinstanceCur in dubnc.Instances)
			{
				Instance instanceNew = new(this, dinstanceCur);

				instancemapByName[instanceNew.Name] = instanceNew;

				instanceNew.evtDirtyChanged += OnInstanceDirtyChanged;
			}
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	
		public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;

		public event DFieldChanged<string>? evtNameChanged;
		public event DFieldChanged<System.Uri?>? evtHomepageChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<string>>?
			evtAllowedNetworksChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<string>>?
			evtProhibitedNetworksChanged;
		public event DFieldChanged<string>? evtHomeNetworkChanged;
		public event DFieldChanged<string>? evtHomeChanChanged;
		public event DFieldChanged<string>? evtOwnBotChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlyDictionary<string, ServerInfo>>?
			evtServersChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlyDictionary<string, Instance>>?
			evtInstancesChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>?
			evtPortsChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>?
			evtSslPortsChanged;
		public event DFieldChanged<uint?>? evtMaxNetworksPerBouncerInstanceChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public partial class ServerInfo : Platform.DataAndExt.Obj<ServerInfo>
		{
			#region Constructors & Deconstructors
				public ServerInfo(in BNC bncParent)
				{
					this.bncParent = bncParent;

					strName = "";
					strDomain = "";
				}

				public ServerInfo(in ServerInfo serverCopyThis)
				{
					bncParent = serverCopyThis.bncParent;

					strName = serverCopyThis.Name;
					strDomain = serverCopyThis.Domain;
				}

				protected ServerInfo(in BNC.Editable ebncParent, ServerInfo serverOriginal)
					: this(serverOriginal)
					=> bncParent = ebncParent;

				public ServerInfo(in BNC bncParent, in DTO.BncDTO.ServerDTO dto)
				{
					this.bncParent = bncParent;

					strName = dto.Name;
					strDomain = dto.Domain;
				}
			#endregion

			#region Delegates
			#endregion

			#region Events
				public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;


				public event DFieldChanged<string>? evtNameChanged;

				public event DFieldChanged<string>? evtDomainChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
				public class Editable : ServerInfo, System.ComponentModel.INotifyDataErrorInfo
				{
					#region Constructors & Deconstructors
						internal Editable(BNC.Editable ebncParent, in ServerInfo serverOriginal)
							: base(ebncParent, serverOriginal)
							=> this.serverOriginal = serverOriginal;
					#endregion

					#region Delegates
					#endregion

					#region Events
						public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>?
							ErrorsChanged;
					#endregion

					#region Constants
					#endregion

					#region Helper Types
					#endregion

					#region Members
						public readonly ServerInfo serverOriginal;
					#endregion

					#region Properties
						public bool DomainIsValid
						{
							get
							{
								System.Uri uriTestOnDomain;

								try
								{
									uriTestOnDomain = new(Domain);
								}
								catch(System.UriFormatException)
								{
									return false;
								}

								return true;
							}
						}

						public bool DomainIsUnique
							=> DomainIsValid && (!serverOriginal.bncParent.AllServersByDomain.ContainsKey(Domain) ||
								serverOriginal.bncParent.AllServersByDomain[Domain] != serverOriginal);

						public bool NameIsUnique
							=> Name != "" && (!serverOriginal.bncParent.AllServersByName.ContainsKey(Name) ||
								serverOriginal.bncParent.AllServersByName[Name] != serverOriginal);

						public bool HasErrors
							=> strName != "" && strDomain != null && !DomainIsUnique && !NameIsUnique;

						public new string Name
						{
							get => base.Name;

							set
							{
								if(base.Name != value)
								{
									base.Name = value;

									WereChangesMade = true;
								}
							}
						}

						public new string Domain
						{
							get => base.Domain;

							set
							{
								if(base.Domain != value)
								{
									base.Domain = value;

									WereChangesMade = true;
								}
							}
						}

						public bool WereChangesMade
						{
							get;

							private set;
						}
					#endregion

					#region Methods
						public System.Collections.IEnumerable GetErrors(string? strPropToGetErrorsFor)
						{
							System.Collections.Generic.SortedSet<string> strsetErrors = [];

							string[] astrPropListToGetErrorsFor = strPropToGetErrorsFor == null
								? [nameof(Name), nameof(Domain)]
								: [strPropToGetErrorsFor];

							foreach(string strCurPropToGetErrorFor in astrPropListToGetErrorsFor)
								switch(strCurPropToGetErrorFor)
								{
									case nameof(Name):
										if(Name == "")
											strsetErrors.Add(Rsrcs.strBncNameBlank);
										else if(!NameIsUnique)
											strsetErrors.Add(Rsrcs.strBncNameTakenFmt);

										break;

									case nameof(Domain):
										if(Domain == "")
											strsetErrors.Add(Rsrcs.strBncServerDomainBlank);
										else if(!DomainIsValid)
											strsetErrors.Add(Rsrcs.strBncServerDomainInvalidFmt);
										else if(!DomainIsUnique)
											strsetErrors.Add(Rsrcs.strBncServerDomainNotUniqueFmt);

										break;

									default:
										throw new System.InvalidProgramException("Unknown property during error test");
								}

							return strsetErrors;
						}

						public void Save()
						{
							serverOriginal.Name = Name;
							serverOriginal.Domain = Domain;
						}
					#endregion

					#region Event Handlers
					#endregion
				}
			#endregion

			#region Members
				public readonly BNC bncParent;

				private string strName;

				private string strDomain;
			#endregion

			#region Properties
				public string Name
				{
					get => strName;

					protected set
					{
						if(strName != value)
						{
							string strOldName = strName;

							strName = value;

							MakeDirty();

							FireNameChanged(strOldName);
						}
					}
				}

				public string Domain
				{
					get => strDomain;

					protected set
					{
						if(strDomain != value)
						{
							string strOldDomain = strDomain;

							strDomain = GetDomainTrimmerRegex().Replace(value, "$1");

							MakeDirty();

							FireDomainChanged(strOldDomain);
						}
					}
				}
			#endregion

			#region Methods
				private void FirePropChanged(in string strWhichProp)
					=> PropertyChanged?.Invoke(this, new(strWhichProp));

				private void FireNameChanged(in string strOldName)
				{
					FirePropChanged(nameof(Name));

					evtNameChanged?.Invoke(this, strOldName, strName);
				}

				private void FireDomainChanged(in string strOldDomain)
				{
					FirePropChanged(nameof(Domain));

					evtDomainChanged?.Invoke(this, strOldDomain, strDomain);
				}

				public DTO.BncDTO.ServerDTO ToDTO()
					=> new(strName, strDomain);

				public Editable MakeEditable(BNC.Editable ebnc)
					=> new(ebnc, this);

				[System.Text.RegularExpressions.GeneratedRegex("(.*?)([/#].*)")]
				private static partial System.Text.RegularExpressions.Regex GetDomainTrimmerRegex();
			#endregion

			#region Event Handlers
		#endregion
		}
		public class Editable : BNC
		{
			#region Constructors & Deconstructors
				public Editable(in BNC bncOriginal) :
					base(bncOriginal)
					=> this.bncOriginal = bncOriginal;
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
				public readonly BNC bncOriginal;
			#endregion

			#region Properties
				public bool WereChangedMade
				{
					get;

					private set;
				}

				public new string DisplayName
				{
					get => base.Name;

					set
					{
						if (base.Name != value)
						{
							base.Name = value;

							WereChangedMade = true;
						}
					}
				}

				public new System.Uri? HomePage
				{
					get => base.HomePage;

					set
					{
						if (base.HomePage != value)
						{
							base.HomePage = value;

							WereChangedMade = true;
						}
					}
				}

				public new string HomeNetwork
				{
					get => base.HomeNet;

					protected set
					{
						if(base.HomeNet != value)
						{
							base.HomeNet = value;

							WereChangedMade = true;
						}
					}
				}

				public new string HomeChan
				{
					get => base.HomeChan;

					protected set
					{
						if(base.HomeChan != value)
						{
							base.HomeChan = value;

							WereChangedMade = true;
						}
					}
				}

				public new string OwnBot
				{
					get => base.OwnBot;

					set
					{
						if(base.OwnBot != value)
						{
							base.OwnBot = value;

							WereChangedMade = true;
						}
					}
				}

				public new System.Collections.Generic.IReadOnlyDictionary<string, ServerInfo> AllServersByName
					=> bncOriginal.AllServersByName;

				public new System.Collections.Generic.IReadOnlySet<ushort> Ports
					=> bncOriginal.AllPorts;

				public new System.Collections.Generic.IReadOnlySet<ushort> SslPorts
					=> bncOriginal.AllSslPorts;

				public new uint? MaxNetworksPerBouncerInstance
				{
					get => base.MaxNetworksPerBouncerInstance;

					set
					{
						if(base.MaxNetworksPerBouncerInstance != value)
						{
							base.MaxNetworksPerBouncerInstance = value;

							WereChangedMade = true;
						}
					}
				}
			#endregion

			#region Methods
				public new void AddAllowedNetwork(string strNewAllowedNetwork)
				{
					base.AddAllowedNetwork(strNewAllowedNetwork);

					WereChangedMade = true;
				}

				public new void RemoveAllowedNetwork(string strNetworkToRemoveFromAllowedList)
				{
					base.RemoveAllowedNetwork(strNetworkToRemoveFromAllowedList);

					WereChangedMade = true;
				}

				public new void ClearAllowedNetworks()
				{
					base.ClearAllowedNetworks();

					WereChangedMade = true;
				}

				public new void AddProhibitedNetwork(string strNewProhibitedNetwork)
				{
					base.AddAllowedNetwork(strNewProhibitedNetwork);

					WereChangedMade = true;
				}

				public new void RemoveProhibitedNetwork(string strNetworkToRemoveFromProhibitedList)
				{
					base.RemoveAllowedNetwork(strNetworkToRemoveFromProhibitedList);

					WereChangedMade = true;
				}

				public new void ClearProhibitedNetworks()
				{
					base.ClearAllowedNetworks();

					WereChangedMade = true;
				}

				public new void AddServer(ServerInfo serverNew)
				{
					if(!AllServersByName.ContainsKey(serverNew.Name))
					{
						base.AddServer(serverNew);

						WereChangedMade = true;
					}
				}

				public new void RemoveServer(string strNameOfServerToRemove)
				{
					if(AllServersByName.ContainsKey(strNameOfServerToRemove))
					{
						base.RemoveServer(strNameOfServerToRemove);

						WereChangedMade = true;
					}
				}

				public new void RemoveServer(ServerInfo serverBeingRemoved)
				{
					if(AllServersByName.ContainsKey(serverBeingRemoved.Name))
					{
						base.RemoveServer(serverBeingRemoved);

						WereChangedMade = true;
					}
				}

				public new void ClearServers()
				{
					if(AllServersByName.Count > 0)
					{
						base.ClearServers();

						WereChangedMade = true;
					}
				}

				public new void AddInstance(Instance instanceNew)
				{
					if(!AllInstancesByName.ContainsKey(instanceNew.Name))
					{
						base.AddInstance(instanceNew);

						WereChangedMade = true;
					}
				}

				public new void RemoveInstance(string strNameOfInstanceToRemove)
				{
					if(AllInstancesByName.ContainsKey(strNameOfInstanceToRemove))
					{
						base.RemoveInstance(strNameOfInstanceToRemove);

						WereChangedMade = true;
					}
				}

				public new void RemoveInstance(Instance instanceToRemove)
				{
					if(AllInstancesByName.ContainsKey(instanceToRemove.Name))
					{
						base.RemoveInstance(instanceToRemove);

						WereChangedMade = true;
					}
				}

				public new void ClearInstances()
				{
					if(AllInstancesByName.Count > 0)
					{
						base.ClearInstances();

						WereChangedMade = true;
					}
				}

				public new void AddPort(ushort usNewPort)
				{
					if(!Ports.Contains(usNewPort))
					{
						bncOriginal.AddPort(usNewPort);

						WereChangedMade = true;
					}
				}

				public new void RemovePort(ushort usPortToRemove)
				{
					if(Ports.Contains(usPortToRemove))
					{
						bncOriginal.RemovePort(usPortToRemove);

						WereChangedMade = true;
					}
				}

				public new void ClearPorts()
				{
					if(Ports.Count > 0)
					{
						bncOriginal.ClearPorts();

						WereChangedMade = true;
					}
				}

				public new void AddSslPort(ushort usNewSslPort)
				{
					if(!SslPorts.Contains(usNewSslPort))
					{
						bncOriginal.AddSslPort(usNewSslPort);

						WereChangedMade = true;
					}
				}

				public new void RemoveSslPort(ushort usSslPortToRemove)
				{
					if(SslPorts.Contains(usSslPortToRemove))
					{
						bncOriginal.RemoveSslPort(usSslPortToRemove);

						WereChangedMade = true;
					}
				}

				public new void ClearSslPorts()
				{
					if(SslPorts.Count > 0)
					{
						bncOriginal.ClearSslPorts();

						WereChangedMade = true;
					}
				}

				public void Save()
				{
					bncOriginal.Name = DisplayName;
					bncOriginal.HomePage = HomePage;
					bncOriginal.HomeNet = HomeNetwork;
					bncOriginal.HomeChan = HomeChan;
					bncOriginal.OwnBot = OwnBot;
					bncOriginal.MaxNetworksPerBouncerInstance = MaxNetworksPerBouncerInstance;

					if(!AllowedNets.SetEquals(bncOriginal.AllowedNets))
					{
						bncOriginal.ClearAllowedNetworks();

						foreach(string strCurAllowedNetwork in AllowedNets)
							bncOriginal.AddAllowedNetwork(strCurAllowedNetwork);
					}

					if(!ProhibitedNets.SetEquals(bncOriginal.ProhibitedNets))
					{
						bncOriginal.ClearProhibitedNetworks();

						foreach(string strCurProhibitedNetwork in ProhibitedNets)
							bncOriginal.AddProhibitedNetwork(strCurProhibitedNetwork);
					}

					if(!AllServersByName.Values.ToHashSet().SetEquals(bncOriginal.AllServersByName.Values))
					{
						bncOriginal.ClearServers();

						foreach(ServerInfo serverCur in AllServersByName.Values)
							bncOriginal.AddServer(serverCur);
					}

					if(!AllInstancesByName.Values.ToHashSet().SetEquals(bncOriginal.AllInstancesByName.Values))
					{
						bncOriginal.ClearInstances();

						foreach(Instance instanceCur in AllInstancesByName.Values)
							bncOriginal.AddInstance(instanceCur);
					}

					if(!Ports.SetEquals(bncOriginal.AllPorts))
					{
						bncOriginal.ClearPorts();

						foreach(ushort usCurPort in Ports)
							bncOriginal.AddPort(usCurPort);
					}

					if(!SslPorts.SetEquals(bncOriginal.AllSslPorts))
					{
						bncOriginal.ClearSslPorts();

						foreach(ushort usCurSslPort in SslPorts)
							bncOriginal.AddSslPort(usCurSslPort);
					}
				}
			#endregion

			#region Event Handlers
			#endregion
		}

		public class Instance : Platform.DataAndExt.Obj<Instance>
		{
			#region Constructors & Deconstructors
				public Instance(Instance instanceCopyThis)
				{
					bncOwner = instanceCopyThis.bncOwner;

					strName = instanceCopyThis.Name;
					serverAssigned = instanceCopyThis.AssignedServer;
				}

				public Instance(BNC bncOwner, DTO.UserBncDTO.InstanceDTO dinstance)
				{
					this.bncOwner = bncOwner;

					strName = dinstance.Name;
					serverAssigned = bncOwner.AllServersByName.ContainsKey(dinstance.AssignedServer)
						? bncOwner.AllServersByName[dinstance.AssignedServer]
						: throw new System.InvalidProgramException($"Can't find the assigned BNC server {dinstance
							.AssignedServer}");
				}
			#endregion

			#region Delegates
			#endregion

			#region Events
				public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;


				public event DFieldChanged<string>? evtNameChanged;

				public event DFieldChanged<ServerInfo>? evtAssignedServerChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
				public class Editable : Instance, System.ComponentModel.INotifyDataErrorInfo
				{
					#region Constructors & Deconstructors
						internal Editable(Instance instanceWhatsBeingEdited) :
							base(instanceWhatsBeingEdited)
							=> instanceOriginal = instanceWhatsBeingEdited;
					#endregion

					#region Delegates
					#endregion

					#region Events
						public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>?
							ErrorsChanged;
					#endregion

					#region Constants
					#endregion

					#region Helper Types
					#endregion

					#region Members
						public readonly Instance instanceOriginal;
					#endregion

					#region Properties
						public bool WereChangedMade
						{
							get;

							private set;
						}

						public new string Name
						{
							get => base.Name;

							set
							{
								if(base.Name != value)
								{
									base.Name = value;

									WereChangedMade = true;

									ErrorsChanged?.Invoke(this, new(nameof(Name)));
								}
							}
						}

						public new ServerInfo? AssignedServer
						{
							get => base.AssignedServer;

							set
							{
								if(base.AssignedServer != value)
								{
									base.AssignedServer = value;

									WereChangedMade = true;

									ErrorsChanged?.Invoke(this, new(nameof(AssignedServer)));
								}
							}
						}

						public bool HasErrors
							=> Name == "" || instanceOriginal.bncOwner.AllInstancesByName.ContainsKey(Name) &&
								instanceOriginal.bncOwner.AllInstancesByName[Name] != instanceOriginal || AssignedServer ==
								null;
					#endregion

					#region Methods
						public System.Collections.IEnumerable GetErrors(string? strPropToGetErrorsFor)
						{
							System.Collections.Generic.SortedSet<string> strsetErrors = [];

							string[] astrPropsToGetErrorsFor = strPropToGetErrorsFor == null
								? [nameof(Name), nameof(AssignedServer)]
								: [strPropToGetErrorsFor];

							foreach(string strCurPropToGetErrorsFor in astrPropsToGetErrorsFor)
								switch(strCurPropToGetErrorsFor)
								{
									case nameof(Name):
										if(Name == "")
											strsetErrors.Add(Rsrcs.strBncInstanceNameBlankFmt.Fmt(bncOwner.Name));
										else if(instanceOriginal.bncOwner.AllInstancesByName.ContainsKey(Name) &&
												instanceOriginal.bncOwner.AllInstancesByName[Name] != instanceOriginal)
											strsetErrors.Add(Rsrcs.strBncInstanceNameNotUniqueFmt.Fmt(Name, bncOwner.Name));
										break;

									case nameof(AssignedServer):
										if(AssignedServer == null)
											strsetErrors.Add(Rsrcs.strBncInstanceServerNeededFmt.Fmt(bncOwner.Name));
										break;

									default:
										throw new System.InvalidProgramException("Unknown property");
								}

							return strsetErrors;
						}

						public void Save()
						{
							if(HasErrors)
								throw new System.InvalidProgramException("We can't save until the errors are " +
									"resolved");

							instanceOriginal.Name = Name;
							instanceOriginal.AssignedServer = AssignedServer;
						}
					#endregion

					#region Event Handlers
					#endregion
				}
			#endregion

			#region Members
				public readonly BNC bncOwner;

				private string strName;

				private ServerInfo? serverAssigned;
			#endregion

			#region Properties
				public BNC OwnerBNC
					=> bncOwner;

				public string Name
				{
					get => strName;

					set
					{
						if(strName != value)
						{
							string strOldName = strName;

							strName = value;

							MakeDirty();

							FireNameChanged(strOldName);
						}
					}
				}

				public ServerInfo? AssignedServer
				{
					get => serverAssigned;

					protected set
					{
						if(serverAssigned != value)
						{
							if(!bncOwner.AllServersByName.ContainsKey(value.Name))
								throw new System.InvalidOperationException($"Can't find the server {value.Name} on the BNC {
									bncOwner.Name}");

							ServerInfo serverOldAssigned = serverAssigned;

							serverAssigned = value;

							MakeDirty();

							FireAssignedServerChanged(serverOldAssigned);
						}
					}
				}
			#endregion

			#region Methods
				private void FirePropChanged(in string strWhichProp)
					=> PropertyChanged?.Invoke(this, new(strWhichProp));

				private void FireNameChanged(in string strOldName)
				{
					FirePropChanged(nameof(Name));

					evtNameChanged?.Invoke(this, strOldName, strName);
				}

				private void FireAssignedServerChanged(in ServerInfo serverOldAssignedServer)
				{
					FirePropChanged(nameof(AssignedServer));

					evtAssignedServerChanged?.Invoke(this, serverOldAssignedServer, serverAssigned);
				}

				public DTO.UserBncDTO.InstanceDTO ToDTO()
					=> new(strName, serverAssigned!.Name);

				public Editable MakeEditable()
					=> new(this);
			#endregion

			#region Event Handlers
			#endregion
		}
	#endregion

	#region Members
		public readonly bool bIsPredefined;

		private string strName = "";

		private System.Uri? uriHomePage;

		private readonly System.Collections.Generic.SortedSet<string> strsetAllowedNets;

		private readonly System.Collections.Generic.SortedSet<string> strsetProhibitedNets;

		private string? strHomeNetwork = null;

		private string? strHomeChan = null;

		private string? strOwnBot = null;

		private readonly System.Collections.Generic.SortedList<string, ServerInfo> servermapServersByName =
			[];

		private readonly System.Collections.Generic.Dictionary<string, ServerInfo> servermapServersByDomain =
			[];

		private readonly System.Collections.Generic.SortedList<string, Instance> instancemapByName =
			[];

		private readonly System.Collections.Generic.SortedSet<ushort> ussetPorts;

		private readonly System.Collections.Generic.SortedSet<ushort> ussetSslPorts;

		private uint? uiMaxNetworksPerBouncerInstance;
	#endregion

	#region Properties
		public virtual bool IsPredefined
			=> true;

		public string Name
		{
			get => strName;

			protected set
			{
				if (strName != value)
				{
					string strOldName = strName;

					strName = value;

					MakeDirty();

					FireDisplayNameChanged(strOldName);
				}
			}
		}

		public System.Uri? HomePage
		{
			get => uriHomePage;

			protected set
			{
				if (uriHomePage != value)
				{
					System.Uri? uriOldHomepage = uriHomePage;

					uriHomePage= value;

					MakeDirty();
				}
			}
		}

		public System.Collections.Generic.IReadOnlySet<string> AllowedNets
			=> strsetAllowedNets;

		public string AllowedNetsAsText
			=> strsetAllowedNets.Join(", ");

		public System.Collections.Generic.IReadOnlySet<string> ProhibitedNets
			=> strsetProhibitedNets;

		public string ProhibitedNetsAsText
			=> strsetProhibitedNets.Join(", ");

		public string? HomeNet
		{
			get => strHomeNetwork;

			protected set
			{
				if($"{strHomeNetwork}" != $"{value}")
				{
					if(value != null && strsetProhibitedNets.Contains(value))
						throw new System.InvalidProgramException($"Can't make “{value}” the homepage for the " +
							$"bouncer {strName} as it's in the list of prohibited networks for the same " +
							"bouncer.  It's unlikely they'd be there.");

					string? strOldHomeNetwork = strHomeNetwork;

					strHomeNetwork = value == ""
						? null
						: value;

					MakeDirty();

					FireHomeNetChanged(strOldHomeNetwork);
				}
			}
		}

		public string? HomeChan
		{
			get => strHomeChan;

			protected set
			{
				if($"{strHomeChan}" != $"{value}")
				{
					string? strOldHomeChan = strHomeChan;

					strHomeChan = value == ""
						? null
						: value;

					MakeDirty();

					FireHomeChanChanged(strOldHomeChan);
				}
			}
		}

		public string HomeNetAndChannel
			=> HasValidTechSupportChan
				? $"{strHomeNetwork}/{strHomeChan}"
				: "";

		public string? OwnBot
		{
			get => strOwnBot;

			protected set
			{
				if($"{strOwnBot}" != $"{value}")
				{
					string? strOldOwnBot = strOwnBot;

					strOwnBot = value == ""
						? null
						: value;

					MakeDirty();

					FireOwnBotChanged(strOldOwnBot);
				}
			}
		}

		public System.Collections.Generic.IReadOnlyDictionary<string, ServerInfo> AllServersByName
			=> servermapServersByName;

		public string AllServersByNameAsText
			=> servermapServersByName.Values.Select(serverCur => serverCur.Name).Join(", ");

		public System.Collections.Generic.IReadOnlyDictionary<string, ServerInfo> AllServersByDomain
			=> servermapServersByDomain;

		public System.Collections.Generic.IReadOnlyDictionary<string, Instance> AllInstancesByName
			=> instancemapByName;

		public string AllInstancesByNameAsText
			=> servermapServersByName.Values.Select(serverCur => serverCur.Name).Join(", ");

		public System.Collections.Generic.IReadOnlySet<ushort> AllPorts
			=> ussetPorts;

		public string AllPortsAsText
			=> ussetPorts.Join(", ");

		public System.Collections.Generic.IReadOnlySet<ushort> AllSslPorts
			=> ussetSslPorts;

		public string AllSslPortsAsText
			=> ussetSslPorts.Join(", ");

		public uint? MaxNetworksPerBouncerInstance
		{
			get => uiMaxNetworksPerBouncerInstance;

			set
			{
				if(uiMaxNetworksPerBouncerInstance != value)
				{
					uint? uiOldMaxNetworksPerBouncerInstance = uiMaxNetworksPerBouncerInstance;

					uiMaxNetworksPerBouncerInstance = value;

					MakeDirty();

					FireMaxNetworksPerBouncerInstanceChanged(uiOldMaxNetworksPerBouncerInstance);
				}
			}
		}

		public string TypeOfBNC
			=> bIsPredefined
				? instancemapByName.Count > 0
					? Rsrcs.strPredefinedButModifiedBncType
					: Rsrcs.strPredefinedBncType
				: Rsrcs.strCustomBncType;

		public bool HasErrors
			=> strName == "" || BncMgr.mgr.AllItems.ContainsKey(strName) && BncMgr.mgr.AllItems[strName] != this
				|| servermapServersByName.Count == 0 || instancemapByName.Count == 0 || ussetPorts.Count == 0 && ussetSslPorts
				.Count == 0;

		public bool IsValid
			=> !HasErrors;

		public bool HasValidTechSupportChan
			=> strHomeNetwork != null && strHomeChan != null;

		public ushort? NextAvailablePort
		{
			get
			{
				ushort usNextUnusedPort = 0;

				while(usNextUnusedPort < ushort.MaxValue && (ussetPorts.Contains(usNextUnusedPort) ||
						ussetSslPorts.Contains(usNextUnusedPort)))
					usNextUnusedPort++;

				return usNextUnusedPort == ushort.MaxValue || ussetPorts.Contains(usNextUnusedPort) ||
						ussetSslPorts.Contains(usNextUnusedPort)
					? null
					: usNextUnusedPort;
			}
		}
	#endregion

	#region Methods
		protected void FirePropChanged(string strWhichProp)
			=> PropertyChanged?.Invoke(this, new(strWhichProp));

		protected void FireDisplayNameChanged(string strOldName)
		{
			FirePropChanged(nameof(Name));

			evtNameChanged?.Invoke(this, strOldName, strName);
		}

		protected void FireHomepageChanged(System.Uri uriOldHomepage)
		{
			FirePropChanged(nameof(HomePage));

			evtHomepageChanged?.Invoke(this, uriOldHomepage, uriHomePage);
		}

		protected void FireAllowedNetsChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(AllowedNets));

			evtAllowedNetworksChanged?.Invoke(this, strsetAllowedNets, collectionChangeType);
		}

		protected void FireProhibitedNetsChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(ProhibitedNets));

			evtProhibitedNetworksChanged?.Invoke(this, strsetAllowedNets, collectionChangeType);
		}

		protected void FireHomeNetChanged(string? strOldHomeNetwork)
		{
			FirePropChanged(nameof(HomeNet));

			evtHomeNetworkChanged?.Invoke(this, strOldHomeNetwork, strHomeNetwork);
		}

		protected void FireHomeChanChanged(string? strOldHomeNetwork)
		{
			FirePropChanged(nameof(HomeChan));

			evtHomeChanChanged?.Invoke(this, strOldHomeNetwork, strHomeNetwork);
		}

		protected void FireOwnBotChanged(string? strOldHomeNetwork)
		{
			FirePropChanged(nameof(OwnBot));

			evtOwnBotChanged?.Invoke(this, strOldHomeNetwork, strHomeNetwork);
		}

		protected void FireServersChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(AllServersByName));

			evtServersChanged?.Invoke(this, servermapServersByName, collectionChangeType);
		}

		protected void FireInstancesChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(AllInstancesByGUID));
			FirePropChanged(nameof(AllInstancesByName));
			FirePropChanged(nameof(AllInstancesByNameAsText));

			evtInstancesChanged?.Invoke(this, instancemapByName, collectionChangeType);
		}

		protected void FirePortsChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(AllPorts));

			evtPortsChanged?.Invoke(this, AllPorts, collectionChangeType);
		}

		protected void FireSslPortsChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(AllSslPorts));

			evtSslPortsChanged?.Invoke(this, AllSslPorts, collectionChangeType);
		}

		protected void FireMaxNetworksPerBouncerInstanceChanged(uint? uiOldMaxNetworksPerBouncerInstance)
		{
			FirePropChanged(nameof(MaxNetworksPerBouncerInstance));

			evtMaxNetworksPerBouncerInstanceChanged?.Invoke(this, uiOldMaxNetworksPerBouncerInstance,
				uiMaxNetworksPerBouncerInstance);
		}

		protected void AddAllowedNetwork(string strNewAllowedNetwork)
		{
			if(!strsetAllowedNets.Contains(strNewAllowedNetwork))
			{
				if(strsetProhibitedNets.Contains(strNewAllowedNetwork))
					throw new System.InvalidProgramException($"Can't add {strNewAllowedNetwork} to the " +
						$"list of allowed networks for the bouncer {strName} as that network is in the list " +
						"of prohibited networks for the same bouncer.  It can't be both allowed and prohibited at " +
						"the same time.");

				strsetAllowedNets.Add(strNewAllowedNetwork);

				MakeDirty();

				FireAllowedNetsChanged(CollectionChangeType.add);
			}
		}

		protected void RemoveAllowedNetwork(string strNetworkToRemoveFromAllowedList)
		{
			if(strsetAllowedNets.Contains(strNetworkToRemoveFromAllowedList))
			{
				strsetAllowedNets.Remove(strNetworkToRemoveFromAllowedList);

				MakeDirty();

				FireAllowedNetsChanged(CollectionChangeType.removed);
			}
		}

		protected void ClearAllowedNetworks()
		{
			if(strsetAllowedNets.Count > 0)
			{
				strsetAllowedNets.Clear();

				MakeDirty();

				FireAllowedNetsChanged(CollectionChangeType.removed);
			}
		}

		protected void AddProhibitedNetwork(string strNewProhibitedNetwork)
		{
			if(!strsetProhibitedNets.Contains(strNewProhibitedNetwork))
			{
				if(strsetAllowedNets.Contains(strNewProhibitedNetwork))
					throw new System.InvalidProgramException($"Can't add “{strNewProhibitedNetwork}” to " +
						$"the list of prohibited networks for the bouncer {strName} as that network is on " +
						"the list of allowed networks for the same bouncer.  It can't be on both lists.");

				if(strHomeNetwork == strNewProhibitedNetwork)
					throw new System.InvalidProgramException($"Can't add “{strNewProhibitedNetwork}” to " +
						$"the list of prohibited networks for the bouncer {strName} as that happens to be " +
						"listed as the home network for the same bouncer.  Why would they be there if they don't " +
						"allow that network?");

				strsetProhibitedNets.Add(strNewProhibitedNetwork);

				MakeDirty();

				FireProhibitedNetsChanged(CollectionChangeType.add);
			}
		}

		protected void RemoveProhibitedNetwork(string strNetworkToRemoveFromProhibitedList)
		{
			if(strsetProhibitedNets.Contains(strNetworkToRemoveFromProhibitedList))
			{
				strsetProhibitedNets.Remove(strNetworkToRemoveFromProhibitedList);

				MakeDirty();

				FireProhibitedNetsChanged(CollectionChangeType.removed);
			}
		}

		protected void ClearProhibitedNetworks()
		{
			if(strsetProhibitedNets.Count > 0)
			{
				strsetProhibitedNets.Clear();

				MakeDirty();

				FireProhibitedNetsChanged(CollectionChangeType.removed);
			}
		}

		protected void AddServer(ServerInfo serverNew)
		{
			if(!servermapServersByName.ContainsKey(serverNew.Name))
			{
				servermapServersByName[serverNew.Name] = serverNew;
				servermapServersByDomain[serverNew.Domain] = serverNew;

				MakeDirty();

				FireServersChanged(CollectionChangeType.add);

				serverNew.evtDirtyChanged += OnServerDirtyChanged;

				serverNew.evtNameChanged += OnServerNameChanged;
				serverNew.evtDomainChanged += OnServerDomainChanged;
			}
		}

		protected void RemoveServer(string strNameOfServerToRemove)
		{
			if(servermapServersByName.TryGetValue(strNameOfServerToRemove, out ServerInfo? serverBeingRemoved))
			{
				serverBeingRemoved.evtDirtyChanged -= OnServerDirtyChanged;
				serverBeingRemoved.evtNameChanged -= OnServerNameChanged;
				serverBeingRemoved.evtDomainChanged -= OnServerDomainChanged;

				servermapServersByName.Remove(strNameOfServerToRemove);
				servermapServersByDomain.Remove(serverBeingRemoved.Domain);

				MakeDirty();

				FireServersChanged(CollectionChangeType.removed);
			}
		}

		protected void RemoveServer(ServerInfo serverToRemove)
			=> RemoveServer(serverToRemove.Name);

		protected void ClearServers()
		{
			if(servermapServersByName.Count > 0)
			{
				servermapServersByName.Clear();

				MakeDirty();

				FireServersChanged(CollectionChangeType.removed);
			}
		}

		protected void AddInstance(Instance instanceNew)
		{
			if(!instancemapByName.ContainsKey(instanceNew.Name))
			{
				instancemapByName[instanceNew.Name] = instanceNew;

				MakeDirty();

				FireInstancesChanged(CollectionChangeType.add);

				instanceNew.evtDirtyChanged += OnInstanceDirtyChanged;
			}
		}

		internal void AddInstanceInternal(Instance instanceNew)
			=> AddInstance(instanceNew);

		protected void RemoveInstance(string strNameOfInstanceToRemove)
		{
			if(instancemapByName.TryGetValue(strNameOfInstanceToRemove, out Instance? instanceBeingRemoved))
			{
				instancemapByName.Remove(strNameOfInstanceToRemove);

				MakeDirty();

				FireInstancesChanged(CollectionChangeType.removed);
			}
		}

		protected void RemoveInstance(Instance instanceToRemove)
			=> RemoveInstance(instanceToRemove.Name);

		protected void ClearInstances()
		{
			if(instancemapByName.Count > 0)
			{
				instancemapByName.Clear();

				MakeDirty();

				FireInstancesChanged(CollectionChangeType.removed);
			}
		}

		protected void AddPort(ushort usNewPort)
		{
			if(!ussetPorts.Contains(usNewPort))
			{
				ussetPorts.Add(usNewPort);

				MakeDirty();

				FirePortsChanged(CollectionChangeType.add);
			}
		}

		protected void RemovePort(ushort usPortToRemove)
		{
			if(ussetPorts.Remove(usPortToRemove))
			{
				MakeDirty();

				FirePortsChanged(CollectionChangeType.removed);
			}
		}

		protected void ClearPorts()
		{
			if(ussetPorts.Count > 0)
			{
				ussetPorts.Clear();

				MakeDirty();

				FirePortsChanged(CollectionChangeType.removed);
			}
		}

		protected void AddSslPort(ushort usNewSslPort)
		{
			if(!ussetSslPorts.Add(usNewSslPort))
			{
				MakeDirty();

				FireSslPortsChanged(CollectionChangeType.add);
			}
		}

		protected void RemoveSslPort(ushort usSslPortToRemove)
		{
			if(ussetSslPorts.Remove(usSslPortToRemove))
			{
				MakeDirty();

				FireSslPortsChanged(CollectionChangeType.removed);
			}
		}

		protected void ClearSslPorts()
		{
			if(ussetSslPorts.Count > 0)
			{
				ussetSslPorts.Clear();

				MakeDirty();

				FireSslPortsChanged(CollectionChangeType.removed);
			}
		}

		public Editable MakeEditableVersion()
			=> new(this);

		public DTO.UserBncDTO ToDTO()
			=> instancemapByName.Count > 0
				? new DTO.UserBncDTO(
					Name,
					HomePage,
					[.. AllowedNets],
					[.. ProhibitedNets],
					[.. AllServersByName.Values.Select(
						serverCur
							=> serverCur.ToDTO()
					)],
					HomeNet,
					HomeChan,
					OwnBot,
					[.. instancemapByName.Values.Select(
						instanceCur
							=> instanceCur.ToDTO()
					)],
					[.. AllPorts],
					[.. AllSslPorts],
					MaxNetworksPerBouncerInstance
				)
			: throw new System.InvalidOperationException("Unable to support saving user BNC objects with no " +
				"instances.");

		public System.Collections.IEnumerable GetErrors(string? strPropNameToGetErrorsFor)
		{
			System.Collections.Generic.SortedSet<string> strsetErrors = [];

			string[] astrPropToCheck = strPropNameToGetErrorsFor == null
				? [nameof(Name), nameof(AllInstancesByName), nameof(AllServersByName), nameof(AllPorts),
					nameof(AllSslPorts)]
				: [strPropNameToGetErrorsFor];

			foreach(string strCurPropName in astrPropToCheck)
				switch(strPropNameToGetErrorsFor)
				{
					case nameof(Name):
						if(strName == "")
							strsetErrors.Add(Rsrcs.strBncNameBlank);
						else if(BncMgr.mgr.AllItems.ContainsKey(strName) && BncMgr.mgr.AllItems[strName] != this)
							strsetAllowedNets.Add(Rsrcs.strBncNameTakenFmt.Fmt(strName));

						break;

					case nameof(AllInstancesByName):
						if(instancemapByName.Count == 0)
							strsetErrors.Add(Rsrcs.strBncInstanceNeeded);

						break;

					case nameof(AllServersByName):
						if(servermapServersByName.Count == 0)
							strsetErrors.Add(Rsrcs.strBncServerNeeded);

						break;

					case nameof(AllPorts):
					case nameof(AllSslPorts):
						if(ussetPorts.Count == 0 && ussetSslPorts.Count == 0)
							strsetAllowedNets.Add(Rsrcs.strBncPortNeeded);

						break;

					default: // Just ignore values we don't know what to do with
						break;
				}

			return strsetErrors;
		}
	#endregion

	#region Event Handlers
		private void OnServerDirtyChanged(in ServerInfo objSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}

		private void OnServerNameChanged(in ServerInfo serverSender, in string strOldName, in string
			strNewName)
		{
			servermapServersByName.Remove(strOldName);
			servermapServersByName[strNewName] = serverSender;
		}

		private void OnServerDomainChanged(in ServerInfo serverSender, in string strOldDomainName, in string
			strNewDomainName)
		{
			servermapServersByDomain.Remove(strOldDomainName);
			servermapServersByDomain[strNewDomainName] = serverSender;
		}

		private void OnInstanceDirtyChanged(in Instance objSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}
	#endregion
}