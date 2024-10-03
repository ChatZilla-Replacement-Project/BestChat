using System.Linq;

namespace BestChat.IRC.Views.Desktop;

using Platform.DataAndExt.Ext;

public partial class PortEditorDlg : Avalonia.Controls.Window, System.ComponentModel
	.INotifyPropertyChanged, System.ComponentModel.INotifyDataErrorInfo
{
	#region Constructors & Deconstructors
		public PortEditorDlg()
		{
			InitializeComponent();

			DataContext = this;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public new event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

		public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;
	#endregion

	#region Constants
		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelCreatingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelNewPortFmt, Rsrcs.strCancelNewPortTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox
			.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelEditingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelChangingPortFmt, Rsrcs.strCancelChangingPortTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox
			.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);
	#endregion

	#region Helper Types
		public enum Modes
		{
			invalid,
			@new,
			changing,
		}
	#endregion

	#region Members
		private Modes mode = Modes.invalid;

		private ushort? usExistingPort;

		private ushort usCurPort;

		private readonly System.Collections.Generic.HashSet<ushort> ussetUnavailablePorts = [];
	#endregion

	#region Properties
		public Modes Mode
		{
			get => mode;

			set => mode = value;
		}

		public ushort? ExistingPort
		{
			get => usExistingPort;

			set
			{
				if(usExistingPort != value)
				{
					usExistingPort = value;

					PropertyChanged?.Invoke(this, new(nameof(ExistingPort)));
				}
			}
		}

		public ushort CurPort
		{
			get => usCurPort;

			set
			{
				if(usCurPort != value)
				{
					usCurPort = value;

					PropertyChanged?.Invoke(this, new(nameof(CurPort)));

					ErrorsChanged?.Invoke(this, new(nameof(CurPort)));
				}
			}
		}

		public bool WereChangesMade
			=> usCurPort != usExistingPort;

		private System.Collections.Generic.IEnumerable<ushort> UnavailablePorts
			=> ussetUnavailablePorts;

		private bool IsOkToClose
			=> (mode switch
					{
						Modes.invalid
							=> throw new System.InvalidProgramException("This dialog should never have been shown with an " +
								"invalid mode"),

						Modes.@new
							=> msgboxCancelCreatingAreYouSure,

						Modes.changing
							=> msgboxCancelEditingAreYouSure,

						_
							=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
								"While handling the cancel button being clicked"),
					}).ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums.ButtonResult.Yes;

		public bool HasErrors
			=> !ussetUnavailablePorts.Contains(usCurPort);

		public bool IsValid
			=> !HasErrors;
	#endregion

	#region Methods
		public System.Collections.IEnumerable GetErrors(string? strPropNameToGetErrorsFor)
		{
			System.Collections.Generic.SortedSet<string> strsetErrors = [];

			switch(strPropNameToGetErrorsFor)
			{
				case null or nameof(CurPort):
					if(ussetUnavailablePorts.Contains(usCurPort))
						strsetErrors.Add(Rsrcs.strPortTaken);

					break;

				default: // Just ignore values we don't know what to do with
					break;
			}

			return strsetErrors;
		}
	#endregion

	#region Event Handlers
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