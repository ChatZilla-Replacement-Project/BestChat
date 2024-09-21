// Ignore Spelling: IRC Defs evt unet Serv Ssl dunetwork Bnc Pwd Sasl

using System.Linq;

namespace BestChat.IRC.Data.Defs;

public class UserNet : Net, IDataDef<Net>
{
	#region Constructors & Deconstructors
		public UserNet()
		{
		}

		public UserNet(in Net netPredefinedParent) :
			base(netPredefinedParent)
		{
			if(netPredefinedParent != null && netPredefinedParent.GetType() != typeof(PredefinedNet))
				throw new System.InvalidProgramException("The parent network for a user network must be " +
					$"predefined unless {nameof(netPredefinedParent)} is null.");

			this.netPredefinedParent = netPredefinedParent;
		}

		public UserNet(in Net netPredefinedParent, in string name, in System.Uri uriHomepage, params
				NetServerInfo[] allServers) :
			base(name, uriHomepage, allServers)
		{
			if(netPredefinedParent != null && netPredefinedParent.GetType() != typeof(PredefinedNet))
				throw new System.InvalidProgramException($"The parent network for a user network must be predefined" +
					$" unless {nameof(netPredefinedParent)} is null.");

			this.netPredefinedParent = netPredefinedParent;
		}

		public UserNet(in UserNet unetCopyThis) : base(unetCopyThis)
		{
			netPredefinedParent = unetCopyThis.netPredefinedParent;

			bAutoConnect = unetCopyThis.AutoConnect;
			bHidden = unetCopyThis.IsHidden;
			bUseSsl = unetCopyThis.UseSsl;
			usPortToUse = unetCopyThis.PortToUse;
			logInMode = unetCopyThis.LogInMode;
			strLogInChallengeUserName = unetCopyThis.IsLogInChallengeTextValid
				? unetCopyThis.LogInChallengeUserName
				: null;
			strLogInChallengeBncName = unetCopyThis.IsLogInChallengeTextValid
				? unetCopyThis.LogInChallengeBncName
				: null;
			strLogInChallengePwd = unetCopyThis.IsLogInChallengeTextValid
				? unetCopyThis.LogInChallengePwd
				: null;
			strLogInUserName = unetCopyThis.IsLogInUserNameValid
				? unetCopyThis.LogInUserName
				: null;
			strLogInPwd = unetCopyThis.IsLogInPwdValid
				? unetCopyThis.LogInPwd
				: null;
			if(unetCopyThis.IsLogInCustomStepsValid)
				foreach(string newCustomLogInStep in unetCopyThis.LogInCustomSteps)
					ocLogInCustomSteps.Add(newCustomLogInStep);
			fileLogInSaslCert = unetCopyThis.IsLogInSaslCertValid
				? unetCopyThis.LogInSaslCert
				: null;
		}

		public UserNet(in DTO.UserNetDTO dunetworkUs) : base(dunetworkUs)
		{
			bAutoConnect = dunetworkUs.AutoConnect;
			bHidden = dunetworkUs.Hidden;
			bUseSsl = dunetworkUs.UseSSL;
			usPortToUse = dunetworkUs.PortToUse;
			logInMode = dunetworkUs.LogInMode;
			strLogInChallengeUserName = dunetworkUs.LogInChallengeUserName;
			strLogInChallengeBncName = dunetworkUs.LogInChallengeBncName;
			strLogInChallengePwd = dunetworkUs.LogInChallengePwd;
			strLogInUserName = dunetworkUs.LogInUserName;
			strLogInPwd = dunetworkUs.LogInPwd;
			if(dunetworkUs.LogInCustomSteps != null)
				foreach(string curCustomLogInStep in dunetworkUs.LogInCustomSteps)
					ocLogInCustomSteps.Add(curCustomLogInStep);
			fileLogInSaslCert = dunetworkUs.LogInSaslCert;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event DBoolFieldChanged? evtAutoConnectChanged;
		public event DBoolFieldChanged? evtIsHiddenChanged;
		public event DBoolFieldChanged? evtUseSslChanged;
		public event DFieldChanged<ushort?>? evtPortToUseChanged;
		public event DFieldChanged<LogInModes>? evtLogInModeChanged;
		public event DFieldChanged<string?>? evtLogInChallengeUserNameChanged;
		public event DFieldChanged<string?>? evtLogInChallengeBncNameChanged;
		public event DFieldChanged<string?>? evtLogInChallengePwdChanged;
		public event DFieldChanged<string?>? evtLogInUserNameChanged;
		public event DFieldChanged<string?>? evtLogInPwdChanged;
		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlyCollection<string>>?
			evtLogInCustomStepsChanged;
		public event DFieldChanged<System.IO.FileInfo?>? evtLogInSaslCertChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types

	#endregion

	#region Members
		public readonly Net? netPredefinedParent;


		private bool bAutoConnect;

		private bool bHidden;

		private bool bUseSsl;

		private ushort? usPortToUse;

		private LogInModes logInMode = LogInModes.custom;

		// ReSharper disable once InconsistentNaming
		private string? strLogInChallengeUserName = null;

		// ReSharper disable once InconsistentNaming
		private string? strLogInChallengeBncName = null;

		// ReSharper disable once InconsistentNaming
		private string? strLogInChallengePwd = null;

		// ReSharper disable once InconsistentNaming
		private string? strLogInUserName = null;

		// ReSharper disable once InconsistentNaming
		private string? strLogInPwd = null;

		private readonly System.Collections.ObjectModel.ObservableCollection<string> ocLogInCustomSteps = [];

		private System.IO.FileInfo? fileLogInSaslCert = null;


		private static readonly System.Collections.Generic.SortedDictionary<char, ChanMode> mapDefChanModesByChar = new()
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

		public bool UseSsl
		{
			get => bUseSsl;

			set
			{
				if(bUseSsl != value)
				{
					bUseSsl = value;

					MakeDirty();

					FireUseSslChanged();
				}
			}
		}

		public bool IsCustom
			=> netPredefinedParent == null;

		public ushort? PortToUse
		{
			get => usPortToUse;

			set
			{
				if(usPortToUse != value)
				{
					ushort? usOldPortToUse = usPortToUse;

					usPortToUse = value;

					MakeDirty();

					FirePortToUseChanged(usOldPortToUse);
				}
			}
		}

		public bool IsBestChatChoosingPortToUse
			=> usPortToUse == null;

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

		public string LogInModeAsText
			=> logInMode switch
			{
				LogInModes.none
					=> Rsrcs.strLogInModeNoneText,

				LogInModes.saslUserNamePwd
					=> Rsrcs.strLogInModeSaslUserNamePwdText,

				LogInModes.saslCert
					=> Rsrcs.strLogInModeSaslCertText,

				LogInModes.serverPwd
					=> Rsrcs.strLogInModeServerPwdText,

				LogInModes.nickServMsg
					=> Rsrcs.strLogInModeNickServMsgText,

				LogInModes.nickServNs
					=> Rsrcs.strLogInModeNickServNsText,

				LogInModes.challenge
					=> Rsrcs.strLogInModeChallengeText,

				LogInModes.custom
					=> Rsrcs.strLogInModeCustomText,

				_
					=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<LogInModes>(logInMode,
						"While selecting the display text for the current log in mode"),
			};

		public string LogInModeDesc
			=> logInMode switch
			{
				LogInModes.none
					=> Rsrcs.strLogInModeNoneToolTip,

				LogInModes.saslUserNamePwd
					=> Rsrcs.strLogInModeSaslUserNamePwdToolTip,

				LogInModes.saslCert
					=> Rsrcs.strLogInModeSaslCertToolTip,

				LogInModes.serverPwd
					=> Rsrcs.strLogInModeServerPwdToolTip,

				LogInModes.nickServMsg
					=> Rsrcs.strLogInModeNickServMsgToolTip,

				LogInModes.nickServNs
					=> Rsrcs.strLogInModeNickServNsToolTip,

				LogInModes.challenge
					=> Rsrcs.strLogInModeChallengeToolTip,

				LogInModes.custom
					=> Rsrcs.strLogInModeCustomToolTip,

				_
					=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<LogInModes>(logInMode,
						"While selecting the descriptive text for the current log in mode"),
			};

		public bool IsLogInChallengeTextValid
			=> logInMode == LogInModes.challenge;

		public string? LogInChallengeUserName
		{
			get => IsLogInChallengeTextValid
				? strLogInChallengeUserName
				: throw new System.InvalidOperationException("The log in challenge user name field can't be accessed in " +
					"this mode.");

			set
			{
				if(!IsLogInChallengeTextValid)
					throw new System.InvalidOperationException("In order to set the log in challenge user name, you must " +
						"change the mode.");

				if(strLogInChallengeUserName != value)
				{
					string? oldLogInChallengeUserName = strLogInChallengeUserName;

					strLogInChallengeUserName = value;

					MakeDirty();

					FireLogInChallengeUserNameChanged(oldLogInChallengeUserName);
				}
			}
		}

		public string? LogInChallengeBncName
		{
			get => IsLogInChallengeTextValid
					? strLogInChallengeBncName
					: throw new System.InvalidOperationException("The current log in mode doesn't allow a bnc name");

			set
			{
				if(!IsLogInChallengeTextValid)
					throw new System.InvalidOperationException("Change the log in mode to set the user name");

				if(strLogInChallengeBncName != value)
				{
					string? oldLogInBncName = strLogInChallengeBncName;

					strLogInChallengeBncName = value;

					MakeDirty();

					FireLogInChallengeBncNameChanged(oldLogInBncName);
				}
			}
		}

		public string? LogInChallengePwd
		{
			get => IsLogInChallengeTextValid
					? strLogInChallengePwd
					: throw new System.InvalidOperationException("The current log in mode doesn't allow a password");

			set
			{
				if(!IsLogInChallengeTextValid)
					throw new System.InvalidOperationException("Change the log in mode to set the challenge text " +
						"password.");

				if(strLogInChallengePwd != value)
				{
					string? oldLogInPwd = strLogInChallengePwd;

					strLogInChallengePwd = value;

					MakeDirty();

					FireLogInChallengePwdChanged(oldLogInPwd);
				}
			}
		}

		public string FullLogInChallengeText
			=> IsLogInChallengeTextValid
				? strLogInChallengePwd == null
					? throw new System.InvalidOperationException("The challenge text can't be built until the " +
						"password is set.")
					: strLogInChallengeUserName == null || strLogInChallengeBncName == null
						? strLogInChallengePwd
						: $"{strLogInChallengeUserName}/{strLogInChallengeBncName}:{strLogInChallengePwd}"
				: throw new System.InvalidOperationException("Before you can access the full challenge text, you " +
					"need to set the mode to challenge and set the challenge password.");

		public bool IsLogInUserNameValid
			=> logInMode == LogInModes.saslUserNamePwd || logInMode == LogInModes.nickServMsg || logInMode == LogInModes
				.nickServNs;

		public string? LogInUserName
		{
			get => IsLogInUserNameValid
					? strLogInUserName
					: throw new System.InvalidOperationException("The current log in mode doesn't allow a user name");

			set
			{
				if(!IsLogInUserNameValid)
					throw new System.InvalidOperationException("Change the log in mode to set the user name");

				if(strLogInUserName != value)
				{
					string? oldLogInUserName = strLogInUserName;

					strLogInUserName = value;

					MakeDirty();

					FireLogInUserNameCanged(oldLogInUserName);
				}
			}
		}

		public bool IsLogInPwdValid
			=> logInMode == LogInModes.saslUserNamePwd || logInMode == LogInModes.nickServMsg || logInMode == LogInModes
				.nickServNs || logInMode == LogInModes.serverPwd;

		public string? LogInPwd
		{
			get => IsLogInPwdValid
					? strLogInPwd
					: throw new System.InvalidOperationException("The current log in mode doesn't allow a password");

			set
			{
				if(!IsLogInPwdValid)
					throw new System.InvalidOperationException("Change the log in mode to set the password");

				if(strLogInPwd != value)
				{
					string? oldLogInPwd = strLogInPwd;

					strLogInPwd = value;

					MakeDirty();

					FireLogInPwdChanged(oldLogInPwd);
				}
			}
		}

		public bool IsLogInSaslCertValid
			=> logInMode == LogInModes.saslCert;

		public System.IO.FileInfo? LogInSaslCert
		{
			get => IsLogInSaslCertValid
				? fileLogInSaslCert
				: throw new System.InvalidOperationException("The SASL certificate file is invalid in this log in " +
					"mode.");

			set
			{
				if(!IsLogInSaslCertValid)
					throw new System.InvalidOperationException("In order to set the SASL certificate file, first " +
						"change the mode.");

				if(fileLogInSaslCert != value)
				{
					System.IO.FileInfo fileOldLogInSaslCert = fileLogInSaslCert;

					fileLogInSaslCert = value;

					MakeDirty();

					FireLogInSaslCertChanged(fileOldLogInSaslCert);
				}
			}
		}

		public bool IsLogInCustomStepsValid
			=> logInMode == LogInModes.custom;

		public System.Collections.Generic.IReadOnlyCollection<string> LogInCustomSteps
			=> IsLogInCustomStepsValid
				? ocLogInCustomSteps
				: throw new System.InvalidOperationException("In order to access the custom log in steps, change " +
					"the mode.");

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

		public override bool HasPredefinition
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
				from NetServerInfo serverCur in EnabledServersInSearchOrder
					from ushort usCurPortOnCurServer in bUseSsl ? serverCur.SslPorts : serverCur.Ports
						select new System.Uri((bUseSsl ? "ircs://" : "irc://") + $"{serverCur.Domain}:{usCurPortOnCurServer}");
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
			FirePropChanged(nameof(UserNet));

			evtUseSslChanged?.Invoke(this, bUseSsl);
		}

		protected void FirePortToUseChanged(ushort? usOldPortToUse)
		{
			FirePropChanged(nameof(PortToUse));

			evtPortToUseChanged?.Invoke(this, usOldPortToUse, usPortToUse);
		}

		protected void FireLogInModeChanged(in LogInModes oldLogInMode)
		{
			FirePropChanged(nameof(LogInMode));
			FirePropChanged(nameof(IsLogInChallengeTextValid));
			FirePropChanged(nameof(IsLogInCustomStepsValid));
			FirePropChanged(nameof(IsLogInPwdValid));
			FirePropChanged(nameof(IsLogInUserNameValid));
			FirePropChanged(nameof(IsLogInSaslCertValid));

			evtLogInModeChanged?.Invoke(this, oldLogInMode, logInMode);
		}

		protected void FireLogInChallengeUserNameChanged(in string? oldChallengeUserName)
		{
			FirePropChanged(nameof(LogInChallengeUserName));

			evtLogInChallengeUserNameChanged?.Invoke(this, oldChallengeUserName, strLogInChallengeUserName);
		}

		protected void FireLogInChallengeBncNameChanged(in string? oldChallengeBncName)
		{
			FirePropChanged(nameof(LogInChallengeBncName));

			evtLogInChallengeBncNameChanged?.Invoke(this, oldChallengeBncName, strLogInChallengeBncName);
		}

		protected void FireLogInChallengePwdChanged(in string? oldChallengePwd)
		{
			FirePropChanged(nameof(LogInChallengePwd));

			evtLogInChallengePwdChanged?.Invoke(this, oldChallengePwd, strLogInChallengePwd);
		}

		protected void FireLogInUserNameCanged(in string? oldLogInUserName)
		{
			FirePropChanged(nameof(LogInUserName));

			evtLogInUserNameChanged?.Invoke(this, oldLogInUserName, strLogInUserName);
		}

		protected void FireLogInPwdChanged(in string? oldPwd)
		{
			FirePropChanged(nameof(LogInPwd));

			evtLogInPwdChanged?.Invoke(this, oldPwd, strLogInPwd);
		}

		protected void FireLogInCustomStepsChanged(in CollectionChangeType howTheCollectionChanged)
		{
			FirePropChanged(nameof(FireLogInCustomStepsChanged));

			evtLogInCustomStepsChanged?.Invoke(this, ocLogInCustomSteps, howTheCollectionChanged);
		}

		protected void FireLogInSaslCertChanged(in System.IO.FileInfo? fileOldLogInSaslCert)
		{
			FirePropChanged(nameof(LogInSaslCert));

			evtLogInSaslCertChanged?.Invoke(this, fileOldLogInSaslCert, fileLogInSaslCert);
		}

		protected void AddLogInCustomStep(in string newLogInCustomStep)
		{
			if(!IsLogInCustomStepsValid)
				throw new System.InvalidOperationException("Before you can add custom log in steps, you must change" +
					" the mode.");

			ocLogInCustomSteps.Add(newLogInCustomStep);

			MakeDirty();

			FireLogInCustomStepsChanged(CollectionChangeType.add);
		}

		protected void RemoveLogInStep(in string logInStepToRemove)
		{
			if(!IsLogInCustomStepsValid)
				throw new System.InvalidOperationException("Before you can remove custom log in steps, you must change" +
					" the mode.");

			ocLogInCustomSteps.Remove(logInStepToRemove);

			MakeDirty();

			FireLogInCustomStepsChanged(CollectionChangeType.removed);
		}

		protected void ChangeLogInStep(in string existingLogInStep, in string newLogInStep)
		{
			if(!IsLogInCustomStepsValid)
				throw new System.InvalidOperationException("Before you can remove custom log in steps, you must change" +
					" the mode.");

			ocLogInCustomSteps[ocLogInCustomSteps.IndexOf(existingLogInStep)] = newLogInStep;

			MakeDirty();

			FireLogInCustomStepsChanged(CollectionChangeType.changed);
		}

		protected void MoveCustomLogInStepUp(in string whichCustomLogInStep)
		{
			if(!IsLogInCustomStepsValid)
				throw new System.InvalidOperationException("Before you can move custom log in steps, you must change" +
					" the mode.");

			int iOldIndexOfCustomLogInStepBeingMoved = ocLogInCustomSteps.IndexOf(whichCustomLogInStep);

			if(iOldIndexOfCustomLogInStepBeingMoved <= 0)
				return;

			ocLogInCustomSteps.RemoveAt(iOldIndexOfCustomLogInStepBeingMoved);

			ocLogInCustomSteps.Insert(iOldIndexOfCustomLogInStepBeingMoved - 1, whichCustomLogInStep);

			MakeDirty();

			FireLogInCustomStepsChanged(CollectionChangeType.changed);
		}

		protected void MoveCustomLogInStepDown(in string whichCustomLogInStep)
		{
			if(!IsLogInCustomStepsValid)
				throw new System.InvalidOperationException("Before you can move custom log in steps, you must change" +
					" the mode.");

			int iOldIndexOfCustomLogInStepBeingMoved = ocLogInCustomSteps.IndexOf(whichCustomLogInStep);

			if(iOldIndexOfCustomLogInStepBeingMoved >= ocLogInCustomSteps.Count - 1)
				return;

			ocLogInCustomSteps.RemoveAt(iOldIndexOfCustomLogInStepBeingMoved);

			ocLogInCustomSteps.Insert(iOldIndexOfCustomLogInStepBeingMoved, whichCustomLogInStep);

			MakeDirty();

			FireLogInCustomStepsChanged(CollectionChangeType.changed);
		}

		protected void ClearCustomLogInSteps()
		{
			if(!IsLogInCustomStepsValid)
				throw new System.InvalidOperationException("Before you can move custom log in steps, you must change" +
					" the mode.");

			ocLogInCustomSteps.Clear();

			MakeDirty();

			FireLogInCustomStepsChanged(CollectionChangeType.removed);
		}

		protected override void ResetServerDomainList()
		{
			if(netPredefinedParent != null)
			{
				ClearServerDomainList();

				foreach(NetServerInfo serverCur in netPredefinedParent.AllUnsortedServers)
					AddServerDomain(serverCur);
			}
		}

		public UserNetEditable MakeEditableVersion()
			=> new(this);

		public DTO.UserNetDTO ToDto()
			=> new(
				Name,
				ServersSortedByName.Select(serverCur
					=> serverCur.ToDTO()
				).ToArray(),
				HomePage,
				NickServ,
				ChanServ,
				AlisStatus,
				QStatus,
				bAutoConnect,
				bHidden,
				bUseSsl,
				usPortToUse,
				logInMode,
				strLogInChallengeUserName,
				strLogInChallengeBncName,
				strLogInChallengePwd,
				strLogInUserName,
				strLogInPwd,
				[.. ocLogInCustomSteps],
				fileLogInSaslCert);

		public void SaveFrom(UserNetEditable eunet)
		{
			// ReSharper disable once InvertIf
			if(IsDirty)
			{
				Name = eunet.Name;
				HomePage = eunet.HomePage;

				if(eunet.AllUnsortedServers.Any(
						(NetServerInfo serverCur)
							=> serverCur.IsDirty))
				{
					ClearServerDomainList();

					foreach(NetServerInfo serverCur in eunet.AllUnsortedServers)
						AddServerDomain(serverCur);
				}

				AutoConnect = eunet.AutoConnect;
				IsHidden = eunet.IsHidden;
				UseSsl = eunet.UseSsl;
				PortToUse = eunet.PortToUse;
				LogInMode = eunet.logInMode;
				if(eunet.IsLogInChallengeTextValid)
				{
					LogInChallengeUserName = eunet.LogInChallengeUserName;
					LogInChallengeBncName = eunet.LogInChallengeBncName;
					LogInChallengePwd = eunet.LogInChallengePwd;
				}

				if(eunet.IsLogInUserNameValid)
					LogInUserName = eunet.LogInUserName;
				if(eunet.IsLogInPwdValid)
					LogInPwd = eunet.LogInPwd;
				if(eunet.IsLogInCustomStepsValid)
				{
					ClearCustomLogInSteps();
					// ReSharper disable once InconsistentNaming
					foreach(string strCurCustomLogInStep in eunet.LogInCustomSteps)
						AddLogInCustomStep(strCurCustomLogInStep);
				}

				if(eunet.IsLogInSaslCertValid)
					LogInSaslCert = eunet.LogInSaslCert;
			}
		}
	#endregion

	#region Event Handlers
	#endregion
}