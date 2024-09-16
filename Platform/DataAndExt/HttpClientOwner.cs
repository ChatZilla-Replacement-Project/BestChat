namespace BestChat.Platform.DataAndExt
{
	public abstract class HttpClientOwner
	{
		private static HttpClientOwner? instance;

		public static HttpClientOwner? Instance
			=> instance;

		protected HttpClientOwner()
			=> instance = this;

		protected virtual System.Net.Http.HttpClient? ClientToUse
			=> null;
	}
}