// Ignore Spelling: Util

namespace BestChat.Platform.DataAndExt.Attr
{
	using Ext;

	[System.AttributeUsage(System.AttributeTargets.All)]
	public class LocalizedDescAttribute : System.ComponentModel.DescriptionAttribute
	{
		public LocalizedDescAttribute(string strKey, string strDefaultVal, string strExtendedKey, string strExtendedDefaultVal, System.Type
			typeToUseToFindResources)
		{
			System.Resources.ResourceManager rm = new(typeToUseToFindResources);

			string strFoundVal = rm.GetString(strKey) ?? strDefaultVal;
			string strFoundExtendedVal = rm.GetString(strExtendedKey) ?? strExtendedDefaultVal;

			strTranslatedVal = strDefaultVal.IsEmpty() ? strDefaultVal : strFoundVal;

			strTranslatedExtendedVal =  strFoundExtendedVal.IsEmpty() ? strExtendedDefaultVal : strFoundExtendedVal;
		}

		public readonly string strTranslatedVal;
		public readonly string strTranslatedExtendedVal;

		public override string Description => strTranslatedVal;
		public string ExtendedDesc => strTranslatedExtendedVal;
	}
}