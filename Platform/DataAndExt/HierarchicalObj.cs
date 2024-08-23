using System.Linq;

namespace BestChat.Platform.DataAndExt;

/// <summary>
/// Abstract class that describes some tools for hierarchical data objects.
/// </summary>
/// <typeparam name="TypeOfObj">A type derived from <see cref="HierarchicalObj{TypeOfObj, ChildType}"/></typeparam>
/// <typeparam name="ChildType">The type of the child entries.  Must be derived from <see cref="HierarchicalObj{TypeOfObj, ChildType}
/// .IChildObj"/></typeparam>
public abstract class HierarchicalObj<TypeOfObj, ChildType> : Obj<TypeOfObj>
	where TypeOfObj : HierarchicalObj<TypeOfObj, ChildType>
	where ChildType : HierarchicalObj<TypeOfObj, ChildType>.IChildObj
{
	/// <summary>
	/// Interface that describes the child of a <see cref="HierarchicalObj{TypeOfObj, ChildType}"/> instance.
	/// </summary>
	public interface IChildObj
	{
		/// <summary>
		/// Returns the name of this object.  Assumes the child object can be identified by a path containing it's parent's name plus this one.  Derived classes
		/// are responsible for implementing paths.
		/// </summary>
		string Name
		{
			get;
		}

		/// <summary>
		/// Returns if the Child is dirty or not.  Normally, this will be implemented by <see cref="Obj{TypeOfObj}.IsDirty"/>.
		/// </summary>
		bool IsDirty
		{
			get;
		}
	}

	/// <summary>
	/// Constructs this part of a <see cref="HierarchicalObj{TypeOfObj, ChildType}"/> instance.
	/// </summary>
	/// <param name="parent">The parent object or <see langword="null"/> if there is no parent.</param>
	protected HierarchicalObj(in TypeOfObj? parent = null)
	{
		this.parent = parent;

		mapAllInstances[guid] = this;
	}

	/// <summary>
	/// Removes this instance's <see cref="System.Guid"/> from the list of all instances.
	/// </summary>
	~HierarchicalObj()
		=> mapAllInstances.Remove(guid);

	/// <summary>
	/// Provides readonly access to the parent.  This will be <see langword="null"/> if there is no parent.
	/// </summary>
	public readonly TypeOfObj? parent;

	/// <summary>
	/// Stores a all children indexed by their <see cref="IChildObj.Name"/> attribute.
	/// </summary>
	private readonly System.Collections.Generic.Dictionary<string, ChildType> mapChildrenByName = [];

	/// <summary>
	/// The name of this <see cref="HierarchicalObj{TypeOfObj, ChildType}"/> instance.  Override to have the name be something other than  blank string.
	/// </summary>
	public virtual string Name
		=> "";

	/// <summary>
	/// Returns a readonly dictionary of all children indexed by their <see cref="IChildObj.Name"/> attribute.
	/// </summary>
	public System.Collections.Generic.IReadOnlyDictionary<string, ChildType> ChildrenByName
		=> mapChildrenByName;

	/// <summary>
	/// Indexes all instances of <see cref="HierarchicalObj{TypeOfObj, ChildType}"/> by their <see cref="System.Guid"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	///		Note: Each expansion of <see cref="HierarchicalObj{TypeOfObj, ChildType}"/> has its own copy of this list.  Change one of the type parameters and
	///		.NET sees it as a new type.
	/// </para>
	/// </remarks>
	private static readonly System.Collections.Generic.SortedDictionary<System.Guid, HierarchicalObj<TypeOfObj, ChildType>> mapAllInstances =
		[];

	/// <summary>
	/// Returns a readonly dictionary of all <see cref="HierarchicalObj{TypeOfObj, ChildType}"/> indexed by <see cref="System.Guid"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	///		Note: Each expansion of <see cref="HierarchicalObj{TypeOfObj, ChildType}"/> has its own copy of this list.  Change one of the type parameters and
	///		.NET sees it as a new type.
	/// </para>
	/// </remarks>
	public static new System.Collections.Generic.IReadOnlyDictionary<System.Guid, HierarchicalObj<TypeOfObj, ChildType>> AllInstancesByGUID
		=> mapAllInstances;

	/// <summary>
	/// Returns <see langword="true"/> if the <see cref="Obj{TypeOfObj}.IsDirty"/> returns <see langword="true"/> or any child is dirty.
	/// </summary>
	public override bool IsDirty => base.IsDirty || mapChildrenByName.Values.Any(curChild
		=> curChild.IsDirty);

	/// <summary>
	/// Called by derived classes when they have a new child.  This child doesn't have to be exclusive to this parent.
	/// </summary>
	/// <param name="childNew">Which child was added.</param>
	protected void NewChild(in ChildType childNew)
		=> mapChildrenByName[childNew.Name] = childNew;

	/// <summary>
	/// Called by derived classes when a child was removed.
	/// </summary>
	/// <param name="childRemoved"></param>
	protected void ChildRemoved(in ChildType childRemoved)
		=> mapChildrenByName.Remove(childRemoved.Name);
}