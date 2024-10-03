namespace BestChat.Platform.DataAndExt.Prefs;

public class GlobalAppearanceTimeStampPrefs : AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAppearanceTimeStampPrefs(in AbstractMgr mgrParent) :
			base(mgrParent, "Time Stamp", Rsrcs.strGlobalAppearanceTimeStampTitle, Rsrcs
				.strGlobalAppearanceTimeStampDesc)
		{
			show = new(this, "Show the time stamp", Rsrcs
					.strGlobalAppearanceTimeStampShowTitle, Rsrcs.strGlobalAppearanceTimeStampShowDesc,
				true);
			fmt = new(this, "Format", Rsrcs.strGlobalAppearanceTimeStampFmtTitle,
				Rsrcs.strGlobalAppearanceTimeStampFmtDesc, "G");
			howOftenToRepeat = new(this, "How Often to Repeat", Rsrcs
				.strGlobalAppearanceTimeStampHowOftenToRepeatTitle, Rsrcs
				.strGlobalAppearanceTimeStampHowOftenToRepeatDesc, HowOftenToRepeatOpts.everyThirtySeconds);
		}

		public GlobalAppearanceTimeStampPrefs(in AbstractMgr mgrParent, in DTO.PrefsDTO.GlobalDTO.AppearanceDTO
			.TimeStampDTO dto) :
			base(mgrParent, "Time Stamp", Rsrcs.strGlobalAppearanceTimeStampTitle, Rsrcs
				.strGlobalAppearanceTimeStampDesc)
		{
			show = new(this, "Show the time stamp", Rsrcs
					.strGlobalAppearanceTimeStampShowTitle, Rsrcs.strGlobalAppearanceTimeStampShowDesc,
				true, dto.Show);
			fmt = new(this, "Format", Rsrcs.strGlobalAppearanceTimeStampFmtTitle,
				Rsrcs.strGlobalAppearanceTimeStampFmtDesc, "G", dto.Fmt);
			howOftenToRepeat = new(this, "How Often to Repeat", Rsrcs
				.strGlobalAppearanceTimeStampHowOftenToRepeatTitle, Rsrcs
				.strGlobalAppearanceTimeStampHowOftenToRepeatDesc, HowOftenToRepeatOpts.everyThirtySeconds, dto
				.HowOftenToRepeat);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public enum HowOftenToRepeatOpts : byte
		{
			[Attr.LocalizedDesc(nameof(Rsrcs.strTimeStampRepeatEveryEvt), "Every Other Event",
				nameof(Rsrcs.strTimeStampRepeatEveryEvtToolTip), "Best Chat will display time " +
				"stamps only for every other event.", typeof(HowOftenToRepeatOpts))]
			everyEvt,

			[Attr.LocalizedDesc(nameof(Rsrcs.strTimeStampRepeatEveryThirtySecs), "Every 30 " +
				"seconds", nameof(Rsrcs.strTimeStampRepeatEveryThirtySecsToolTip), "Best Chat " +
				"will wait 30 seconds to display a new time stamp.", typeof(HowOftenToRepeatOpts))]
			everyThirtySeconds,

			[Attr.LocalizedDesc(nameof(Rsrcs.strTimeStampRepeatEveryOtherEvt), "Every Other Event",
				nameof(Rsrcs.strTimeStampRepeatEveryOtherEvtToolTip), "Best Chat will display " +
				"time stamps only for every other event.", typeof(HowOftenToRepeatOpts))]
			everyOtherEvt,

			[Attr.LocalizedDesc(nameof(Rsrcs.strTimeStampRepeatEveryMinute), "Every Minute",
				nameof(Rsrcs.strTimeStampRepeatEveryMinuteToolTip), "Best Chat will wait 1 " +
				"minute to display a new time stamp.", typeof(HowOftenToRepeatOpts))]
			everyMinute,

			[Attr.LocalizedDesc(nameof(Rsrcs.strTimeStampRepeatEveryFiveEvts), "Every 5 Events",
				nameof(Rsrcs.strTimeStampRepeatEveryFiveEvtsToolTip), "Best Chat will display " +
				"time stamps only every five (5) events.", typeof(HowOftenToRepeatOpts))]
			everyFiveEvts,

			[Attr.LocalizedDesc(nameof(Rsrcs.strTimeStampRepeatEveryTenMinutes), "Every 10 " +
				"Minutes", nameof(Rsrcs.strTimeStampRepeatEveryTenMinutesToolTip), "Best Chat " +
				"will display time stamps only every ten (10) minutes.", typeof(HowOftenToRepeatOpts))]
			everyTenMinutes,

			[Attr.LocalizedDesc(nameof(Rsrcs.strTimeStampRepeatEveryTwentyEvts), "Every 20 " +
				"Events", nameof(Rsrcs.strTimeStampRepeatEveryTwentyEvtsToolTip), "Best Chat " +
				"will display time stamps only every twenty (20) events", typeof(HowOftenToRepeatOpts))]
			everyTwentyEvts,
		}
	#endregion

	#region Members
		private readonly Item<bool> show;

		private readonly Item<string> fmt;

		private readonly Item<HowOftenToRepeatOpts> howOftenToRepeat;
	#endregion

	#region Properties
		public Item<bool> Show
			=> show;

		public Item<string> Fmt
			=> fmt;

		public Item<HowOftenToRepeatOpts> HowOftenToRepeat
			=> howOftenToRepeat;
	#endregion

	#region Methods
		public virtual DTO.PrefsDTO.GlobalDTO.AppearanceDTO.TimeStampDTO ToDTO()
			=> new(
				show.CurVal,
				Fmt.CurVal,
				HowOftenToRepeat: howOftenToRepeat.CurVal
			);
	#endregion

	#region Event Handlers
	#endregion
}