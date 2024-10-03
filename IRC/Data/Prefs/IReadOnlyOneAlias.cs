namespace BestChat.IRC.Data.Prefs
{
public interface IReadOnlyOneAlias
{
	string Name
	{
		get;
	}

	string Cmd
	{
		get;
	}

	System.Guid GUID
	{
		get;
	}
}
}