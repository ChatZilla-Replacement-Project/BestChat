namespace BestChat.IRC.Data.Prefs.DTO
{
public record GlobalAutoPerformDTO
(
	GlobalAutoPerformDTO.OneStepDTO[]? WhenStartingBestChat = null,
	GlobalAutoPerformDTO.OneStepDTO[]? WhenJoiningNet = null,
	GlobalAutoPerformDTO.OneStepDTO[]? WhenJoiningChan = null,
	GlobalAutoPerformDTO.OneStepDTO[]? WhenOpeningUserChat = null,
	string? KeyOverride = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/AutoPerform")
{
	public record OneStepDTO
	(
		System.Guid GUID,
		string WhatToDo
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global/AutoPerform/OnEvt/OneStep");
}
}