//// Ignore Spelling: Ctrls Evts

//namespace BestChat.Platform.Ctrls.Desktop
//{
//	internal class TemplateSelectorForCardConversationEvts : Avalonia.Controls.Templates.IDataTemplate
//	{
//		public TemplateSelectorForCardConversationEvts() => Templates = [];

//		[Avalonia.Metadata.Content]
//		public System.Collections.Generic.Dictionary<string, Avalonia.Controls.Templates.IDataTemplate> Templates
//		{
//			get;

//			set;
//		}

//		private Avalonia.Controls.Templates.IDataTemplate SelectTemplate(object? objItem)
//			=> objItem is char
//				? Templates["dtempCharIcon"]
//				: objItem is Avalonia.Media.IImage
//					? Templates["dtempImgIcon"]
//					: throw new System.InvalidProgramException("Unsupported type with no matching template");

//		public Avalonia.Controls.Control Build(object? objItem)
//			=> SelectTemplate(objItem)?.Build(objItem) ?? throw new System.InvalidProgramException($"Can't find template for object of " +
//				$"type {objItem.GetType()}");

//		public bool Match(object? objItem)
//			=> objItem is char || objItem is Avalonia.Media.IImage;

//		[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification =
//			"This needs to be non-static so Avalonia can query it at runtime.")]
//		public bool SupportsRecycling => false;
//	}
//}