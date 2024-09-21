// Ignore Spelling: IRC Defs eunet evt Ssl dserver eus unet

using System.Linq;

namespace BestChat.IRC.Data.Defs;

public class NetServerInfo : Platform.DataAndExt.Obj<NetServerInfo>
{
	#region Constants
		public const string strPortDelimiter = ", ";
	#endregion

	#region Helper Types

	#endregion

	#region Constructors & Deconstructors

	public NetServerInfo(in Net netParent)
	{
		this.Parent = netParent;
		strDomain = "";
	}

	public NetServerInfo(
		in Net netParent,
		// ReSharper disable once InconsistentNaming
		in string strDomain,
		in System.Collections.Generic.IEnumerable<ushort> eusPorts,
		in System.Collections.Generic.IEnumerable<ushort> eusSslPorts)
	{
		this.Parent = netParent;
		this.strDomain = strDomain;
		bEnabled = true;

		ussetPorts.UnionWith(eusPorts);
		ussetSslPorts.UnionWith(eusSslPorts);

		MakeDirty();
	}

	protected NetServerInfo(in UserNetEditable eunetParent, in NetServerInfo serverCopyThis)
	{
		if(eunetParent.unetOriginal != serverCopyThis.Parent)
			throw new System.InvalidProgramException(
				$"Editable ServerInfo instances must be owned by an " +
				$"Editable created from the parent of the value in {nameof(serverCopyThis)}");

		Parent = eunetParent;

		strDomain = serverCopyThis.strDomain;
		bEnabled = serverCopyThis.bEnabled;

		ussetPorts.UnionWith(serverCopyThis.Ports);
		ussetSslPorts.UnionWith(serverCopyThis.SslPorts);
	}

	public NetServerInfo(in Net netParent, in DTO.NetServerInfoDTO dserverUs)
	{
		this.Parent = netParent;

		strDomain = dserverUs.Domain;
		bEnabled = dserverUs.IsEnabled;
		ussetPorts.UnionWith(dserverUs.Ports);
		ussetSslPorts.UnionWith(dserverUs.SslPorts);
	}

	#endregion

	#region Delegates

	#endregion

	#region Events

	public event DFieldChanged<string>? evtDomainChanged;
	public event DBoolFieldChanged? evtIsEnabledChanged;
	public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>? evtPortsChanged;
	public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<ushort>>? evtSslPortsChanged;

	#endregion

	#region Members

	private string strDomain;

	private bool bEnabled;

	private readonly System.Collections.Generic.SortedSet<ushort> ussetPorts = [];

	private readonly System.Collections.Generic.SortedSet<ushort> ussetSslPorts = [];

	#endregion

	#region Properties

	public Net Parent
	{
		get;
	}

	public string Domain
	{
		get => strDomain;

		set
		{
			if(strDomain == value)
				return;

			string strOldDomain = strDomain;

			strDomain = value;

			MakeDirty();

			FireDomainChanged(strOldDomain);
		}
	}

	public bool IsEnabled
	{
		get => bEnabled;

		protected set
		{
			if(Parent is not UserNet)
				throw new System.InvalidProgramException(
					"ServerInfo instances owned by anything other than a UserNetwork are readonly.");

			if(bEnabled != value)
			{
				bEnabled = value;

				MakeDirty();

				FireEnabledChanged();
			}
		}
	}

	public System.Collections.Generic.IReadOnlySet<ushort> Ports
		=> ussetPorts;

	public string PortsAsText
		=> string.Join(strPortDelimiter, ussetPorts);

	public System.Collections.Generic.IReadOnlySet<ushort> SslPorts
		=> ussetSslPorts;

	public System.Collections.Generic.IEnumerable<ushort> AllPossiblePorts
		=> ussetPorts.Union(ussetSslPorts);

	public string SslPortsAsText
		=> string.Join(strPortDelimiter, ussetSslPorts);

	public ushort? NextAvailablePort
	{
		get
		{
			ushort usNextUnusedPort = 0;

			while(usNextUnusedPort < ushort.MaxValue &&
						(ussetPorts.Contains(usNextUnusedPort) ||
						ussetSslPorts.Contains(usNextUnusedPort)))
				usNextUnusedPort++;

			return usNextUnusedPort == ushort.MaxValue ||
						ussetPorts.Contains(usNextUnusedPort) ||
						ussetSslPorts.Contains(usNextUnusedPort)
				? null
				: usNextUnusedPort;
		}
	}

	#endregion

	#region Methods

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

		evtPortsChanged?.Invoke(this, ussetPorts, collectionChangeType);
	}

	protected void FireSslPortsChanged(in CollectionChangeType collectionChangeType)
	{
		FirePropChanged(nameof(SslPorts));

		evtSslPortsChanged?.Invoke(this, SslPorts, collectionChangeType);
	}

	protected void AddPort(in ushort usNewPort)
	{
		if(ussetPorts.Add(usNewPort))
		{
			MakeDirty();

			FirePortsChanged(CollectionChangeType.add);
		}
	}

	protected void RemovePort(in ushort usPortToRemove)
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

	protected void AddSslPort(in ushort usNewSslPort)
	{
		if(ussetSslPorts.Add(usNewSslPort))
		{
			MakeDirty();

			FireSslPortsChanged(CollectionChangeType.add);
		}
	}

	protected void RemoveSslPort(in ushort usSslPortToRemove)
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

	public NetServerInfoEditable MakeEditableVersion(in UserNetEditable eunetParent)
		=> new(eunetParent, this);

	public void SaveFrom(NetServerInfoEditable eserver)
	{
		strDomain = eserver.Domain;
		bEnabled = eserver.IsEnabled;

		if(Ports.SetEquals(eserver.Ports))
		{
			ClearPorts();
			foreach(ushort usCurPort in eserver.Ports)
				AddPort(usCurPort);
		}

		if(SslPorts.SetEquals(eserver.SslPorts))
		{
			ClearSslPorts();
			foreach(ushort usCurSslPort in eserver.SslPorts)
				AddSslPort(usCurSslPort);
		}

		MakeDirty();
	}

	public DTO.NetServerInfoDTO ToDTO()
		=> new(strDomain, [.. ussetPorts], [.. ussetSslPorts], bEnabled);

	#endregion

	#region Event Handlers

	#endregion
}