// Ignore Spelling: Sel Evt revt fcb

using System.Linq;

namespace BestChat.GUI.Ctrls
{
	/// <summary>
	/// Interaction logic for FontWeightComboBox.xaml
	/// </summary>
	public partial class FontWeightComboBox : System.Windows.Controls.ComboBox
	{
		#region Constructors & Deconstructors
			public FontWeightComboBox()
			{
				InitializeComponent();
			}
		#endregion

		#region Delegates
		#endregion

		#region Events
			public event System.Windows.RoutedEventHandler evtSelChanged
			{
				add => AddHandler(evtSelChangedEvent, value);

				remove => RemoveHandler(evtSelChangedEvent, value);
			}
		#endregion

		#region Constants
			#region Dependency Properties
				public new static readonly System.Windows.DependencyProperty SelectedValueProperty = System.Windows
					.DependencyProperty.Register(nameof(SelectedEvent), typeof(System.Windows.FontWeight),
					typeof(FontWeightComboBox), new(System.Windows.FontWeights.Normal, OnSelectedValuePropChanged));
			#endregion

			#region Routed Events
				public static readonly System.Windows.RoutedEvent evtSelChangedEvent = System.Windows.EventManager
					.RegisterRoutedEvent(nameof(evtSelChanged), System.Windows.RoutingStrategy.Bubble, typeof(System.Windows.FontWeight),
					typeof(FontWeightComboBox));
			#endregion
		#endregion

		#region Helper Types
		#endregion

		#region Members
			private static readonly System.Collections.Generic.Dictionary<System.Windows.FontWeight, string> mapWeightsToText = new
			()
			{
				[System.Windows.FontWeights.Thin] = Ctrls.Resources.strFontWeightThin,
				[System.Windows.FontWeights.ExtraLight] = Ctrls.Resources.strFontWeightExtraLight,
				[System.Windows.FontWeights.UltraLight] = Ctrls.Resources.strFontWeightExtraLight,
				[System.Windows.FontWeights.Light] = Ctrls.Resources.strFontWeightLight,
				[System.Windows.FontWeights.Normal] = Ctrls.Resources.strFontWeightNormal,
				[System.Windows.FontWeights.Regular] = Ctrls.Resources.strFontWeightNormal,
				[System.Windows.FontWeights.Medium] = Ctrls.Resources.strFontWeightMedium,
				[System.Windows.FontWeights.DemiBold] = Ctrls.Resources.strFontWeightDemiBold,
				[System.Windows.FontWeights.SemiBold] = Ctrls.Resources.strFontWeightDemiBold,
				[System.Windows.FontWeights.Bold] = Ctrls.Resources.strFontWeightBold,
				[System.Windows.FontWeights.ExtraBold] = Ctrls.Resources.strFontWeightExtraBold,
				[System.Windows.FontWeights.UltraBold] = Ctrls.Resources.strFontWeightExtraBold,
				[System.Windows.FontWeights.Black] = Ctrls.Resources.strFontWeightHeavy,
				[System.Windows.FontWeights.Heavy] = Ctrls.Resources.strFontWeightHeavy,
				[System.Windows.FontWeights.UltraBlack] = Ctrls.Resources.strFontWeightExtraBlack,
				[System.Windows.FontWeights.ExtraBlack] = Ctrls.Resources.strFontWeightExtraBlack,
			};

			private bool bSelChanging = false;
		#endregion

		#region Properties
			public new System.Windows.FontWeight SelectedValue
			{
				get => (System.Windows.FontWeight)GetValue(SelectedValueProperty);

				set => SetValue(SelectedValueProperty, value);
			}
		#endregion

		#region Methods
			protected override void OnInitialized(System.EventArgs e)
			{
				base.OnInitialized(e);

				foreach(System.Collections.Generic.KeyValuePair<System.Windows.FontWeight, string> kvCurWeightInfo in mapWeightsToText)
					Items.Add(kvCurWeightInfo);
			}

			protected override void OnSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
			{
				base.OnSelectionChanged(e);

				if(!bSelChanging)
				{
					bSelChanging = true;

					SetValue(SelectedValueProperty, ((System.Collections.Generic.KeyValuePair<System.Windows.FontWeight, string>)base.SelectedValue)
						.Key);

					bSelChanging = false;

					RaiseEvent(new(evtSelChangedEvent, this));
				}
			}
		#endregion

		#region Event Handlers
			private static void OnSelectedValuePropChanged(System.Windows.DependencyObject doChanged, System.Windows
				.DependencyPropertyChangedEventArgs e)
			{
				FontWeightComboBox fcbChanged = (FontWeightComboBox)doChanged;

				if(!fcbChanged.bSelChanging)
				{
					fcbChanged.bSelChanging = true;

					((System.Windows.Controls.ComboBox)fcbChanged).SelectedValue = mapWeightsToText.First((System.Collections.Generic
						.KeyValuePair<System.Windows.FontWeight, string> kvCurWeight) => kvCurWeight.Key == (System.Windows.FontWeight)e.NewValue);

					fcbChanged.bSelChanging = false;

					fcbChanged.RaiseEvent(new(evtSelChangedEvent, fcbChanged));
				}
			}
		#endregion
	}
}