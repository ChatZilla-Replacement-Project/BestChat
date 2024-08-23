// Ignore Spelling: Ctrls dtemp

namespace BestChat.GUI.Ctrls
{
	/// <summary>
	/// Interaction logic for Templates.xaml
	/// </summary>
	public partial class Templates : System.Windows.ResourceDictionary
	{
		public Templates()
		{
			InitializeComponent();

			CharIcon = (System.Windows.DataTemplate?)this["dtempCharIcon"] ?? throw new System.InvalidProgramException("Can't find " +
				"data template dtempCharIcon");
			ImgIcon = (System.Windows.DataTemplate?)this["dtempImgIcon"] ?? throw new System.InvalidProgramException("Can't find " +
				"data template dtempImgIcon");
		}

		public static readonly Templates instance = [];

		public System.Windows.DataTemplate CharIcon
		{
			get;

			init;
		}

		public System.Windows.DataTemplate ImgIcon
		{
			get;

			init;
		}
	}
}