// Ignore Spelling: dcm Defs

using System.Collections.Generic;
using System.ComponentModel;
using BestChat.IRC.Data.Defs.DTO;
using BestChat.Platform.DataAndExt.Exceptions;

namespace BestChat.IRC.Data.Defs
{
	[ImmutableObject(true)]
	public class ChanMode : IMode
	{
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
			Secret,
		}

		#endregion

		#region Constructors & Deconstructors

		internal ChanMode(
			in char chModeChar,
			in LocalizedTextSystem textDesc,
			in bool bNotAlwaysAvailable = false,
			in IEnumerable<ModeParam>? @params = null,
			in string strFmtAsSentToNetwork = "",
			in bool bIsOperRequiredToChange = false,
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

		internal ChanMode(ChanModeDTO dcmUs)
		{
			chModeChar = dcmUs.Mode;
			textDesc = new LocalizedTextSystem(dcmUs.LocalizedDesc, dcmUs.DefaultDesc);
			bNotAlwaysAvailable = dcmUs.NotAlwaysAvailable;
			if(dcmUs.Parameters != null)
				foreach(ModeParamDTO dmpCurParam in dcmUs.Parameters)
					mapParamsByName[dmpCurParam.Name] = new ModeParam(dmpCurParam);
			strFmtAsSentToNetwork = dcmUs.FmtAsSentToNetwork;
			bIsOperRequiredToChange = dcmUs.IsOperRequiredToChange;
			smt = dcmUs.StdModeType switch
						{
							ChanModeDTO.StdModeTypes.TopicLock => StdModeTypes.TopicLock,
							ChanModeDTO.StdModeTypes.Moderated => StdModeTypes.Moderated,
							ChanModeDTO.StdModeTypes.ColorStrip => StdModeTypes.ColorStrip,
							ChanModeDTO.StdModeTypes.NoOutsideMsg => StdModeTypes.NoOutsideMsg,
							ChanModeDTO.StdModeTypes.InviteOnly => StdModeTypes.InviteOnly,
							ChanModeDTO.StdModeTypes.Keyword => StdModeTypes.Keyword,
							ChanModeDTO.StdModeTypes.Private => StdModeTypes.Private,
							ChanModeDTO.StdModeTypes.Secret => StdModeTypes.Secret,
							null => null,
							_ => throw new UnknownOrInvalidEnumException<ChanModeDTO.StdModeTypes?>(
								dcmUs.StdModeType,
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

		#region Members

		public readonly char chModeChar;

		public readonly LocalizedTextSystem textDesc;

		public readonly bool bNotAlwaysAvailable;

		private readonly SortedDictionary<string, ModeParam> mapParamsByName =
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

		public IReadOnlyDictionary<string, ModeParam>? ParamsByName
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
}