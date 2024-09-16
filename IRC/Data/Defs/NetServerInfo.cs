// Ignore Spelling: IRC Defs eunet evt Ssl dserver eus unet

using System.Linq;

namespace BestChat.IRC.Data.Defs;

public class NetServerInfo : Platform.DataAndExt.Obj<NetServerInfo>
{
	#region Constructors & Deconstructors
		public NetServerInfo(in Net netParent)
		{
			this.netParent = netParent;
			strDomain = "";
		}

		public NetServerInfo(in Net netParent, in string strDomain, in System.Collections.Generic.IEnumerable<ushort>
			eusPorts, in System.Collections.Generic.IEnumerable<ushort> eusSslPorts)
		{
			this.netParent = netParent;
			this.strDomain = strDomain;
			bEnabled = true;

			setPorts.UnionWith(eusPorts);
			setSslPorts.UnionWith(eusSslPorts);

			MakeDirty();
		}

		protected NetServerInfo(in UserNet.Editable eunetParent, in NetServerInfo serverCopyThis)
		{
			if(eunetParent.unetOriginal != serverCopyThis.netParent)
				throw new System.InvalidProgramException($"Editable ServerInfo instances must be owned by an " +
					$"Editable created from the parent of the value in {nameof(serverCopyThis)}");

			netParent = eunetParent;

			strDomain = serverCopyThis.strDomain;
			bEnabled = serverCopyThis.bEnabled;

			setPorts.UnionWith (serverCopyThis.Ports);
			setSslPorts.UnionWith(serverCopyThis.SslPorts);
		}

		public NetServerInfo(in Net netParent, in DTO.NetServerInfoDTO dserverUs)
		{
			this.netParent = netParent;

			strDomain = dserverUs.Domain;
			bEnabled = dserverUs.IsEnabled;
			setPorts.UnionWith(dserverUs.Ports);
			setSslPorts.UnionWith(dserverUs.SslPorts);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event DFieldChanged<string>? evtDomainChanged;
		public event DBoolFieldChanged? evtIsEnabledChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>? evtPortsChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>? evtSslPortsChanged;

		public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
		public const string strPortDelimiter = ", ";
	#endregion

	#region Helper Types
		public class Editable : NetServerInfo, System.ComponentModel.INotifyDataErrorInfo
		{
			public Editable(in UserNet.Editable eunetParent, in NetServerInfo serverOriginal) :
				base(eunetParent, serverOriginal)
			{
				this.serverOriginal = serverOriginal;
				this.eunetParent = eunetParent;
			}

			public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;

			public readonly NetServerInfo serverOriginal;

			public readonly UserNet.Editable eunetParent;

			public new UserNet.Editable Parent
				=> eunetParent;

			public bool HasErrors
				=> Domain != "" && (Ports.Count > 0 || SslPorts.Count > 0);

			public new string Domain
			{
				get => base.Domain;

				set
				{
					base.Domain = value;

					WereChangesMade = true;
				}
			}

			public bool WereChangesMade
			{
				get;

				private set;
			}

			public new void AddPort(in ushort usNewPort)
			{
				if(!Ports.Contains(usNewPort))
				{
					base.AddPort(usNewPort);

					WereChangesMade = true;
				}
			}

			public new void RemovePort(in ushort usPortToRemove)
			{
				if(Ports.Contains(usPortToRemove))
				{
					base.RemovePort(usPortToRemove);

					WereChangesMade = true;
				}
			}

			public new void ClearPorts()
			{
				if(Ports.Count > 0)
				{
					base.ClearPorts();

					WereChangesMade = true;
				}
			}

			public new void AddSslPort(in ushort usNewSslPort)
			{
				if(!SslPorts.Contains(usNewSslPort))
				{
					base.AddSslPort(usNewSslPort);

					WereChangesMade = true;
				}
			}

			public new void RemoveSslPort(in ushort usSslPortToRemove)
			{
				if(SslPorts.Contains(usSslPortToRemove))
				{
					base.RemoveSslPort(usSslPortToRemove);

					WereChangesMade = true;
				}
			}

			public new void ClearSslPorts()
			{
				if(SslPorts.Count > 0)
				{
					base.ClearSslPorts();

					WereChangesMade = true;
				}
			}
		
			public System.Collections.IEnumerable GetErrors(string? strPropNameToGetErrorsFor)
			{
				System.Collections.Generic.SortedSet<string> strsetErrors = [];

				System.Collections.Generic.SortedSet<string> strsetProp = strPropNameToGetErrorsFor == null
					? [nameof(Domain), nameof(Ports), nameof(SslPorts)]
					: [strPropNameToGetErrorsFor];

				foreach(string strCurPropToGetErrorsFor in strsetProp)
					switch(strCurPropToGetErrorsFor)
					{
						case nameof(Domain):
							if(Domain == "")
								strsetErrors.Add(Rsrcs.strServerInvalidWithOutDomain);

							break;

						case nameof(Ports):
						case nameof(SslPorts):
							if(Ports.Count == 0 || SslPorts.Count == 0)
								strsetErrors.Add(Rsrcs.strServerInvalidWithOutAtLeastOnePort);

							break;

						default: // Just ignore values we don't know what to do with
							break;
					}

				return strsetErrors;
			}

			public void Save()
			{
				if(WereChangesMade)
				{
					serverOriginal.Domain = Domain;
					serverOriginal.bEnabled = bEnabled;

					if(Ports.SetEquals(serverOriginal.Ports))
					{
						serverOriginal.ClearPorts();
						foreach(ushort usCurPort in Ports)
							serverOriginal.AddPort(usCurPort);
					}

					if(SslPorts.SetEquals(serverOriginal.SslPorts))
					{
						serverOriginal.ClearSslPorts();
						foreach(ushort usCurSslPort in SslPorts)
							serverOriginal.AddSslPort(usCurSslPort);
					}
				}
			}
		}
	#endregion

	#region Members
		private readonly Net netParent;


		private string strDomain;

		private bool bEnabled;

		private readonly System.Collections.Generic.SortedSet<ushort> setPorts = [];

		private readonly System.Collections.Generic.SortedSet<ushort> setSslPorts = [];
	#endregion

	#region Properties
		public Net Parent
			=> netParent;

		public string Domain
		{
			get => strDomain;

			set
			{
				if(strDomain != value)
				{
					string strOldDomain = strDomain;

					strDomain = value;

					MakeDirty();

					FireDomainChanged(strOldDomain);
				}
			}
		}

		public bool IsEnabled
		{
			get => bEnabled;

			protected set
			{
				if(netParent is not UserNet)
					throw new System.InvalidProgramException("ServerInfo instances owned by anything other than a UserNetwork are readonly.");

				if(bEnabled != value)
				{
					bEnabled = value;

					MakeDirty();

					FireEnabledChanged();
				}
			}
		}

		public System.Collections.Generic.IReadOnlySet<ushort> Ports
			=> setPorts;

		public string PortsAsText
			=> string.Join(strPortDelimiter, setPorts);

		public System.Collections.Generic.IReadOnlySet<ushort> SslPorts
			=> setSslPorts;

		public System.Collections.Generic.IEnumerable<ushort> AllPossiblePorts
			=> setPorts.Union(setSslPorts);

		public string SslPortsAsText
			=> string.Join(strPortDelimiter, setSslPorts);
	#endregion

	#region Methods
		protected void FirePropChanged(in string strPropName)
			=> PropertyChanged?.Invoke(this, new(strPropName));

		protected void FireDomainChanged(in string strOldDomain)
		{
			FirePropChanged(nameof(Domain));

			evtDomainChanged?.Invoke(this, strOldDomain, strDomain);
		}

		protected void FireEnabledChanged()
		{
			FirePropChanged(nameof(IsEnabled));

			evtIsEnabledChanged?.Invoke(this, bEnabled);
		}

		protected void FirePortsChanged(in CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(Ports));

			evtPortsChanged?.Invoke(this, setPorts, collectionChangeType);
		}

		protected void FireSslPortsChanged(in CollectionChangeType collectionChangeType)
		{
			FirePropChanged(nameof(SslPorts));

			evtSslPortsChanged?.Invoke(this, SslPorts, collectionChangeType);
		}

		protected void AddPort(in ushort usNewPort)
		{
			if(setPorts.Add(usNewPort))
			{
				MakeDirty();

				FirePortsChanged(CollectionChangeType.add);
			}
		}

		protected void RemovePort(in ushort usPortToRemove)
		{
			if(setPorts.Remove(usPortToRemove))
			{
				MakeDirty();

				FirePortsChanged(CollectionChangeType.removed);
			}
		}

		protected void ClearPorts()
		{
			if(setPorts.Count > 0)
			{
				setPorts.Clear();

				MakeDirty();

				FirePortsChanged(CollectionChangeType.removed);
			}
		}

		protected void AddSslPort(in ushort usNewSslPort)
		{
			if(setSslPorts.Add(usNewSslPort))
			{
				MakeDirty();

				FireSslPortsChanged(CollectionChangeType.add);
			}
		}

		protected void RemoveSslPort(in ushort usSslPortToRemove)
		{
			if(setSslPorts.Remove(usSslPortToRemove))
			{
				MakeDirty();

				FireSslPortsChanged(CollectionChangeType.removed);
			}
		}

		protected void ClearSslPorts()
		{
			if(setSslPorts.Count > 0)
			{
				setSslPorts.Clear();

				MakeDirty();

				FireSslPortsChanged(CollectionChangeType.removed);
			}
		}

		public Editable MakeEditableVersion(in UserNet.Editable eunetParent)
			=> new(eunetParent, this);

		public DTO.NetServerInfoDTO ToDTO()
			=> new(strDomain, [.. setPorts], [.. setSslPorts], bEnabled);
	#endregion

	#region Event Handlers
	#endregion
}