// Ignore Spelling: bnc Defs evt Ssl

namespace BestChat.IRC.Data.Defs;

public class Bnc : Platform.DataAndExt.Obj<Bnc>
{
	#region Constructors & Deconstructors
		public Bnc()
		{
		}

		protected Bnc(in Bnc bncCopyThis)
		{
			bIsPredefined = bncCopyThis.bIsPredefined;
			strDisplayName = bncCopyThis.DisplayName;
			uriHomepage = bncCopyThis.Homepage;
			strHomeNetwork = bncCopyThis.HomeNetwork;
			strHomeChan = bncCopyThis.HomeChan;
			strOwnBot = bncCopyThis.OwnBot;
			uiMaxNetworksPerBouncerInstance = bncCopyThis.MaxNetworksPerBouncerInstance;

			foreach(string strCurAllowedNetwork in bncCopyThis.AllowedNetworks)
				setAllowedNetworks.Add(strCurAllowedNetwork);

			foreach(string strCurProhibitedNetwork in bncCopyThis.ProhibitedNetworks)
				setProhibitedNetworks.Add(strCurProhibitedNetwork);

			foreach(string strCurServer in bncCopyThis.Servers)
				setServers.Add(strCurServer);

			foreach(ushort usCurPort in bncCopyThis.Ports)
				ussetPorts.Add(usCurPort);

			foreach(ushort usCurSslPort in bncCopyThis.SslPorts)
				ussetSslPorts.Add(usCurSslPort);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

		public event DFieldChanged<string>? evtDisplayNameChanged;
		public event DFieldChanged<System.Uri?>? evtHomepageChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<string>>?
			evtAllowedNetworksChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<string>>?
			evtProhibitedNetworksChanged;
		public event DFieldChanged<string>? evtHomeNetworkChanged;
		public event DFieldChanged<string>? evtHomeChanChanged;
		public event DFieldChanged<string>? evtOwnBotChanged;
		public event DFieldChanged<uint?>? evtMaxNetworksPerBouncerInstanceChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<string>>?
			evtServersChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>?
			evtPortsChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>?
			evtSslPortsChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public class Editable : Bnc
		{
			#region Constructors & Deconstructors
				public Editable(in Bnc bncOriginal) : base(bncOriginal) => this.bncOriginal = bncOriginal;
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
				public readonly Bnc bncOriginal;
			#endregion

			#region Properties
				public bool WereChangedMade
				{
					get;

					private set;
				}

				public new string DisplayName
				{
					get => base.DisplayName;

					set
					{
						if (base.DisplayName != value)
						{
							base.DisplayName = value;

							WereChangedMade = true;
						}
					}
				}

				public new System.Uri? Homepage
				{
					get => base.Homepage;

					set
					{
						if (base.Homepage != value)
						{
							base.Homepage = value;

							WereChangedMade = true;
						}
					}
				}

				public new string HomeNetwork
				{
					get => base.HomeNetwork;

					protected set
					{
						if(base.HomeNetwork != value)
						{
							base.HomeNetwork = value;

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

				public new System.Collections.Generic.IReadOnlySet<string> Servers => bncOriginal.Servers;

				public new System.Collections.Generic.IReadOnlySet<ushort> Ports => bncOriginal.Ports;

				public new System.Collections.Generic.IReadOnlySet<ushort> SslPorts => bncOriginal.SslPorts;
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
					bncOriginal.DisplayName = DisplayName;
					bncOriginal.Homepage = Homepage;
					bncOriginal.HomeNetwork = HomeNetwork;
					bncOriginal.HomeChan = HomeChan;
					bncOriginal.OwnBot = OwnBot;
					bncOriginal.MaxNetworksPerBouncerInstance = MaxNetworksPerBouncerInstance;

					if(!AllowedNetworks.SetEquals(bncOriginal.AllowedNetworks))
					{
						bncOriginal.ClearAllowedNetworks();

						foreach(string strCurAllowedNetwork in AllowedNetworks)
							bncOriginal.AddAllowedNetwork(strCurAllowedNetwork);
					}

					if(!ProhibitedNetworks.SetEquals(bncOriginal.ProhibitedNetworks))
					{
						bncOriginal.ClearProhibitedNetworks();

						foreach(string strCurProhibitedNetwork in ProhibitedNetworks)
							bncOriginal.AddProhibitedNetwork(strCurProhibitedNetwork);
					}

					if(!Servers.SetEquals(bncOriginal.Servers))
					{
						bncOriginal.ClearServers();

						foreach(string strCurServer in Servers)
							bncOriginal.AddServer(strCurServer);
					}

					if(!Ports.SetEquals(bncOriginal.Ports))
					{
						bncOriginal.ClearPorts();

						foreach(ushort usCurPort in Ports)
							bncOriginal.AddPort(usCurPort);
					}

					if(!SslPorts.SetEquals(bncOriginal.SslPorts))
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
	#endregion

	#region Members
		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonPropertyName("IsPredefined")]
		public readonly bool bIsPredefined;

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonRequired]
		[System.Text.Json.Serialization.JsonPropertyName("DisplayName")]
		private string strDisplayName = "";

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonRequired]
		[System.Text.Json.Serialization.JsonPropertyName("Homepage")]
		private System.Uri? uriHomepage;

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonPropertyName("AllowedNetworks")]
		private readonly System.Collections.Generic.SortedSet<string> setAllowedNetworks = new
			();

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonPropertyName("ProhibitedNetworks")]
		private readonly System.Collections.Generic.SortedSet<string> setProhibitedNetworks = new
			();

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonPropertyName("HomeNetwork")]
		private string strHomeNetwork = "";

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonPropertyName("HomeChan")]
		private string strHomeChan = "";

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonPropertyName("OwnBot")]
		private string strOwnBot = "";

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonPropertyName("MaxNetworksPerBouncerInstance")]
		private uint? uiMaxNetworksPerBouncerInstance;

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonRequired]
		[System.Text.Json.Serialization.JsonPropertyName("Servers")]
		private readonly System.Collections.Generic.SortedSet<string> setServers = new();

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonPropertyName("Ports")]
		private readonly System.Collections.Generic.SortedSet<ushort> ussetPorts = new();

		[System.Text.Json.Serialization.JsonInclude]
		[System.Text.Json.Serialization.JsonPropertyName("SslPorts")]
		private readonly System.Collections.Generic.SortedSet<ushort> ussetSslPorts = new();
	#endregion

	#region Properties
		public bool IsPredefined
			=> bIsPredefined;

		public string DisplayName
		{
			get => strDisplayName;

			protected set
			{
				if (strDisplayName != value)
				{
					string strOldName = strDisplayName;

					strDisplayName = value;

					MakeDirty();

					FireDisplayNameChanged(strOldName);
				}
			}
		}

		public System.Uri? Homepage
		{
			get => uriHomepage;

			protected set
			{
				if (uriHomepage != value)
				{
					System.Uri? uriOldHomepage = uriHomepage;

					uriHomepage= value;

					MakeDirty();
				}
			}
		}

		public System.Collections.Generic.IReadOnlySet<string> AllowedNetworks => setAllowedNetworks;

		public System.Collections.Generic.IReadOnlySet<string> ProhibitedNetworks => setProhibitedNetworks;

		public string HomeNetwork
		{
			get => strHomeNetwork;

			protected set
			{
				if(strHomeNetwork != value)
				{
					if(setProhibitedNetworks.Contains(value))
						throw new System.InvalidProgramException($"Can't make “{value}” the homepage for the " +
							$"bouncer {strDisplayName} as it's in the list of prohibited networks for the same " +
							"bouncer.  It's unlikely they'd be there.");

					string strOldHomeNetwork = strHomeNetwork;

					strHomeNetwork = value;

					MakeDirty();

					FireHomeNetworkChanged(strOldHomeNetwork);
				}
			}
		}

		public string HomeChan
		{
			get => strHomeChan;

			protected set
			{
				if(strHomeChan != value)
				{
					string strOldHomeChan = strHomeChan;

					strHomeChan = value;

					MakeDirty();

					FireHomeChanChanged(strOldHomeChan);
				}
			}
		}

		public string OwnBot
		{
			get => strOwnBot;

			protected set
			{
				if(strOwnBot != value)
				{
					string strOldOwnBot = strOwnBot;

					strOwnBot = value;

					MakeDirty();

					FireOwnBotChanged(strOldOwnBot);
				}
			}
		}

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

		public System.Collections.Generic.IReadOnlySet<string> Servers => setServers;

		public System.Collections.Generic.IReadOnlySet<ushort> Ports => ussetPorts;

		public System.Collections.Generic.IReadOnlySet<ushort> SslPorts => ussetSslPorts;
	#endregion

	#region Methods
		protected void FirePropChanged(string strWhichProp)
			=> PropertyChanged?.Invoke(this, new(strWhichProp));

		protected void FireDisplayNameChanged(string strOldName)
		{
			FirePropChanged(nameof(DisplayName));

			evtDisplayNameChanged?.Invoke(this, strOldName, strDisplayName);
		}

		protected void FireHomepageChanged(System.Uri uriOldHomepage)
		{
			FirePropChanged(nameof(Homepage));

			evtHomepageChanged?.Invoke(this, uriOldHomepage, uriHomepage);
		}

		protected void FireAllowedNetworksChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(AllowedNetworks));

			evtAllowedNetworksChanged?.Invoke(this, setAllowedNetworks, collectionChangeType);
		}

		protected void FireProhibitedNetworksChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(ProhibitedNetworks));

			evtProhibitedNetworksChanged?.Invoke(this, setAllowedNetworks, collectionChangeType);
		}

		protected void FireHomeNetworkChanged(string strOldHomeNetwork)
		{
			FirePropChanged(nameof(HomeNetwork));

			evtHomeNetworkChanged?.Invoke(this, strOldHomeNetwork, strHomeNetwork);
		}

		protected void FireHomeChanChanged(string strOldHomeNetwork)
		{
			FirePropChanged(nameof(HomeChan));

			evtHomeChanChanged?.Invoke(this, strOldHomeNetwork, strHomeNetwork);
		}

		protected void FireOwnBotChanged(string strOldHomeNetwork)
		{
			FirePropChanged(nameof(OwnBot));

			evtOwnBotChanged?.Invoke(this, strOldHomeNetwork, strHomeNetwork);
		}

		protected void FireMaxNetworksPerBouncerInstanceChanged(uint? uiOldMaxNetworksPerBouncerInstance)
		{
			FirePropChanged(nameof(MaxNetworksPerBouncerInstance));

			evtMaxNetworksPerBouncerInstanceChanged?.Invoke(this, uiOldMaxNetworksPerBouncerInstance,
				uiMaxNetworksPerBouncerInstance);
		}

		protected void FireServersChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(Servers));

			evtServersChanged?.Invoke(this, setServers, collectionChangeType);
		}

		protected void FirePortsChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(Ports));

			evtPortsChanged?.Invoke(this, Ports, collectionChangeType);
		}

		protected void FireSslPortsChanged(CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(SslPorts));

			evtSslPortsChanged?.Invoke(this, SslPorts, collectionChangeType);
		}

		protected void AddAllowedNetwork(string strNewAllowedNetwork)
		{
			if(!setAllowedNetworks.Contains(strNewAllowedNetwork))
			{
				if(setProhibitedNetworks.Contains(strNewAllowedNetwork))
					throw new System.InvalidProgramException($"Can't add {strNewAllowedNetwork} to the " +
						$"list of allowed networks for the bouncer {strDisplayName} as that network is in the list " +
						"of prohibited networks for the same bouncer.  It can't be both allowed and prohibited at " +
						"the same time.");

				setAllowedNetworks.Add(strNewAllowedNetwork);

				MakeDirty();

				FireAllowedNetworksChanged(CollectionChangeType.add);
			}
		}

		protected void RemoveAllowedNetwork(string strNetworkToRemoveFromAllowedList)
		{
			if(setAllowedNetworks.Contains(strNetworkToRemoveFromAllowedList))
			{
				setAllowedNetworks.Remove(strNetworkToRemoveFromAllowedList);

				MakeDirty();

				FireAllowedNetworksChanged(CollectionChangeType.removed);
			}
		}

		protected void ClearAllowedNetworks()
		{
			if(setAllowedNetworks.Count > 0)
			{
				setAllowedNetworks.Clear();

				MakeDirty();

				FireAllowedNetworksChanged(CollectionChangeType.removed);
			}
		}

		protected void AddProhibitedNetwork(string strNewProhibitedNetwork)
		{
			if(!setProhibitedNetworks.Contains(strNewProhibitedNetwork))
			{
				if(setAllowedNetworks.Contains(strNewProhibitedNetwork))
					throw new System.InvalidProgramException($"Can't add “{strNewProhibitedNetwork}” to " +
						$"the list of prohibited networks for the bouncer {strDisplayName} as that network is on " +
						"the list of allowed networks for the same bouncer.  It can't be on both lists.");

				if(strHomeNetwork == strNewProhibitedNetwork)
					throw new System.InvalidProgramException($"Can't add “{strNewProhibitedNetwork}” to " +
						$"the list of prohibited networks for the bouncer {strDisplayName} as that happens to be " +
						"listed as the home network for the same bouncer.  Why would they be there if they don't " +
						"allow that network?");

				setProhibitedNetworks.Add(strNewProhibitedNetwork);

				MakeDirty();

				FireProhibitedNetworksChanged(CollectionChangeType.add);
			}
		}

		protected void RemoveProhibitedNetwork(string strNetworkToRemoveFromProhibitedList)
		{
			if(setProhibitedNetworks.Contains(strNetworkToRemoveFromProhibitedList))
			{
				setProhibitedNetworks.Remove(strNetworkToRemoveFromProhibitedList);

				MakeDirty();

				FireProhibitedNetworksChanged(CollectionChangeType.removed);
			}
		}

		protected void ClearProhibitedNetworks()
		{
			if(setProhibitedNetworks.Count > 0)
			{
				setProhibitedNetworks.Clear();

				MakeDirty();

				FireProhibitedNetworksChanged(CollectionChangeType.removed);
			}
		}

		protected void AddServer(string strNewServer)
		{
			if(!setServers.Contains(strNewServer))
			{
				setServers.Add(strNewServer);

				MakeDirty();

				FireServersChanged(CollectionChangeType.add);
			}
		}

		protected void RemoveServer(string strServerToRemove)
		{
			if(setServers.Contains(strServerToRemove))
			{
				setServers.Remove(strServerToRemove);

				MakeDirty();

				FireServersChanged(CollectionChangeType.removed);
			}
		}

		protected void ClearServers()
		{
			if(setServers.Count > 0)
			{
				setServers.Clear();

				MakeDirty();

				FireServersChanged(CollectionChangeType.removed);
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

		public Editable MakeEditableVersion() => new(this);
	#endregion

	#region Event Handlers
	#endregion
}