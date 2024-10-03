namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class Int : Abstract
{
	private Int() :
		base(@"int", Rsrcs.strParamTypeInt, Rsrcs.strParamTypeIntDesc, typeof(int))
	{
	}

	public static Int Instance
	{
		get;

		private set;
	} = new();
}