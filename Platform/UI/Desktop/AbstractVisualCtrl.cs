// Ignore Spelling: Ctrls Ctrl Ctnts Hdr

namespace BestChat.Platform.UI.Desktop;

/// <summary>
/// Interaction logic for NewAbstractVisualTabCtrl.xaml
/// </summary>
public abstract class AbstractVisualCtrl : Avalonia.Controls.UserControl
{
	#region Constructors & Deconstructors
		#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
			public AbstractVisualCtrl()
			{
				if(!Avalonia.Controls.Design.IsDesignMode)
					throw new System.InvalidProgramException("The default constructors of BestChat.GUI.Ctrls.AbstractVisualCtrl and its derived " +
						"classes are for designer use only.  They aren't not meant for use at runtime.");
			}
		#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

		public AbstractVisualCtrl(in string strLocalizedShortName, in string strLocalizedLongDesc)
		{
			LocalizedShortName = strLocalizedShortName;

			LocalizedLongDesc = strLocalizedLongDesc;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		#region Dependency Properties
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "WPF dependency " +
				"property names act more like names for actual properties even though they're just static members.")]
			public static readonly Avalonia.AvaloniaProperty HdrCtntsProperty = Avalonia.AvaloniaProperty.RegisterDirect<AbstractVisualCtrl,
				object?>(nameof(HdrCtnts), avcSender => avcSender.HdrCtnts, (avcSender, valNew) => avcSender
				.HdrCtnts = valNew, default, Avalonia.Data.BindingMode.Default);

			[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "WPF dependency " +
				"property names act more like names for actual properties even though they're just static members.")]
			public static readonly Avalonia.AvaloniaProperty CtntsProperty = Avalonia.AvaloniaProperty.RegisterDirect<AbstractVisualCtrl,
				object?>(nameof(Ctnts), avcSender => avcSender.Ctnts, (avcSender, objVal) => avcSender.Ctnts =
				objVal, default, Avalonia.Data.BindingMode.Default);
		#endregion

		#region Routed Events
		#endregion
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private readonly Avalonia.Controls.DockPanel dock = new();

		private readonly Avalonia.Controls.ContentControl ccHdr = new();

		private readonly Avalonia.Controls.ScrollViewer svCtnts = new();
	#endregion

	#region Properties
		public string LocalizedShortName
		{
			get;

			private init;
		}

		public string LocalizedLongDesc
		{
			get;

			private init;
		}

		public object? HdrCtnts
		{
			get => ccHdr.Content;

			set => ccHdr.Content = value;
		}

		[Avalonia.Metadata.Content]
		public object? Ctnts
		{
			get => svCtnts.Content;

			set => svCtnts.Content = value;
		}
	#endregion

	#region Methods
		protected override void OnInitialized()
		{
			base.OnInitialized();

			Content = dock;

			dock.Children.Add(ccHdr);
			Avalonia.Controls.DockPanel.SetDock(ccHdr, Avalonia.Controls.Dock.Top);

			dock.Children.Add(svCtnts);
		}
	#endregion

	#region Event Handlers
	#endregion
}