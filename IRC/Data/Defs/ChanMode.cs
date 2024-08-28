// Ignore Spelling: dcm Defs

namespace BestChat.IRC.Data.Defs;

[System.ComponentModel.ImmutableObject(true)]
public class ChanMode : IMode
{
	#region Constructors & Deconstructors
		internal ChanMode(in char chModeChar, in LocalizedTextSystem textDesc, in bool bNotAlwaysAvailable = false, in System.Collections
			.Generic.IEnumerable<ModeParam>? @params = null, in string strFmtAsSentToNetwork = "", in bool bIsOperRequiredToChange = false,
			StdModeTypes? smt = null)
		{
			this.chModeChar = chModeChar;
			this.textDesc = textDesc;
			this.bNotAlwaysAvailable = bNotAlwaysAvailable;
			if(@params != null)
				foreach(ModeParam mpCur in @params)
					mapParamsByName[mpCur.Name] = mpCur;
			this.strFmtAsSentToNetwork = strFmtAsSentToNetwork;
			this.bIsOperRequiredToChange = bIsOperRequiredToChange;
			this.smt = smt;
		}

		internal ChanMode(DTO.ChanModeDTO dcmUs)
		{
			chModeChar = dcmUs.Mode;
			textDesc = new(dcmUs.LocalizedDesc, dcmUs.DefaultDesc);
			bNotAlwaysAvailable = dcmUs.NotAlwaysAvailable;
			if(dcmUs.Parameters != null)
				foreach(DTO.ModeParamDTO dmpCurParam in dcmUs.Parameters)
					mapParamsByName[dmpCurParam.Name] = new(dmpCurParam);
			strFmtAsSentToNetwork = dcmUs.FmtAsSentToNetwork;
			bIsOperRequiredToChange = dcmUs.IsOperRequiredToChange;
			smt = dcmUs.StdModeType switch
			{
				DTO.ChanModeDTO.StdModeTypes.TopicLock => StdModeTypes.TopicLock,
				DTO.ChanModeDTO.StdModeTypes.Moderated => StdModeTypes.Moderated,
				DTO.ChanModeDTO.StdModeTypes.ColorStrip => StdModeTypes.ColorStrip,
				DTO.ChanModeDTO.StdModeTypes.NoOutsideMsg => StdModeTypes.NoOutsideMsg,
				DTO.ChanModeDTO.StdModeTypes.InviteOnly => StdModeTypes.InviteOnly,
				DTO.ChanModeDTO.StdModeTypes.Keyword => StdModeTypes.Keyword,
				DTO.ChanModeDTO.StdModeTypes.Private => StdModeTypes.Private,
				DTO.ChanModeDTO.StdModeTypes.Secret => StdModeTypes.Secret,
				null => null,
				_ => throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<DTO.ChanModeDTO.StdModeTypes?>(dcmUs.StdModeType,
					"Loading standard mode type for channel mode."),
			};
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public enum StdModeTypes
		{
			TopicLock,
			Moderated,
			ColorStrip,
			NoOutsideMsg,
			InviteOnly,
			Keyword,
			Private,
			Secret
		}
	#endregion

	#region Members
		public readonly char chModeChar;

		public readonly LocalizedTextSystem textDesc;

		public readonly bool bNotAlwaysAvailable;

		private readonly System.Collections.Generic.SortedDictionary<string, ModeParam> mapParamsByName =
			new();

		public readonly string strFmtAsSentToNetwork;

		public readonly bool bIsOperRequiredToChange;

		public readonly StdModeTypes? smt;
	#endregion

	#region Properties
		public char ModeChar
			=> chModeChar;

		public LocalizedTextSystem Desc
			=> textDesc;

		public bool NotAlwaysAvailable
			=> bNotAlwaysAvailable;

		public System.Collections.Generic.IReadOnlyDictionary<string, ModeParam>? ParamsByName
			=> mapParamsByName;

		public string FmtAsSentToNetwork
			=> strFmtAsSentToNetwork;

		public bool IsOperRequiredToChange
			=> bIsOperRequiredToChange;

		public StdModeTypes? StdModeType
			=> smt;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}