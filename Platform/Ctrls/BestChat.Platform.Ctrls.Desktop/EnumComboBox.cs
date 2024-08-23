// Ignore Spelling: Ctrl Ctrls evt Sel

namespace BestChat.GUI.Ctrls
{
	public class EnumComboBox<EnumType> : System.Windows.Controls.ComboBox
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
			public event System.Windows.RoutedEventHandler evtSelValChanged
			{
				add => AddHandler(evtSelValChangedEvent, value);

				remove => RemoveHandler(evtSelValChangedEvent, value);
			}
		#endregion

		#region Constants
			#region Dependency Properties
				public static readonly System.Windows.DependencyProperty SelValProperty = System.Windows
					.DependencyProperty.Register(nameof(SelVal), typeof(EnumType?), typeof(EnumComboBox<EnumType>));
			#endregion

			#region Routed Events
				public static readonly System.Windows.RoutedEvent evtSelValChangedEvent = System.Windows
					.EventManager.RegisterRoutedEvent(nameof(evtSelValChanged), System.Windows.RoutingStrategy
					.Bubble, typeof(System.Windows.RoutedEventHandler), typeof(EnumComboBox<EnumType>));
			#endregion
		#endregion

		#region Helper Types
		#endregion

		#region Members
		#endregion

		#region Properties
			[System.ComponentModel.Description("This is the value selected in the combobox in the " +
				"desired type.  Other Selected properties will show you the index or a label object.  Don't use "
				+ "SelectedValue or SelectedItem")]
			[System.ComponentModel.Category("Common")]
			public EnumType? SelVal
			{
				get => SelectedIndex == -1 || Items.Count == 0 ? default : (EnumType)((System.Windows.Controls
					.Label)Items[SelectedIndex]).Tag;

				set
				{
					if(null == value || Items.Count == 0)
						SelectedIndex = -1;
					else
					{
						int iIndexOfCurItem = 0;

						foreach(object objCurItem in Items)
						{
							if(objCurItem is System.Windows.Controls.Label labelCurItem && labelCurItem.Tag ==
								(object?)value)
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
			private static void GetEnumDesc(object objEnumVal, out string strDesc, out string strExtendedDesc)
			{
				string strEnumValAsStr = objEnumVal.ToString() ?? throw new System.InvalidProgramException("Unexpected "
					+ "null");
				System.Reflection.FieldInfo? fieldInfo = objEnumVal.GetType().GetField(strEnumValAsStr) ?? throw new System
					.InvalidProgramException(
					$"Can't find field on instance of type {typeof(EnumType).FullName} for {strEnumValAsStr}");

				object[] attribArray = fieldInfo.GetCustomAttributes(typeof(Util.Attr.LocalizedDescAttribute), false);

				if(attribArray.Length == 0)
				{
					strDesc =strEnumValAsStr;
					strExtendedDesc = "";
				}
				else
				{
					Util.Attr.LocalizedDescAttribute attrib = (Util.Attr.LocalizedDescAttribute)attribArray[0];

					strDesc = attrib.Description;
					strExtendedDesc = attrib.ExtendedDesc;
				}
			}
		#endregion

		#region Event Handlers
			protected override void OnSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e) => base
				.OnSelectionChanged(e);
		#endregion
	}
}