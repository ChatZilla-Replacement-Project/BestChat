// Ignore Spelling: Ctrl gvc Prefs cmgr Defs Loc iprot Prot

using BestChat.Platform.UI.Desktop.Prefs;

namespace BestChat.Platform.UI.Desktop;

public sealed class ProtocolGuiMgr : DataAndExt.Protocol.Mgr<ProtocolGuiMgr.IProtocolGuiDef>
{
	#region Constructors & Deconstructors
		private ProtocolGuiMgr(System.IO.DirectoryInfo dirProfileLoc, System.Func<ProtocolMetaData, bool>
				funcNewProtocolEnabler) :
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

			DataAndExt.Prefs.AbstractChildMgr? RootPrefForProtocol
				=> null;

			DataAndExt.Prefs.AbstractChildMgr? LoadAllPrefsData(in System.IO.StreamReader stream)
				=> null;

			void SaveAllPrefsData(in System.IO.StreamWriter stream)
			{
			}

			AbstractVisualConversationCtrl MakeConversationCtrl(DataAndExt.Conversations.IGroupViewOrConversation
				gvcWhatWeNeedCtrlFor);

			Avalonia.Controls.MenuItem? FileMenuItem
				=> null;
		}
	#endregion

	#region Members
		public static readonly ProtocolGuiMgr? mgr = null;
	#endregion

	#region Properties
		public static ProtocolGuiMgr Mgr => mgr ?? throw new System.InvalidProgramException("Call BestChat.Platform" +
			".Ctrls.Desktop.ProtocolGuiMgr.Init before accessing Mgr.");
	#endregion

	#region Methods
		public static ProtocolGuiMgr Init(in System.IO.DirectoryInfo dirProfileLoc, System.Func<ProtocolMetaData, bool>
				funcNewProtocolEnabler)
			=> new(dirProfileLoc, funcNewProtocolEnabler);

		public override void TellAllProtocolsToSave()
		{
			foreach(IProtocolGuiDef iprotCur in AllEnabledProtocols)
			{
				using System.IO.StreamWriter writer = new(GetFileNameForProt(iprotCur));

				iprotCur.SaveAllPrefsData(writer);
			}
		}

		protected override void OnProtLoaded(IProtocolGuiDef iprotNew)
		{
			using System.IO.StreamReader stream = new(GetFileNameForProt(iprotNew));

			DataAndExt.Prefs.AbstractChildMgr? cmgrOfPrefs = iprotNew.LoadAllPrefsData(stream);

			if(cmgrOfPrefs != null && RootPrefs.IsReady)
				RootPrefs.Instance.RegisterNewProtMgr(cmgrOfPrefs);
		}
	#endregion

	#region Event Handlers
	#endregion
}