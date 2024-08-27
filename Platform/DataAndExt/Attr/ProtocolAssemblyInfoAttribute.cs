// Ignore Spelling: dt

namespace BestChat.Platform.DataAndExt.Attr
{
	[System.ComponentModel.ImmutableObject(true)]
	[System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple = false)]
	public class ProtocolAssemblyInfoAttribute : System.Attribute
	{
		public ProtocolAssemblyInfoAttribute(string strName, string strPublisher, string strTranslatedDescKey, string strDefDesc, System.Type
			typeToUseToFindResources, string strVersion, string? strLastUpdatedOn = null, string? strAuthorHomePage = null, string?
				strProjectHomePage = null)
		{
			System.Resources.ResourceManager rm = new System.Resources.ResourceManager(typeToUseToFindResources);


			this.strName = strName;
			this.strPublisher = strPublisher;
			strDesc = rm.GetString(strTranslatedDescKey) ?? strDefDesc;
			this.strVersion = strVersion;
			dtLastUpdatedOn = strLastUpdatedOn == null
				? System.DateTime.Now
				: System.DateTime.Parse(strLastUpdatedOn);
			if(strAuthorHomePage != null)
				uriAuthorHomePage = new(strAuthorHomePage);
			if(strProjectHomePage != null)
				uriProjectHomePage = new(strProjectHomePage);
		}

		public readonly string strName;

		public readonly string strPublisher;

		public readonly string strDesc;

		public readonly string strVersion;

		public readonly System.DateTime? dtLastUpdatedOn;

		public readonly System.Uri? uriAuthorHomePage;

		public readonly System.Uri? uriProjectHomePage;
	}
}