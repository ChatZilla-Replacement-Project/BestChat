// Ignore Spelling: Defs dmp

namespace BestChat.IRC.Data.Defs;

[System.ComponentModel.ImmutableObject(true)]
public class ModeParam
{
	#region Constructors & Deconstructors
		internal ModeParam(in string strName, in Types type, in LocalizedTextSystem textDisplayName, in LocalizedTextSystem textDesc, in
			LocalizedTextSystem? textPostFixLabel = null)
		{
			this.strName = strName;
			this.type = type;
			this.textDisplayName = textDisplayName;
			this.textDesc = textDesc;
			this.textPostFixLabel = textPostFixLabel;
			iMinForNum = null;
			iMaxForNum = null;
		}

		internal ModeParam(in string strName, in LocalizedTextSystem textDisplayName, in LocalizedTextSystem textDesc, in LocalizedTextSystem?
			textPostFixLabel = null, in int? iMin = null, in int? iMax = null)
		{
			this.strName = strName;
			type = Types.number;
			this.textDisplayName = textDisplayName;
			this.textDesc = textDesc;
			this.textPostFixLabel = textPostFixLabel;
			iMinForNum = iMin;
			iMaxForNum = iMax;
		}

		internal ModeParam(DTO.ModeParamDTO dmpUs)
		{
			strName = dmpUs.Name;
			type = dmpUs.Type switch
			{
				DTO.ModeParamDTO.Types.@string => Types.@string,
				DTO.ModeParamDTO.Types.number => Types.number,
				DTO.ModeParamDTO.Types.chanName => Types.chanName,
				_ => throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<DTO.ModeParamDTO.Types>(dmpUs.Type, "A mode " +
					"param was loading as part of a predefined database of networks"),
			};
			textDisplayName = new(dmpUs.LocalizedDisplayNames, dmpUs.DefaultDisplayName);
			textDesc = new(dmpUs.LocalizedDesc, dmpUs.DefaultDesc);
			textPostFixLabel = dmpUs.DefaultPostFixLabel == null ? null : new(dmpUs
				.LocalizedPostFixLabel, dmpUs.DefaultPostFixLabel);
			if(type == Types.number)
			{
				iMinForNum = dmpUs.MinForNumber;
				iMaxForNum = dmpUs.MaxForNumber;
			}
		}
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public enum Types
		{
			@string,
			number,
			chanName
		}
	#endregion

	#region Members
		public readonly string strName;

		public readonly Types type;

		public readonly LocalizedTextSystem textDisplayName;

		public readonly LocalizedTextSystem textDesc;

		public readonly LocalizedTextSystem? textPostFixLabel;

		public readonly int? iMinForNum;

		public readonly int? iMaxForNum;
	#endregion

	#region Properties
		public string Name
			=> strName;

		public Types Type
			=> type;

		public LocalizedTextSystem DisplayName
			=> textDisplayName;

		public LocalizedTextSystem Desc
			=> textDesc;

		public LocalizedTextSystem? PostFixLabel
			=> textPostFixLabel;

		public int? MinForNum
			=> iMinForNum;

		public int MinForNumSafe
			=> iMinForNum ?? int.MinValue;

		public int? MaxForNum
			=> iMaxForNum;

		public int MaxForNumSafe
			=> iMaxForNum ?? int.MaxValue;
	#endregion
}