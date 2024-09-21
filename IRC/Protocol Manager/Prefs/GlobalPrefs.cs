// Ignore Spelling: Prefs dto Mirc Tele

namespace BestChat.IRC.ProtocolMgr.Prefs;

public class GlobalPrefs : Data.Prefs.Prefs<GlobalPrefs, DTO.GlobalDTO>.GlobalPrefs
{
	#region Constructors & Deconstructors
		public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent)
			=> fmt = new(this);

		public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.GlobalDTO dto)
			: base(mgrParent, dto)
			=> fmt =new(this, dto.Fmt);
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public class FmtPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
				public FmtPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
					base(mgrParent, "Format", Rsrcs.strPrefsGlobalFmtTitle, Rsrcs.strTranslatedProtDesc)
				{
					bold = new(this, "Bold", Rsrcs.strPrefsGlobalFmtBoldTitle, Rsrcs
						.strPrefsGlobalFmtBoldDesc, true);
					italics = new(this, "Italics", Rsrcs.strPrefsGlobalFmtItalicsTitle, Rsrcs
						.strPrefsGlobalFmtItalicsDesc, true);
					underline = new(this, "Underline", Rsrcs.strPrefsGlobalFmtUnderlineTitle, Rsrcs
						.strPrefsGlobalFmtUnderlineDesc, true);
					strikeThrough = new(this, "Strike Through", Rsrcs
						.strPrefsGlobalFmtStrikeThroughTitle, Rsrcs.strPrefsGlobalFmtStrikeThroughDesc, false);
					teleType = new(this, "Teletype", Rsrcs.strPrefsGlobalFmtTeleTypeTitle,
						Rsrcs.strPrefsGlobalFmtTeleTypeDesc, true);
					mircColors = new(this, "MIRC Colors", Rsrcs.strPrefsGlobalFmtItalicsTitle, Rsrcs
						.strPrefsGlobalFmtMircColorsDesc, true);
					markerType = new(this, "Marker Type", Rsrcs
						.strPrefsGlobalFmtMarkerTypeTitle, Rsrcs.strPrefsGlobalFmtMarkerTypeDesc, MarkerTypes
						.traditional);
				}

				public FmtPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.GlobalDTO.FmtDTO dto) :
					base(mgrParent, "Format", Rsrcs.strPrefsGlobalFmtTitle, Rsrcs.strTranslatedProtDesc)
				{
					bold = new(this, "Bold", Rsrcs.strPrefsGlobalFmtBoldTitle, Rsrcs
						.strPrefsGlobalFmtBoldDesc, true, dto.Bold);
					italics = new(this, "Italics", Rsrcs.strPrefsGlobalFmtItalicsTitle, Rsrcs
						.strPrefsGlobalFmtItalicsDesc, true, dto.Italics);
					underline = new(this, "Underline", Rsrcs.strPrefsGlobalFmtUnderlineTitle, Rsrcs
						.strPrefsGlobalFmtUnderlineDesc, true, dto.Underline);
					strikeThrough = new(this, "Strike Through", Rsrcs
						.strPrefsGlobalFmtStrikeThroughTitle, Rsrcs.strPrefsGlobalFmtStrikeThroughDesc, false, dto
						.StrikeThrough);
					teleType = new(this, "Teletype", Rsrcs.strPrefsGlobalFmtTeleTypeTitle,
						Rsrcs.strPrefsGlobalFmtTeleTypeDesc, true, dto.TeleType);
					mircColors = new(this, "MIRC Colors", Rsrcs.strPrefsGlobalFmtItalicsTitle, Rsrcs
						.strPrefsGlobalFmtMircColorsDesc, true, dto.MircColors);
					markerType = new(this, "Marker Type", Rsrcs
						.strPrefsGlobalFmtMarkerTypeTitle, Rsrcs.strPrefsGlobalFmtMarkerTypeDesc, MarkerTypes
						.traditional, dto.MarkerType);
				}
			#endregion

			#region Delegates
			#endregion

			#region Events
			#endregion

			#region Constants
			#endregion

			#region Helper Types
				public enum MarkerTypes : byte
				{
					[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strFmtMarkerTypeTraditional),
						"Traditional", nameof(Rsrcs.strFmtMarkerTypeTraditonalToolTip),
						"When you use the UI to select formatting other than color, Best Chat will " +
						"insert into the text ASCII formatting marks.  So if you want \"text\" in bold and use the UI "
						+ "rather than typing, you'll get \"*text*\".", typeof(FmtPrefs))]
					traditional,

					[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strFmtMarkerTypeMIRC), "MIRC",
						nameof(Rsrcs.strFmtMarkerTypeMircToolTip), "When you use the UI to select " +
						"formatting other than color, Best Chat will insert into the text control characters you won't"
						+ " be able to see.  Some clients will see these and trigger the formatting.", typeof(FmtPrefs))]
					mirc,
				}
			#endregion

			#region Members
				private readonly Platform.DataAndExt.Prefs.Item<bool> bold;

				private readonly Platform.DataAndExt.Prefs.Item<bool> italics;

				private readonly Platform.DataAndExt.Prefs.Item<bool> underline;

				private readonly Platform.DataAndExt.Prefs.Item<bool> strikeThrough;

				private readonly Platform.DataAndExt.Prefs.Item<bool> teleType;

				private readonly Platform.DataAndExt.Prefs.Item<bool> mircColors;

				private readonly Platform.DataAndExt.Prefs.Item<MarkerTypes> markerType;
			#endregion

			#region Properties
				public Platform.DataAndExt.Prefs.Item<bool> Bold
					=> bold;

				public Platform.DataAndExt.Prefs.Item<bool> Italics
					=> italics;

				public Platform.DataAndExt.Prefs.Item<bool> Underline
					=> underline;

				public Platform.DataAndExt.Prefs.Item<bool> StrikeThrough
					=> strikeThrough;

				public Platform.DataAndExt.Prefs.Item<bool> TeleType
					=> teleType;

				public Platform.DataAndExt.Prefs.Item<bool> MircColors
					=> mircColors;

				public Platform.DataAndExt.Prefs.Item<MarkerTypes> MarkerType
					=> markerType;
			#endregion

			#region Methods
				public DTO.GlobalDTO.FmtDTO ToDTO()
					=> new(
						bold.CurVal,
						italics.CurVal,
						underline.CurVal,
						strikeThrough.CurVal,
						teleType.CurVal,
						mircColors.CurVal,
						markerType.CurVal
					);
			#endregion

			#region Event Handlers
			#endregion
		}
	#endregion

	#region Members
		private readonly FmtPrefs fmt;
	#endregion

	#region Properties
		public FmtPrefs Fmt
			=> fmt;
	#endregion

	#region Methods
		public override DTO.GlobalDTO ToDTO()
			=> new(
				AutoPerform.ToDTO(),
				DCC.ToDTO(),
				Conn.ToDTO(),
				fmt.ToDTO(),
				Aliases.ToDTO(),
				AltNicks.ToDTO(),
				StalkWords.ToDTO()
			);
	#endregion

	#region Event Handlers
	#endregion
}