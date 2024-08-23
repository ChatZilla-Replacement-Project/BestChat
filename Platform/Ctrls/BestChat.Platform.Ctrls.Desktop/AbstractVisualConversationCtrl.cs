// Ignore Spelling: Ctrls Ctrl gvc

using BestChat.GUI.Ctrls;

namespace BestChat.Platform.Ctrls.Desktop;

public abstract class AbstractVisualConversationCtrl : AbstractVisualCtrl
{
	#region Constructors & Deconstructors
		public AbstractVisualConversationCtrl(in DataAndExt.Conversations.IGroupViewOrConversation gvc) :
			base(gvc.ProperName, gvc.LongDesc)
		{
			this.gvc = gvc;

			DataContext = gvc;
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
		protected override void OnInitialized(System.EventArgs e)
		{
			base.OnInitialized(e);

			Ctnts = lvCtnts;


		}
	#endregion

	#region Event Handlers
	#endregion
}