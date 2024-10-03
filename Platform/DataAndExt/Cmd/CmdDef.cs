namespace BestChat.Platform.DataAndExt.Cmd;

public class CmdDef : Obj<CmdDef>
{
	public CmdDef(in string strName, in string strDoc, in System.Collections.Generic.IReadOnlyList<Param>?
		irolPositionalParams = null, in System.Collections.Generic.IReadOnlyList<Param>? irolNamedParams = null)
	{
		Name = strName;
		Doc = strDoc;
		this.irolPositionalParams = irolPositionalParams;
		this.irolNamedParams = irolNamedParams;

		mapAllInstancesByName[strName] = this;
	}

	~CmdDef()
		=> mapAllInstancesByName.Remove(Name);

	public string Name
	{
		get;

		private init;
	}

	public string Doc
	{
		get;

		private init;
	}

	public System.Collections.Generic.IReadOnlyList<Param>? irolPositionalParams
	{
		get;

		private init;
	}

	public System.Collections.Generic.IReadOnlyList<Param>? irolNamedParams
	{
		get;

		private init;
	}

	private static readonly System.Collections.Generic.SortedDictionary<string, CmdDef> mapAllInstancesByName = [];

	public static System.Collections.Generic.IReadOnlyDictionary<string, CmdDef> AllInstancesByName
		=> mapAllInstancesByName;
}