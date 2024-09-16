// Ignore Spelling: Ctrls

using System.Linq;

namespace BestChat.Platform.UI.Desktop;

/// <summary>
/// Interaction logic for FontSelectionComboBox.xaml
/// </summary>
public partial class FontSelectionComboBox : Avalonia.Controls.ComboBox
{
	#region Constructors & Deconstructors
		public FontSelectionComboBox()
		{
			SelectionChanged += OnSelChanged;

			InitializeIfNeeded();
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification
			= "Due to naming standards that are inherited")]
		public static readonly Avalonia.DirectProperty<FontSelectionComboBox, bool> ListOnlyFixedWidthFontsProperty 
			= Avalonia.AvaloniaProperty.RegisterDirect<FontSelectionComboBox, bool>(
				nameof(ListOnlyFixedWidthFonts),
				instance
					=> instance.ListOnlyFixedWidthFonts,
				(instance, bNewVal)
					=> instance.ListOnlyFixedWidthFonts = bNewVal);
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private static readonly System.Collections.Generic.List<SkiaSharp.SKTypeface> listtfAllSysFonts =
			new(SkiaSharp.SKFontManager.Default.FontFamilies.Select(strCurFontFamilyName => SkiaSharp
			.SKTypeface.FromFamilyName(strCurFontFamilyName)));
	#endregion

	#region Properties
		public bool ListOnlyFixedWidthFonts
		{
			get;

			set;
		}

		private static System.Collections.Generic.IEnumerable<SkiaSharp.SKTypeface> AllFixedWidthFonts
			=> listtfAllSysFonts.Where(tf
				=> tf.IsFixedPitch);
	#endregion

	#region Methods
		protected override void OnInitialized()
		{
			base.OnInitialized();

			Fill();
		}

		private void Fill()
		{
			SkiaSharp.SKTypeface? tfSel = (SkiaSharp.SKTypeface?)SelectedValue;

			Items.Clear();

			foreach(SkiaSharp.SKTypeface tfCur in (ListOnlyFixedWidthFonts ? AllFixedWidthFonts : listtfAllSysFonts).OrderBy(tfCur => tfCur.FamilyName))
			{
				Items.Add(tfCur);

				if(tfCur == tfSel)
					SelectedValue = tfSel;
			}

			if(SelectedValue == null)
				SelectedIndex = 0;
		}
	#endregion

	#region Event Handlers
		private void OnSelChanged(object? objSender, Avalonia.Controls.SelectionChangedEventArgs e)
		{
			if(SelectedValue == null)
				SelectedIndex = 0;
		}
#endregion
}