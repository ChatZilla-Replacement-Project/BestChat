// Ignore Spelling: Prefs evt

namespace BestChat.Platform.DataAndExt.Prefs
{
	public abstract class ItemBase : Obj<ItemBase>
	{
		#region Constructors & Deconstructors
			public ItemBase(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string strLocalizedLongDesc)
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
			public interface IEditable
			{
				public abstract void Save();
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

			public abstract System.Func<ItemBase, IEditable> EditableMaker
			{
				get;
			}
		#endregion
	}

	public class Item<TypeOfItem> : ItemBase, System.ComponentModel.INotifyPropertyChanged
	{
		#region Constructors & Deconstructors
			public Item(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string strLocalizedLongDesc, in
				TypeOfItem def, in TypeOfItem? valCur) : base(mgrParent, strItemName,
				strLocalizedName, strLocalizedLongDesc)
			{
				if(typeof(TypeOfItem) == typeof(int) && this is Item<int>)
					throw new System.InvalidProgramException("Instead of directly using Item<int>, use IntItem.  It provides and enforces " +
						"minimum and maximum values.");

				this.def = def;
				this.valCur = valCur ?? def;

				mgrParent.Add(this);
			}

			public Item(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string strLocalizedLongDesc, in
				TypeOfItem def) : base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
			{
				if(typeof(TypeOfItem) == typeof(int) && this is Item<int>)
					throw new System.InvalidProgramException("Instead of directly using Item<int>, use IntItem.  It provides and enforces " +
						"minimum and maximum values.");

				valCur = this.def = def;

				mgrParent.Add(this);
			}
		#endregion

		#region Delegates
		#endregion

		#region Events
			public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

			public event DFieldChanged<TypeOfItem>? evtCurValChanged;
		#endregion

		#region Constants
		#endregion

		#region Helper Types
			public class Editable<OriginalType> : IEditable, System.ComponentModel.INotifyPropertyChanged
				where OriginalType : Item<TypeOfItem>
			{
				#region Constructors & Deconstructors
					private Editable(OriginalType original)
					{
						this.original = original;
						valCur = original.valCur;
					}
				#endregion

				#region Delegates
				#endregion

				#region Events
					public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

					public event System.Action<Editable<OriginalType>, TypeOfItem, TypeOfItem>? evtCurValChanged;
				#endregion

				#region Constants
				#endregion

				#region Helper Types
				#endregion

				#region Members
					public readonly Item<TypeOfItem> original;

					private TypeOfItem valCur;
				#endregion

				#region Properties
					public TypeOfItem CurVal
					{
						get => valCur;

						set
						{
							if(valCur == null && value != null || valCur != null && value == null || valCur != null && valCur.Equals(value))
							{
								TypeOfItem valOld = valCur;

								valCur = value;

								FireCurValChanged(valOld);
							}
						}
					}
				#endregion

				#region Methods
					public static IEditable Make(ItemBase original)
						=> original is OriginalType realOrigianl
							? new Editable<OriginalType>(realOrigianl)
							: throw new System.InvalidProgramException($"{typeof(Editable<OriginalType>).FullName}.Make expected a {typeof(
								OriginalType).FullName} instance, but got an instance of {original.GetType().FullName} instead");

					private void FirePropChanged(string strPropName)
						=> PropertyChanged?.Invoke(this, new(strPropName));

					private void FireCurValChanged(TypeOfItem valOld)
					{
						FirePropChanged(nameof(CurVal));

						evtCurValChanged?.Invoke(this, valOld, valCur);
					}

					public void Reset() => original.Reset();

					public void Save()
						=> original.CurVal = CurVal;
				#endregion

				#region Event Handlers
				#endregion
			}
		#endregion

		#region Members
			public readonly TypeOfItem def;

			private TypeOfItem valCur;
		#endregion

		#region Properties
			public TypeOfItem Def => def;

			public virtual TypeOfItem CurVal
			{
				get => valCur;

				protected set
				{
					if(valCur != null && !valCur.Equals(value) || value != null)
					{
						TypeOfItem oldVal = valCur;

						valCur = value;

						PropertyChanged?.Invoke(this, new(nameof(CurVal)));

						evtCurValChanged?.Invoke(this, oldVal, value);
					}
				}
			}

			public override string ValAsText
				=> valCur?.ToString() ?? "";

			public override System.Func<ItemBase, IEditable> EditableMaker => Editable<Item<TypeOfItem>>.Make;
		#endregion

		#region Methods
			private void Reset()
				=> CurVal = def;
		#endregion

		#region Event Handlers
		#endregion
	}

	public class IntItem : Item<int>
	{
		public IntItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string strLocalizedLongDesc, int iDef) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, iDef)
		{
			iMinVal = null;
			iMaxVal = null;
		}

		public IntItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string strLocalizedLongDesc, int iDef, in
			int? iMinVal = null, in int? iMaxVal = null) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, iDef)
		{
			this.iMinVal = iMinVal;
			this.iMaxVal = iMaxVal;
		}

		public IntItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string strLocalizedLongDesc, int iDef, in
			int iCurVal) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, iDef, iCurVal)
		{
			iMinVal = null;
			iMaxVal = null;
		}

		public IntItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in string strLocalizedLongDesc, int iDef, in
			int iCurVal, in int? iMinVal = null, in int? iMaxVal = null) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, iDef, iCurVal)
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

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible",
			Justification = "Only checked when CurVal is set.")]
		public static bool bThrowExceptionOnValSetOutSideRange = false;

		public class ValOutsideRangeException : System.Exception
		{
			internal ValOutsideRangeException(in int iValFound, in int? iMinVal, in int? iMaxVal, in string strMsg, System.Exception? exceptInner
				= null) :
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
			protected set
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
}