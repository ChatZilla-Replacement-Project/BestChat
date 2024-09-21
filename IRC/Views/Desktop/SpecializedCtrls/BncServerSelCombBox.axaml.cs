// Ignore Spelling: Sel

using System.Linq;

namespace BestChat.IRC.Views.Desktop.SpecializedCtrls;

public partial class BncServerSelCombBox : Avalonia.Controls.ComboBox
{
	#region Constructors & Deconstructors
		public BncServerSelCombBox()
			=> InitializeComponent();
	#endregion

	#region Constants
		#pragma warning disable IDE1006 // Naming Styles
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles",
				Justification = "Due to naming standards that are inherited")]
			public static readonly Avalonia.DirectProperty<BncServerSelCombBox, Data.Defs.BncServerInfo?>
					SelServerProperty = Avalonia.AvaloniaProperty.RegisterDirect<BncServerSelCombBox, Data.Defs.BncServerInfo?>(
				nameof(SelServer),
				comboSender
					=> comboSender.SelServer,
				(comboSender, serverNewVal)
						=> comboSender.SelServer = serverNewVal,
				null
			);

			[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles",
				Justification = "Due to naming standards that are inherited")]
			public static readonly Avalonia.DirectProperty<BncServerSelCombBox, System.Collections.Generic
					.IEnumerable<Data.Defs.BncServerInfo>?> AllServersProperty = Avalonia.AvaloniaProperty
					.RegisterDirect<BncServerSelCombBox, System.Collections.Generic.IEnumerable<Data.Defs.BncServerInfo>?>(
				nameof(AllServers),
				comboSender
					=> comboSender.AllServers,
				(comboSender, eservers)
						=> comboSender.AllServers = eservers,
				null
			);
		#pragma warning restore IDE1006 // Naming Styles
	#endregion

	#region Properties
		public Data.Defs.BncServerInfo? SelServer
		{
			get => (Data.Defs.BncServerInfo?)SelectedItem;

			set => SelectedItem = value;
		}

		public System.Collections.Generic.IEnumerable<Data.Defs.BncServerInfo>? AllServers
		{
			get => ItemsSource?.Cast<Data.Defs.BncServerInfo>();

			set => ItemsSource = value;
		}
	#endregion
}