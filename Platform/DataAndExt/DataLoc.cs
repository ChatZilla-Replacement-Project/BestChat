// Ignore Spelling: Loc

namespace BestChat.Platform.DataAndExt;

public abstract class DataLoc
{
	private static DataLoc? instance;

	public static DataLoc? Instance
		=> instance;

	protected DataLoc()
		=> instance = this;

	public abstract System.IO.DirectoryInfo? ProfileLoc
	{
		get;
	}
}