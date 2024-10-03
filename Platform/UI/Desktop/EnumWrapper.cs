namespace BestChat.Platform.UI.Desktop;

internal interface IEnumWrapper
{
	string Name
	{
		get;
	}

	string ToolTipText
	{
		get;
	}

	object RawValAsObj
	{
		get;
	}
}

internal class EnumWrapper<EnumType> : IEnumWrapper
	where EnumType : struct, System.Enum
{
	public EnumWrapper(in EnumType valRaw)
	{
		this.valRaw = valRaw;
		string strEnumValAsStr = valRaw.ToString() ?? throw new System.InvalidProgramException("Unexpected null");
		System.Reflection.FieldInfo? fieldInfo = valRaw
																							.GetType()
																							.GetField(strEnumValAsStr) ??
																						throw new System
																							.InvalidProgramException(
																								$"Can't find field on instance of type {
																									typeof(EnumType).FullName
																								} for {
																									strEnumValAsStr
																								}");

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

	public object RawValAsObj
		=> valRaw;

	public string Name => strName;

	public string ToolTipText => strToolTipText;
}