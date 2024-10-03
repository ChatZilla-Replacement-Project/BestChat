namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class String : Abstract
{
	private String() :
		base(@"string", Rsrcs.strParamTypeStr, Rsrcs.strParamTypeStrDesc, typeof(string))
	{
	}

	public static String Instance
	{
		get;

		private set;
	} = new();
}