// Ignore Spelling: Prefs cmgr

using System.Linq;

namespace BestChat.Platform.DataAndExt.Prefs;

public abstract class AbstractMgr : Obj<AbstractMgr>
{
	#region Constructors & Deconstructors
		protected AbstractMgr()
		{
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public abstract record AbstractDTO
		(
			[System.Xml.Serialization.XmlIgnore]
			string Key
		);
	#endregion

	#region Members
		private readonly System.Collections.Generic.SortedDictionary<string, ItemBase> mapItemsByName =
			[];

		private readonly System.Collections.Generic.SortedDictionary<string, AbstractChildMgr>
			mapChildMgrByName = [];

		private bool bEditMode = false;
	#endregion

	#region Properties
		public System.Collections.Generic.IReadOnlyCollection<ItemBase> ItemsByName
			=> mapItemsByName.Values;

		public System.Collections.Generic.IReadOnlyCollection<AbstractChildMgr> ChildMgrByName
			=> mapChildMgrByName.Values;

		public bool IsEditMode
			=> bEditMode;
	#endregion

	#region Methods
		public void Add<ItemType>(in Item<ItemType> itemNew)
		{
			mapItemsByName[itemNew.LocalizedName] = itemNew;

			itemNew.evtDirtyChanged += OnChildItemDirtyChanged;
		}

		public void Add(in AbstractChildMgr cmgrNew)
		{
			mapChildMgrByName[cmgrNew.LocalizedName] = cmgrNew;

			cmgrNew.evtDirtyChanged += OnChildMgrDirtyChanged;
		}

		protected void RemoveChildMgr(AbstractChildMgr cmgrToBeRemoved)
		{
			mapChildMgrByName.Remove(cmgrToBeRemoved.Name);

			cmgrToBeRemoved.evtDirtyChanged -= OnChildMgrDirtyChanged;
		}

		public void PrepareForEdit()
		{
			if(this is AbstractChildMgr)
				throw new System.InvalidProgramException("Only root managers can start the edit");

			if(bEditMode)
				throw new ItemBase.EditingException(ItemBase.EditingException.WhenPossibilities
					.preparingForEdit);

			InternalPrepareForEdit();
		}

		private void InternalPrepareForEdit()
		{
			bEditMode = true;

			foreach(AbstractChildMgr cmgrCur in mapChildMgrByName.Values)
				cmgrCur.InternalPrepareForEdit();

			foreach(ItemBase itemCur in mapItemsByName.Values)
				itemCur.PrepareForEdit();
		}

		public void SaveEdits()
		{
			if(this is AbstractChildMgr)
				throw new System.InvalidProgramException("Only root managers can save the edit");

			if(!IsEditMode)
				throw new ItemBase.EditingException(ItemBase.EditingException.WhenPossibilities.saving);

			InternalSaveEdits();
		}

		private void InternalSaveEdits()
		{
			foreach(AbstractChildMgr cmgrCur in mapChildMgrByName.Values)
				cmgrCur.InternalSaveEdits();

			foreach(ItemBase itemCur in mapItemsByName.Values)
				itemCur.SaveEdits();

			bEditMode = false;
		}

		public void RevertEdits()
		{
			if(this is AbstractChildMgr)
				throw new System.InvalidProgramException("Only root managers can revert the edit");

			if(!IsEditMode)
				throw new ItemBase.EditingException(ItemBase.EditingException.WhenPossibilities.reverting);

			InternalRevertEdits();
		}

		private void InternalRevertEdits()
		{
			foreach(AbstractChildMgr cmgrCur in mapChildMgrByName.Values)
				cmgrCur.InternalRevertEdits();

			foreach(ItemBase itemCur in mapItemsByName.Values)
				itemCur.RevertEdits();

			bEditMode = false;
		}
	#endregion

	#region Event Handlers
		private void OnChildItemDirtyChanged(in ItemBase objSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}

		private void OnChildMgrDirtyChanged(in AbstractMgr objSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}
	#endregion
}