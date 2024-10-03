// Ignore Spelling: Prefs Ctrl

namespace BestChat.Platform.UI.Desktop.Prefs;

/// <summary>
/// Interaction logic for PrefGroupTreeListerCtrl.xaml
/// </summary>
public partial class PrefGroupTreeListerCtrl : Avalonia.Controls.ItemsControl
{
	#region Constructors & Deconstructors
		public PrefGroupTreeListerCtrl()
		{
			InitializeIfNeeded();
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#region Dependency Properties
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "While field names "
				+ "are supposed to begin with lower case characters, if they refer to a property, the requirement is for an upper case character.")]
			public static readonly Avalonia.AvaloniaProperty<System.Collections.Generic.IReadOnlyCollection<VisualPrefsTreeData>?>
				ChildrenProperty = Avalonia.AvaloniaProperty.RegisterDirect<PrefGroupTreeListerCtrl, System.Collections.Generic
				.IReadOnlyCollection<VisualPrefsTreeData>?>
				(
					nameof(Children),
					pgtrcSender
						=> pgtrcSender.Children,
					(pgtrcSender, valNew)
						=> pgtrcSender.Children = valNew,
					[]
				);
		#endregion
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private readonly System.Collections.Generic.List<VisualPrefsTreeData> listChildren = [];
	#endregion

	#region Properties
		public System.Collections.Generic.IReadOnlyCollection<VisualPrefsTreeData>? Children
		{
			get => listChildren;

			set
			{
				listChildren.Clear();
				if(value != null && value.Count > 0)
					listChildren.AddRange(value);

				IsVisible = listChildren.Count > 0;
			}
		}
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}