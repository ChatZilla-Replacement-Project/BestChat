// Ignore Spelling: Ctxt

namespace BestChat.IRC.Views.Desktop.SpecializedCtrls;

public partial class BncInstanceListCtrl : GroupBox.Avalonia.Controls.GroupBox
{
	#region Constructors & Deconstructors
		public BncInstanceListCtrl()
			=> InitializeComponent();
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		#pragma warning disable IDE1006 // Naming Styles
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles",
				Justification = "Due to naming standards that are inherited")]
			public static readonly Avalonia.DirectProperty<BncInstanceListCtrl, Data.Defs.BncEditable?> CtxtBNCProperty =
				Avalonia.AvaloniaProperty.RegisterDirect<BncInstanceListCtrl, Data.Defs.BncEditable?>(
					nameof(CtxtBNC),
					ctrl
						=> ctrl.CtxtBNC,
					(ctrl, bncNewVal)
						=> ctrl.CtxtBNC = bncNewVal,
					null
				);
		#pragma warning restore IDE1006 // Naming Styles
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private Data.Defs.BncEditable? ebncCtxt = null;
	#endregion

	#region Properties
		public Data.Defs.BncEditable? CtxtBNC
		{
			get => ebncCtxt;

			set => DataContext = ebncCtxt = value;
		}
	#endregion

	#region Methods
		protected override void OnInitialized()
		{
			base.OnInitialized();

			if(ebncCtxt == null)
				throw new System.InvalidProgramException("Set the context with CtxtBNC.");
		}
	#endregion

	#region Event Handlers
		private void OnAddInstanceClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(ebncCtxt != null)
			{

			}
		}

		private void OnEditInstanceClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
		}

		private void OnDelInstanceClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
		}
	#endregion
}