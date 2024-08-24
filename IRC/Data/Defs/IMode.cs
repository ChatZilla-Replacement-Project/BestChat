// Ignore Spelling: Defs

namespace BestChat.IRC.Data.Defs
{
	public interface IMode
	{
		char ModeChar
		{
			get;
		}

		LocalizedTextSystem Desc
		{
			get;
		}

		bool NotAlwaysAvailable
		{
			get;
		}

		System.Collections.Generic.IReadOnlyDictionary<string, ModeParam>? ParamsByName
		{
			get;
		}
	}
}