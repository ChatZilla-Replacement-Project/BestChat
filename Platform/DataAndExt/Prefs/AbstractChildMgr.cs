// Ignore Spelling: Prefs

namespace BestChat.Platform.DataAndExt.Prefs;

public abstract class AbstractChildMgr : AbstractMgr
{
	#region Constructors & Deconstructors
		protected AbstractChildMgr(in AbstractMgr mgrParent, in string strName, in string strLocalizedName, in string strLocalizedLongDesc)
		{
			this.mgrParent = mgrParent;
			this.strName = strName;
			this.strLocalizedName = strLocalizedName;
			this.strLocalizedLongDesc = strLocalizedLongDesc;

			mgrParent.Add(this);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public new class Editable : AbstractMgr.Editable
		{
			private Editable(AbstractChildMgr original) :
				base(original) => this.original = original;

			public new readonly AbstractChildMgr original;

			public readonly Editable emgrParent;

			public Editable Parent
				=> emgrParent;

			public string Name
				=> original.Name;

			public string LocalizedName
				=> original.LocalizedName;

			public string LocalizedLongDesc
				=> original.LocalizedLongDesc;

			public static Editable Make(AbstractChildMgr original)
				=> new(original);
		}
	#endregion

	#region Members
		public readonly AbstractMgr mgrParent;

		public readonly string strName;

		public readonly string strLocalizedName;

		public readonly string strLocalizedLongDesc;
	#endregion

	#region Properties
		public AbstractMgr Parent
			=> mgrParent;

		public string Name
			=> strName;

		public string LocalizedName
			=> strLocalizedName;

		public string LocalizedLongDesc
			=> strLocalizedLongDesc;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}