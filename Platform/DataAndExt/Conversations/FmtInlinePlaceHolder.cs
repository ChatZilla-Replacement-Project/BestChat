// Ignore Spelling: Inline Ctnts

namespace BestChat.Platform.DataAndExt.Conversations;

public class FmtInlinePlaceHolder : AbstractGroupInlinePlaceHolder
{
	#region Constructors & Deconstructors
		public FmtInlinePlaceHolder(in System.Collections.Generic.IEnumerable<IInlinePlaceHolder> ieCtnts, in FmtTypes ft) :
			base(ieCtnts)
			=> this.ft = ft;
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public enum FmtTypes
		{
			bold,
			italics,
			strikeThrough,
			underline,
			fixedWidth,
		}
	#endregion

	#region Members
		private readonly FmtTypes ft;
	#endregion

	#region Properties
		public FmtTypes FT
			=> ft;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}