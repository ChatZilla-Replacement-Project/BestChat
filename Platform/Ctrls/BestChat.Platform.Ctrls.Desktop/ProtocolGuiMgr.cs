// Ignore Spelling: Ctrl gvc Prefs cmgr Defs

namespace BestChat.Platform.Ctrls.Desktop;

public sealed class ProtocolGuiMgr : DataAndExt.Protocol.ProtocolMgr
{
	#region Constructors & Deconstructors
		public ProtocolGuiMgr()
		{
		}

		static ProtocolGuiMgr() => mgr = new();
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public abstract class ProtocolGuiDef : DataAndExt.Protocol.ProtocolMgr.IProtocolDef
		{
			public abstract string Name
			{
				get;
			}

			public abstract string Publisher
			{
				get;
			}

			public abstract System.Uri Homepage
			{
				get;
			}

			public abstract System.Uri PublisherHomepage
			{
				get;
			}

			public abstract DataAndExt.Prefs.AbstractChildMgr? ProtocolMgrForRootPrefObj
			{
				get;
			}

			public abstract DataAndExt.Conversations.IGroupViewOrConversation? TopLevelViewGroupOrConversation
			{
				get;
			}

			public abstract bool GuiRecommended
			{
				get;
			}

			public abstract void RegisterPrefCtrlMap();

			public abstract AbstractVisualConversationCtrl MakeConversationCtrl(DataAndExt.Conversations.IGroupViewOrConversation
				gvcWhatWeNeedCtrlFor);

			public virtual Avalonia.Controls.MenuItem? FileMenuItem => null;
		}
	#endregion

	#region Members
		public static readonly ProtocolGuiMgr mgr;
	#endregion

	#region Properties
		public new System.Collections.Generic.IReadOnlyDictionary<string, ProtocolGuiDef> AllProtocolDefsByName
			=> (System.Collections.Generic.IReadOnlyDictionary<string, ProtocolGuiDef>)base.AllProtocolDefsByName;

		public static ProtocolGuiMgr Mgr => mgr;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}