namespace BestChat.IRC.Data.Defs
{
	public class NetServerInfoEditable : NetServerInfo, System.ComponentModel.INotifyDataErrorInfo
	{
		public readonly UserNet.Editable eunetParent;

		public readonly NetServerInfo serverOriginal;

		public NetServerInfoEditable(in UserNet.Editable eunetParent, in NetServerInfo serverOriginal) :
			base(eunetParent, serverOriginal)
		{
			this.serverOriginal = serverOriginal;
			this.eunetParent = eunetParent;
		}

		public new UserNet.Editable Parent
			=> eunetParent;

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

		public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;

		public bool HasErrors
			=> Domain != "" && (Ports.Count > 0 || SslPorts.Count > 0);

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
						if(Ports.Count == 0 ||
							SslPorts.Count == 0)
							strsetErrors.Add(Rsrcs.strServerInvalidWithOutAtLeastOnePort);

						break;
				}

			return strsetErrors;
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
}