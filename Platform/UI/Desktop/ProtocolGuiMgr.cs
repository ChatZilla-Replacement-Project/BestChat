// Ignore Spelling: Ctrl gvc Prefs cmgr Defs Loc

namespace BestChat.Platform.Ctrls.Desktop;

public sealed class ProtocolGuiMgr : DataAndExt.Protocol.Mgr<ProtocolGuiMgr.IProtocolGuiDef>
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
		public interface IProtocolGuiDef : DataAndExt.Protocol.IProtocolDef
		{
			void RegisterPrefCtrlMap();

			AbstractVisualConversationCtrl MakeConversationCtrl(DataAndExt.Conversations.IGroupViewOrConversation gvcWhatWeNeedCtrlFor);

			Avalonia.Controls.MenuItem? FileMenuItem
				=> null;
		}
	#endregion

	#region Members
		public static readonly ProtocolGuiMgr? mgr = null;
	#endregion

	#region Properties
		public static ProtocolGuiMgr Mgr => mgr ?? throw new System.InvalidProgramException("Call BestChat.Platform.Ctrls.Desktop" +
			".ProtocolGuiMgr.Init before accessing Mgr.");
	#endregion

	#region Methods
		public static ProtocolGuiMgr Init(in System.IO.DirectoryInfo dirProfileLoc, System.Func<ProtocolMetaData, bool> funcNewProtocolEnabler)
			=> new(dirProfileLoc, funcNewProtocolEnabler);
	#endregion

	#region Event Handlers
	#endregion
}