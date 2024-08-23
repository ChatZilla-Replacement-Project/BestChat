// Ignore Spelling: Ctrls

using System.Linq;

namespace BestChat.GUI.Ctrls
{
	using Util.Ext.WPF;

	/// <summary>
	/// Interaction logic for FontSelectionComboBox.xaml
	/// </summary>
	public partial class FontSelectionComboBox : System.Windows.Controls.ComboBox
	{
		#region Constructors & Deconstructors
			public FontSelectionComboBox()
			{
				InitializeComponent();
			}
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
			#region Dependency Properties
				[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "The standard for "
					+ "dependency properties requires the identifier match the associated property")]
				public static readonly System.Windows.DependencyProperty ListOnlyFixedWidthFontsProperty = System.Windows.DependencyProperty
					.Register(nameof(ListOnlyFixedWidthFonts), typeof(bool), typeof(FontSelectionComboBox), new System.Windows
					.UIPropertyMetadata(false, OnListOnlyFixedWidthFontsPropChanged, null, true));
			#endregion

			#region Routed Events
			#endregion
		#endregion

		#region Helper Types
		#endregion

		#region Members
		#endregion

		#region Properties
			[System.ComponentModel.Category("Common")]
			[System.ComponentModel.Description("Controls whether or not variable width fonts are allowed.")]
			public bool ListOnlyFixedWidthFonts
			{
				get => (bool)GetValue(ListOnlyFixedWidthFontsProperty);

				set => SetValue(ListOnlyFixedWidthFontsProperty, value);
			}

			private static System.Collections.Generic.IEnumerable<System.Windows.Media.FontFamily>
				AllFixedWidthFonts => System.Windows.Media.Fonts.SystemFontFamilies.Where((System.Windows.Media
				.FontFamily fontCur) => fontCur.FontIsFixedWidth());
		#endregion

		#region Methods
			protected override void OnInitialized(System.EventArgs e)
			{
				base.OnInitialized(e);

				Fill();

				IsEditable = true;
			}

			private void Fill()
			{
				System.Windows.Media.FontFamily ffSel = (System.Windows.Media.FontFamily)SelectedValue;

				Items.Clear();

				foreach(System.Windows.Media.FontFamily ffCur in (ListOnlyFixedWidthFonts ? AllFixedWidthFonts :
					System.Windows.Media.Fonts.SystemFontFamilies).OrderBy(x => x.GetPlainName()))
				{
					Items.Add(ffCur);

					if(ffCur == ffSel)
						SelectedValue = ffSel;
				}

				if(SelectedValue == null)
					SelectedIndex = 0;
			}

			protected override void OnSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
			{
				base.OnSelectionChanged(e);

				if(SelectedValue == null)
					SelectedIndex = 0;
			}
		#endregion

		#region Event Handlers
			private static void OnListOnlyFixedWidthFontsPropChanged(System.Windows.DependencyObject doChanged, System.Windows
				.DependencyPropertyChangedEventArgs e) => ((FontSelectionComboBox)doChanged).Fill();
		#endregion
	}
}