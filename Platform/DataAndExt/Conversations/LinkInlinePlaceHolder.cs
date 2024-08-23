// Ignore Spelling: Dest Ctxt gvc Inline

namespace BestChat.Platform.DataAndExt.Conversations;

using Ext;

public partial class LinkInlinePlaceHolder : IInlinePlaceHolder
{
	#region Constructors & Deconstructors
		public LinkInlinePlaceHolder(in System.Uri uriDest, in string strText)
		{
			this.uriDest = uriDest;
			this.strText = strText;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		public static readonly System.Text.RegularExpressions.Regex regexUriMatcher = GenUriMatcher();

		public static readonly System.Text.RegularExpressions.Regex regexChanNameMatcher = GenChanNameMatcher();

		public static readonly System.Text.RegularExpressions.Regex regexPathEditor = GenPathEditor();

		public static readonly System.Text.RegularExpressions.Regex regexBestChatPathMatcher = GetBestChatPathMatcher();
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly System.Uri uriDest;

		public readonly string strText;
	#endregion

	#region Properties
		public System.Uri Dest
			=> uriDest;

		public string Text
			=> strText;

		public string AsText
			=> strText;
	#endregion

	#region Methods
		public static LinkInlinePlaceHolder? ParseLink(in string strTextFound, in IGroupViewOrConversation gvcCtxt)
			=> strTextFound.IsEmpty()
				? throw new System.ArgumentNullException(nameof(strTextFound), $"When LinkInlinePlaceHolder.ParseLink is called, {
					nameof(strTextFound)} must be a non-null, non-empty string.")
				: gvcCtxt == null
					? throw new System.ArgumentNullException(nameof(gvcCtxt), $"{nameof(gvcCtxt)} must be a non-null instance.")
					: regexUriMatcher.IsMatch(strTextFound)
						? new(new(strTextFound), strTextFound)
						: regexChanNameMatcher.IsMatch(strTextFound)
							? new(new(regexPathEditor.Replace(gvcCtxt.Path, $"$ParentPath{strTextFound}")), strTextFound)
							: regexBestChatPathMatcher.IsMatch(strTextFound)
								? new(new(strTextFound), strTextFound)
								: null;

		[System.Text.RegularExpressions.GeneratedRegex("""^((https?|ircs?|ftps?):/{2,3})?(([a-z\d\-_]+?)\.)*?[a-z\d-_]+?(/[a-z\d%\-_]+?)*?(\?[a-z\-_\d0]+?=[a-z\d\-_%]+?)?(\#[a-z\d\-_%]+?)?$""")]
		private static partial System.Text.RegularExpressions.Regex GenUriMatcher();

		[System.Text.RegularExpressions.GeneratedRegex("""^\#{1,2}[a-z\d\-_]+?$""")]
		private static partial System.Text.RegularExpressions.Regex GenChanNameMatcher();

		[System.Text.RegularExpressions.GeneratedRegex("""^(?<ParentPath>/?([a-z\d\-_]+?/)*?)(\#{,2}[a-z\d\-_]+?)$""")]
		private static partial System.Text.RegularExpressions.Regex GenPathEditor();

		[System.Text.RegularExpressions.GeneratedRegex("""^(/?([a-z\d\-_]+?/)?)/\#{,2}([a-z\d\-_]+?)$""")]
		private static partial System.Text.RegularExpressions.Regex GetBestChatPathMatcher();
	#endregion

	#region Event Handlers
	#endregion
}