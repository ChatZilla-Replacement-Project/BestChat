namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class DateOnly : Abstract
{
	private DateOnly() :
		base(@"date only", Rsrcs.strParamTypeDateOnly, Rsrcs.strParamTypeDateOnlyDesc, typeof(System.DateOnly))
	{
	}

	public static DateOnly instance
	{
		get;

		private set;
	} = new();
}