// Ignore Spelling: evt Prefs

using System.Linq;

namespace BestChat.Platform.DataAndExt.Prefs;

public class MappedListItem<KeyType, EntryType> : ItemBase, System.Collections.Generic.IDictionary<KeyType,
		EntryType>, System.Collections.Generic.IReadOnlyDictionary<KeyType, EntryType>
	where KeyType : notnull
{
	#region Constructors & Deconstructors
		public MappedListItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string
				strLocalizedLongDesc, System.Collections.Generic.IEnumerable<EntryType> def, in System.Func<EntryType,
				KeyType> funcKeyObtainer)
			: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			Def = def;
			this.funcKeyObtainer = funcKeyObtainer;

			mapEntriesByMainKey = new System.Collections.Generic.Dictionary<KeyType, EntryType>();

			DefTester = TestCurValForDef;
		}

		public MappedListItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string
				strLocalizedLongDesc, System.Collections.Generic.IEnumerable<EntryType> def, in System.Collections.Generic
				.IEnumerable<EntryType> val, in System.Func<EntryType, KeyType> funcKeyObtainer)
			: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			Def = def;
			this.funcKeyObtainer = funcKeyObtainer;

			mapEntriesByMainKey = new System.Collections.Generic.Dictionary<KeyType, EntryType>();
			foreach(EntryType entryCur in val)
				Add(entryCur);

			DefTester = TestCurValForDef;
		}

		protected MappedListItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string
				strLocalizedLongDesc, in System.Collections.Generic.IEnumerable<EntryType> def, in System.Func<EntryType,
				KeyType> funcKeyObtainer, in System.Collections.Generic.IDictionary<KeyType, EntryType> mapBackEnd)
			: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			Def = def;
			this.funcKeyObtainer = funcKeyObtainer;

			mapEntriesByMainKey = mapBackEnd;

			DefTester = TestCurValForDef;
		}

		protected MappedListItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string
				strLocalizedLongDesc, System.Collections.Generic.IEnumerable<EntryType> def, in System.Collections.Generic
				.IEnumerable<EntryType> val, in System.Func<EntryType, KeyType> funcKeyObtainer, in System.Collections
				.Generic.IDictionary<KeyType, EntryType> mapBackEnd)
			: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			Def = def;
			this.funcKeyObtainer = funcKeyObtainer;

			mapEntriesByMainKey = mapBackEnd;
			foreach(EntryType entryCur in val)
				Add(entryCur);

			DefTester = TestCurValForDef;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles",
			Justification = "Name required to implement interface")]
		public event System.Collections.Specialized.NotifyCollectionChangedEventHandler? CollectionChanged;

		public event DCollectionFieldChanged<System.Collections.Generic.IEnumerable<EntryType>>? evtEntriesChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private EntryType[]? backedUpVal = null;

		private readonly System.Collections.Generic.IDictionary<KeyType, EntryType> mapEntriesByMainKey;

		private readonly System.Func<EntryType, KeyType> funcKeyObtainer;
	#endregion

	#region Properties
		public System.Collections.Generic.IEnumerable<EntryType> Def
		{
			get;

			init;
		}

		public System.Func<System.Collections.Generic.IEnumerable<EntryType>, bool> DefTester
		{
			get;

			set;
		}

		public System.Action? ResetToDefMethod
		{
			get;

			set;
		} = null;

		public override string ValAsText
			=> string.Join(',', mapEntriesByMainKey.Values);

		public int Count
			=> mapEntriesByMainKey.Count;

		public bool IsEmpty
			=> mapEntriesByMainKey.Count == 0;

		public bool IsReadOnly
			=> false;

		public override bool IsDefaulted
			=> mapEntriesByMainKey.Values.SequenceEqual(Def);

		public override bool IsReadyToEdit
			=> backedUpVal != null && backedUpVal.Length > 0;

		public System.Collections.Generic.IEnumerable<KeyType> Keys
			=> mapEntriesByMainKey.Keys;

		public System.Collections.Generic.IEnumerable<EntryType> Values
			=> mapEntriesByMainKey.Values;

		System.Collections.Generic.ICollection<KeyType> System.Collections.Generic.IDictionary<KeyType,
			EntryType>.Keys
			=> mapEntriesByMainKey.Keys;

		System.Collections.Generic.ICollection<EntryType> System.Collections.Generic.IDictionary<KeyType,
			EntryType>.Values
				=> mapEntriesByMainKey.Values;

		public EntryType this[KeyType key]
		{
			get => mapEntriesByMainKey[key];
		
			set => throw new System.NotImplementedException();
		}
	#endregion

	#region Methods
		public void Add(KeyType key, EntryType val)
			=> throw new System.NotImplementedException();

		public bool ContainsKey(KeyType key)
			=> mapEntriesByMainKey.ContainsKey(key);

		public bool Remove(KeyType key)
			=> throw new System.NotImplementedException();

		public bool TryGetValue(KeyType key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)]
				out EntryType value)
			=> throw new System.NotImplementedException();

		public void Add(System.Collections.Generic.KeyValuePair<KeyType, EntryType> item)
			=> throw new System.NotImplementedException();

		public virtual void Clear()
		{
			if(backedUpVal == null)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			foreach(EntryType entryCur in mapEntriesByMainKey.Values)
				OnEntryRemoved(entryCur);

			mapEntriesByMainKey.Clear();
		}

		public bool Contains(System.Collections.Generic.KeyValuePair<KeyType, EntryType> item)
			=> mapEntriesByMainKey.Contains(item);

		public virtual bool ContainsKey(System.Guid guidToTestFor)
			=> false;

		public void CopyTo(System.Collections.Generic.KeyValuePair<KeyType, EntryType>[] array, int
				iArrayIndex)
			=> mapEntriesByMainKey.CopyTo(array, iArrayIndex);

		public bool Remove(System.Collections.Generic.KeyValuePair<KeyType, EntryType> item)
			=> throw new System.NotImplementedException();

		public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<KeyType,
			EntryType>> GetEnumerator()
				=> mapEntriesByMainKey.GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> mapEntriesByMainKey.GetEnumerator();

		public void Add(System.Guid key, EntryType value)
			=> throw new System.NotImplementedException();

		public bool Remove(System.Guid key)
			=> throw new System.NotImplementedException();

		public bool TryGetValue(System.Guid guidKey, [System.Diagnostics.CodeAnalysis
				.MaybeNullWhen(false)] out EntryType value)
			=> throw new System.NotImplementedException();

		public void Add(System.Collections.Generic.KeyValuePair<System.Guid, EntryType> item)
			=> throw new System.NotImplementedException();

		public bool Contains(System.Collections.Generic.KeyValuePair<System.Guid, EntryType> item)
			=> throw new System.NotImplementedException();

		public bool Remove(System.Collections.Generic.KeyValuePair<System.Guid, EntryType> item)
			=> throw new System.NotImplementedException();

		System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<KeyType, EntryType>>
			System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<KeyType, EntryType>>
			.GetEnumerator()
				=> mapEntriesByMainKey.GetEnumerator();

		private bool TestCurValForDef(System.Collections.Generic.IEnumerable<EntryType> entries)
			=> entries.SequenceEqual(Def);

		public void ResetValToDef()
		{
			if(ResetToDefMethod == null)
			{
				foreach(EntryType entryCur in mapEntriesByMainKey.Values)
					OnEntryRemoved(entryCur);

				mapEntriesByMainKey.Clear();

				foreach(EntryType entryCur in Def)
					Add(entryCur);
			}
			else
				ResetToDefMethod();
		}

		public bool Add(EntryType entryNew)
		{
			if(backedUpVal == null)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			KeyType keyForEntry = funcKeyObtainer(entryNew);

			if(entryNew is ObjBase objNewEntry && ContainsKey(objNewEntry.guid) || mapEntriesByMainKey
					.ContainsKey(keyForEntry))
				return false;

			mapEntriesByMainKey[keyForEntry] = entryNew;

			OnNewEntry(entryNew);

			return true;
		}

		public bool Remove(EntryType entryRemoveThis)
		{
			if(backedUpVal == null)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			KeyType keyForEntry = funcKeyObtainer(entryRemoveThis);

			if(entryRemoveThis is ObjBase objEntryToRemove && ContainsKey(objEntryToRemove.guid) ||
					!mapEntriesByMainKey.ContainsKey(keyForEntry))
				return false;

			mapEntriesByMainKey.Remove(keyForEntry);

			OnEntryRemoved(entryRemoveThis);

			return true;
		}

		protected virtual void OnNewEntry(EntryType entryNew)
		{
		}

		protected virtual void OnEntryRemoved(EntryType entryDeleted)
		{
		}

		internal override void PrepareForEdit()
		{
			if(!mgrParent.IsEditMode)
				throw new System.InvalidProgramException("Parent isn't editable");

			if(IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.preparingForEdit);

			backedUpVal = new EntryType[mapEntriesByMainKey.Count];
			mapEntriesByMainKey.Values.CopyTo(backedUpVal, 0);
		}

		internal override void SaveEdits()
		{
			if(backedUpVal == null)
				throw new System.InvalidProgramException("We don't seem to be editing, but we were told to save");

			bool bChangesWereMade = mapEntriesByMainKey.Values.SequenceEqual(backedUpVal);
			if(!bChangesWereMade)
				return;

			backedUpVal = null;

			MakeDirty();

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Reset));
			evtEntriesChanged?.Invoke(this, mapEntriesByMainKey.Values, CollectionChangeType.other);
		}

		internal override void RevertEdits()
		{
			if(!mgrParent.IsEditMode)
				throw new System.InvalidProgramException("Parent not editing");

			if(backedUpVal == null || backedUpVal.Length == 0)
				throw new EditingException(EditingException.WhenPossibilities.reverting);

			foreach(EntryType entryCur in mapEntriesByMainKey.Values)
				OnEntryRemoved(entryCur);

			mapEntriesByMainKey.Clear();

			foreach(EntryType entryCur in backedUpVal)
				Add(entryCur);

			backedUpVal = null;
		}
	#endregion

	#region Event Handlers
	#endregion
}