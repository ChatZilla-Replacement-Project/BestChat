// Ignore Spelling: IRC Defs evt unet Serv Ssl dunetwork

using System.Linq;

namespace BestChat.IRC.Data.Defs;

public class UserNetwork : Network, IDataDef<Network>
{
	#region Constructors & Deconstructors
		public UserNetwork()
		{
		}

		public UserNetwork(in Network netPredefinedParent) :
			base(netPredefinedParent)
		{
			if(netPredefinedParent != null && netPredefinedParent.GetType() != typeof(PredefinedNetwork))
				throw new System.InvalidProgramException("The parent network for a user network must be " +
					$"predefined unless {nameof(netPredefinedParent)} is null.");

			this.netPredefinedParent = netPredefinedParent;
		}

		public UserNetwork(in Network netPredefinedParent, in string strName, in System.Uri uriHomepage, params ServerInfo[] allServers) :
			base(strName, uriHomepage, allServers)
		{
			if(netPredefinedParent != null && netPredefinedParent.GetType() != typeof(PredefinedNetwork))
				throw new System.InvalidProgramException($"The parent network for a user network must be predefined unless {nameof(
					netPredefinedParent)} is null.");

			this.netPredefinedParent = netPredefinedParent;
		}

		public UserNetwork(in UserNetwork unetCopyThis) : base(unetCopyThis)
		{
			netPredefinedParent = unetCopyThis.netPredefinedParent;

			bAutoConnect = unetCopyThis.AutoConnect;
			bHidden = unetCopyThis.IsHidden;
			bUseSSL = unetCopyThis.UseSSL;
		}

		public UserNetwork(in DTO.UserNetworkDTO dunetworkUs) : base(dunetworkUs)
		{
			bAutoConnect = dunetworkUs.AutoConnect;
			bHidden = dunetworkUs.Hidden;
			bUseSSL = dunetworkUs.UseSSL;
			logInMode = LogInModes.manual;// TODO: Once we know how login mode information will be stored, add it.
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event DBoolFieldChanged? evtAutoConnectChanged;
		public event DBoolFieldChanged? evtIsHiddenChanged;
		public event DBoolFieldChanged? evtUseSSLChanged;
		public event DFieldChanged<LogInModes>? evtLogInModeChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public class Editable : UserNetwork
		{
			public Editable(UserNetwork unetOriginal) :
				base(unetOriginal)
				=> this.unetOriginal = unetOriginal;

			public readonly UserNetwork unetOriginal;

			public void Save()
			{
				if(IsDirty)
				{
					if(unetOriginal.netPredefinedParent == null)
					{
						unetOriginal.Name = Name;
						unetOriginal.Homepage = Homepage;
					}

					if(AllUnsortedServers.Any((ServerInfo serverCur)
						=> serverCur.IsDirty))
					{
						unetOriginal.ClearServerDomainList();

						foreach(ServerInfo serverCur in AllUnsortedServers)
							unetOriginal.AddServerDomain(serverCur);
					}

					unetOriginal.AutoConnect = AutoConnect;
					unetOriginal.IsHidden = IsHidden;
					unetOriginal.UseSSL = UseSSL;
				}
			}

			public ServerInfo.Editable GetBlankNewServerDomain()
				=> new ServerInfo(this).MakeEditableVersion(this);

			public new string Name
			{
				get => base.Name;

				set
				{
					base.Name = value;

					WereChangesMade = true;
				}
			}

			public new System.Uri? Homepage
			{
				get => base.Homepage;

				set
				{
					base.Homepage = value;

					WereChangesMade = true;
				}
			}

			public new NickServOpts? NickServ
			{
				get => unetOriginal.NickServ;

				set
				{
					if(unetOriginal.NickServ != value)
					{
						unetOriginal.NickServ = value;

						WereChangesMade = true;
					}
				}
			}

			public new ChanServOpts? ChanServ
			{
				get => unetOriginal.ChanServ;

				set
				{
					if(unetOriginal.ChanServ != value)
					{
						unetOriginal.ChanServ = value;

						WereChangesMade = true;
					}
				}
			}

			public new bool? HasAlis
			{
				get => unetOriginal.HasAlis;

				set
				{
					if(unetOriginal.HasAlis != value)
					{
						unetOriginal.HasAlis = value;

						WereChangesMade = true;
					}
				}
			}

			public new bool? HasQ
			{
				get => unetOriginal.HasQ;

				set
				{
					if(unetOriginal.HasQ != value)
					{
						unetOriginal.HasQ = value;

						WereChangesMade = true;
					}
				}
			}

			public override bool IsServerListDefaulted
				=> unetOriginal.IsServerListDefaulted;

			public new void AddServerDomain(in ServerInfo server)
			{
				base.AddServerDomain(server);

				WereChangesMade = true;
			}

			public new void DelServerDomain(in ServerInfo server)
			{
				base.DelServerDomain(server);

				WereChangesMade = true;
			}

			public new void ClearServerDomainList()
			{
				base.ClearServerDomainList();

				WereChangesMade = true;
			}

			public new void MoveServerDownSearchList(in ServerInfo server)
			{
				base.MoveServerDownSearchList(server);

				WereChangesMade = true;
			}

			public new void MoveServerUpSearchList(in ServerInfo server)
			{
				base.MoveServerUpSearchList(server);

				WereChangesMade = true;
			}

			public new void ResetServerDomainList()
			{
				unetOriginal.ResetServerDomainList();

				WereChangesMade = true;
			}

			public bool WereChangesMade
			{
				get;

				private set;
			}
		}
	#endregion

	#region Members
		public readonly Network? netPredefinedParent;


		private bool bAutoConnect;

		private bool bHidden;

		private bool bUseSSL;

		private LogInModes logInMode = LogInModes.manual;


		private static readonly System.Collections.Generic.SortedDictionary<char, ChanMode> mapDefChanModesByChar =
			new()
			{
				['n'] = new('n', Rsrcs.strDefChanModeNoExternMsgDesc),
				['t'] = new('t', Rsrcs.strDefChanModeTopicLockDesc),
				['s'] = new('s', Rsrcs.strDefChanModeSecretDesc),
				['p'] = new('p', Rsrcs.strDefChanModePrivateDesc),
				['m'] = new('m', Rsrcs.strDefChanModeModDesc),
				['i'] = new('i', Rsrcs.strDefChanModeInviteOnlyDesc),
				['r'] = new('r', Rsrcs.strDefChanModeRegisteredUserOnlyDesc),
				['c'] = new('c', Rsrcs.strDefChanModeNoColorDesc),
				['z'] = new('z', Rsrcs.strDefChanModeOpsModDesc),
				['f'] = new('f', Rsrcs.strDefChanModeForwardDesc, @params: new
					ModeParam[] {new("Destination", ModeParam.Types.chanName,
					Rsrcs.strDefChanModeForwardDetParamName, Rsrcs.strDefChanModeForwardDestParamDesc)}),
				['k'] = new('k', Rsrcs.strDefChanModeKeyDesc, @params: new ModeParam[]
				{new("Key", ModeParam.Types.@string, Rsrcs.strDefChanModeKeyParamName,
					Rsrcs.strDefChanModeKeyParamDesc)})
			};

		private static readonly System.Collections.Generic.SortedDictionary<char, UserMode> mapDefUserModesByChar =
			new()
			{
				['o'] = new('o', Rsrcs.strDefUserModeIrcOpDesc),
				['i'] = new('i', Rsrcs.strDefUserModeInvisibleDesc),
				['g'] = new('g', Rsrcs.strDefUserModeMsgRestrictDesc),
				['w'] = new('w', Rsrcs.strDefUserModeWallopsDesc),
				['G'] = new('g', Rsrcs.strDefUserModeMsgRestrictSoftDesc),
				['R'] = new('R', Rsrcs.strDefUserModeRestrictMsgUnidentUsersDesc)
			};
	#endregion

	#region Properties
		public bool AutoConnect
		{
			get => bAutoConnect;

			set
			{
				if(bAutoConnect != value)
				{
					bAutoConnect = value;

					MakeDirty();

					FireAutoConnectChanged();
				}
			}
		}

		public bool IsHidden
		{
			get => bHidden;

			set
			{
				if(bHidden != value)
				{
					bHidden = value;

					MakeDirty();

					FireIsHiddenChanged();
				}
			}
		}

		public bool UseSSL
		{
			get => bUseSSL;

			set
			{
				if(bUseSSL != value)
				{
					bUseSSL = value;

					MakeDirty();

					FireUseSslChanged();
				}
			}
		}

		public bool IsCustom => netPredefinedParent == null;

		public string NetworkInfoType
			=> bHidden
				? Rsrcs.strNetworkInfoTypeHidden
				: netPredefinedParent == null
					? Rsrcs.strNetworkInfoTypeCustom
					: IsServerListDefaulted
						? Rsrcs.strNetworkInfoTypeModified
						: Rsrcs.strNetworkInfoTypePredefined;

		public string NetworkInfoTypeDesc
			=> bHidden
				? Rsrcs.strNetworkInfoTypeHiddenToolTipText
				: netPredefinedParent == null
					? Rsrcs.strNetworkInfoTypeCustomToolTipText
					: IsServerListDefaulted
						? Rsrcs.strNetworkInfoTypeModifiedToolTipText
						: Rsrcs.strNetworkInfoTypePredefinedToolTipText;

		public LogInModes LogInMode
		{
			get => logInMode;

			protected set
			{
				if(logInMode != value)
				{
					LogInModes oldLogInMode = logInMode;

					logInMode = value;

					MakeDirty();

					FireLogInModeChanged(oldLogInMode);
				}
			}
		}

		public override bool IsServerListDefaulted
		{
			get
			{
				if(netPredefinedParent == null)
					return false;

				int iOurSearchLength = EnabledServerDomainsInSearchOrder.Count();
				int iPredefinedSearchLength = netPredefinedParent.EnabledServerDomainsInSearchOrder.Count();

				if(iOurSearchLength != iPredefinedSearchLength)
					return false;

				for(int iCurServerDomainIndex = 0; iCurServerDomainIndex < iOurSearchLength && iCurServerDomainIndex < iPredefinedSearchLength;
						iCurServerDomainIndex++)
					if(EnabledServerDomainsInSearchOrder.ElementAt(iCurServerDomainIndex) == netPredefinedParent.EnabledServerDomainsInSearchOrder
							.ElementAt(iCurServerDomainIndex))
						return false;

				return true;
			}
		}

		public bool HasPredefinition
			=> netPredefinedParent != null;


		public static System.Collections.Generic.IReadOnlyDictionary<char, ChanMode> AllDefChanModesByChar
			=> mapDefChanModesByChar;

		public static System.Collections.Generic.IReadOnlyDictionary<char, UserMode> AllDefUserModesByChar
			=> mapDefUserModesByChar;


		public override System.Collections.Generic.IReadOnlyDictionary<char, ChanMode> ChanModesByModeChar
			=> netPredefinedParent != null ? netPredefinedParent.ChanModesByModeChar : mapDefChanModesByChar;

		public override System.Collections.Generic.IReadOnlyDictionary<char, UserMode> UserModesByModeChar
			=> netPredefinedParent != null ? netPredefinedParent.UserModesByModeChar : mapDefUserModesByChar;


		public System.Collections.Generic.IEnumerable<System.Uri> AllEnabledServerUris
			=>
				from ServerInfo serverCur in EnabledServersInSearchOrder
					from ushort usCurPortOnCurServer in bUseSSL ? serverCur.SslPorts : serverCur.Ports
						select new System.Uri((bUseSSL ? "ircs://" : "irc://") + $"{serverCur.Domain}:{usCurPortOnCurServer}");
	#endregion

	#region Methods
		protected void FireAutoConnectChanged()
		{
			FirePropChanged(nameof(AutoConnect));

			evtAutoConnectChanged?.Invoke(this, bAutoConnect);
		}

		protected void FireIsHiddenChanged()
		{
			FirePropChanged(nameof(IsHidden));

			evtIsHiddenChanged?.Invoke(this, bHidden);
		}

		protected void FireUseSslChanged()
		{
			FirePropChanged(nameof(UserNetwork));

			evtUseSSLChanged?.Invoke(this, bUseSSL);
		}

		protected void FireLogInModeChanged(in LogInModes oldLogInMode)
		{
			FirePropChanged(nameof(LogInMode));

			evtLogInModeChanged?.Invoke(this, oldLogInMode, logInMode);
		}

		protected override void ResetServerDomainList()
		{
			if(netPredefinedParent != null)
			{
				ClearServerDomainList();

				foreach(ServerInfo serverCur in netPredefinedParent.AllUnsortedServers)
					AddServerDomain(serverCur);
			}
		}

		public Editable MakeEditableVersion()
			=> new(this);
	#endregion

	#region Event Handlers
	#endregion
}