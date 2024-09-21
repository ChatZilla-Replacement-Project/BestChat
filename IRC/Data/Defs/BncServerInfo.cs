namespace BestChat.IRC.Data.Defs
{
	public partial class BNC
	{
		public partial class BncServerInfo : Platform.DataAndExt.Obj<BncServerInfo>
		{
			#region Constructors & Deconstructors
			public BncServerInfo(in BNC bncParent)
			{
				this.bncParent = bncParent;

				strName = "";
				strDomain = "";
			}

			public BncServerInfo(in BncServerInfo serverCopyThis)
			{
				bncParent = serverCopyThis.bncParent;

				strName = serverCopyThis.Name;
				strDomain = serverCopyThis.Domain;
			}

			protected BncServerInfo(in BNC.Editable ebncParent, BncServerInfo serverOriginal)
				: this(serverOriginal)
				=> bncParent = ebncParent;

			public BncServerInfo(in BNC bncParent, in DTO.BncDTO.ServerDTO dto)
			{
				this.bncParent = bncParent;

				strName = dto.Name;
				strDomain = dto.Domain;
			}
			#endregion

			#region Delegates
			#endregion

			#region Events
			public event DFieldChanged<string>? evtNameChanged;

			public event DFieldChanged<string>? evtDomainChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
			#endregion

			#region Members
			public readonly BNC bncParent;

			private string strName;

			private string strDomain;
			#endregion

			#region Properties
			public string Name
			{
				get => strName;

				protected set
				{
					if(strName != value)
					{
						string strOldName = strName;

						strName = value;

						MakeDirty();

						FireNameChanged(strOldName);
					}
				}
			}

			public string Domain
			{
				get => strDomain;

				protected set
				{
					if(strDomain != value)
					{
						string strOldDomain = strDomain;

						strDomain = GetDomainTrimmerRegex().Replace(value, "$1");

						MakeDirty();

						FireDomainChanged(strOldDomain);
					}
				}
			}
			#endregion

			#region Methods
			private void FireNameChanged(in string strOldName)
			{
				FirePropChanged(nameof(Name));

				evtNameChanged?.Invoke(this, strOldName, strName);
			}

			private void FireDomainChanged(in string strOldDomain)
			{
				FirePropChanged(nameof(Domain));

				evtDomainChanged?.Invoke(this, strOldDomain, strDomain);
			}

			public DTO.BncDTO.ServerDTO ToDTO()
				=> new(strName, strDomain);

			public BncServerEditable MakeEditable(BNC.Editable ebnc)
				=> new(ebnc, this);

			[System.Text.RegularExpressions.GeneratedRegex("(.*?)([/#].*)")]
			private static partial System.Text.RegularExpressions.Regex GetDomainTrimmerRegex();
			#endregion

			#region Event Handlers
			#endregion
		}
	}
}