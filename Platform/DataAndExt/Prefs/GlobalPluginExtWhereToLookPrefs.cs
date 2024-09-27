namespace BestChat.Platform.DataAndExt.Prefs
{
public class GlobalPluginExtWhereToLookPrefs : AbstractChildMgr
{
	#region Constructors & Deconstructors
	public GlobalPluginExtWhereToLookPrefs(AbstractMgr mgrParent) :
		base(mgrParent, "Where To Look", Rsrcs
			.strGlobalPluginsExtWhereToLookTitle, Rsrcs
			.strGlobalPluginsExtWhereToLookDesc)
	{
		paths = new(this, "Paths", Rsrcs
			.strGlobalPluginsExtWhereToLookPathsTitle, Rsrcs
			.strGlobalPluginsExtWhereToLookPathsDesc, []);

		includeSysPath = new(this, "Include Your System Path " +
			"Environment Variable in the Search", Rsrcs
				.strGlobalPluginsExtWhereToLookIncludeSysPathTitle, Rsrcs
				.strGlobalPluginsExtWhereToLookIncludeSysPathDesc, true);
	}

	internal GlobalPluginExtWhereToLookPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.PluginsDTO
		.ExtDTO.WhereToLookDTO dto) :
		base(mgrParent, "Where To Look", Rsrcs
			.strGlobalPluginsExtWhereToLookTitle,Rsrcs.strGlobalPluginsExtWhereToLookDesc)
	{
		paths = new(this, "Paths", Rsrcs
				.strGlobalPluginsExtWhereToLookPathsTitle, Rsrcs
				.strGlobalPluginsExtWhereToLookPathsDesc, [], dto.Paths ??
			[]);


		includeSysPath = new(this, "Include Your System Path " +
			"Environment Variable in the Search", Rsrcs
				.strGlobalPluginsExtWhereToLookIncludeSysPathTitle, Rsrcs
				.strGlobalPluginsExtWhereToLookIncludeSysPathDesc, dto.IncludeSysPaths);
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
	private readonly ReorderableListItem<System.IO.DirectoryInfo> paths;

	private readonly Item<bool> includeSysPath;
	#endregion

	#region Properties
	public ReorderableListItem<System.IO.DirectoryInfo> Paths
		=> paths;

	public Item<bool> IncludeSysPath
		=> includeSysPath;
	#endregion

	#region Methods
	public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.WhereToLookDTO ToDTO()
		=> new([.. paths], includeSysPath.CurVal);
	#endregion

	#region Event Handlers
	#endregion
}
}