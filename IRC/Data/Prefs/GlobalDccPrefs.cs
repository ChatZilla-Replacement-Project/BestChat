namespace BestChat.IRC.Data.Prefs
{
public class GlobalDccPrefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
	public GlobalDccPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
		: base(mgrParent, "DCC (Direct Client Chat)", PrefsRsrcs.strGlobalDccTitle, PrefsRsrcs
			.strGlobalDccTitle)
	{
		enabled = new(this, "Enabled", PrefsRsrcs.strGlobalDccEnabledTitle,
			PrefsRsrcs.strGlobalDccEnabledDesc, false);
		getIpFromServer = new(this, "Get IP From Server", PrefsRsrcs
				.strGlobalDccGetIpFromServerTitle, PrefsRsrcs.strGlobalDccGetIpFromServerDesc,
			false);
		downloadsFolder = new(this, "Downloads Folder", PrefsRsrcs
				.strGlobalDccDownloadsFolderTitle, PrefsRsrcs.strGlobalDccDownloadsFolderDesc,
			null);
		llistPorts = [];
	}

	public GlobalDccPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO<GlobalDtoType>
		.GlobalDTO.DccDTO dto) :
		base(mgrParent, "DCC (Direct Client Chat)", PrefsRsrcs.strGlobalDccTitle, PrefsRsrcs.strGlobalDccTitle)
	{
		enabled = new(this, "Enabled", PrefsRsrcs.strGlobalDccEnabledTitle,
			PrefsRsrcs.strGlobalDccEnabledDesc, dto.Enabled);
		getIpFromServer = new(this, "Get IP From Server", PrefsRsrcs
			.strGlobalDccGetIpFromServerTitle, PrefsRsrcs.strGlobalDccGetIpFromServerDesc, dto
			.GetLocalIpFromServer ?? false);
		downloadsFolder = new(this, "Downloads Folder", PrefsRsrcs
			.strGlobalDccDownloadsFolderTitle, PrefsRsrcs.strGlobalDccDownloadsFolderDesc, dto
			.DownloadsFolder);
		llistPorts = dto.Ports == null
			? []
			: new(dto.Ports);
	}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
	private readonly Platform.DataAndExt.Prefs.Item<bool> enabled;

	private readonly Platform.DataAndExt.Prefs.Item<bool> getIpFromServer;

	private readonly Platform.DataAndExt.Prefs.Item<System.IO.DirectoryInfo?> downloadsFolder;

	private readonly System.Collections.Generic.LinkedList<int> llistPorts;
	#endregion

	#region Properties
	public Platform.DataAndExt.Prefs.Item<bool> Enabled
		=> enabled;

	public Platform.DataAndExt.Prefs.Item<bool> GetIpFromServer
		=> getIpFromServer;

	public Platform.DataAndExt.Prefs.Item<System.IO.DirectoryInfo?> DownloadsFolder
		=> downloadsFolder;

	public System.Collections.Generic.IReadOnlyCollection<int> Ports
		=> llistPorts;
	#endregion

	#region Methods
	public virtual DTO.IrcDTO<GlobalDtoType>.GlobalDTO.DccDTO ToDTO()
		=> new(
			enabled.CurVal,
			getIpFromServer.CurVal,
			downloadsFolder.CurVal,
			[.. llistPorts]
		);
	#endregion

	#region Event Handlers
	#endregion
}
}