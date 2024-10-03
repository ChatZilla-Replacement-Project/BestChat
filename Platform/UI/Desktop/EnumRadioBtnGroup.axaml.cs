// Ignore Spelling: Ctrl Ctrls evt Sel

namespace BestChat.Platform.UI.Desktop;

public partial class EnumRadioBtnGroup : Avalonia.Controls.ItemsControl
{
	public EnumRadioBtnGroup()
		=> InitializeIfNeeded();

	// ReSharper disable once InconsistentNaming
	public readonly string strGroupName = new System.Guid().ToString();

	public string GroupName
		=> strGroupName;
}

public class EnumRadioBtnGroup<EnumType> : EnumRadioBtnGroup
	where EnumType : struct, System.Enum
{
	#region Constructors & Deconstructors
		public EnumRadioBtnGroup()
		{
			ContainerPrepared += OnChildContainerReady;
			Initialized += OnInitialized;
		}
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
		#region Dependency Properties
		#endregion

		#region Routed Events
		// ReSharper disable once InconsistentNaming
		public static readonly Avalonia.Interactivity.RoutedEvent evtSelValChangedEvent =
				Avalonia.Interactivity.RoutedEvent.Register<EnumRadioBtnGroup<EnumType>, Avalonia.Controls
				.SelectionChangedEventArgs>(nameof(evtSelValChanged), Avalonia.Interactivity.RoutingStrategies
				.Direct);
		#endregion
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private EnumType? valSel;
	#endregion

	#region Properties
		[System.ComponentModel.Description("This is the value selected in the combobox in the desired type.  Other Selected properties will " +
			"show you the index or a label object.  Don't use SelectedValue or SelectedItem")]
		[System.ComponentModel.Category("Common")]
		public EnumType? SelVal
		{
			get => valSel;

			set
			{
				if(!valSel.Equals(value))
				{
					valSel = value;

					RaiseEvent(new(evtSelValChangedEvent, this));
				}
			}
		}
	#endregion

	#region Methods
		private void OnInitialized(object? objSender, System.EventArgs e)
		{
			foreach(EnumType valCur in typeof(EnumType).GetEnumValues())
				Items.Add(new EnumWrapper<EnumType>(valCur));
		}
	#endregion

	#region Event Handlers
		private void OnChildRadioBtnChecked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(objSender is Avalonia.Controls.RadioButton rbSender && !valSel.Equals(rbSender.Tag))
			{
				valSel = (EnumType?)rbSender.Tag;

				RaiseEvent(new(evtSelValChangedEvent, this));
			}
		}

		private void OnChildContainerReady(object? objSender, Avalonia.Controls.ContainerPreparedEventArgs e)
		{
			if(e.Container is Avalonia.Controls.RadioButton rbNew)
			{
				rbNew.IsCheckedChanged += OnChildRadioBtnChecked;

				rbNew.IsChecked = rbNew.Tag is EnumWrapper<EnumType> wrapper && wrapper.RawVal.Equals(valSel);
			}
		}
	#endregion
}