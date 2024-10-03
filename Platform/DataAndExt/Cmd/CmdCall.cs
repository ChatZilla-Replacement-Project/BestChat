namespace BestChat.Platform.DataAndExt.Cmd;

public abstract class AbstractCmdCall(CmdDef cmdToRun, string strFullTextAsEntered)
{
	public CmdDef CmdDef
		=> cmdToRun;

	public string FullTextAsEntered
		=> strFullTextAsEntered;

	public class BlankCmdCall
	(
	) : AbstractCmdCall(new("", ""), "");

	public static readonly BlankCmdCall cmdcBlank = new BlankCmdCall();
}