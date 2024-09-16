// Ignore Spelling: Prefs Conf

namespace BestChat.Platform.UI.Desktop.Prefs.DTO;

internal record RootDTO
(
	RootDTO.GlobalDTO Global
) : DataAndExt.Prefs.DTO.PrefsDTO
{
	public new record GlobalDTO : DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO
	{
		public GlobalDTO(in AppearanceDTO appearance, in PluginsDTO plugins) :
			base(plugins)
			=> Appearance = appearance;

		public new record AppearanceDTO : DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO
		{
			public AppearanceDTO
			(
				ConfModeDTO confMode,
				TimeStampDTO timeStamp,
				UserListDTO userList,
				FontDTO fonts
			) : base(confMode, timeStamp, userList)
				=> Fonts = fonts;

			public record FontDTO(FontDTO.OneFontBlockDTO AppFontData, FontDTO.OneFontBlockDTO ViewFontData)
			{
				public record OneFontBlockDTO
				(
					OneFontBlockDTO.InfoPairDTO<Avalonia.Media.FontFamily> NormalFamily,
					OneFontBlockDTO.InfoPairDTO<Avalonia.Media.FontFamily> FixedFontFamily,
					OneFontBlockDTO.InfoPairDTO<double> Size,
					OneFontBlockDTO.InfoPairDTO<Avalonia.Media.FontWeight> Weight
				) : DataAndExt.Prefs.AbstractMgr.AbstractDTO("Global/Appearance/Fonts")
				{
					public record InfoPairDTO<FieldType>
					(
						bool IsOverridden,
						FieldType? OverriddenVal
					);
				}
			}

			public FontDTO Fonts
			{
				get;

				private init;
			}
		}

		public AppearanceDTO Appearance
		{
			get;

			init;
		}

		public override DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO BaseAppearance
			=> Appearance;
	}

	public override DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO BaseGlobal => Global;
}