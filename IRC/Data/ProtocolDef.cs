// Ignore Spelling: Ctrl cmgr gvc vgc Prefs

namespace BestChat.IRC.ProtocolModule;

using Platform.DataAndExt.Ext;

public abstract partial class ProtocolDef : Platform.DataAndExt.Protocol.Mgr.IProtocolDef
{
	#region Constructors & Deconstructors
		protected ProtocolDef()
		{
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		public readonly partial struct Const
		{
			public const char chMainBoldDelimiter = '*';

			public const char chMainItalicDelimiter = '/';

			public const char chMainFixedWidthDelimiter = '|';

			public const char chMainUnderlineDelimiter = '_';

			public const char chMainStrikeThroughDelimiter = '~';

			public const char chStartOfAltCode = '%';

			public const string strAltBoldStart = $"%B";

			public const string strAltItalicStart = "%I";

			public const string strAltFixedWidth = "%F";

			public const string strAltUnderlineStart = "%U";

			public const string strAltStrikeThroughStart = "%S";

			public const string strEscapedStartOfAltCode = "%%";

			public static readonly System.Text.RegularExpressions.Regex regexColorMatcher = GenColorParser();

			[System.Text.RegularExpressions.GeneratedRegex("""\%C(?<Fg>\d{1,2})(,(?<Bg>\d{1,2}))?""")]
			private static partial System.Text.RegularExpressions.Regex GenColorParser();
		}
	#endregion

	#region Helper Types
		private enum WhatTriggersParseEnd
		{
			root,
			bold,
			italic,
			boldItalic,
			fixedWidth,
			underline,
			strikeThrough,
			altReset,
		}
	#endregion

	#region Members
	#endregion

	#region Properties
		public string Name => Rsrcs.strModuleTitle;

		public string Publisher => Rsrcs.strModulePublisher;

		public System.Uri Homepage => new("https://github.com/ChatZilla-Replacement-Project");

		public System.Uri PublisherHomepage => new("https://github.com/ChatZilla-Replacement-" +
			"Project");

		public abstract Platform.DataAndExt.Prefs.AbstractChildMgr? ProtocolMgrForRootPrefObj
		{
			get;
		}

		public Platform.DataAndExt.Conversations.IGroupViewOrConversation? TopLevelViewGroupOrConversation
		{
			get;

			private set;
		}

		public bool GuiRecommended => false;
	#endregion

	#region Methods
		public System.Collections.Generic.IEnumerable<Platform.DataAndExt.Conversations.IInlinePlaceHolder>Parse(in string strParseIt)
		{
			if(strParseIt.IsEmpty())
				throw new System.ArgumentNullException(nameof(strParseIt), $"When you all IProtocolInfo.Parse {nameof(Platform.DataAndExt
					.Conversations.FmtInlinePlaceHolder)}, {nameof(strParseIt)} must be a non-null, non-empty string.");

			string strEditableParseIt = strParseIt;
			int iCharToStartAt = 0;

			return ParseInternal(ref strEditableParseIt, ref iCharToStartAt, WhatTriggersParseEnd.root);
		}

		private System.Collections.Generic.IEnumerable<Platform.DataAndExt.Conversations.IInlinePlaceHolder> ParseInternal(ref string
			strWhatIsBeingParsed, ref int iStartChar, in WhatTriggersParseEnd whatTriggersParseEnd)
		{
			System.Collections.Generic.LinkedList<Platform.DataAndExt.Conversations.IInlinePlaceHolder> llResult = [];

			for(int iCurChar = iStartChar; iCurChar < strWhatIsBeingParsed.Length; iCurChar++)
			{
				switch(strWhatIsBeingParsed[iCurChar])
				{
					case Const.chStartOfAltCode:
					{
						if(strWhatIsBeingParsed.Length == iCurChar + 1)
							break;

						System.Text.RegularExpressions.Match match = Const.regexColorMatcher.Match(strWhatIsBeingParsed[iStartChar..]);

						if(match != System.Text.RegularExpressions.Match.Empty)
						{
							int iFgColor = int.Parse(match.Groups["Fg"].Value);
							System.Collections.Generic.IEnumerable<Platform.DataAndExt.Conversations.IInlinePlaceHolder> ieInternalParseResults =
								ParseInternal(ref strWhatIsBeingParsed, ref iCurChar, WhatTriggersParseEnd.altReset);

							if(match.Groups.ContainsKey("Bg"))
								llResult.AddLast(new Data.InlinePlaceHolders.ColorFmtInlinePlaceHolder(ieInternalParseResults, iFgColor,
									int.Parse(match.Groups["Bg"].Value)));
							else
								llResult.AddLast(new Data.InlinePlaceHolders.ColorFmtInlinePlaceHolder(ieInternalParseResults, iFgColor));

							return llResult;
						}
						switch(strWhatIsBeingParsed[iCurChar..(iCurChar + 1)])
						{
							case Const.strAltBoldStart:
								llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(ParseInternal(ref strWhatIsBeingParsed,
									ref iCurChar, WhatTriggersParseEnd.altReset), Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.bold));

								break;

							case Const.strAltItalicStart:
								llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(ParseInternal(ref strWhatIsBeingParsed, ref
									iCurChar, WhatTriggersParseEnd.altReset), Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.italics));

								break;

							case Const.strAltUnderlineStart:
								llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(ParseInternal(ref strWhatIsBeingParsed, ref
									iCurChar, WhatTriggersParseEnd.altReset), Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.underline));

								break;

							case Const.strAltFixedWidth:
								llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(ParseInternal(ref strWhatIsBeingParsed, ref
									iCurChar, WhatTriggersParseEnd.altReset), Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.fixedWidth));

								break;

							case Const.strAltStrikeThroughStart:
								llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(ParseInternal(ref strWhatIsBeingParsed, ref
									iCurChar, WhatTriggersParseEnd.altReset), Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.strikeThrough));

								break;

							case Const.strEscapedStartOfAltCode:
								strWhatIsBeingParsed = strWhatIsBeingParsed[0..(iCurChar - 1)] + Const.chStartOfAltCode + (strWhatIsBeingParsed.Length >
									iCurChar + 2
										? strWhatIsBeingParsed[(iCurChar + 2)..]
										: "");

								break;
						}

						break;
					}

					case Const.chMainBoldDelimiter:
						if(whatTriggersParseEnd == WhatTriggersParseEnd.bold)
						{
							if(llResult.Count > 0)
								break;

							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(new Platform.DataAndExt.Conversations
								.IInlinePlaceHolder[]{new Platform.DataAndExt.Conversations.TextInlinePlaceHolder(strWhatIsBeingParsed[iStartChar
								..iCurChar])}, Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.bold));
						}
						else
							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(llResult, Platform.DataAndExt.Conversations
								.FmtInlinePlaceHolder.FmtTypes.bold));

						break;

					case Const.chMainItalicDelimiter:
						if(whatTriggersParseEnd == WhatTriggersParseEnd.italic)
						{
							if(llResult.Count > 0)
								break;

							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(new Platform.DataAndExt.Conversations
								.IInlinePlaceHolder[]{new Platform.DataAndExt.Conversations.TextInlinePlaceHolder(strWhatIsBeingParsed[iStartChar
								..iCurChar])}, Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.italics));
						}
						else
							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(llResult, Platform.DataAndExt.Conversations
								.FmtInlinePlaceHolder.FmtTypes.italics));

						break;

					case Const.chMainUnderlineDelimiter:
						if(whatTriggersParseEnd == WhatTriggersParseEnd.underline)
						{
							if(llResult.Count > 0)
								break;

							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(new Platform.DataAndExt.Conversations
								.IInlinePlaceHolder[]{new Platform.DataAndExt.Conversations.TextInlinePlaceHolder(strWhatIsBeingParsed[iStartChar
								..iCurChar])}, Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.underline));
						}
						else
							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(llResult, Platform.DataAndExt.Conversations
								.FmtInlinePlaceHolder.FmtTypes.underline));

						break;

					case Const.chMainFixedWidthDelimiter:
						if(whatTriggersParseEnd == WhatTriggersParseEnd.fixedWidth)
						{
							if(llResult.Count > 0)
								break;

							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(new Platform.DataAndExt.Conversations
								.IInlinePlaceHolder[]{new Platform.DataAndExt.Conversations.TextInlinePlaceHolder(strWhatIsBeingParsed[iStartChar
								..iCurChar])}, Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.fixedWidth));
						}
						else
							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(llResult, Platform.DataAndExt.Conversations
								.FmtInlinePlaceHolder.FmtTypes.fixedWidth));

						break;

					case Const.chMainStrikeThroughDelimiter:
						if(whatTriggersParseEnd == WhatTriggersParseEnd.strikeThrough)
						{
							if(llResult.Count > 0)
								break;

							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(new Platform.DataAndExt.Conversations
								.IInlinePlaceHolder[]{new Platform.DataAndExt.Conversations.TextInlinePlaceHolder(strWhatIsBeingParsed[iStartChar
								..iCurChar])}, Platform.DataAndExt.Conversations.FmtInlinePlaceHolder.FmtTypes.strikeThrough));
						}
						else
							llResult.AddLast(new Platform.DataAndExt.Conversations.FmtInlinePlaceHolder(llResult, Platform.DataAndExt.Conversations
								.FmtInlinePlaceHolder.FmtTypes.strikeThrough));

						break;

					default:
						break; // Just go to the next character
				}
			}

			return llResult;
		}
	#endregion

	#region Event Handlers
	#endregion
}