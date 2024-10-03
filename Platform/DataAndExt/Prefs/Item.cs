// Ignore Spelling: Prefs evt

using System.Linq;

namespace BestChat.Platform.DataAndExt.Prefs;

public abstract class ItemBase : Obj<ItemBase>
{
	#region Constructors & Deconstructors
		public ItemBase(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
				string strLocalizedLongDesc, System.Guid guid = default) :
			base(guid)
		{
			this.mgrParent = mgrParent;
			this.strItemName = strItemName;
			this.strLocalizedName = strLocalizedName;
			this.strLocalizedLongDesc = strLocalizedLongDesc;
		}
	#endregion

	#region Members
		public readonly AbstractMgr mgrParent;

		public readonly string strItemName;

		public readonly string strLocalizedName;

		public readonly string strLocalizedLongDesc;
	#endregion

	#region Helper Types
		public class EditingException(EditingException.WhenPossibilities when) :
			System.Exception
		{
			public enum WhenPossibilities : byte
			{
				preparingForEdit,
				saving,
				reverting,
				notReadyToEdit
			}

			public override string Message
				=> when switch
				{
					WhenPossibilities.preparingForEdit
						=> "It looks like this item is already editing",
					WhenPossibilities.saving
						=> "This item can't save as no edit is in progress",
					WhenPossibilities.reverting
						=> "This item can't reverse as no edit is in progress",
					WhenPossibilities.notReadyToEdit
						=> "Before you can set the value of this item, you must enable editing",
					_
						=> throw new System.NotImplementedException(),
				};
		}
	#endregion

	#region Properties
		public AbstractMgr Parent
			=> mgrParent;

		public string ItemName
			=> strItemName;

		public string LocalizedName
			=> strLocalizedName;

		public string LocalizedLongDesc
			=> strLocalizedLongDesc;

		public abstract string ValAsText
		{
			get;
		}

		public abstract bool IsDefaulted
		{
			get;
		}

		public bool CanReset
			=> !IsDefaulted;

		public abstract bool IsReadyToEdit
		{
			get;
		}
	#endregion

	#region Methods
		internal abstract void PrepareForEdit();

		internal abstract void SaveEdits();

		internal abstract void RevertEdits();
	#endregion
}

public class Item<TypeOfItem> : ItemBase, System.ComponentModel.INotifyPropertyChanged
{
	#region Constructors & Deconstructors
		public Item(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
				string strLocalizedLongDesc, in TypeOfItem def, in TypeOfItem? valCur, System.Guid guid = default) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, guid)
		{
			if(typeof(TypeOfItem) == typeof(int) && this is Item<int>)
				throw new System.InvalidProgramException("Instead of directly using Item<int>, use IntItem." +
					"  It provides and enforces minimum and maximum values.");

			this.def = def;
			this.valCur = valCur ?? def;

			mgrParent.Add(this);
		}

		public Item(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
				string strLocalizedLongDesc, in TypeOfItem def, System.Guid guid = default) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, guid)
		{
			if(typeof(TypeOfItem) == typeof(int) && this is Item<int>)
				throw new System.InvalidProgramException("Instead of directly using Item<int>, use IntItem." +
					"  It provides and enforces minimum and maximum values.");

			valCur = this.def = def;

			mgrParent.Add(this);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event DFieldChanged<TypeOfItem>? evtCurValChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly TypeOfItem def;

		private TypeOfItem valCur;

		private TypeOfItem? valBackedUpDuringEdit = default;
	#endregion

	#region Properties
		public TypeOfItem Def
			=> def;

		public virtual TypeOfItem CurVal
		{
			get => valCur;

			set
			{
				if(!IsReadyToEdit)
					throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

				if(valCur != null && !valCur.Equals(value) || value != null)
				{
					TypeOfItem oldVal = valCur;

					valCur = value;

					FirePropChanged(nameof(CurVal));

					evtCurValChanged?.Invoke(this, oldVal, value);
				}
			}
		}

		public override string ValAsText
			=> valCur?.ToString() ?? "";

		public override bool IsDefaulted
			=> valCur == null && def == null || (valCur?.Equals(def) ?? false);

		public override bool IsReadyToEdit
			=> valBackedUpDuringEdit != null;

		internal override void PrepareForEdit()
		{
			if(IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.preparingForEdit);

			valBackedUpDuringEdit = valCur;
		}

		internal override void SaveEdits()
		{
			if(valBackedUpDuringEdit == null)
				throw new EditingException(EditingException.WhenPossibilities.saving);

			valBackedUpDuringEdit = default;

			MakeDirty();
		}

		internal override void RevertEdits()
		{
			if(valBackedUpDuringEdit is null)
				throw new EditingException(EditingException.WhenPossibilities.reverting);

			valCur = valBackedUpDuringEdit;
			valBackedUpDuringEdit = default;
		}
	#endregion

	#region Methods
		public virtual void ResetValToDef()
		{
			CurVal = def;

			MakeDirty();
		}
	#endregion

	#region Event Handlers
	#endregion
}

public partial class IntItem : Item<int>
{
	public IntItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
			string strLocalizedLongDesc, int iDef, System.Guid guid = default) :
		base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, iDef, guid)
	{
		iMinVal = null;
		iMaxVal = null;
	}

	public IntItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
			string strLocalizedLongDesc, int iDef, in int? iMinVal = null, in int? iMaxVal = null, System.Guid guid =
			default) :
		base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, iDef, guid)
	{
		this.iMinVal = iMinVal;
		this.iMaxVal = iMaxVal;
	}

	public IntItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
			string strLocalizedLongDesc, int iDef, in int iCurVal, System.Guid guid = default) :
		base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, iDef, iCurVal, guid)
	{
		iMinVal = null;
		iMaxVal = null;
	}

	public IntItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
			string strLocalizedLongDesc, int iDef, in int iCurVal, in int? iMinVal = null, in int? iMaxVal =
			null, System.Guid guid = default) :
		base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, iDef, iCurVal, guid)
	{
		this.iMinVal = iMinVal;
		this.iMaxVal = iMaxVal;
	}

	public readonly int? iMinVal;

	public readonly int? iMaxVal;

	public int? MinVal
		=> iMinVal;

	public int? MaxVal
		=> iMaxVal;

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant " +
		"fields should not be visible", Justification = "Only checked when CurVal is set.")]
	public static bool bThrowExceptionOnValSetOutSideRange = false;

	public class ValOutsideRangeException : System.Exception
	{
		internal ValOutsideRangeException(in int iValFound, in int? iMinVal, in int? iMaxVal, in string
			strMsg, System.Exception? exceptInner = null) :
			base(strMsg, exceptInner)
		{
			this.iValFound = iValFound;
			this.iMinVal = iMinVal;
			this.iMaxVal = iMaxVal;
		}

		public readonly int iValFound;

		public readonly int? iMinVal;

		public readonly int? iMaxVal;
	}

	public override int CurVal
	{
		set
		{
			if(iMinVal != null && value < iMinVal || iMaxVal != null && value > iMaxVal)
			{
				if(bThrowExceptionOnValSetOutSideRange)
					throw new ValOutsideRangeException(value, iMinVal, iMinVal, "Value was outside range");

				return;
			}

			base.CurVal = value;
		}
	}
}