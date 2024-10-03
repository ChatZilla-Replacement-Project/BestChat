namespace BestChat.IRC.Data.Defs;

public class BncInfoEditable : BncServerInfo, System.ComponentModel.INotifyDataErrorInfo
{
	#region Constructors & Deconstructors
		internal BncInfoEditable(BncEditable ebncParent, in BncServerInfo serverOriginal)
			: base(ebncParent, serverOriginal)
			=> this.serverOriginal = serverOriginal;
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly BncServerInfo serverOriginal;
	#endregion

	#region Properties
		public bool DomainIsValid
		{
			get
			{
				System.Uri uriTestOnDomain;

				try
				{
					uriTestOnDomain = new(Domain);
				}
				catch(System.UriFormatException)
				{
					return false;
				}

				return true;
			}
		}

		public bool DomainIsUnique
			=> DomainIsValid && (!serverOriginal.bncParent.AllServersByDomain.ContainsKey(Domain) || serverOriginal.bncParent
				.AllServersByDomain[Domain] != serverOriginal);

		public bool NameIsUnique
			=> Name != "" && (!serverOriginal.bncParent.AllServersByName.ContainsKey(Name) || serverOriginal.bncParent
				.AllServersByName[Name] != serverOriginal);

		public bool HasErrors
			=> Name != "" && Domain != null && !DomainIsUnique && !NameIsUnique;

		public bool IsValid
			=> !HasErrors;

		public new string Name
		{
			get => base.Name;

			set
			{
				if(base.Name == value)
					return;

				base.Name = value;

				WereChangesMade = true;
			}
		}

		public new string Domain
		{
			get => base.Domain;

			set
			{
				if(base.Domain == value)
					return;

				base.Domain = value;

				WereChangesMade = true;
			}
		}

		public bool WereChangesMade
		{
			get;

			private set;
		}
	#endregion

	#region Methods
		public System.Collections.IEnumerable GetErrors(string? strPropToGetErrorsFor)
		{
			System.Collections.Generic.SortedSet<string> strsetErrors = [];

			string[] astrPropListToGetErrorsFor = strPropToGetErrorsFor == null
				? [nameof(Name), nameof(Domain)]
				: [strPropToGetErrorsFor];

			// ReSharper disable once InconsistentNaming
			foreach(string strCurPropToGetErrorFor in astrPropListToGetErrorsFor)
				switch(strCurPropToGetErrorFor)
				{
					case nameof(Name):
						if(Name == "")
							strsetErrors.Add(Rsrcs.strBncNameBlank);
						else if(!NameIsUnique)
							strsetErrors.Add(Rsrcs.strBncNameTakenFmt);

						break;

					case nameof(Domain):
						if(Domain == "")
							strsetErrors.Add(Rsrcs.strBncServerDomainBlank);
						else if(!DomainIsValid)
							strsetErrors.Add(Rsrcs.strBncServerDomainInvalidFmt);
						else if(!DomainIsUnique)
							strsetErrors.Add(Rsrcs.strBncServerDomainNotUniqueFmt);

						break;

					default:
						throw new System.InvalidProgramException("Unknown property during error test");
				}

			return strsetErrors;
		}

		public void Save()
			=> serverOriginal.SaveFrom(this);
		#endregion

		#region Event Handlers

		#endregion
}