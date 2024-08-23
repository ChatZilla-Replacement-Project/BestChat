// Ignore Spelling: Inline Ctnts

namespace BestChat.Platform.DataAndExt.Conversations;

public class TextInlinePlaceHolder : IInlinePlaceHolder
{
	#region Constructors & Deconstructors
		public TextInlinePlaceHolder(in string strCtnts)
			=> this.strCtnts = strCtnts;
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly string strCtnts;
	#endregion

	#region Properties
		public string Ctnts
			=> strCtnts;

		public string AsText
			=> strCtnts;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}