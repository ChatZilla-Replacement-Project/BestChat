// Ignore Spelling: Ctrls

using BestChat.GUI.Ctrls;

namespace BestChat.Platform.Ctrls.Desktop;

/// <summary>
/// Interaction logic for CardConversationEventView.xaml
/// </summary>
public partial class CardConversationEventView : Avalonia.Controls.UserControl
{
	#region Constructors & Deconstructors
		public CardConversationEventView() => InitializeIfNeeded();
	#endregion

	#region Constants
		#region Dependency Properties
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "WPF dependency " +
				"property names act more like names for actual properties even though they're just static members.")]
			public static readonly Avalonia.AvaloniaProperty IsSenderLocalProperty = Avalonia.AvaloniaProperty
				.RegisterDirect<CardConversationEventView, bool>(nameof(IsSenderLocal), ccevSender => ccevSender.IsSenderLocal,
				(ccevSender, bNewVal) => ccevSender.IsSenderLocal = bNewVal, false, Avalonia.Data.BindingMode
				.Default);
		#endregion
	#endregion

	#region Helper Types
		private class TemplateSelector : System.Windows.Controls.DataTemplateSelector
		{
			public TemplateSelector()
			{
			}

			public override System.Windows.DataTemplate SelectTemplate(object objItem, System.Windows
				.DependencyObject doContainer) => objItem is char ? Templates.instance.CharIcon : objItem
				is System.Windows.Media.ImageSource ? Templates.instance.ImgIcon : throw new System
				.InvalidProgramException("Unsupported type with no matching template");
		}
	#endregion

	#region Properties
		public bool IsSenderLocal
		{
			get;

			set;
		}
	#endregion

	#region Methods
		protected override void OnInitialized()
		{
			base.OnInitialized();

			ccIcon.ContentTemplateSelector = new TemplateSelector();
		}
	#endregion

	#region Event Handlers
		protected void OnSizeChanged(object objSender, Avalonia.Controls.SizeChangedEventArgs e) => textDescOfEvent.MaxWidth =
			e.NewSize.Width * .8;
	#endregion
}