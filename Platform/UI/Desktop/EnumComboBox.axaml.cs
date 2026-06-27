// Ignore Spelling: Ctrl Ctrls evt Sel

using System.Linq;

namespace BestChat.Platform.UI.Desktop;

public abstract class EnumComboBox<EnumType> : Avalonia.Controls.ComboBox
	where EnumType : struct, System.Enum
{
	#region Constructors & Deconstructors
		// ReSharper disable once EmptyConstructor
		protected EnumComboBox()
			=> InitializeComponent();
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event System.EventHandler<Avalonia.Controls.SelectionChangedEventArgs> evtSelValChanged
		{
			add => AddHandler(evtSelValChangedEvent, value);

			remove => RemoveHandler(evtSelValChangedEvent, value);
		}
	#endregion

	#region Constants
		#region Routed Events
			public static readonly Avalonia.Interactivity.RoutedEvent<Avalonia.Controls.SelectionChangedEventArgs> evtSelValChangedEvent = Avalonia.Interactivity.RoutedEvent.Register<EnumComboBox<EnumType>, Avalonia.Controls.SelectionChangedEventArgs>(nameof(evtSelValChanged), Avalonia.Interactivity.RoutingStrategies.Direct);
		#endregion
	#endregion

	#region Helper Types
	#endregion

	#region Members
	#endregion

	#region Properties
		[System.ComponentModel.Description("This is the value selected in the combobox in the desired type.  Other Selected properties will show you the index or a label object.  Don't use SelectedValue or SelectedItem.")]
		[System.ComponentModel.Category("Common")]
		public EnumType? SelVal
		{
			get => SelectedIndex == -1 || Items.Count == 0 || Items[SelectedIndex] is not Avalonia.Controls.Label label || label.Tag is not EnumType
				val
					? default
					: val;

			set
			{
				if(value is null || Items.Count == 0)
					SelectedIndex = -1;
				else
				{
					int iIndexOfCurItem = 0;

					foreach(object? objCurItem in Items)
					{
						if(objCurItem is Avalonia.Controls.Label labelCurItem && labelCurItem.Tag == (object?)value)
						{
							SelectedIndex = iIndexOfCurItem;

							break;
						}

						iIndexOfCurItem++;
					}
				}
			}
		}
	#endregion

	#region Methods
		protected override void OnInitialized()
		{
			base.OnInitialized();

			System.Collections.Generic.IEnumerable<EnumType> enumValues = System.Enum.GetValues(typeof(EnumType)).Cast<EnumType>();
			System.Collections.Generic.List<EnumWrapper<EnumType>> listWrappedValues = new(enumValues.Count());
			listWrappedValues.AddRange(enumValues.Select(curVal
				=> new EnumWrapper<EnumType>(curVal)));
			ItemsSource = listWrappedValues;
		}
	#endregion

	#region Event Handlers
	#endregion
}