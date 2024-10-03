namespace BestChat.Platform.UI.Desktop.Prefs;

public class GlobalAppearanceAnimationPrefs : DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAppearanceAnimationPrefs(GlobalAppearancePrefs mgrParent) :
			base(mgrParent, "Animation", Rsrcs.strGlobalAppearanceAnimationTitle, Rsrcs
				.strGlobalAppearanceAnimationDesc)
		{
			gifs = new(this, "GIF", Rsrcs
				.strGlobalAppearanceAnimationGifTitle, Rsrcs.strGlobalAppearanceAnimationGifDesc,
				GifAnimationOpts.once);
			avatars = new(this, "Avatars", Rsrcs
				.strGlobalAppearanceAnimationAvatarsTitle, Rsrcs
				.strGlobalAppearanceAnimationAvatarsDesc, GifAnimationOpts.neverPlay);
			resumeOnMouseOver = new(this, "Resume on Mouse Over", Rsrcs
				.strGlobalAppearanceAnimationResumeOnMouseOverTitle, Rsrcs
				.strGlobalAppearanceAnimationResumeOnMouseOverDesc, true);
		}

		internal GlobalAppearanceAnimationPrefs(GlobalAppearancePrefs mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO
				.AnimationDTO dto) :
			base(mgrParent, "Animation", Rsrcs.strGlobalAppearanceAnimationTitle, Rsrcs
				.strGlobalAppearanceAnimationDesc)
		{
			gifs = new(this, "GIF", Rsrcs
				.strGlobalAppearanceAnimationGifTitle, Rsrcs.strGlobalAppearanceAnimationGifDesc,
				GifAnimationOpts.once, GifAnimationOpts.once);
			avatars = new(this, "Avatars", Rsrcs
				.strGlobalAppearanceAnimationAvatarsTitle, Rsrcs
				.strGlobalAppearanceAnimationAvatarsDesc, GifAnimationOpts.neverPlay, dto.Avatars);
			resumeOnMouseOver = new(this, "Resume on Mouse Over", Rsrcs
				.strGlobalAppearanceAnimationResumeOnMouseOverTitle, Rsrcs
				.strGlobalAppearanceAnimationResumeOnMouseOverDesc, true, dto.ResumeOnMouseOver);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public enum GifAnimationOpts
		{
			[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strGifAnimationNever), "Never",
				nameof(Rsrcs.strGifAnimationNeverToolTip), "Prevents GIFs from being" +
				" animated.  This has no effect on any other animations.  However, an option below " +
				"starts the animation if you mouse over the object.", typeof(GifAnimationOpts))]
			neverPlay,

			[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strGifAnimationOnce), "Once",
				nameof(Rsrcs.strGifAnimationOnceToolTip), "Plays the GIF animation once, " +
				"but only once.", typeof(GifAnimationOpts))]
			once,

			[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strGifAnimationTwice), "Twice",
				nameof(Rsrcs.strGifAnimationTwiceToolTip), "Plays the GIF animation twice " +
				"and then stops the animation.", typeof(GifAnimationOpts))]
			twice,

			[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strGifAnimationContinuously),
				"Continuously", nameof(Rsrcs.strGifAnimationContinuouslyToolTip),
				"Plays the GIF animation endlessly.", typeof(GifAnimationOpts))]
			continuously,
		}
	#endregion

	#region Members
		private readonly DataAndExt.Prefs.Item<GifAnimationOpts> gifs;

		private readonly DataAndExt.Prefs.Item<GifAnimationOpts> avatars;

		private readonly DataAndExt.Prefs.Item<bool> resumeOnMouseOver;
	#endregion

	#region Properties
		public DataAndExt.Prefs.Item<GifAnimationOpts> GIFs
			=> gifs;

		public DataAndExt.Prefs.Item<GifAnimationOpts> Avatars
			=> avatars;

		public DataAndExt.Prefs.Item<bool> ResumeOnMouseOver
			=> resumeOnMouseOver;
	#endregion

	#region Methods
		internal DTO.RootDTO.GlobalDTO.AppearanceDTO.AnimationDTO ToDTO()
			=> new(
				gifs.CurVal,
				avatars.CurVal,
				resumeOnMouseOver.CurVal
			);
	#endregion

	#region Event Handlers
	#endregion
}