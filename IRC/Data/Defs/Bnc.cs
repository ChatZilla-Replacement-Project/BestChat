// Ignore Spelling: bnc Defs evt Ssl ebnc

using System.Linq;

namespace BestChat.IRC.Data.Defs;

using Platform.DataAndExt.Ext;

public partial class BNC : Platform.DataAndExt.Obj<BNC>, IDataDef<BNC>, System.ComponentModel
	.INotifyDataErrorInfo
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
			strHomeNet = bncCopyThis.HomeNet;
			strHomeChan = bncCopyThis.HomeChan;
			strOwnBot = bncCopyThis.OwnBot;
			strsetAllowedNets = [.. bncCopyThis.AllowedNets];
			strsetProhibitedNets = [.. bncCopyThis.ProhibitedNets];
			foreach(BncServerInfo serverCur in bncCopyThis.AllServersByName.Values)
			{
				BncServerInfo serverCopy = new(serverCur);

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
			strHomeNet = dto.HomeNet;
			strHomeChan = dto.HomeChan;
			strOwnBot = dto.OwnBot;
			strsetAllowedNets = [.. dto.AllowedNets];
			strsetProhibitedNets = [.. dto.ProhibitedNets];
			foreach(DTO.BncDTO.ServerDTO dserverCur in dto.Servers)
			{
				BncServerInfo serverCopy = new(this, dserverCur);

				servermapServersByName[serverCopy.Name] = serverCopy;

				serverCopy.evtDirtyChanged += OnServerDirtyChanged;
			}
			ussetPorts = dto.Ports is null
				? []
				: [.. dto.Ports];
			ussetSslPorts = dto.SslPorts is null
				? []
				: [.. dto.SslPorts];
		}

		public BNC(in DTO.UserBncDTO dubnc)
			: this((DTO.BncDTO)dubnc)
		{
			if(dubnc.Instances is null)
				return;

			foreach(DTO.UserBncDTO.InstanceDTO dinstanceCur in dubnc.Instances)
			{
				BncInstance instanceNew = new(this, dinstanceCur);

				instancemapByName[instanceNew.Name] = instanceNew;

				instanceNew.evtDirtyChanged += OnInstanceDirtyChanged;
			}
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;

		public event DFieldChanged<string>? evtNameChanged;
		public event DFieldChanged<System.Uri?>? evtHomepageChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<string>>?
			evtAllowedNetworksChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<string>>?
			evtProhibitedNetworksChanged;
		public event DFieldChanged<string?>? evtHomeNetworkChanged;
		public event DFieldChanged<string?>? evtHomeChanChanged;
		public event DFieldChanged<string?>? evtOwnBotChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlyDictionary<string,
			BncServerInfo>>? evtServersChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlyDictionary<string, BncInstance>>?
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

	#endregion

	#region Members
		public readonly bool bIsPredefined;

		private string strName = "";

		private System.Uri? uriHomePage;

		private readonly System.Collections.Generic.SortedSet<string> strsetAllowedNets;

		private readonly System.Collections.Generic.SortedSet<string> strsetProhibitedNets;

		private string? strHomeNet = null;

		private string? strHomeChan = null;

		private string? strOwnBot = null;

		private readonly System.Collections.Generic.SortedList<string, BncServerInfo> servermapServersByName =
			[];

		private readonly System.Collections.Generic.Dictionary<string, BncServerInfo> servermapServersByDomain =
			[];

		private readonly System.Collections.Generic.SortedList<string, BncInstance> instancemapByName =
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
					System.Uri? uriOldHomePage = uriHomePage;

					uriHomePage= value;

					MakeDirty();

					FireHomePageChanged(uriOldHomePage);
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
			get => strHomeNet;

			protected set
			{
				if($"{strHomeNet}" != $"{value}")
				{
					if(value != null && strsetProhibitedNets.Contains(value))
						throw new System.InvalidProgramException($"Can't make “{value}” the homepage for the " +
							$"bouncer {strName} as it's in the list of prohibited networks for the same " +
							"bouncer.  It's unlikely they'd be there.");

					string? strOldHomeNetwork = strHomeNet;

					strHomeNet = value == ""
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

		public string HomeNetAndChan
			=> HasValidTechSupportChan
				? $"{strHomeNet}/{strHomeChan}"
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

		public System.Collections.Generic.IReadOnlyDictionary<string, BncServerInfo> AllServersByName
			=> servermapServersByName;

		public string AllServersByNameAsText
			=> servermapServersByName.Values.Select(serverCur => serverCur.Name).Join(", ");

		public System.Collections.Generic.IReadOnlyDictionary<string, BncServerInfo> AllServersByDomain
			=> servermapServersByDomain;

		public System.Collections.Generic.IReadOnlyDictionary<string, BncInstance> AllInstancesByName
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
			=> strHomeNet != null && strHomeChan != null;

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
		protected void FireDisplayNameChanged(string strOldName)
		{
			FirePropChanged(nameof(Name));

			evtNameChanged?.Invoke(this, strOldName, strName);
		}

		protected void FireHomePageChanged(System.Uri? uriOldHomepage)
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

			evtHomeNetworkChanged?.Invoke(this, strOldHomeNetwork, strHomeNet);
		}

		protected void FireHomeChanChanged(string? strOldHomeNetwork)
		{
			FirePropChanged(nameof(HomeChan));

			evtHomeChanChanged?.Invoke(this, strOldHomeNetwork, strHomeNet);
		}

		protected void FireOwnBotChanged(string? strOldHomeNetwork)
		{
			FirePropChanged(nameof(OwnBot));

			evtOwnBotChanged?.Invoke(this, strOldHomeNetwork, strHomeNet);
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

				if(strHomeNet == strNewProhibitedNetwork)
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

		protected void AddServer(BncServerInfo serverNew)
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
			if(servermapServersByName.TryGetValue(strNameOfServerToRemove, out BncServerInfo? serverBeingRemoved))
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

		protected void RemoveServer(BncServerInfo serverToRemove)
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

		protected void AddInstance(BncInstance instanceNew)
		{
			if(!instancemapByName.ContainsKey(instanceNew.Name))
			{
				instancemapByName[instanceNew.Name] = instanceNew;

				MakeDirty();

				FireInstancesChanged(CollectionChangeType.add);

				instanceNew.evtDirtyChanged += OnInstanceDirtyChanged;
			}
		}

		internal void AddInstanceInternal(BncInstance instanceNew)
			=> AddInstance(instanceNew);

		protected void RemoveInstance(string strNameOfInstanceToRemove)
		{
			if(instancemapByName.ContainsKey(strNameOfInstanceToRemove))
			{
				instancemapByName.Remove(strNameOfInstanceToRemove);

				MakeDirty();

				FireInstancesChanged(CollectionChangeType.removed);
			}
		}

		protected void RemoveInstance(BncInstance instanceToRemove)
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

		public BncEditable MakeEditableVersion()
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

			foreach(string strCurPropNameToGetErrorsFor in astrPropToCheck)
				switch(strCurPropNameToGetErrorsFor)
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

		public void SaveFrom(BncEditable ebncSaveFrom)
		{
			strName = ebncSaveFrom.Name;
			uriHomePage = ebncSaveFrom.HomePage;
			strHomeNet = ebncSaveFrom.HomeNet;
			strHomeChan = ebncSaveFrom.HomeChan;
			strOwnBot = ebncSaveFrom.OwnBot;
			uiMaxNetworksPerBouncerInstance = ebncSaveFrom.MaxNetworksPerBouncerInstance;

			if(!AllowedNets.SetEquals(ebncSaveFrom.AllowedNets))
			{
				ClearAllowedNetworks();

				foreach(string strCurAllowedNetwork in ebncSaveFrom.AllowedNets)
					AddAllowedNetwork(strCurAllowedNetwork);
			}

			if(!ProhibitedNets.SetEquals(ebncSaveFrom.ProhibitedNets))
			{
				ClearProhibitedNetworks();

				// ReSharper disable once InconsistentNaming
				foreach(string strCurProhibitedNetwork in ProhibitedNets)
					AddProhibitedNetwork(strCurProhibitedNetwork);
			}

			if(!AllServersByName
					.Values
					.ToHashSet()
					.SetEquals(ebncSaveFrom.AllServersByName.Values)
				)
			{
				ClearServers();

				foreach(BncServerInfo serverCur in ebncSaveFrom.AllServersByName.Values)
					AddServer(serverCur);
			}

			if(!AllInstancesByName
					.Values
					.ToHashSet()
					.SetEquals(ebncSaveFrom.AllInstancesByName.Values))
			{
				ClearInstances();

				foreach(BncInstance instanceCur in ebncSaveFrom.AllInstancesByName.Values)
					AddInstance(instanceCur);
			}

			if(!AllPorts.SetEquals(ebncSaveFrom.AllPorts))
			{
				ClearPorts();

				foreach(ushort usCurPort in ebncSaveFrom.AllPorts)
					AddPort(usCurPort);
			}

			// ReSharper disable once InvertIf
			if(!AllSslPorts.SetEquals(ebncSaveFrom.AllSslPorts))
			{
				ClearSslPorts();

				foreach(ushort usCurSslPort in ebncSaveFrom.AllSslPorts)
					AddSslPort(usCurSslPort);
			}
		}

	#endregion

	#region Event Handlers
		private void OnServerDirtyChanged(in BncServerInfo objSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}

		private void OnServerNameChanged(in BncServerInfo serverSender, in string strOldName, in string
			strNewName)
		{
			servermapServersByName.Remove(strOldName);
			servermapServersByName[strNewName] = serverSender;
		}

		private void OnServerDomainChanged(in BncServerInfo serverSender, in string strOldDomainName, in string
			strNewDomainName)
		{
			servermapServersByDomain.Remove(strOldDomainName);
			servermapServersByDomain[strNewDomainName] = serverSender;
		}

		private void OnInstanceDirtyChanged(in BncInstance objSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}
	#endregion
}