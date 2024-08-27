// Ignore Spelling: Ctrl gvc Prefs cmgr Defs Loc

namespace BestChat.Platform.Ctrls.Desktop;

public sealed class ProtocolGuiMgr : DataAndExt.Protocol.Mgr<ProtocolGuiMgr.ProtocolGuiDef>
{
	#region Constructors & Deconstructors
		private ProtocolGuiMgr(System.IO.DirectoryInfo dirProfileLoc, System.Func<ProtocolMetaData, bool> funcNewProtocolEnabler) :
			base(strMaskForProtocolModules, dirProfileLoc, funcNewProtocolEnabler)
		{
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
			public const string strMaskForProtocolModules = "*.ProtocolModule.Desktop.dll";
	#endregion

	#region Helper Types
		public abstract class ProtocolGuiDef : IProtocolDef
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

			public abstract DataAndExt.Prefs.AbstractChildMgr? RootPrefForProtocol
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

			public virtual Avalonia.Controls.MenuItem? FileMenuItem
				=> null;
		}
	#endregion

	#region Members
		public static readonly ProtocolGuiMgr mgr;
	#endregion

	#region Properties
		public static ProtocolGuiMgr Mgr => mgr;
	#endregion

	#region Methods
		public static ProtocolGuiMgr Init(in System.IO.DirectoryInfo dirProfileLoc, System.Func<ProtocolMetaData, bool> funcNewProtocolEnabler)
			=> new(dirProfileLoc, funcNewProtocolEnabler);
	#endregion

	#region Event Handlers
	#endregion
}