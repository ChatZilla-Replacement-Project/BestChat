namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class TimeOnly : Abstract
{
	private TimeOnly() :
		base(@"time only", Rsrcs.strParamTypeDateOnly, Rsrcs.strParamTypeDateOnlyDesc, typeof(System.TimeOnly))
	{
	}

	public static TimeOnly Instance
	{
		get;

		private set;
	} = new();
}