namespace BestChat.IRC.Data.Prefs;

public interface IReadOnlyOneStep
{
	System.Guid GUID
	{
		get;
	}

	GlobalAutoPerformOneStep.CmdCall WhatToDo
	{
		get;
	}
}