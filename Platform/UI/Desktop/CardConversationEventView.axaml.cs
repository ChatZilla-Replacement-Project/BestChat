// Ignore Spelling: Ctrls

namespace BestChat.Platform.UI.Desktop;

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
	#endregion

	#region Properties

		public bool IsSenderLocal
		{
			get;

			set;
		}
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
		protected void OnSizeChanged(object objSender, Avalonia.Controls.SizeChangedEventArgs e)
			=> textDescOfEvent.MaxWidth = e.NewSize.Width * .8;
	#endregion
}