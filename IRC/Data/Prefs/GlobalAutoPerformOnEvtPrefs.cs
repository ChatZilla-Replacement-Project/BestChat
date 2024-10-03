using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class GlobalAutoPerformOnEvtPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAutoPerformOnEvtPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string strName, in string
			strLocalizedName, in string strLocalizedDesc) :
			base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
			=> steps = new(this, "Steps", strLocalizedName, strLocalizedDesc, []);

		public GlobalAutoPerformOnEvtPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string strName, in string
			strLocalizedName, in string strLocalizedDesc, in DTO.GlobalAutoPerformOneStepDTO[]? dto) :
			base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
			=> steps = new(
				this,
				"Steps",
				strLocalizedName,
				strLocalizedDesc,
				[],
				dto?.Select(dstep
					=> new GlobalAutoPerformOneStep(dstep)
				) ?? []);
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
		private readonly Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep> steps;
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAutoPerformOneStep> Steps
			=> steps;
	#endregion

	#region Methods
		public DTO.GlobalAutoPerformOneStepDTO[]? ToDTO()
			=> steps.Select(aliasCur
				=> aliasCur.ToDTO()
			).ToArray();
	#endregion

	#region Event Handlers
	#endregion
}