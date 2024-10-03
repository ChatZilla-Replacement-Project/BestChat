namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class FloatingPoint : Abstract
{
	private FloatingPoint() :
		base(@"floating point", Rsrcs.strParamTypeFloatingPoint, Rsrcs.strParamTypeFloatingPointDesc, typeof(double))
	{
	}

	public static FloatingPoint Instance
	{
		get;

		private set;
	} = new();
}