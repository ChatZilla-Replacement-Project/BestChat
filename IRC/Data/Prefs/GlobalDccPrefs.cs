namespace BestChat.IRC.Data.Prefs;

public class GlobalDccPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalDccPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
			: base(mgrParent, "DCC (Direct Client Chat)", PrefsRsrcs.strGlobalDccTitle, PrefsRsrcs
				.strGlobalDccTitle)
		{
			enabled = new(this, "Enabled", PrefsRsrcs.strGlobalDccEnabledTitle, PrefsRsrcs.strGlobalDccEnabledDesc,
				false);
			getIpFromServer = new(this, "Get IP From Server", PrefsRsrcs.strGlobalDccGetIpFromServerTitle,
				PrefsRsrcs.strGlobalDccGetIpFromServerDesc, false);
			downloadsFolder = new(this, "Downloads Folder", PrefsRsrcs.strGlobalDccDownloadsFolderTitle, PrefsRsrcs
				.strGlobalDccDownloadsFolderDesc, null);
			rliPorts = new(this, "Allowed Ports", PrefsRsrcs.strGlobalDccTitle, PrefsRsrcs.strGlobalDccDesc,
				[]);
		}

		public GlobalDccPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.GlobalDccDTO dto) :
			base(mgrParent, "DCC (Direct Client Chat)", PrefsRsrcs.strGlobalDccTitle, PrefsRsrcs.strGlobalDccTitle)
		{
			enabled = new(this, "Enabled", PrefsRsrcs.strGlobalDccEnabledTitle, PrefsRsrcs.strGlobalDccEnabledDesc,
				dto.Enabled);
			getIpFromServer = new(this, "Get IP From Server", PrefsRsrcs.strGlobalDccGetIpFromServerTitle,
				PrefsRsrcs.strGlobalDccGetIpFromServerDesc, dto.GetLocalIpFromServer ?? false);
			downloadsFolder = new(this, "Downloads Folder", PrefsRsrcs.strGlobalDccDownloadsFolderTitle, PrefsRsrcs
				.strGlobalDccDownloadsFolderDesc, dto.DownloadsFolder);
			rliPorts = new(this, "Allowed Ports", PrefsRsrcs.strGlobalDccTitle, PrefsRsrcs.strGlobalDccDesc,
				dto?.Ports ?? [], []);
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

		private readonly Platform.DataAndExt.Prefs.ReorderableListItem<int> rliPorts;
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.Item<bool> Enabled
			=> enabled;

		public Platform.DataAndExt.Prefs.Item<bool> GetIpFromServer
			=> getIpFromServer;

		public Platform.DataAndExt.Prefs.Item<System.IO.DirectoryInfo?> DownloadsFolder
			=> downloadsFolder;

		public Platform.DataAndExt.Prefs.ReorderableListItem<int> Ports
			=> rliPorts;
	#endregion

	#region Methods
		public virtual DTO.GlobalDccDTO ToDTO()
			=> new(
				enabled.CurVal,
				getIpFromServer.CurVal,
				downloadsFolder.CurVal,
				[.. rliPorts, ]
			);
	#endregion

	#region Event Handlers
	#endregion
}