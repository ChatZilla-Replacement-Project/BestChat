// Ignore Spelling: Defs evt IRC enet eunet Serv dnetwork Pwd

using System.Linq;

namespace BestChat.IRC.Data.Defs;

public abstract class Net : Platform.DataAndExt.Obj<Net>, IDataDef<Net>
{
	#region Constructors & Deconstructors
		public Net()
		{
			mapGuidToNet[guid] = mapAllNetByName[string.Empty] = this;

			strName = string.Empty;
			uriHomepage = null;
		}

		public Net(in string strName, in System.Uri uriHomepage, params NetServerInfo[] allServers)
		{
			mapGuidToNet[guid] = mapAllNetByName[strName] = this;

			this.strName = strName;
			this.uriHomepage = uriHomepage;

			foreach(NetServerInfo serverCur in allServers)
				mapServers[serverCur.Domain] = serverCur;
		}

		protected Net(in Net netCopyThis)
		{
			mapGuidToNet[guid] = mapAllNetByName[netCopyThis.strName] = this;

			strName = netCopyThis.Name;
			uriHomepage = netCopyThis.Homepage;
			foreach(NetServerInfo serverCur in netCopyThis.AllUnsortedServers)
				mapServers[serverCur.Domain] = serverCur;
			nickServ = netCopyThis.NickServ;
			chanServ = netCopyThis.ChanServ;
			alisStatus = netCopyThis.AlisStatus;
			qStatus = netCopyThis.QStatus;
		}

		public Net(in DTO.NetDTO dnetworkUs)
		{
			mapGuidToNet[guid] = mapAllNetByName[dnetworkUs.Name] = this;

			strName = dnetworkUs.Name;
			uriHomepage = dnetworkUs.Homepage;
			foreach(DTO.NetServerInfoDTO dserverCur in dnetworkUs.Servers)
				mapServers[dserverCur.Domain] = new(this, dserverCur);
			nickServ = dnetworkUs.NickServ;
			chanServ = dnetworkUs.ChanServ;
			alisStatus = dnetworkUs.AlisStatus;
			qStatus = dnetworkUs.QStatus;
		}

		~Net()
		{
			mapAllNetByName.Remove(strName);
			mapGuidToNet.Remove(guid);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event DFieldChanged<string>? evtNameChanged;
		public event DFieldChanged<System.Uri?>? evtHomepageChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlyDictionary<string, NetServerInfo>>?
			evtServersSortedByNameChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IEnumerable<string>>?
			evtEnabledServerDomainsInSearchOrder;
		public event DFieldChanged<NickServOpts?>? evtNickServChanged;
		public event DFieldChanged<ChanServOpts?>? evtChanServChanged;
		public event DFieldChanged<AlisOpts>? evtHasAlisChanged;
		public event DFieldChanged<QOpts>? evtQStatusChanged;

		public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private static readonly System.Collections.Generic.SortedDictionary<string, Net> mapAllNetByName =
			[];

		private static readonly System.Collections.Generic.Dictionary<System.Guid, Net> mapGuidToNet =
			[];


		private string strName;

		private System.Uri? uriHomepage;


		private readonly System.Collections.Generic.Dictionary<string, NetServerInfo> mapServers =
			[];

		private NickServOpts? nickServ = null;

		private ChanServOpts? chanServ = null;

		private AlisOpts alisStatus = AlisOpts.unknown;

		private QOpts qStatus = QOpts.unknown;
	#endregion

	#region Properties
		public static System.Collections.Generic.IReadOnlyDictionary<string, Net> AllNetByName
			=> mapAllNetByName;

		public static System.Collections.Generic.IReadOnlyDictionary<System.Guid, Net> AllNetByGUID
			=> mapGuidToNet;


		public string Name
		{
			get => strName;

			protected set
			{
				if(GetType() == typeof(Net))
					throw new System.InvalidProgramException($"The names of predefined networks, like {Name}, is readonly.");

				if(strName != value)
				{
					string strOldValue = strName;

					strName = value;

					MakeDirty();

					FireNameChanged(strOldValue);
				}
			}
		}

		public System.Uri? Homepage
		{
			get => uriHomepage;

			protected set
			{
				if(GetType() == typeof(Net))
					throw new System.InvalidProgramException($"The homepage of predefined networks, like {Name}, is readonly.");

				if(uriHomepage != value)
				{
					System.Uri? uriOldHomepage = uriHomepage;

					uriHomepage = value;

					MakeDirty();

					FireHomepageChanged(uriOldHomepage);
				}
			}
		}

		public System.Collections.Generic.IEnumerable<NetServerInfo> ServersSortedByName
			=>
				from NetServerInfo curServer in mapServers.Values
				orderby curServer.Domain
				select curServer;

		public System.Collections.Generic.IEnumerable<string> EnabledServerDomainsInSearchOrder
			=> 
				from NetServerInfo curServer in mapServers.Values
				where curServer.IsEnabled
				select curServer.Domain;

		public System.Collections.Generic.IEnumerable<NetServerInfo> EnabledServersInSearchOrder
			=>
				from NetServerInfo curServer in mapServers.Values
				where curServer.IsEnabled
				select curServer;

		public System.Collections.Generic.IReadOnlyCollection<NetServerInfo> AllUnsortedServers
			=> mapServers.Values;

		public string EnabledServerDomainsInSearchOrderAsText
			=> string.Join(" ⪢ ", EnabledServerDomainsInSearchOrder);

		public NickServOpts? NickServ
		{
			get => nickServ;

			protected set
			{
				if(nickServ != value)
				{
					NickServOpts? oldNickServ = nickServ;

					nickServ = value;

					MakeDirty();

					FireNickServChanged(oldNickServ);
				}
			}
		}

		public string NickServAsText
			=> nickServ switch
				{
					null => Rsrcs.strNickServOptNull,
					NickServOpts.unknown => Rsrcs.strNickServOptUnknown,
					NickServOpts.atTheme => Rsrcs.strNickServOptAtTheme,
					_ => throw new System.NotImplementedException(),
				};

		public string NickServToolTip
			=> nickServ switch
				{
					null => Rsrcs.strNickServOptNullToolTip,
					NickServOpts.unknown => Rsrcs.strNickServOptUnknownToolTip,
					NickServOpts.atTheme => Rsrcs.strNickServOptAtThemeToolTip,
					_ => throw new System.NotImplementedException(),
				};

		public ChanServOpts? ChanServ
		{
			get => chanServ;

			protected set
			{
				if(chanServ != value)
				{
					ChanServOpts? oldChanServ = chanServ;

					chanServ = value;

					MakeDirty();

					FireChanServChanged(oldChanServ);
				}
			}
		}

		public string ChanServAsText
			=> chanServ switch
				{
					null => Rsrcs.strChanServOptNull,
					ChanServOpts.unknown => Rsrcs.strChanServOptUnknown,
					ChanServOpts.atTheme => Rsrcs.strChanServOptAtTheme,
					_ => throw new System.NotImplementedException(),
				};

		public string ChanServToolTip
			=> chanServ switch
				{
					null => Rsrcs.strChanServOptNullToolTip,
					ChanServOpts.unknown => Rsrcs.strChanServOptUnknownToolTip,
					ChanServOpts.atTheme => Rsrcs.strChanServOptAtThemeToolTip,
					_ => throw new System.NotImplementedException(),
				};

		public AlisOpts AlisStatus
		{
			get => alisStatus;

			set
			{
				if(alisStatus != value)
				{
					AlisOpts oldHasAlis = alisStatus;

					alisStatus = value;

					MakeDirty();

					FireHasAlisChanged(oldHasAlis);
				}
			}
		}

		public string HasAlisAsText
			=> alisStatus switch
				{
					AlisOpts.unknown
						=> Rsrcs.strHasAlisUnknown,

					AlisOpts.present
						=> Rsrcs.strHasAlisFalse,

					AlisOpts.notPresent
						=> Rsrcs.strHasAlisTrue,

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<AlisOpts>(alisStatus,
							"While selecting ALIS display text."),
				};

		public string HasAlisToolTip
			=> alisStatus switch
				{
					AlisOpts.unknown
						=> Rsrcs.strHasAlisUnknownToolTip,

					AlisOpts.present
						=> Rsrcs.strHasAlisTrueToolTip,

					AlisOpts.notPresent
						=> Rsrcs.strHasAlisFalseToolTip,

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<AlisOpts>(alisStatus,
							"While selecting ALIS tooltip text"),
				};

		public QOpts QStatus
		{
			get => qStatus;

			set
			{
				if(qStatus!= value)
				{
					QOpts oldHasQ = qStatus;

					qStatus = value;

					MakeDirty();

					FireHasQChanged(oldHasQ);
				}
			}
		}

		public string QStatusAsText
			=> qStatus switch
				{
					QOpts.unknown
						=> Rsrcs.strHasQUnknown,

					QOpts.notPresent
						=> Rsrcs.strHasQFalse,

					QOpts.present
						=> Rsrcs.strHasQTrue,

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<QOpts>(qStatus,
							"While getting the status of the q server as text.")
				};

		public string QStatusToolTip
			=> qStatus switch
				{
					QOpts.unknown
						=> Rsrcs.strHasQUnknownToolTip,

					QOpts.notPresent
						=> Rsrcs.strHasQFalseToolTip,

					QOpts.present
						=> Rsrcs.strHasQTrueToolTip,

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<QOpts>(qStatus,
							"While getting the tooltip for the status of the q server.")
				};

		public virtual bool IsServerListDefaulted
			=> false;

		public bool IsCustomized
			=> UserNetMgr.mgr.AllItems.ContainsKey(strName);

		public System.Collections.Generic.IEnumerable<ushort> AllPossiblePortsFromAllServers
			=> mapServers.Values.Aggregate(
					new System.Collections.Generic.HashSet<ushort>(),
					(setExisting, setNew)
						=> setExisting.Union(setNew.AllPossiblePorts).ToHashSet()
				);

		public System.Collections.Generic.IEnumerable<ushort> AllPossibleSslPortsFromAllServers
			=> mapServers.Values.Aggregate(
					new System.Collections.Generic.HashSet<ushort>(),
					(setExisting, setNew)
						=> setExisting.Union(setNew.SslPorts).ToHashSet()
				);

		public System.Collections.Generic.IEnumerable<ushort> AllPossibleNonSslPortsFromAllServers
			=> mapServers.Values.Aggregate(
					new System.Collections.Generic.HashSet<ushort>(),
					(setExisting, setNew)
						=> setExisting.Union(setNew.Ports).ToHashSet()
				);

		public abstract bool HasPredefinition
		{
			get;
		}

		public abstract System.Collections.Generic.IReadOnlyDictionary<char, ChanMode> ChanModesByModeChar
		{
			get;
		}

		public abstract System.Collections.Generic.IReadOnlyDictionary<char, UserMode> UserModesByModeChar
		{
			get;
		}
	#endregion

	#region Methods
		protected void FirePropChanged(in string strPropName)
			=> PropertyChanged?.Invoke(this, new(strPropName));

		protected void FireNameChanged(in string strOldVal)
		{
			FirePropChanged(nameof(Name));

			evtNameChanged?.Invoke(this, strOldVal, strName);
		}

		protected void FireHomepageChanged(in System.Uri? uriOldHomepage)
		{
			FirePropChanged(nameof(Homepage));

			evtHomepageChanged?.Invoke(this, uriOldHomepage, uriHomepage);
		}

		protected void FireServersSortedByNameChanged(in CollectionChangeType howTheCollectionChanged)
		{
			FirePropChanged(nameof(ServersSortedByName));

			evtServersSortedByNameChanged?.Invoke(this, mapServers, howTheCollectionChanged);
		}

		protected void FireEnabledServerDomainsInSearchOrderChanged(in CollectionChangeType howThCollectionChanged)
		{
			FirePropChanged(nameof(EnabledServerDomainsInSearchOrder));

			evtEnabledServerDomainsInSearchOrder?.Invoke(this, EnabledServerDomainsInSearchOrder, howThCollectionChanged);
		}

		protected void FireNickServChanged(in NickServOpts? oldNickServ)
		{
			FirePropChanged(nameof(NickServ));

			evtNickServChanged?.Invoke(this, oldNickServ, nickServ);
		}

		protected void FireChanServChanged(in ChanServOpts? oldNickServ)
		{
			FirePropChanged(nameof(ChanServ));

			evtChanServChanged?.Invoke(this, oldNickServ, chanServ);
		}

		protected void FireHasAlisChanged(in AlisOpts oldHasAlis)
		{
			FirePropChanged(nameof(AlisStatus));

			evtHasAlisChanged?.Invoke(this, oldHasAlis, alisStatus);
		}

		protected void FireHasQChanged(in QOpts oldQStatus)
		{
			FirePropChanged(nameof(QStatus));

			evtQStatusChanged?.Invoke(this, oldQStatus, qStatus);
		}

		protected void AddServerDomain(in NetServerInfo server)
		{
			if(mapServers.ContainsKey(server.Domain))
				throw new System.NotSupportedException("This server is already present");

			mapServers[server.Domain] = server;

			FireServersSortedByNameChanged(CollectionChangeType.add);

			server.evtIsEnabledChanged += OnServerEnabledStateChanged;
			FireEnabledServerDomainsInSearchOrderChanged(CollectionChangeType.changed);

			FirePropChanged(nameof(IsServerListDefaulted));
		}

		protected void DelServerDomain(in NetServerInfo server)
		{
			if(!mapServers.ContainsKey(server.Domain))
				return;

			mapServers.Remove(server.Domain);

			FireServersSortedByNameChanged(CollectionChangeType.removed);
			FireEnabledServerDomainsInSearchOrderChanged(CollectionChangeType.changed);

			FirePropChanged(nameof(IsServerListDefaulted));
		}

		protected void ClearServerDomainList()
		{
			mapServers.Clear();

			MakeDirty();

			FireEnabledServerDomainsInSearchOrderChanged(CollectionChangeType.changed);

			FirePropChanged(nameof(IsServerListDefaulted));
		}

		protected void MoveServerDownSearchList(in NetServerInfo server)
		{
			if(!mapServers.ContainsKey(server.Domain))
				throw new System.InvalidProgramException($"Can't move the server {server.Domain} in the search list" +
					$" for the network '{Name}' as it doesn't exist in the network.");

			System.Collections.Generic.LinkedList<NetServerInfo> llServers = new(mapServers.Values);
			mapServers.Clear();

			System.Collections.Generic.LinkedListNode<NetServerInfo>? llnNextServer = (llServers.Find(server)?.Next) ??
				llServers.Last;

			llServers.Remove(server);

			if(llnNextServer == null || llnNextServer == llServers.Last)
				llServers.AddLast(server);
			else
				llServers.AddAfter(llnNextServer, server);

			foreach(NetServerInfo serverCur in llServers)
				mapServers[serverCur.Domain] = serverCur;

			FirePropChanged(nameof(IsServerListDefaulted));
		}

		protected void MoveServerUpSearchList(in NetServerInfo server)
		{
			if(!mapServers.ContainsKey(server.Domain))
				throw new System.InvalidProgramException($"Can't move the server {server.Domain} in the search list" +
					$" for the network '{Name}' as it doesn't exist in the network.");

			System.Collections.Generic.LinkedList<NetServerInfo> llServers = new(mapServers.Values);
			mapServers.Clear();

			System.Collections.Generic.LinkedListNode<NetServerInfo>? llnPrevServer = (llServers.Find(server)?.Previous) ??
				llServers.First;

			llServers.Remove(server);

			if(llnPrevServer == null || llnPrevServer == llServers.First)
				llServers.AddFirst(server);
			else
				llServers.AddBefore(llnPrevServer, server);

			foreach(NetServerInfo serverCur in llServers)
				mapServers[serverCur.Domain] = serverCur;

			FirePropChanged(nameof(IsServerListDefaulted));
		}

		protected virtual void ResetServerDomainList()
		{
		}
	#endregion

	#region Event Handlers
		private void OnServerEnabledStateChanged(in NetServerInfo objSender, in bool bNewVal)
			=> FireEnabledServerDomainsInSearchOrderChanged(CollectionChangeType.changed);
	#endregion
}