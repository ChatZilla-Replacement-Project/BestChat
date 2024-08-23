// Ignore Spelling: Cnt

namespace BestChat.Platform.DataAndExt.Collections;

/// <summary>
/// Hash table that indexes in two dimensions.
/// </summary>
/// <typeparam name="Key1">The first key.  This key is looked up first.</typeparam>
/// <typeparam name="Key2">The second key.</typeparam>
/// <typeparam name="ElementType">The type of the element the table holds.</typeparam>
public class HashTable<Key1, Key2, ElementType> : System.Collections.Generic.IReadOnlyDictionary<Key1, System
  .Collections.Generic.IReadOnlyDictionary<Key2, ElementType?>?>
  where Key1 : notnull
  where Key2 : notnull
  where ElementType : class
{
  #region Constructors & Destructors
		/// <summary>
		/// Constructs a blank table using the default table size.
		/// </summary>
		public HashTable()
		{
			items = new System.Collections.Generic.SortedList<Key1, System.Collections.Generic.SortedList<Key2,
				ElementType?>?>();

			iInnerTableSize = -1;
		}

		/// <summary>
		/// Constructs a table using the specified table sizes.
		/// </summary>
		/// <param name="iOuterTableSize">The size of the outer table</param>
		/// <param name="iInnerTableSize">The size of each inner table</param>
		public HashTable(in int iOuterTableSize, in int iInnerTableSize)
		{
			items = new System.Collections.Generic.SortedList<Key1, System.Collections.Generic.SortedList<Key2,
				ElementType?>?>(iOuterTableSize);

			this.iInnerTableSize = iInnerTableSize;
		}
  #endregion

  #region Members
		/// <summary>
		/// This is the list of items.
		/// </summary>
		private readonly System.Collections.Generic.SortedList<Key1, System.Collections.Generic.SortedList<Key2,
			ElementType?>?> items;

		/// <summary>
		/// The size of each inner table
		/// </summary>
		public readonly int iInnerTableSize;
  #endregion

  #region Properties
		/// <summary>
		/// Adds or removes items from the inner list.
		/// </summary>
		/// <param name="keyOuter">The outer key.  It's looked up first.</param>
		/// <param name="keyInner">The inner key.</param>
		/// <returns>The requested value</returns>
		/// <remarks>
		/// When setting this value, this property will add or delete entries from the outer list in <see cref="items"/>
		/// as needed.  If either key doesn't exist when you call the get accessor, it returns <c>null</c>.
		/// </remarks>
		public ElementType? this[in Key1 keyOuter, in Key2 keyInner]
		{
			get
			{
				if (!items.ContainsKey(keyOuter))
					return null;

				System.Collections.Generic.SortedList<Key2, ElementType?>? inner = items[keyOuter];
				return inner == null ? null : inner[keyInner];
			}

			set
			{
				if (value == null)
				{
					System.Collections.Generic.SortedList<Key2, ElementType?>? listToConsiderDeleting = items[keyOuter];

					if (listToConsiderDeleting != null && listToConsiderDeleting.Count == 1 && listToConsiderDeleting
							.ContainsKey(keyInner))
						items[keyOuter] = null;
				}
				else
				{
					System.Collections.Generic.SortedList<Key2, ElementType?>? listForOuterKey = items[keyOuter];
					if (listForOuterKey == null)
						items[keyOuter] = listForOuterKey = new System.Collections.Generic.SortedList<Key2,
							ElementType?>(iInnerTableSize);

					listForOuterKey[keyInner] = value;
				}
			}
		}

		/// <summary>
		/// Returns the total number of elements in all the sub-tables in <see cref="items"/>.
		/// </summary>
		public int TotalCnt
		{
			get
			{
				int iCnt = 0;

				foreach (System.Collections.Generic.SortedList<Key2, ElementType?>? curList in items.Values)
					if (curList != null)
						iCnt += curList.Count;

				return iCnt;
			}
		}
  #endregion

  #region Methods
		/// <summary>
		/// Tests to see if the set contains both the <typeparamref name="Key1"/> key and the <typeparamref name="Key2"/> key.
		/// </summary>
		/// <param name="keyOuter">The outer key to test for.  This will be tested first.</param>
		/// <param name="keyInner">The inner key.  Ignored if <paramref name="keyOuter"/> can't be found.</param>
		/// <returns><c>true</c> if the key combination is found and <c>false</c> otherwise</returns>
		/// <remarks>
		/// Don't confuse this method with <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/>.
		/// </remarks>
		public bool ContainsKeyCombo(in Key1 keyOuter, in Key2 keyInner)
		{
			if (items.TryGetValue(keyOuter, out System.Collections.Generic.SortedList<Key2, ElementType?>? value))
			{
				System.Collections.Generic.SortedList<Key2, ElementType?>? inner = value;

				return inner != null && inner.ContainsKey(keyInner);
			}

			return false;
		}
  #endregion

  #region Explicit Interface Implementations
		#region IReadOnlyDictionary
			System.Collections.Generic.IReadOnlyDictionary<Key2, ElementType?>? System.Collections.Generic.IReadOnlyDictionary<Key1, System
					.Collections.Generic.IReadOnlyDictionary<Key2, ElementType?>?>.this[Key1 key]
				=> items[key];

			int System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<Key1, System.Collections.Generic
					.IReadOnlyDictionary<Key2, ElementType?>?>>.Count
				=> items.Count;

			System.Collections.Generic.IEnumerable<Key1> System.Collections.Generic.IReadOnlyDictionary<Key1, System.Collections.Generic
					.IReadOnlyDictionary<Key2, ElementType?>?>.Keys
				=> items.Keys;

			System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlyDictionary<Key2, ElementType?>?> System.Collections.Generic
					.IReadOnlyDictionary<Key1, System.Collections.Generic.IReadOnlyDictionary<Key2, ElementType?>?>.Values
				=> items.Values;

			bool System.Collections.Generic.IReadOnlyDictionary<Key1, System.Collections.Generic.IReadOnlyDictionary<Key2, ElementType?>?>
					.ContainsKey(Key1 key)
				=> items.ContainsKey(key);

			bool System.Collections.Generic.IReadOnlyDictionary<Key1, System.Collections.Generic.IReadOnlyDictionary<Key2,
				ElementType?>?>.TryGetValue(Key1 key, out System.Collections.Generic.IReadOnlyDictionary<Key2, ElementType?>?
				val)
			{

				bool bRetValue = items.TryGetValue(key, out System.Collections.Generic.SortedList<Key2, ElementType?>?
					outVal);

				val = outVal;

				return bRetValue;
			}
		#endregion

		#region IEnumerable
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
				=> items.GetEnumerator();

			System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Key1, System.Collections.Generic
					.IReadOnlyDictionary<Key2, ElementType?>?>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<Key1,
					System.Collections.Generic.IReadOnlyDictionary<Key2, ElementType?>?>>.GetEnumerator()
				=> (System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Key1, System.Collections.Generic
					.IReadOnlyDictionary<Key2, ElementType?>?>>)items.GetEnumerator();
		#endregion
  #endregion
}