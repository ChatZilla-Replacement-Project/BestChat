using System.Linq;

namespace BestChat.Platform.DataAndExt.Prefs;

public class MappedClonedAndGroupListItem<PrimaryKeyType, SecondaryKeyType, EntryType> : ItemBase, System.Collections
	.Generic.IDictionary<PrimaryKeyType, System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType>>,
	System.Collections.Generic.IReadOnlyDictionary<PrimaryKeyType, System.Collections.Generic
	.IReadOnlyDictionary<SecondaryKeyType, EntryType>>
	where PrimaryKeyType : notnull
	where SecondaryKeyType : notnull
{
	#region Constructors & Deconstructors
		public MappedClonedAndGroupListItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
				/* ReSharper disable once InconsistentNaming*/ string strLocalizedLongDesc, in System.Func<EntryType,
				PrimaryKeyType> funcPrimaryKeyObtainer, System.Action<EntryType, DCollectionFieldChanged<System.Collections
				.Generic.IReadOnlyCollection<PrimaryKeyType>, PrimaryKeyType>> actionPrimaryKeyChangedSubscriber,
				MappedListItem<SecondaryKeyType, EntryType> mliSrc)
			: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			this.mliSrc = mliSrc;
			mliSrc.evtEntriesChanged += OnSrcEntriesChanged;

			this.funcPrimaryKeyObtainer = funcPrimaryKeyObtainer;
			this.actionPrimaryKeyChangedSubscriber = actionPrimaryKeyChangedSubscriber;

			mapEntries = [];
			foreach(SecondaryKeyType skCur in mliSrc.Keys)
				AddEntry(skCur, mliSrc[skCur]);
		}
	#endregion

	#region Events
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification =
			"Name required to implement interface")]
		public event System.Collections.Specialized.NotifyCollectionChangedEventHandler? CollectionChanged;

		public event DMapFieldChanged<System.Collections.Generic.IReadOnlyDictionary<PrimaryKeyType, System.Collections
			.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType>>, PrimaryKeyType, System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType, EntryType>>? evtEntriesChanged;
	#endregion

	#region Helper Types
		public class UseSrcMapException(string strDescOfWhatHappened) : System.Exception(strDescOfWhatHappened);
	#endregion

	#region Members
		private readonly System.Collections.Generic.Dictionary<PrimaryKeyType, System.Collections.Generic
			.SortedDictionary<SecondaryKeyType, EntryType>> mapEntries;

		private MappedListItem<SecondaryKeyType, EntryType> mliSrc;

		private readonly System.Func<EntryType, PrimaryKeyType> funcPrimaryKeyObtainer;

		private readonly System.Action<EntryType, DCollectionFieldChanged<System.Collections.Generic
			.IReadOnlyCollection<PrimaryKeyType>, PrimaryKeyType>> actionPrimaryKeyChangedSubscriber;
	#endregion

	#region Properties
		public override string ValAsText
			=> throw new System.NotImplementedException();

		public override bool IsDefaulted
			=> throw new System.NotImplementedException();

		public override bool IsReadyToEdit
			=> throw new System.NotImplementedException();

		public int Count
			=> mapEntries.Count;

		public bool IsReadOnly
			=> mliSrc.IsReadOnly;
	#endregion

	#region Methods
		private void AddEntry(SecondaryKeyType sk, EntryType entry)
		{
			PrimaryKeyType pk = funcPrimaryKeyObtainer(mliSrc[sk]);

			System.Collections.Generic.SortedDictionary<SecondaryKeyType, EntryType>? map = (mapEntries.TryGetValue(pk, out
				System.Collections.Generic.SortedDictionary<SecondaryKeyType, EntryType>? value) ? value : null) ??
				(mapEntries[pk] = new());

			map[sk] = mliSrc[sk];
		}

		internal override void PrepareForEdit()
			=> throw new System.NotImplementedException();

		internal override void SaveEdits()
			=> throw new System.NotImplementedException();

		internal override void RevertEdits()
			=> throw new System.NotImplementedException();

		System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<PrimaryKeyType, System.Collections
			.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType>>> System.Collections.Generic.IEnumerable<System
			.Collections.Generic.KeyValuePair<PrimaryKeyType, System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType,
			EntryType>>>.GetEnumerator()
				=> throw new System.NotImplementedException();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> throw new System.NotImplementedException();

		public void Add(System.Collections.Generic.KeyValuePair<PrimaryKeyType, System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType, EntryType>> item)
				=> throw new System.NotImplementedException();

		public bool Remove(System.Collections.Generic.KeyValuePair<PrimaryKeyType, System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType, EntryType>> item)
				=> throw new System.NotImplementedException();

		public void Clear()
			=> throw new System.NotImplementedException();

		public bool Contains(System.Collections.Generic.KeyValuePair<PrimaryKeyType, System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType, EntryType>> item)
				=> throw new System.NotImplementedException();

		public void CopyTo(System.Collections.Generic.KeyValuePair<PrimaryKeyType, System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType, EntryType>>[] array, int arrayIndex)
				=> throw new System.NotImplementedException();

		public bool Contains(System.Collections.Generic.KeyValuePair<PrimaryKeyType, System.Collections.Generic
			.IDictionary<SecondaryKeyType, EntryType>> item)
				=> throw new System.NotImplementedException();

		public void CopyTo(System.Collections.Generic.KeyValuePair<PrimaryKeyType, System.Collections.Generic
			.IDictionary<SecondaryKeyType, EntryType>>[] array, int arrayIndex)
				=> throw new System.NotImplementedException();

		public bool Remove(System.Collections.Generic.KeyValuePair<PrimaryKeyType, System.Collections.Generic
			.IDictionary<SecondaryKeyType, EntryType>> item)
				=> throw new System.NotImplementedException();

		public void Add(PrimaryKeyType key, System.Collections.Generic.IDictionary<SecondaryKeyType, EntryType> value)
			=> throw new System.NotImplementedException();

		public void Add(PrimaryKeyType key, System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType> value)
			=> throw new System.NotImplementedException();

		bool System.Collections.Generic.IDictionary<PrimaryKeyType, System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType,EntryType>>.ContainsKey(PrimaryKeyType key)
				=> throw new System.NotImplementedException();

		public bool TryGetValue(PrimaryKeyType key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out
			System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType> value)
				=> throw new System.NotImplementedException();

		System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType> System.Collections.Generic
			.IReadOnlyDictionary<PrimaryKeyType, System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType>>
			.this[PrimaryKeyType key]
				=> throw new System.NotImplementedException();

		System.Collections.Generic.IEnumerable<PrimaryKeyType> System.Collections.Generic
			.IReadOnlyDictionary<PrimaryKeyType, System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType>>
			.Keys
				=> throw new System.NotImplementedException();

		System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType>>
			System.Collections.Generic.IReadOnlyDictionary<PrimaryKeyType, System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType, EntryType>>.Values
				=> throw new System.NotImplementedException();

		public bool Remove(PrimaryKeyType key)
			=> throw new System.NotImplementedException();

		public bool TryGetValue(PrimaryKeyType key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out
			System.Collections.Generic.IDictionary<SecondaryKeyType, EntryType> value)
				=> throw new System.NotImplementedException();

		bool System.Collections.Generic.IReadOnlyDictionary<PrimaryKeyType, System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType, EntryType>>.ContainsKey(PrimaryKeyType key)
				=> throw new System.NotImplementedException();

		public System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType> this[PrimaryKeyType key]
		{
			get => throw new System.NotImplementedException();
			set => throw new System.NotImplementedException();
		}

		System.Collections.Generic.ICollection<PrimaryKeyType> System.Collections.Generic
			.IDictionary<PrimaryKeyType, System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType>>.Keys
				=> throw new System.NotImplementedException();

		System.Collections.Generic.ICollection<System.Collections.Generic.IReadOnlyDictionary<SecondaryKeyType, EntryType>>
			System.Collections.Generic.IDictionary<PrimaryKeyType, System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType, EntryType>>.Values
				=> throw new System.NotImplementedException();
	#endregion

	#region Event Handlers
		private void OnSrcEntriesChanged(in ItemBase objSender, in System.Collections.Generic
			.IReadOnlyDictionary<SecondaryKeyType, EntryType> mapThatChanged, in System.Collections.Generic
			.IEnumerable<SecondaryKeyType>? eNewKeys = null, in System.Collections.Generic.IEnumerable<System
			.Tuple<SecondaryKeyType, EntryType>>? eRemovedKeys = null, in System.Collections.Generic.IEnumerable<System
			.Tuple<SecondaryKeyType, SecondaryKeyType, EntryType>>? eChangedKeys = null)
		{
			if(eNewKeys != null)
				foreach(SecondaryKeyType skCur in eNewKeys)
					AddEntry(skCur, mliSrc[skCur]);

			if(eRemovedKeys != null)
				foreach(System.Tuple<SecondaryKeyType, EntryType> tupleCur in eRemovedKeys)
					mapEntries[funcPrimaryKeyObtainer(tupleCur.Item2)].Remove(tupleCur.Item1);
		}
	#endregion
}