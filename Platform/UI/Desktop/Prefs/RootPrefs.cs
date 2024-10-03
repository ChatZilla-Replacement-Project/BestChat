// Ignore Spelling: Prefs Loc metadata cmgr Prot dto Emoticons Ctrl Msgs

using System.Linq;
namespace BestChat.Platform.UI.Desktop.Prefs;

public class RootPrefs : DataAndExt.Prefs.Prefs<GlobalPrefs, GlobalAppearancePrefs>
{
	#region Constructors & Deconstructors
		private RootPrefs(Avalonia.Controls.Window wndMain, System.IO.DirectoryInfo dirDataLoc)
		{
			this.wndMain = wndMain;
			global = new(this);


			ProtocolGuiMgr mgr = ProtocolGuiMgr.Init(dirDataLoc, AskUserIfTheyWantToEnableNewProtocol);

			foreach(ProtocolGuiMgr.IProtocolGuiDef? iprotCur in mgr.AllEnabledProtocols)
				if(iprotCur.TopLevelPrefsMgr != null)
					RegisterNewProtMgr(iprotCur.TopLevelPrefsMgr);
		}

		private RootPrefs(Avalonia.Controls.Window wndMain, System.IO.DirectoryInfo dirDataLoc, DTO.RootDTO dto)
		{
			this.wndMain = wndMain;
			global = new(this, dto.Global);


			ProtocolGuiMgr mgr = ProtocolGuiMgr.Init(dirDataLoc, AskUserIfTheyWantToEnableNewProtocol);

			foreach(ProtocolGuiMgr.IProtocolGuiDef? iprotCur in mgr.AllEnabledProtocols)
				if(iprotCur.TopLevelPrefsMgr != null)
					RegisterNewProtMgr(iprotCur.TopLevelPrefsMgr);
		}
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
		private static RootPrefs? instance = null;

		public readonly Avalonia.Controls.Window wndMain;

		public readonly GlobalPrefs global;
	#endregion

	#region Properties
		public static bool IsReady
			=> instance != null;

		public static RootPrefs Instance
			=> instance ?? throw new System.InvalidProgramException("Call BestChat.Desktop.RootPrefs.Load " +
				"before accessing the instance");

		public override GlobalPrefs Global
			=> global;
	#endregion

	#region Methods
		public static void Load(Avalonia.Controls.Window wndMain, System.IO.DirectoryInfo dirDataLoc)
		{
			if(instance != null)
				instance = Load<DTO.RootDTO>(dirDataLoc) is DTO.RootDTO dto
					? new(wndMain, dirDataLoc, dto)
					: new(wndMain, dirDataLoc);
		}

		protected override DataAndExt.Prefs.DTO.PrefsDTO ToDTO()
			=> new DTO.RootDTO(global.ToDTO());

		public bool AskUserIfTheyWantToEnableNewProtocol(DataAndExt.Protocol.Mgr<ProtocolGuiMgr.IProtocolGuiDef>
				.ProtocolMetaData metadataForProtocol)
			=> MsBox.Avalonia.MessageBoxManager.GetMessageBoxCustom(new()
			{
				ButtonDefinitions =
					[
						new()
							{
								Name = UiDesktopRsrcs.strQuestionNo,
								IsDefault = true,
								IsCancel = true,
							},
							new()
							{
								Name = UiDesktopRsrcs.strQuestionYes,
							},
					],
				ContentTitle = UiDesktopRsrcs.strPermNeededToEnableProtocolCaption,
				ContentMessage = UiDesktopRsrcs.strPermNeededToEnableProtocolQuestion,
				Icon = MsBox.Avalonia.Enums.Icon.Warning,
				WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner,
				CanResize = false,
				SizeToContent = Avalonia.Controls.SizeToContent.WidthAndHeight,
				ShowInCenter = true,
				FontFamily = Instance.Global.Appearance.Fonts.AppFonts.NormalFontFamily.OverriddenVal.CurVal,
			}).ShowWindowDialogAsync(wndMain).Result == UiDesktopRsrcs.strQuestionYes;

		public void RegisterNewProtMgr(DataAndExt.Prefs.AbstractChildMgr cmgrForProt)
			=> Add(cmgrForProt);
	#endregion

	#region Event Handlers
	#endregion
}