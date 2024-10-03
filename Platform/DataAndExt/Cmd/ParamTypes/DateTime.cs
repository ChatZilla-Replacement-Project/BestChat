namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class DateTime : Abstract
{
	private DateTime() :
		base("date and time", Rsrcs.strParamTypeDateTime, Rsrcs.strParamTypeDateTImeDesc, typeof(System.DateOnly))
	{
	}

	public static DateTime Instance
	{
		get;

		private set;
	} = new();
}