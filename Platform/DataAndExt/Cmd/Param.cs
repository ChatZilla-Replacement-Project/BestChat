namespace BestChat.Platform.DataAndExt.Cmd;

public class Param(string strName, ParamTypes.Abstract pt, bool bIsRequired = false, string? strDoc = null) : Obj<Param>
{
	public string Name
		=> strName;

	public ParamTypes.Abstract TypeOfParam
		=> pt;

	public bool IsRequired
		=> bIsRequired;

	public string? Doc
		=> strDoc;

	public static Param Invalid
	{
		get;

		private set;
	} = new(System.Guid.NewGuid().ToString(), ParamTypes.Abstract.Invalid.Instance);
}