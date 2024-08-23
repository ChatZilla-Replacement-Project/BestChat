// Ignore Spelling: Util

namespace BestChat.Platform.DataAndExt.Exceptions
{
	public class UnknownOrInvalidEnumException<EnumType> : System.Exception
	{
		#region Constructors & Destructors
			public UnknownOrInvalidEnumException(in EnumType objFoundThis, in string strActionDesc)
			{
				System.Diagnostics.Debug.Assert(strActionDesc != null && strActionDesc.Length > 0);

				this.objFoundThis = objFoundThis;
				this.strActionDesc = strActionDesc;
			}
		#endregion

		// Members
		#region Members
			public readonly EnumType objFoundThis;
			public readonly string strActionDesc;
		#endregion

		// Properties
		#region Properties
			public override string Message
				=> $"Unknown or invalid {typeof(EnumType)} enum value while {strActionDesc}: {objFoundThis}";
		#endregion
	}
}