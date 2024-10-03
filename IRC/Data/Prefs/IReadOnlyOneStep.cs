namespace BestChat.IRC.Data.Prefs
{
public interface IReadOnlyOneStep
{
	System.Guid GUID
	{
		get;
	}

	string WhatToDo
	{
		get;
	}
}
}