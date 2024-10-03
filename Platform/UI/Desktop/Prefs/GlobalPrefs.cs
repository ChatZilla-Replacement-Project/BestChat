namespace  BestChat.Platform.UI.Desktop.Prefs;

public class GlobalPrefs : DataAndExt.Prefs.GlobalPrefsBase<GlobalPrefs, GlobalAppearancePrefs>
{
	#region Constructors & Deconstructors
		internal GlobalPrefs(RootPrefs mgrParent) :
			base(mgrParent)
		{
			appearance = new(this);
			composition = new(this);
		}

		internal GlobalPrefs(RootPrefs mgrParent, DTO.RootDTO.GlobalDTO dto) :
			base(mgrParent, dto)
		{
			appearance = new(this, dto.Appearance);
			composition = new(this, dto.Composition);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private readonly GlobalAppearancePrefs appearance;

		private readonly GlobalCompositionPrefs composition;
	#endregion

	#region Properties
		public override GlobalAppearancePrefs Appearance
			=> appearance;

		public GlobalCompositionPrefs Composition
			=> composition;
	#endregion

	#region Methods
		internal DTO.RootDTO.GlobalDTO ToDTO()
			=> new(
				appearance.ToDTO(),
				Plugins.ToDTO(),
				composition.ToDTO()
			);
	#endregion

	#region Event Handlers
	#endregion
}