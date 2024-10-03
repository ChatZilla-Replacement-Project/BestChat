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
		=> new(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder
			.ApplicationData), "Best Chat"));
}

public class DataLoc : Platform.DataAndExt.DataLoc
{
	public override System.IO.DirectoryInfo? ProfileLoc
		=> new System.IO.DirectoryInfo(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment
			.SpecialFolder.ApplicationData), "ChatZilla Replacement Project", "BestChat"));
}