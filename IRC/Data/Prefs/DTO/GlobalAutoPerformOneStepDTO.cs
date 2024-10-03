namespace BestChat.IRC.Data.Prefs.DTO
{
public record GlobalAutoPerformOneStepDTO
(
	System.Guid GUID,
	string WhatToDo
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global/AutoPerform/OnEvt/OneStep");
}