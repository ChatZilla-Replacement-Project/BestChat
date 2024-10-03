using System.Linq;

namespace BestChat.IRC.Views.Desktop;

using Platform.DataAndExt.Ext;

public partial class UserNetEditorDlg : Avalonia.Controls.Window, System.ComponentModel
	.INotifyPropertyChanged
{
	#region Constructors & Deconstructors
		public UserNetEditorDlg()
			=> InitializeIfNeeded();
	#endregion

	#region Delegates
	#endregion

	#region Events
		public new System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles",
			Justification = "Naming convention for property declarations require a certain format even though " +
			"these aren't properties")]
		public static readonly Avalonia.DirectProperty<UserNetEditorDlg, Modes> ModeProperty = Avalonia.AvaloniaProperty
			.RegisterDirect<UserNetEditorDlg, Modes>(
				nameof(Mode),
				sender
					=> sender.Mode,
				(sender, modeNew)
					=> sender.Mode = modeNew,
				Modes.invalid
			);


		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelAreYouSure =
			MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strDelServerDomainAreYouSure, Rsrcs
			.strDelServerDomainAreYouSureTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question,
			Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelCreatingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
				.strCancelCreatingUserNetTitle, Rsrcs.strCancelCreatingUserNetMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo,
				MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelEditingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
				.strCancelEditingUserNetTitle, Rsrcs.strCancelCreatingUserNetMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo,
				MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

	#endregion

	#region Helper Types
		public enum Modes : byte
		{
			invalid,
			edit,
			create,
		}

		private abstract class PortWrapper(string strValAsText)
		{
			public string ValAsText
				=> strValAsText;
		}

		private class GeneralPortWrapper(ushort usVal) : PortWrapper(usVal.ToString())
		{
			public ushort Val
				=> usVal;

			public static implicit operator ushort(GeneralPortWrapper pwConvertThis)
				=> pwConvertThis.Val;
		}

		private class PlaceHolderPortWrapper(PlaceHolderPortWrapper.Types pwtType) :
			PortWrapper(pwtType switch
				{
					Types.letBestChatDecide
						=> Rsrcs.strDefPortToUseForNetworks,

					Types.notListed
						=> Rsrcs.strNetworkPortTouseNotListed,

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Types>(pwtType,
							"While constructing placeholder port wrapper"),
				})
		{
			public enum Types : byte
			{
				letBestChatDecide,
				notListed,
			}
		}
	#endregion

	#region Members
		private Data.Defs.UserNetEditable? eunetCtxt = null;

		private Modes mode = Modes.invalid;

		private ushort? usPortToUse;

		private System.Collections.Generic.SortedDictionary<ushort, GeneralPortWrapper> mapNonSslPortsToWrapper = [];

		private System.Collections.Generic.SortedDictionary<ushort, GeneralPortWrapper> mapSslPortsToWrapper = [];

		private static readonly PlaceHolderPortWrapper pwLetBestChatDecide = new(PlaceHolderPortWrapper.Types
			.letBestChatDecide);

		private static readonly PlaceHolderPortWrapper pwUnlisted = new(PlaceHolderPortWrapper.Types.notListed);
	#endregion

	#region Properties
		public Data.Defs.UserNetEditable? UserNetCtxt
		{
			get => eunetCtxt;

			set
			{
				eunetCtxt = value;

				if (eunetCtxt != null)
				{
					foreach(ushort usCurAvailableSslPort in eunetCtxt.AllPossibleSslPortsFromAllServers)
						mapSslPortsToWrapper[usCurAvailableSslPort] = new(usCurAvailableSslPort);

					foreach(ushort usCurAvailableNonSslPort in eunetCtxt.AllPossibleNonSslPortsFromAllServers)
						mapNonSslPortsToWrapper[usCurAvailableNonSslPort] = new(usCurAvailableNonSslPort);

					if(eunetCtxt is Data.Defs.UserNet unetData)
						usPortToUse = unetData.PortToUse;

					PropertyChanged?.Invoke(this, new(nameof(PortsToShow)));
					PropertyChanged?.Invoke(this, new(nameof(CurPortToUseSel)));
				}
			}
		}

		public Modes Mode
		{
			get => mode;

			set
			{
				if(value != mode)
				{
					mode = value;

					PropertyChanged?.Invoke(this, new(nameof(Mode)));
				}
			}
		}

		private System.Collections.Generic.IEnumerable<PortWrapper> PortsToShow
			=> eunetCtxt == null
				? [pwUnlisted]
				: (chkUseSsl.IsChecked == true
						? mapSslPortsToWrapper.Values
						: mapNonSslPortsToWrapper.Values
					).Append<PortWrapper>(pwLetBestChatDecide).Append(pwUnlisted);

		private PortWrapper? CurPortToUseSel
			=> (chkUseSsl.IsChecked == true
				? mapSslPortsToWrapper
				: mapNonSslPortsToWrapper
			).Values.FirstOrDefault(pwCur => pwCur != null && pwCur == usPortToUse, null);

		private bool DidUserRequestAnUnlistedPort
			=> comboPortToUse.SelectedItem == pwUnlisted;

		private bool IsOkToClose
			=> (mode switch
					{
						Modes.invalid
							=> throw new System.InvalidProgramException(
								"This dialog should never have been shown with an " +
								"invalid mode"),

						Modes.create
							=> msgboxCancelCreatingAreYouSure,

						Modes.edit
							=> msgboxCancelEditingAreYouSure,

						_
							=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(
								mode,
								"While handling the cancel button being clicked"),
					}).ShowWindowDialogAsync(this)
				.Result ==
				MsBox.Avalonia.Enums.ButtonResult.Yes;

	#endregion

	#region Methods
		private void UpdateTitle()
			=> Title = eunetCtxt == null
				? throw new System.InvalidOperationException("Set the network being edited before showing a network " +
					"editor")
				: mode switch
					{
						Modes.invalid
							=> throw new System.InvalidOperationException("Set the mode before showing a network editor"),

						Modes.edit
							=> Rsrcs.strCreatingNetTitleFmt,

						Modes.create
							=> Rsrcs.strEditingNetTitleFmt.Fmt(eunetCtxt.Name),

						_
							=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
								"While setting the title for a network editor"),
					};
	#endregion

	#region Event Handlers
		private void OnAddDomain(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(eunetCtxt == null)
				return;

			ServerDomainEditorDlg dlg = new()
			{
				Mode = ServerDomainEditorDlg.Modes.creatingNew,
				ServerCtxt = eunetCtxt.GetBlankNewServerDomain()
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				eunetCtxt.AddServerDomain(dlg.ServerCtxt!);
		}

		private void OnEditDomain(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(eunetCtxt == null)
				return;

			ServerDomainEditorDlg dlg = new()
				{
					Mode = ServerDomainEditorDlg.Modes.editingExisting,
					ServerCtxt = ((Data.Defs.NetServerInfo)dgServerDomains.SelectedItem).MakeEditableVersion(eunetCtxt),
				};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				dlg.ServerCtxt!.Save();
		}

		private void OnDelDomain(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(eunetCtxt != null && msgboxCancelAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums
					.ButtonResult.Yes)
				eunetCtxt.DelServerDomain((Data.Defs.NetServerInfo)dgServerDomains.SelectedItem);
		}

		private void OnMoveDomainUp(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{

		}

		private void OnMoveDomainDown(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{

		}

		protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
		{
			if(!e.IsProgrammatic && !IsOkToClose)
				e.Cancel = true;

			base.OnClosing(e);
		}

		private void OnCancelClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(IsOkToClose)
				Close(false);
		}

		private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
			=> Close(true);

		private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
			=> Close(null);
	#endregion
}