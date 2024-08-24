// Ignore Spelling: IRC Defs eunet evt Ssl dserver eus unet

namespace BestChat.IRC.Data.Defs;

public class ServerInfo : Platform.DataAndExt.Obj<ServerInfo>
{
	#region Constructors & Deconstructors
		public ServerInfo(in Network netParent)
		{
			this.netParent = netParent;
			strDomain = "";
		}

		public ServerInfo(in Network netParent, in string strDomain, in System.Collections.Generic.IEnumerable<ushort> eusPorts, in System
			.Collections.Generic.IEnumerable<ushort> eusSslPorts)
		{
			this.netParent = netParent;
			this.strDomain = strDomain;
			bEnabled = true;

			setPorts.UnionWith(eusPorts);
			setSslPorts.UnionWith(eusSslPorts);

			MakeDirty();
		}

		protected ServerInfo(in UserNetwork.Editable eunetParent, in ServerInfo serverCopyThis)
		{
			if(eunetParent.unetOriginal != serverCopyThis.netParent)
				throw new System.InvalidProgramException($"Editable ServerInfo instances must be owned by an Editable created from the parent of the value in {nameof(serverCopyThis)}");

			netParent = eunetParent;

			strDomain = serverCopyThis.strDomain;
			bEnabled = serverCopyThis.bEnabled;

			setPorts.UnionWith (serverCopyThis.Ports);
			setSslPorts.UnionWith(serverCopyThis.SslPorts);
		}

		public ServerInfo(in Network netParent, in DTO.ServerInfoDTO dserverUs)
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
		public event DBoolFieldChanged? evtIsEnabledChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>? evtPortsChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>? evtSslPortsChanged;

		public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
		public const string strPortDelimiter = ", ";
	#endregion

	#region Helper Types
		public class Editable : ServerInfo
		{
			public Editable(in UserNetwork.Editable eunetParent, in ServerInfo serverOriginal) :
				base(eunetParent, serverOriginal) => this.serverOriginal = serverOriginal;

			public readonly ServerInfo serverOriginal;

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

			public void Save()
			{
				if(WereChangesMade)
				{
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
		private readonly Network netParent;


		private readonly string strDomain;

		private bool bEnabled;

		private readonly System.Collections.Generic.SortedSet<ushort> setPorts = [];

		private readonly System.Collections.Generic.SortedSet<ushort> setSslPorts = [];
	#endregion

	#region Properties
		public string Domain
			=> strDomain;

		public bool IsEnabled
		{
			get => bEnabled;

			protected set
			{
				if(netParent is not UserNetwork)
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

		public string SslPortsAsText
			=> string.Join(strPortDelimiter, setSslPorts);
	#endregion

	#region Methods
		protected void FirePropChanged(in string strPropName)
		{
			PropertyChanged?.Invoke(this, new(strPropName));
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

		public Editable MakeEditableVersion(in UserNetwork.Editable eunetParent)
			=> new(eunetParent, this);
	#endregion

	#region Event Handlers
	#endregion
}