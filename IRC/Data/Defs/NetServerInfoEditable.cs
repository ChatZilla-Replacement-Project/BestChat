﻿namespace BestChat.IRC.Data.Defs;

public class NetServerInfoEditable : NetServerInfo, System.ComponentModel.INotifyDataErrorInfo
{
	public readonly UserNetEditable eunetParent;

	public readonly NetServerInfo serverOriginal;

	public NetServerInfoEditable(in UserNetEditable eunetParent, in NetServerInfo serverOriginal) :
		base(eunetParent, serverOriginal)
	{
		this.serverOriginal = serverOriginal;
		this.eunetParent = eunetParent;
	}

	public new UserNetEditable Parent
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
		if(Ports.Contains(usNewPort))
			return;

		base.AddPort(usNewPort);

		WereChangesMade = true;
	}

	public new void RemovePort(in ushort usPortToRemove)
	{
		if(!Ports.Contains(usPortToRemove))
			return;

		base.RemovePort(usPortToRemove);

		WereChangesMade = true;
	}

	public new void ClearPorts()
	{
		if(Ports.Count <= 0)
			return;

		base.ClearPorts();

		WereChangesMade = true;
	}

	public new void AddSslPort(in ushort usNewSslPort)
	{
		if(SslPorts.Contains(usNewSslPort))
			return;

		base.AddSslPort(usNewSslPort);

		WereChangesMade = true;
	}

	public new void RemoveSslPort(in ushort usSslPortToRemove)
	{
		if(!SslPorts.Contains(usSslPortToRemove))
			return;

		base.RemoveSslPort(usSslPortToRemove);

		WereChangesMade = true;
	}

	public new void ClearSslPorts()
	{
		if(SslPorts.Count <= 0)
			return;

		base.ClearSslPorts();

		WereChangesMade = true;
	}

	public void Save()
	{
		if(!WereChangesMade)
			return;

	}
}