using System;

namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public class Enum<EnumType> : Abstract
	where EnumType : System.Enum
{
	private Enum(in string strName, in string strLocalizedName, in string strLocalizedDesc) :
		base(strName, strLocalizedName, strLocalizedDesc, typeof(EnumType))
		=> Instance = this;

	public static Enum<EnumType>? Instance
	{
		get;

		private set;
	}

	public static void Make(in string strName, in string strLocalizedName, in string strLocalizedDesc)
		=> Instance ??= new(strName, strLocalizedName, strLocalizedDesc);
}