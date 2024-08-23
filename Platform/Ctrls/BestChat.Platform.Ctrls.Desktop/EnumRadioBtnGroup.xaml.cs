// Ignore Spelling: Ctrl Ctrls evt Sel

namespace BestChat.GUI.Ctrls
{
	public partial class EnumRadioBtnGroup : System.Windows.Controls.ItemsControl
	{
		public EnumRadioBtnGroup()
		{
			InitializeComponent();
		}
	}

	public class EnumRadioBtnGroup<EnumType> : EnumRadioBtnGroup
		where EnumType : System.Enum
	{
		#region Constructors & Deconstructors
			public EnumRadioBtnGroup()
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
					.DependencyProperty.Register(nameof(SelVal), typeof(EnumType?), typeof(EnumComboBox<EnumType>), new
					(default, OnSelValPropChanged));
			#endregion

			#region Routed Events
				public static readonly System.Windows.RoutedEvent evtSelValChangedEvent = System.Windows
					.EventManager.RegisterRoutedEvent(nameof(evtSelValChanged), System.Windows.RoutingStrategy
					.Bubble, typeof(System.Windows.RoutedEventHandler), typeof(EnumComboBox<EnumType>));
			#endregion
		#endregion

		#region Helper Types
			internal class Wrapper
			{
				public Wrapper(in EnumType valRaw)
				{
					this.valRaw = valRaw;
					string strEnumValAsStr = valRaw.ToString() ?? throw new System.InvalidProgramException("Unexpected "
						+ "null");
					System.Reflection.FieldInfo? fieldInfo = valRaw.GetType().GetField(strEnumValAsStr) ?? throw new System
						.InvalidProgramException(
						$"Can't find field on instance of type {typeof(EnumType).FullName} for {strEnumValAsStr}");

					object[] attribArray = fieldInfo.GetCustomAttributes(typeof(Util.Attr.LocalizedDescAttribute), false);

					if(attribArray.Length == 0)
					{
						strName = strEnumValAsStr;
						strToolTipText = "";
					}
					else
					{
						Util.Attr.LocalizedDescAttribute attrib = (Util.Attr.LocalizedDescAttribute)attribArray[0];

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
		#endregion

		#region Properties
			[System.ComponentModel.Description("This is the value selected in the combobox in the " +
				"desired type.  Other Selected properties will show you the index or a label object.  Don't use "
				+ "SelectedValue or SelectedItem")]
			[System.ComponentModel.Category("Common")]
			public EnumType? SelVal
			{
				get => (EnumType?)GetValue(SelValProperty);

				set => SetValue(SelValProperty, value);
			}
		#endregion

		#region Methods
			protected override void OnInitialized(System.EventArgs e)
			{
				base.OnInitialized(e);

				foreach(EnumType valCur in typeof(EnumType).GetEnumValues())
					AddChild(new Wrapper(valCur));
			}
		#endregion

		#region Event Handlers
			private static void OnSelValPropChanged(System.Windows.DependencyObject doSender, System.Windows.DependencyPropertyChangedEventArgs e)
			{
				if(doSender is EnumRadioBtnGroup sender)
					sender.RaiseEvent(new System.Windows.RoutedEventArgs(evtSelValChangedEvent, sender));
			}
		#endregion
	}
}