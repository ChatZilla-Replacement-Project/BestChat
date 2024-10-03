namespace BestChat.Platform.DataAndExt.Prefs;

public class GlobalPluginPrefs : AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalPluginPrefs(AbstractMgr mgrParent) : base(mgrParent, "Plugins", Rsrcs
			.strGlobalPluginsTitle, Rsrcs.strGlobalPluginsDesc)
		{
			ext = new(this);
		}

		internal GlobalPluginPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.PluginsDTO dto) :
			base(mgrParent, "Plugins", Rsrcs.strGlobalPluginsTitle, Rsrcs
				.strGlobalPluginsDesc)
		{
			ext = new(this, dto.Ext);
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
		private readonly GlobalPluginExtPrefs ext;
	#endregion

	#region Properties
		public GlobalPluginExtPrefs Ext
			=> ext;
	#endregion

	#region Methods
		public DTO.PrefsDTO.GlobalDTO.PluginsDTO ToDTO()
			=> new(ext.ToDTO());
	#endregion

	#region Event Handlers
	#endregion
}