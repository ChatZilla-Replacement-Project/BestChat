using System.Linq;

namespace BestChat.IRC.Data.Defs;

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
			get => base.NickServ;

			set
			{
				// ReSharper disable once InvertIf
				if(base.NickServ != value)
				{
					base.NickServ = value;

					WereChangesMade = true;
				}
			}
		}

		public new ChanServOpts? ChanServ
		{
			get => base.ChanServ;

			set
			{
				// ReSharper disable once InvertIf
				if(unetOriginal.ChanServ != value)
				{
					base.ChanServ = value;

					WereChangesMade = true;
				}
			}
		}

		public new AlisOpts AlisStatus
		{
			get => base.AlisStatus;

			set
			{
				// ReSharper disable once InvertIf
				if(base.AlisStatus != value)
				{
					base.AlisStatus = value;

					WereChangesMade = true;
				}
			}
		}

		public new QOpts QStatus
		{
			get => base.QStatus;

			set
			{
				// ReSharper disable once InvertIf
				if(base.QStatus != value)
				{
					base.QStatus = value;

					WereChangesMade = true;
				}
			}
		}

		public new LogInModes LogInMode
		{
			get => base.LogInMode;

			set
			{
				// ReSharper disable once InvertIf
				if(unetOriginal.LogInMode != value)
				{
					base.LogInMode = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInChallengeUserName
		{
			get => base.LogInChallengeUserName;

			set
			{
				// ReSharper disable once InvertIf
				if(base.LogInChallengeUserName != value)
				{
					base.LogInChallengeUserName = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInChallengeBncName
		{
			get => base.LogInChallengeBncName;

			set
			{
				// ReSharper disable once InvertIf
				if(base.LogInChallengeBncName != value)
				{
					base.LogInChallengeBncName = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInChallengePwd
		{
			get => base.LogInChallengePwd;

			set
			{
				// ReSharper disable once InvertIf
				if(base.LogInChallengePwd != value)
				{
					base.LogInChallengePwd = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInUserName
		{
			get => base.LogInUserName;

			set
			{
				// ReSharper disable once InvertIf
				if(base.LogInUserName != value)
				{
					base.LogInUserName = value;

					WereChangesMade = true;
				}
			}
		}

		public new string? LogInPwd
		{
			get => base.LogInPwd;

			set
			{
				// ReSharper disable once InvertIf
				if(base.LogInPwd != value)
				{
					base.LogInPwd = value;

					WereChangesMade = true;
				}
			}
		}

		public new System.IO.FileInfo? LogInSaslCert
		{
			get => base.LogInSaslCert;

			set
			{
				// ReSharper disable once InvertIf
				if(base.LogInSaslCert != value)
				{
					base.LogInSaslCert = value;

					WereChangesMade = true;
				}
			}
		}

		public bool WereChangesMade
		{
			get;

			private set;
		}

		public bool HasErrors
			=> Name == "" || UserNetMgr.mgr.AllItems.ContainsKey(Name) && UserNetMgr.mgr.AllItems[Name] != unetOriginal;

		public bool IsValid
			=> !HasErrors;
	#endregion

	#region Methods
		public void Save()
			=> unetOriginal.SaveFrom(this);

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
			base.ResetServerDomainList();

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
				?(new string[]
					{
						Rsrcs.strUserNetNameBlank
					})
				:UserNetMgr.mgr.AllItems.ContainsKey(Name) && UserNetMgr.mgr.AllItems[Name] != unetOriginal
					?(new string[]
						{
							Rsrcs.strUserNetNameTaken
						})
					:(System.Collections.IEnumerable)System.Array.Empty<string>();
	#endregion
}