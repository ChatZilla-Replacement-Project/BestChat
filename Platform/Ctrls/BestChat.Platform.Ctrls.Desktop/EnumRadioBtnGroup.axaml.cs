// Ignore Spelling: Ctrl Ctrls evt Sel

namespace BestChat.Platform.Ctrls.Desktop;

public partial class EnumRadioBtnGroup : Avalonia.Controls.ItemsControl
{
	public EnumRadioBtnGroup() => InitializeIfNeeded();
}

public class EnumRadioBtnGroup<EnumType> : EnumRadioBtnGroup
	where EnumType : struct, System.Enum
{
	#region Constructors & Deconstructors
		public EnumRadioBtnGroup()
		{
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
			public static readonly Avalonia.Interactivity.RoutedEvent evtSelValChangedEvent =
				Avalonia.Interactivity.RoutedEvent.Register<EnumRadioBtnGroup<EnumType>, Avalonia.Controls
				.SelectionChangedEventArgs>(nameof(evtSelValChanged), Avalonia.Interactivity.RoutingStrategies.Direct);
		#endregion
	#endregion

	#region Helper Types
		internal class Wrapper
		{
			public Wrapper(in EnumType valRaw)
			{
				this.valRaw = valRaw;
				string strEnumValAsStr = valRaw.ToString() ?? throw new System.InvalidProgramException("Unexpected null");
				System.Reflection.FieldInfo? fieldInfo = valRaw.GetType().GetField(strEnumValAsStr) ?? throw new System
					.InvalidProgramException($"Can't find field on instance of type {typeof(EnumType).FullName} for {strEnumValAsStr}");

				object[] attribArray = fieldInfo.GetCustomAttributes(typeof(DataAndExt.Attr.LocalizedDescAttribute), false);

				if(attribArray.Length == 0)
				{
					strName = strEnumValAsStr;
					strToolTipText = "";
				}
				else
				{
					DataAndExt.Attr.LocalizedDescAttribute attrib = (DataAndExt.Attr.LocalizedDescAttribute)attribArray[0];

					strName = attrib.Description;
					strToolTipText = attrib.ExtendedDesc;
				}
			}

			public readonly EnumType valRaw;

			public readonly string strName;

			public readonly string strToolTipText;

			public EnumType RawVal => valRaw;

			public string Name => strName;

			public string ToolTipText => strToolTipText;
		}
	#endregion

	#region Members
		private readonly string strGroupName = new System.Guid().ToString();

		private EnumType? valSel = default;
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
		protected void OnInitialized()
		{
			foreach(EnumType valCur in typeof(EnumType).GetEnumValues())
				Items.Add(new Wrapper(valCur));
		}
	#endregion

	#region Event Handlers
		private void OnChildRadioBtnClicked(object? objSender, System.EventArgs e)
		{
			if(objSender is Avalonia.Controls.RadioButton rbSender && !valSel.Equals(rbSender.Tag))
			{
				valSel = (EnumType?)rbSender.Tag;

				RaiseEvent(new(evtSelValChangedEvent, this));
			}
		}
	#endregion
}