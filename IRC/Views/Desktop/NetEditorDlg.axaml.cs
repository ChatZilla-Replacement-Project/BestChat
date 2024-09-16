using System.Linq;
using BestChat.Platform.DataAndExt.Ext;

namespace BestChat.IRC.Views.Desktop;

public partial class NetEditorDlg : Avalonia.Controls.Window, System.ComponentModel.INotifyPropertyChanged
{
	#region Constructors & Deconstructors
		public NetEditorDlg()
			=> InitializeIfNeeded();
	#endregion

	#region Delegates
	#endregion

	#region Events
		public new System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification
			= "Naming convention for property declarations require a certain format even though these aren't " +
		"properties")]
		public static readonly Avalonia.DirectProperty<NetEditorDlg, Modes> ModeProperty = Avalonia.AvaloniaProperty
			.RegisterDirect<NetEditorDlg, Modes>(
				nameof(Mode),
				sender
					=> sender.Mode,
				(sender, modeNew)
					=> sender.Mode = modeNew,
				Modes.invalid
			);
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

		private class PlaceHolderPortWrapper(PlaceHolderPortWrapper.Types pwtType) : PortWrapper(pwtType switch
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
		private Data.Defs.UserNet.Editable? eunetWhatsBeingEdited = null;

		private Modes mode = Modes.invalid;

		private ushort? usPortToUse;

		private System.Collections.Generic.SortedDictionary<ushort, GeneralPortWrapper> mapNonSslPortsToWrapper =
			[];

		private System.Collections.Generic.SortedDictionary<ushort, GeneralPortWrapper> mapSslPortsToWrapper =
			[];

		private static readonly PlaceHolderPortWrapper pwLetBestChatDecide =
			new(PlaceHolderPortWrapper.Types.letBestChatDecide);

		private static readonly PlaceHolderPortWrapper pwUnlisted = new(PlaceHolderPortWrapper.Types
			.notListed);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelAreYouSure =
			MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strDelServerDomainAreYouSure, Rsrcs
			.strDelServerDomainAreYouSureTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon
			.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);
	#endregion

	#region Properties
		public Data.Defs.UserNet.Editable? Data
		{
			get => eunetWhatsBeingEdited;

			set
			{
				eunetWhatsBeingEdited = value;

				if (eunetWhatsBeingEdited != null)
				{
					foreach(ushort usCurAvailableSslPort in eunetWhatsBeingEdited.AllPossibleSslPortsFromAllServers)
						mapSslPortsToWrapper[usCurAvailableSslPort] = new(usCurAvailableSslPort);

					foreach(ushort usCurAvailableNonSslPort in eunetWhatsBeingEdited.AllPossibleNonSslPortsFromAllServers)
						mapNonSslPortsToWrapper[usCurAvailableNonSslPort] = new(usCurAvailableNonSslPort);

					if(eunetWhatsBeingEdited is Data.Defs.UserNet unetData)
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

				}
			}
		}

		private System.Collections.Generic.IEnumerable<PortWrapper> PortsToShow
			=> eunetWhatsBeingEdited == null
				? [pwUnlisted]
				: (chkUseSSL.IsChecked
						? mapSslPortsToWrapper.Values
						: mapNonSslPortsToWrapper.Values
					).Append(pwLetBestChatDecide).Append(pwUnlisted);

		private PortWrapper? CurPortToUseSel
			=> (chkUseSsl.IsChecked
				? mapSslPortsToWrapper
				: mapNonSslPortsToWrapper
			).Values.FirstOrDefault(pwCur => pwCur == usPortToUse, null);

		private bool DidUserRequestAnUnlistedPort
			=> comboPortToUse.SelectedItem == pwUnlisted;
	#endregion

	#region Methods
		private void UpdateTitle()
			=> Title = eunetWhatsBeingEdited == null
				? throw new System.InvalidOperationException("Set the network being edited before showing a network " +
					"editor")
				: mode switch
					{
						Modes.invalid
							=> throw new System.InvalidOperationException("Set the mode before showing a network editor"),

						Modes.edit
							=> Rsrcs.strCreatingNetTitleFmt,

						Modes.create
							=> Rsrcs.strEditingNetTitleFmt.Fmt(eunetWhatsBeingEdited.Name),

						_
							=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
								"While setting the title for a network editor"),
					};
	#endregion

	#region Event Handlers
		private void OnAddDomain(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(eunetWhatsBeingEdited == null)
				return;

			ServerDomainEditorDlg dlg = new()
			{
				Mode = ServerDomainEditorDlg.Modes.creatingNew,
				WhatsBeingEdited = eunetWhatsBeingEdited.GetBlankNewServerDomain()
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				eunetWhatsBeingEdited.AddServerDomain(dlg.WhatsBeingEdited!);
		}

		private void OnEditDomain(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			ServerDomainEditorDlg dlg = new(true, ((Data.Defs
				.NetServerInfo)dgServerDomains.SelectedItem).MakeEditableVersion(eunetWhatsBeingEdited))
			{
				Owner = this,
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				dlg.WhatsBeingEdited!.Save();
		}

		private void OnDelDomain(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(eunetWhatsBeingEdited != null && msgboxCancelAreYouSure.ShowWindowDialogAsync(this).Result == MsBox
				.Avalonia.Enums.ButtonResult.Yes)
			{
				eunetWhatsBeingEdited.DelServerDomain((Data.Defs.NetServerInfo)dgServerDomains.SelectedItem);
			}
		}
	#endregion
}