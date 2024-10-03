namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public abstract class AbstractParamType(string strName, string strLocalizedName, string strLocalizedDesc) :
	Obj<AbstractParamType>
{
	public string Name
	{
		get;

		private init;
	} = strName;

	public string LocalizedName
	{
		get;

		private init;
	} = strLocalizedName;

	public string LocalizedDesc
	{
		get;

		private init;
	} = strLocalizedDesc;

	public virtual bool InstancesHaveVal
		=> true;

	private static readonly System.Collections.Generic.List<AbstractParamType> listInstances = [];

	public static System.Collections.Generic.IEnumerable<AbstractParamType> Instances
		=> listInstances;
}