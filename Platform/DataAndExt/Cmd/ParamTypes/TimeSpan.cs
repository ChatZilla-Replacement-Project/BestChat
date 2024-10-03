namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class TimeSpan : Abstract
{
	private TimeSpan() :
		base("time span", Rsrcs.strParamTypeTimeSpan, Rsrcs.strParamTypeTimeSpanDesc, typeof(System.TimeSpan))
	{
	}

	public static TimeSpan Instance
	{
		get;

		private set;
	} = new();
}