// Ignore Spelling: Ctrls Ctrl gvc

namespace BestChat.Platform.Ctrls.Desktop;

public abstract class AbstractVisualConversationCtrl : AbstractVisualCtrl
{
	#region Constructors & Deconstructors
		public AbstractVisualConversationCtrl(in DataAndExt.Conversations.IGroupViewOrConversation gvc) :
			base(gvc.ProperName, gvc.LongDesc)
		{
			this.gvc = gvc;

			DataContext = gvc;

			Initialized += OnInitialized;
		}
	#endregion

	#region Constants
	#endregion

	#region Members
		public readonly DataAndExt.Conversations.IGroupViewOrConversation gvc;

		private readonly ConversationDataListView lvCtnts = new();
	#endregion

	#region Properties
	#endregion

	#region Methods
		protected void OnInitialized(object? objSender, System.EventArgs e)
		{
			Ctnts = lvCtnts;


		}
	#endregion

	#region Event Handlers
	#endregion
}