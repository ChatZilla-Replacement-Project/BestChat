// Ignore Spelling: Sel

using System.Linq;

namespace BestChat.IRC.Views.Desktop.SpecializedCtrls;

public partial class BncServerSelCombBox : Avalonia.Controls.ComboBox
{
	#region Constructors & Deconstructors
	public BncServerSelCombBox()
		=> InitializeComponet();
	#endregion

	#region Constants
		#pragma warning disable IDE1006 // Naming Styles
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles",
				Justification = "Due to naming standards that are inherited")]
			public static readonly Avalonia.DirectProperty<BncServerSelCombBox, Data.Defs.BNC.ServerInfo?>
					SelServerProperty = Avalonia.AvaloniaProperty.RegisterDirect<BncServerSelCombBox, Data.Defs.BNC
					.ServerInfo?>(
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
					.IEnumerable<Data.Defs.BNC.ServerInfo>?> AllServersProperty = Avalonia.AvaloniaProperty
					.RegisterDirect<BncServerSelCombBox, System.Collections.Generic.IEnumerable<Data.Defs.BNC
					.ServerInfo>?>(
				nameof(AllServers),
				comboSender
					=> comboSender.AllServers,
				(comboSender, enumservers)
						=> comboSender.AllServers = enumservers,
				null
			);
		#pragma warning restore IDE1006 // Naming Styles
	#endregion

	#region Properties
		public Data.Defs.BNC.ServerInfo? SelServer
		{
			get => (Data.Defs.BNC.ServerInfo?)SelectedItem;

			set => SelectedItem = value;
		}

		public System.Collections.Generic.IEnumerable<Data.Defs.BNC.ServerInfo>? AllServers
		{
			get => ItemsSource?.Cast<Data.Defs.BNC.ServerInfo>();

			set => ItemsSource = value;
		}
	#endregion
}