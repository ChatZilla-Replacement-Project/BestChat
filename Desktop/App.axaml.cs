// Ignore Spelling: Loc metadata

namespace BestChat.Desktop;

public partial class App : Avalonia.Application
{
	public override void Initialize() => Avalonia.Markup.Xaml.AvaloniaXamlLoader.Load(this);

	public override void OnFrameworkInitializationCompleted()
	{
		if(ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
			desktop.MainWindow = new MainWnd();

		base.OnFrameworkInitializationCompleted();
	}

	public new static App? Current => Avalonia.Application.Current as App;

	public static System.IO.DirectoryInfo LocalDataLoc
		=> new(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
			"Best Chat"));

	public static bool AskUserIfTheyWantToEnableNewProtocol(Platform.DataAndExt.Protocol.Mgr<Platform.UI.Desktop.ProtocolGuiMgr
			.IProtocolGuiDef>.ProtocolMetaData metadataForProtocol)
		=> MsBox.Avalonia.MessageBoxManager.GetMessageBoxCustom(new()
		{
			ButtonDefinitions =
				[
					new()
					{
						Name = Rsrcs.strQuestionNo,
						IsDefault = true,
						IsCancel = true,
					},
					new()
					{
						Name = Rsrcs.strQuestionYes,
					},
				],
			ContentTitle = Rsrcs.strPermNeededToEnableProtocolCaption,
			ContentMessage = Rsrcs.strPermNeededToEnableProtocolQuestion,
			Icon = MsBox.Avalonia.Enums.Icon.Warning,
			WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner,
			CanResize = false,
			SizeToContent = Avalonia.Controls.SizeToContent.WidthAndHeight,
			ShowInCenter = true,
			FontFamily = RootPrefs.Instance.Global.Appearance.Fonts.AppFonts.NormalFontFamily.OverriddenVal.CurVal,
		}).ShowWindowDialogAsync(MainWnd.Instance).Result == Rsrcs.strQuestionYes;
}