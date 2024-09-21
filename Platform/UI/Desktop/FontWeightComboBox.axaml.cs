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
			=> InitializeComponent();
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
		private static readonly System.Collections.Generic.Dictionary<Avalonia.Media.FontWeight, FontWeightEnumWrapper> mapWeightsToText =
			new()
		{
			[Avalonia.Media.FontWeight.Thin] = new(Avalonia.Media.FontWeight.Thin, Rsrcs.strFontWeightThin),
			[Avalonia.Media.FontWeight.ExtraLight] = new(Avalonia.Media.FontWeight.ExtraLight, Rsrcs
				.strFontWeightExtraLight),
			[Avalonia.Media.FontWeight.Light] = new(Avalonia.Media.FontWeight.Light, Rsrcs.strFontWeightLight),
			[Avalonia.Media.FontWeight.Normal] = new(Avalonia.Media.FontWeight.Normal, Rsrcs.strFontWeightNormal),
			[Avalonia.Media.FontWeight.Medium] = new(Avalonia.Media.FontWeight.Medium, Rsrcs.strFontWeightMedium),
			[Avalonia.Media.FontWeight.DemiBold] = new(Avalonia.Media.FontWeight.DemiBold, Rsrcs.strFontWeightDemiBold),
			[Avalonia.Media.FontWeight.Bold] = new(Avalonia.Media.FontWeight.Bold, Rsrcs.strFontWeightBold),
			[Avalonia.Media.FontWeight.ExtraBold] = new(Avalonia.Media.FontWeight.ExtraBold, Rsrcs
				.strFontWeightExtraBold),
			[Avalonia.Media.FontWeight.Black] = new(Avalonia.Media.FontWeight.Black, Rsrcs.strFontWeightHeavy),
			[Avalonia.Media.FontWeight.UltraBlack] = new(Avalonia.Media.FontWeight.UltraBlack, Rsrcs
				.strFontWeightExtraBlack),
		};
	#endregion

	#region Properties
		public Avalonia.Media.FontWeight? SelVal
		{
			get => ((FontWeightEnumWrapper?)SelectedValue)?.FW;

			set => SelectedValue = value;
		}
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
		protected override void OnInitialized()
		{
			foreach(FontWeightEnumWrapper wrapperCur in mapWeightsToText.Values)
				Items.Add(wrapperCur);
		}
	#endregion
}

internal class FontWeightEnumWrapper(Avalonia.Media.FontWeight fw, string strLocalizedName)
{
	// ReSharper disable once InconsistentNaming
	public Avalonia.Media.FontWeight FW
		=> fw;

	public string LocalizedName
		=> strLocalizedName;
}