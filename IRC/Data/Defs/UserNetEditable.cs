using System.Linq;

namespace BestChat.IRC.Data.Defs
{
	public class UserNetEditable : UserNet, System.ComponentModel.INotifyDataErrorInfo
	{
		#region Constructors & Deconstructors
		public UserNetEditable(UserNet unetOriginal) :
			base(unetOriginal)
			=> this.unetOriginal = unetOriginal;
		#endregion

		#region Events
		public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;
		#endregion

		#region Members
		public readonly UserNet unetOriginal;
		#endregion

		#region Properties
		public new string Name
		{
			get => base.Name;

			set
			{
				base.Name = value;

				WereChangesMade = true;
			}
		}

		public new System.Uri? HomePage
		{
			get => base.HomePage;

			set
			{
				base.HomePage = value;

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

		public new AlisOpts AlisStatus
		{
			get => unetOriginal.AlisStatus;

			set
			{
				if(unetOriginal.AlisStatus != value)
				{
					unetOriginal.AlisStatus = value;

					WereChangesMade = true;
				}
			}
		}

		public new QOpts QStatus
		{
			get => unetOriginal.QStatus;

			set
			{
				if(unetOriginal.QStatus != value)
				{
					unetOriginal.QStatus = value;

					WereChangesMade = true;
				}
			}
		}

		public new LogInModes LogInMode
		{
			get => unetOriginal.LogInMode;

			set
			{
				if(unetOriginal.LogInMode != value)
				{
					unetOriginal.LogInMode = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInChallengeUserName
		{
			get => unetOriginal.LogInChallengeUserName;

			set
			{
				if(unetOriginal.LogInChallengeUserName != value)
				{
					unetOriginal.LogInChallengeUserName = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInChallengeBncName
		{
			get => unetOriginal.LogInChallengeBncName;

			set
			{
				if(unetOriginal.LogInChallengeBncName != value)
				{
					unetOriginal.LogInChallengeBncName = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInChallengePwd
		{
			get => unetOriginal.LogInChallengePwd;

			set
			{
				if(unetOriginal.LogInChallengePwd != value)
				{
					unetOriginal.LogInChallengePwd = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInUserName
		{
			get => unetOriginal.LogInUserName;

			set
			{
				if(unetOriginal.LogInUserName != value)
				{
					unetOriginal.LogInUserName = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInPwd
		{
			get => unetOriginal.LogInPwd;

			set
			{
				if(unetOriginal.LogInPwd != value)
				{
					unetOriginal.LogInPwd = value;

					WereChangesMade = true;
				}
			}
		}

		public new System.IO.FileInfo? LogInSaslCert
		{
			get => unetOriginal.LogInSaslCert;

			set
			{
				if(unetOriginal.LogInSaslCert != value)
				{
					unetOriginal.LogInSaslCert = value;

					WereChangesMade = true;
				}
			}
		}

		public override bool IsServerListDefaulted
			=> unetOriginal.IsServerListDefaulted;

		public bool WereChangesMade
		{
			get;

			private set;
		}

		public bool HasErrors
			=> Name == "" || UserNetMgr.mgr.AllItems.ContainsKey(Name) && UserNetMgr.mgr.AllItems[Name] !=
				unetOriginal;
		#endregion

		#region Methods
		public void Save()
		{
			if(IsDirty)
			{
				if(unetOriginal.netPredefinedParent == null)
				{
					unetOriginal.Name = Name;
					unetOriginal.HomePage = HomePage;
				}

				if(AllUnsortedServers.Any((NetServerInfo serverCur)
																		=> serverCur.IsDirty))
				{
					unetOriginal.ClearServerDomainList();

					foreach(NetServerInfo serverCur in AllUnsortedServers)
						unetOriginal.AddServerDomain(serverCur);
				}

				unetOriginal.AutoConnect = AutoConnect;
				unetOriginal.IsHidden = IsHidden;
				unetOriginal.UseSSL = UseSSL;
				unetOriginal.PortToUse= usPortToUse;
				unetOriginal.LogInMode = logInMode;
				if(IsLogInChallengeTextValid)
				{
					unetOriginal.LogInChallengeUserName = strLogInChallengeUserName;
					unetOriginal.LogInChallengeBncName = strLogInChallengeBncName;
					unetOriginal.LogInChallengePwd = strLogInChallengePwd;
				}
				if(IsLogInUserNameValid)
					unetOriginal.LogInUserName = strLogInUserName;
				if(IsLogInPwdValid)
					unetOriginal.LogInPwd = strLogInPwd;
				if(IsLogInCustomStepsValid)
				{
					unetOriginal.ClearCustomLogInSteps();
					foreach(string strCurCustomLogInStep in ocLogInCustomSteps)
						unetOriginal.AddLogInCustomStep(strCurCustomLogInStep);
				}
				if(IsLogInSaslCertValid)
					unetOriginal.LogInSaslCert = fileLogInSaslCert;
			}
		}

		public NetServerInfoEditable GetBlankNewServerDomain()
			=> new NetServerInfo(this).MakeEditableVersion(this);

		public new void AddServerDomain(in NetServerInfo server)
		{
			base.AddServerDomain(server);

			WereChangesMade = true;
		}

		public new void DelServerDomain(in NetServerInfo server)
		{
			base.DelServerDomain(server);

			WereChangesMade = true;
		}

		public new void ClearServerDomainList()
		{
			base.ClearServerDomainList();

			WereChangesMade = true;
		}

		public new void MoveServerDownSearchList(in NetServerInfo server)
		{
			base.MoveServerDownSearchList(server);

			WereChangesMade = true;
		}

		public new void MoveServerUpSearchList(in NetServerInfo server)
		{
			base.MoveServerUpSearchList(server);

			WereChangesMade = true;
		}

		public new void ResetServerDomainList()
		{
			unetOriginal.ResetServerDomainList();

			WereChangesMade = true;
		}

		public new void AddLogInCustomStep(in string strNewLogInCustomStep)
		{
			base.AddLogInCustomStep(strNewLogInCustomStep);

			WereChangesMade = true;
		}

		public new void RemoveLogInStep(in string strLogInStepToRemove)
		{
			base.RemoveLogInStep(strLogInStepToRemove);

			WereChangesMade = true;
		}

		public new void ChangeLogInStep(in string strExistingLogInStep, in string strNewLogInStep)
		{
			base.ChangeLogInStep(strExistingLogInStep, strNewLogInStep);

			WereChangesMade = true;
		}

		public new void MoveCustomLogInStepUp(in string strWhichCustomLogInStep)
		{
			base.MoveCustomLogInStepUp(strWhichCustomLogInStep);

			WereChangesMade = true;
		}

		public new void MoveCustomLogInStepDown(in string strWhichCustomLogInStep)
		{
			base.MoveCustomLogInStepDown(strWhichCustomLogInStep);

			WereChangesMade = true;
		}

		public new void ClearCustomLogInSteps()
		{
			base.ClearCustomLogInSteps();

			WereChangesMade = true;
		}

		public System.Collections.IEnumerable GetErrors(string? strPropToGetErrorsFor)
			=> Name == ""
				? (new string[]{Rsrcs.strUserNetNameBlank})
				: UserNetMgr.mgr.AllItems.ContainsKey(Name) && UserNetMgr.mgr.AllItems[Name] != unetOriginal
					? (new string[]{Rsrcs.strUserNetNameTaken })
					: (System.Collections.IEnumerable)System.Array.Empty<string>();
		#endregion
	}
}