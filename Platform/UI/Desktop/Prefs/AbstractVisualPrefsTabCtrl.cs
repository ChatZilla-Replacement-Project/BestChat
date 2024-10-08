// Ignore Spelling: Prefs Ctrls Ctrl Mgrs Mgr

using System.Linq;

namespace BestChat.Platform.UI.Desktop.Prefs;

public abstract class AbstractVisualPrefsTabCtrl : Platform.UI.Desktop.AbstractVisualCtrl
{
	#region Constructors & Deconstructors
		protected AbstractVisualPrefsTabCtrl()
		{
			if(Avalonia.Application.Current is not null)
				throw new System.InvalidProgramException("The default constructors of BestBestChat.Platform.UI.Desktop.Prefs and its derived " +
					"classes are for designer use only.  They aren't not meant for use at runtime.");
		}

		protected AbstractVisualPrefsTabCtrl(in string strLocalizedShortName, in string strLocalizedLongDesc, in DataAndExt.Prefs
				.AbstractMgr mgrUs) :
			base(strLocalizedShortName, strLocalizedLongDesc)
			=> this.mgrUs = mgrUs;
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
		public readonly DataAndExt.Prefs.AbstractMgr? mgrUs;
	#endregion

	#region Properties
		public DataAndExt.Prefs.AbstractMgr Mgr
			=> mgrUs ?? throw new System.InvalidProgramException("BestChat.Platform.Prefs.Ctrls.VisualPrefsTabCtrl.Mgr accessed at " +
				"runtime.");

		public virtual System.Collections.Generic.IEnumerable<System.Type> HandlesChildMgrsOfType
			=> [];

		public System.Collections.Generic.IEnumerable<DataAndExt.Prefs.AbstractChildMgr> ChildMgrWithDedicatedEditor
			=> mgrUs is null
				? []
				: from DataAndExt.Prefs.AbstractChildMgr cmgrCur in mgrUs.ChildMgrByName
					where HandlesChildMgrsOfType.Contains(cmgrCur.GetType())
					select cmgrCur;
	#endregion

	#region Methods
		protected override void OnInitialized()
			=> DataContext = mgrUs;
	#endregion

	#region Event Handlers
	#endregion
}