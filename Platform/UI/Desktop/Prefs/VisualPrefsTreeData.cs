// Ignore Spelling: Ctrl Prefs Ctrls

using System.Linq;

namespace BestChat.Platform.UI.Desktop.Prefs;

using Platform.DataAndExt.Ext;

public sealed class VisualPrefsTreeData : Avalonia.AvaloniaObject
{
	#region Constructors & Deconstructors
		public VisualPrefsTreeData(DataAndExt.Prefs.AbstractMgr mgr)
		{
			if(!mapDataMgrToCtrlType.ContainsKey(typeof(DataAndExt.Prefs.AbstractMgr)))
				throw new System.ArgumentException(@"Unable to construct the prefs manager to control relationship as no " +
					@"control type was specified.", nameof(mgr));

			Mgr = mgr;

			funcCtrlMaker = mapDataMgrToCtrlType[mgr.GetType()];

			foreach(DataAndExt.Prefs.AbstractChildMgr cmgrCur in mgr.ChildMgrByName.Where(cmgrCur => !UI.HandlesChildMgrsOfType
					.Contains(cmgrCur.GetType())))
				ocChildren.Add(new(cmgrCur));
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
		private static readonly System.Collections.Generic.Dictionary<System.Type, System.Func<DataAndExt.Prefs
			.AbstractMgr, VisualPrefsTabCtrl>?> mapDataMgrToCtrlType = [];

		private readonly System.Collections.ObjectModel.ObservableCollection<VisualPrefsTreeData> ocChildren =
			[];

		private readonly System.Collections.Generic.Dictionary<DataAndExt.Prefs.AbstractMgr, VisualPrefsTabCtrl>
			mapExistingCreatedPages = [];

		private readonly System.Func<DataAndExt.Prefs.AbstractMgr, VisualPrefsTabCtrl>? funcCtrlMaker;
	#endregion

	#region Properties
		public DataAndExt.Prefs.AbstractMgr Mgr
		{
			get;

			private init;
		}

		public VisualPrefsTabCtrl UI
		{
			get
			{
				if(funcCtrlMaker is null)
					return new PrefsGenericTreeListerPage();

				if(mapExistingCreatedPages.TryGetValue(Mgr, out VisualPrefsTabCtrl? value))
					return value;

				return mapExistingCreatedPages[Mgr] = funcCtrlMaker(Mgr);
			}
		}

		public System.Collections.Generic.IReadOnlyList<VisualPrefsTreeData> Children => ocChildren;
	#endregion

	#region Methods
		public static void RegisterDataEditorCtrlType(in System.Type typeOfMgr, in System.Func<DataAndExt.Prefs.AbstractMgr,
			VisualPrefsTabCtrl> funcCtrlMaker)
		{
			if(!typeOfMgr.IsDerivedFrom(typeof(DataAndExt.Prefs.AbstractMgr)))
				throw new System.ArgumentException("When calling BestChat.Platform.TreeData.VisualTreeData.RegisterDataEditorCtrlType, the" +
					$" type specified in {typeOfMgr} must be a BestChat preference manager, either child or main.", nameof(typeOfMgr));

			if(mapDataMgrToCtrlType.ContainsKey(typeOfMgr))
				throw new System.ArgumentException("Chat.Platform.TreeData.VisualTreeData.RegisterDataEditorCtrlType was already called " +
					"with a manager type that was already in the system.", nameof(typeOfMgr));

			mapDataMgrToCtrlType[typeOfMgr] = funcCtrlMaker ?? throw new System.ArgumentNullException(nameof(funcCtrlMaker), "The function passed"
				+ " to BestChat.Platform.TreeData.VisualTreeData.RegisterDataEditorType in was null");
		}
	#endregion

	#region Event Handlers
	#endregion
}