// Ignore Spelling: Ctrl Ctrls evt Sel

namespace BestChat.Platform.Ctrls.Desktop;

public class EnumComboBox<EnumType> : Avalonia.Controls.ComboBox
	where EnumType : System.Enum
{
	#region Constructors & Deconstructors
		public EnumComboBox()
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
		#region Routed Events
			public static readonly Avalonia.Interactivity.RoutedEvent<Avalonia.Controls.SelectionChangedEventArgs> evtSelValChangedEvent =
				Avalonia.Interactivity.RoutedEvent.Register<EnumComboBox<EnumType>, Avalonia.Controls
				.SelectionChangedEventArgs>(nameof(evtSelValChanged), Avalonia.Interactivity.RoutingStrategies.Direct);
		#endregion
	#endregion

	#region Helper Types
	#endregion

	#region Members
	#endregion

	#region Properties
		[System.ComponentModel.Description("This is the value selected in the combobox in the desired type.  Other Selected properties will " +
			"show you the index or a label object.  Don't use SelectedValue or SelectedItem")]
		[System.ComponentModel.Category("Common")]
		public EnumType? SelVal
		{
			get => SelectedIndex == -1 || Items.Count == 0 || Items[SelectedIndex] is not Avalonia.Controls.Label label || label.Tag is not EnumType
				val
					? default
					: val;

			set
			{
				if(null == value || Items.Count == 0)
					SelectedIndex = -1;
				else
				{
					int iIndexOfCurItem = 0;

					foreach(object objCurItem in Items)
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
		private static void GetEnumDesc(in object objEnumVal, out string strDesc, out string strExtendedDesc)
		{
			string strEnumValAsStr = objEnumVal.ToString() ?? throw new System.InvalidProgramException("Unexpected "
				+ "null");
			System.Reflection.FieldInfo? fieldInfo = objEnumVal.GetType().GetField(strEnumValAsStr) ?? throw new System
				.InvalidProgramException(
				$"Can't find field on instance of type {typeof(EnumType).FullName} for {strEnumValAsStr}");

			object[] attribArray = fieldInfo.GetCustomAttributes(typeof(DataAndExt.Attr.LocalizedDescAttribute), false);

			if(attribArray.Length == 0)
			{
				strDesc = strEnumValAsStr;
				strExtendedDesc = "";
			}
			else
			{
				DataAndExt.Attr.LocalizedDescAttribute attrib = (DataAndExt.Attr.LocalizedDescAttribute)attribArray[0];

				strDesc = attrib.Description;
				strExtendedDesc = attrib.ExtendedDesc;
			}
		}
	#endregion

	#region Event Handlers
	#endregion
}