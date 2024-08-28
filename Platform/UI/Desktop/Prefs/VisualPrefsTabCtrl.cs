// Ignore Spelling: Prefs Ctrls Ctrl Mgrs Mgr

namespace BestChat.Platform.UI.Desktop.Prefs;

public abstract class VisualPrefsTabCtrl : Platform.UI.Desktop.AbstractVisualCtrl
{
	#region Constructors & Deconstructors
		protected VisualPrefsTabCtrl()
		{
			if(Avalonia.Application.Current is not null)
				throw new System.InvalidProgramException("The default constructors of BestBestChat.Platform.UI.Desktop.Prefs and its derived " +
					"classes are for designer use only.  They aren't not meant for use at runtime.");

			Initialized += OnInitialized;
		}

		protected VisualPrefsTabCtrl(in string strLocalizedShortName, in string strLocalizedLongDesc, in DataAndExt.Prefs.AbstractMgr mgrUs) :
			base(strLocalizedShortName, strLocalizedLongDesc) => this.mgrUs = mgrUs;
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		#region Dependency Properties
		#endregion

		#region Routed Events
		#endregion
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
	#endregion

	#region Methods
		protected void OnInitialized(object? objSender, System.EventArgs e)
			=> DataContext = mgrUs;
	#endregion

	#region Event Handlers
	#endregion
}