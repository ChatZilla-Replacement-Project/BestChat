// Ignore Spelling: Sel Evt revt fcb Ctrls

using System.Linq;

namespace BestChat.Platform.UI.Desktop;

/// <summary>
/// Interaction logic for FontWeightComboBox.xaml
/// </summary>
public partial class FontWeightComboBox : Avalonia.Controls.ComboBox
{
	#region Constructors & Deconstructors
		public FontWeightComboBox()
		{
			Initialized += OnInitialized;

			InitializeIfNeeded();
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		#region Dependency Properties
			public static readonly Avalonia.AvaloniaProperty<Avalonia.Media.FontWeight?> SelValProperty = Avalonia.AvaloniaProperty
				.RegisterDirect<FontWeightComboBox, Avalonia.Media.FontWeight?>(nameof(SelVal), cbSender => (Avalonia.Media
				.FontWeight?)cbSender.SelectedValue, (cbSender, fwNew) => cbSender.SelectedValue = fwNew, Avalonia.Media
				.FontWeight.Normal);
		#endregion

		#region Routed Events
		#endregion
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private static readonly System.Collections.Generic.Dictionary<Avalonia.Media.FontWeight, string> mapWeightsToText =
			new()
		{
			[Avalonia.Media.FontWeight.Thin] = Rsrcs.strFontWeightThin,
			[Avalonia.Media.FontWeight.ExtraLight] = Rsrcs.strFontWeightExtraLight,
			[Avalonia.Media.FontWeight.UltraLight] = Rsrcs.strFontWeightExtraLight,
			[Avalonia.Media.FontWeight.Light] = Rsrcs.strFontWeightLight,
			[Avalonia.Media.FontWeight.Normal] = Rsrcs.strFontWeightNormal,
			[Avalonia.Media.FontWeight.Regular] = Rsrcs.strFontWeightNormal,
			[Avalonia.Media.FontWeight.Medium] = Rsrcs.strFontWeightMedium,
			[Avalonia.Media.FontWeight.DemiBold] = Rsrcs.strFontWeightDemiBold,
			[Avalonia.Media.FontWeight.SemiBold] = Rsrcs.strFontWeightDemiBold,
			[Avalonia.Media.FontWeight.Bold] = Rsrcs.strFontWeightBold,
			[Avalonia.Media.FontWeight.ExtraBold] = Rsrcs.strFontWeightExtraBold,
			[Avalonia.Media.FontWeight.UltraBold] = Rsrcs.strFontWeightExtraBold,
			[Avalonia.Media.FontWeight.Black] = Rsrcs.strFontWeightHeavy,
			[Avalonia.Media.FontWeight.Heavy] = Rsrcs.strFontWeightHeavy,
			[Avalonia.Media.FontWeight.UltraBlack] = Rsrcs.strFontWeightExtraBlack,
			[Avalonia.Media.FontWeight.ExtraBlack] = Rsrcs.strFontWeightExtraBlack,
		};
	#endregion

	#region Properties
		public Avalonia.Media.FontWeight? SelVal
		{
			get => (Avalonia.Media.FontWeight?)SelectedValue;

			set => SelectedValue = value;
		}
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
		protected void OnInitialized(object? objSender, System.EventArgs e)
		{
			foreach(System.Collections.Generic.KeyValuePair<Avalonia.Media.FontWeight, string> kvCurWeightInfo in mapWeightsToText)
				Items.Add(kvCurWeightInfo);
		}
	#endregion
}