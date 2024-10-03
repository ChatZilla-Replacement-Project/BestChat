using BestChat.Platform.DataAndExt.Prefs;

namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class Switch : Abstract
{
	private Switch() :
		base(@"switch", Rsrcs.strParamTypeSwitch, Rsrcs.strParamTypeSwitchDesc, typeof(bool))
	{
	}

	public static Switch Instance
	{
		get;

		private set;
	} = new();

	public override bool InstancesHaveVal
		=> false;
}