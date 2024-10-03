namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class Word : Abstract
{
	private Word() :
		base(@"word", Rsrcs.strParamTypeWord, Rsrcs.strParamTypeWordDesc, typeof(string))
	{
	}

	public static Word Instance
	{
		get;

		private set;
	} = new();
}