namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class Bool : Abstract
{
	private Bool() :
		base(@"boolean", Rsrcs.strParamTypeBool, Rsrcs.strParamTypeBoolDesc, typeof(bool))
	{
	}

	public static Bool Instance
	{
		get;

		private set;
	} = new();
}