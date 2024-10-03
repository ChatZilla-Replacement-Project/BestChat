// Ignore Spelling: evt Bnc Ctxt

using System.Linq;

namespace BestChat.IRC.Views.Desktop;

public partial class BncNetworkSelectionDlg : Avalonia.Controls.Window, System.ComponentModel
	.INotifyPropertyChanged, System.ComponentModel.INotifyDataErrorInfo
{
	#region Constructors & Deconstructors
		public BncNetworkSelectionDlg()
		{
			InitializeComponent();

			DataContext = this;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		#pragma warning disable IDE1006 // Naming Styles
			public new event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
		#pragma warning restore IDE1006 // Naming Styles

		public event System.Action<BncNetworkSelectionDlg, string, string>? evtCurValChanged;

		public event System.Action<BncNetworkSelectionDlg, string, string>? evtStartingValChanged;

		#pragma warning disable IDE1006 // Naming Styles
			public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;
		#pragma warning restore IDE1006 // Naming Styles
	#endregion

	#region Constants
		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelCreatingAllowedNetAreYouSure = MsBox.Avalonia.MessageBoxManager
			.GetMessageBoxStandard(Rsrcs.strCancelChangingBncAllowedNetMsg, Rsrcs
			.strCancelChangingBncAllowedNetTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums
			.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelChangingAllowedNetAreYouSure = MsBox.Avalonia.MessageBoxManager
			.GetMessageBoxStandard(Rsrcs.strCancelChangingBncAllowedNetMsg, Rsrcs
			.strCancelChangingBncAllowedNetTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums
			.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelCreatingProhibitedNetAreYouSure = MsBox.Avalonia.MessageBoxManager
			.GetMessageBoxStandard(Rsrcs.strCancelChangingBncProhibitedNetMsg, Rsrcs
			.strCancelChangingBncProhibitedNetTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums
			.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelChangingProhibitedNetAreYouSure = MsBox.Avalonia.MessageBoxManager
			.GetMessageBoxStandard(Rsrcs.strCancelChangingBncProhibitedNetMsg, Rsrcs
			.strCancelChangingBncProhibitedNetTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums
			.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);
	#endregion

	#region Helper Types
		public enum Modes : byte
		{
			invalid,
			@new,
			changing,
		}

		public enum NetStatuses : byte
		{
			invalid,
			allowed,
			prohibited,
		}
	#endregion

	#region Members
		private Modes mode = Modes.invalid;

		private NetStatuses statusForNet = NetStatuses.invalid;

		private Data.Defs.BncEditable? ebncCtxt = null;

		private string strCurVal = "";

		private string strStartingVal = "";
	#endregion

	#region Properties
		public Modes Mode
		{
			get => mode;

			set
			{
				if(mode != value)
				{
					mode = value;

					if(statusForNet != NetStatuses.invalid)
						UpdateTitle();
				}
			}
		}

		public NetStatuses NetStatus
		{
			get => statusForNet;

			set
			{
				if(statusForNet != value)
				{
					statusForNet = value;

					if(mode != Modes.invalid)
						UpdateTitle();
				}
			}
		}

		public Data.Defs.BncEditable? CtxtBNC
		{
			get => ebncCtxt;

			set
			{
				if(ebncCtxt != value)
				{
					ebncCtxt = value;

					FirePropChanged(nameof(HasErrors));
				}
			}
		}

		public string CurVal
		{
			get => strCurVal;

			private set
			{
				if(strCurVal != value)
				{
					string strOldCurVal = strCurVal;

					strCurVal = value;

					FireCurValChanged(strOldCurVal);
				}
			}
		}

		public string StartingVal
		{
			get => strStartingVal;

			init
			{
				if(strStartingVal == value)
					return;

				if(value != "")
					mode = Modes.changing;

				string strOldStartingVal = strStartingVal;

				strCurVal = strStartingVal = value;

				FireCurValChanged("");
				FireStartingValChanged(strOldStartingVal);
			}
		}

		public bool WereChangesMade
			=> CurVal != StartingVal;

		private bool IsOkToClose
			=> (mode switch
					{
						Modes.invalid
							=> throw new System.InvalidProgramException("This dialog should never have been shown with "
								+ "an invalid mode"),

						Modes.@new
							=> statusForNet switch
								{
									NetStatuses.invalid
										=> throw new System.InvalidProgramException("This dialog should never have been shown "
											+ "with an invalid mode"),

									NetStatuses.allowed
										=> msgboxCancelCreatingAllowedNetAreYouSure,

									NetStatuses.prohibited
										=> msgboxCancelCreatingProhibitedNetAreYouSure,
									_
										=> throw new Platform.DataAndExt.Exceptions
											.UnknownOrInvalidEnumException<NetStatuses>(statusForNet, "While selecting the " +
											"message box to show when the user tried to close the dialog"),
								},

						Modes.changing
							=> statusForNet switch
								{
									NetStatuses.invalid
										=> throw new System.InvalidProgramException("This dialog should never have been shown "
											+ "with an invalid mode"),

									NetStatuses.allowed
										=> msgboxCancelChangingAllowedNetAreYouSure,

									NetStatuses.prohibited
										=> msgboxCancelChangingProhibitedNetAreYouSure,
									_
										=> throw new Platform.DataAndExt.Exceptions
											.UnknownOrInvalidEnumException<NetStatuses>(statusForNet, "While selecting the " +
											"message box to show when the user tried to close the dialog"),
								},

							_
								=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
									"While handling the cancel button being clicked"),
					}).ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums.ButtonResult.Yes;

		public bool IsValid
			=> !HasErrors;

		public bool IsDirty
			=> strCurVal != strStartingVal;

		private System.Collections.Generic.IEnumerable<Data.Defs.Net> AllKnownNets
			=> Data.Defs.PredefinedNetMgr.mgr.AllItemsSortedByName.Union(Data.Defs.UserNetMgr.mgr
				.AllItemsSortedByName.Cast<Data.Defs.Net>());

		private bool IsCurNetKnown
			=> strCurVal != "" && (Data.Defs.PredefinedNetMgr.mgr.AllItems.ContainsKey(strCurVal) || Data.Defs
				.UserNetMgr.mgr.AllItems.ContainsKey(strCurVal));

		private bool IsCurNetUnknown
			=> strCurVal == "" || !Data.Defs.PredefinedNetMgr.mgr.AllItems.ContainsKey(strCurVal) || !Data.Defs
				.UserNetMgr.mgr.AllItems.ContainsKey(strCurVal);

		private Data.Defs.Net? SpecifiedNet
			=> IsCurNetUnknown
				? null
				: Data.Defs.PredefinedNetMgr.mgr.AllItems.ContainsKey(strCurVal)
					? Data.Defs.PredefinedNetMgr.mgr.AllItems[strCurVal]
					: Data.Defs.UserNetMgr.mgr.AllItems.ContainsKey(strCurVal)
						? Data.Defs.UserNetMgr.mgr.AllItems[strCurVal]
						: null;

		private bool IsCurNetCustom
			=> SpecifiedNet is Data.Defs.UserNet;

		public bool HasErrors
			=> ebncCtxt == null || strCurVal == "" || statusForNet switch
				{
					NetStatuses.invalid
						=> throw new System.InvalidProgramException("Somehow we still have an invalid status"),

					NetStatuses.allowed
						=> ebncCtxt.ProhibitedNets.Contains(strCurVal) || strStartingVal != strCurVal && ebncCtxt
							.AllowedNets.Contains(strCurVal),

					NetStatuses.prohibited
						=> ebncCtxt.AllowedNets.Contains(strCurVal) || strStartingVal != strCurVal && ebncCtxt
							.ProhibitedNets.Contains(strCurVal),

					_
						=> throw new Platform.DataAndExt.Exceptions
						.UnknownOrInvalidEnumException<NetStatuses>(statusForNet, "While testing if we have " +
							"errors"),
				};
	#endregion

	#region Methods
		private void UpdateTitle()
			=> Title = mode switch
				{
					Modes.invalid
						=> throw new System.InvalidProgramException("How did we get here?"),

					Modes.@new
						=> statusForNet switch
							{
								NetStatuses.invalid
									=> throw new System.InvalidProgramException("How did we get here?"),

								NetStatuses.allowed
									=> Rsrcs.strBncAddingAllowedNetDlgTitleFmt,

								NetStatuses.prohibited
									=> Rsrcs.strBncAddingProhibitedNetDlgTitleFmt,

								_
									=> throw new Platform.DataAndExt.Exceptions
										.UnknownOrInvalidEnumException<NetStatuses>(statusForNet, "While selecting a " +
										"title"),
							},

					Modes.changing
						=> statusForNet switch
							{
								NetStatuses.invalid
									=> throw new System.InvalidProgramException("How did we get here?"),

								NetStatuses.allowed
									=> Rsrcs.strBncChangingAllowedNetDlgTitleFmt,

								NetStatuses.prohibited
									=> Rsrcs.strBncChangingProhibitedNetDlgTitleFmt,

								_
									=> throw new Platform.DataAndExt.Exceptions
										.UnknownOrInvalidEnumException<NetStatuses>(statusForNet, "While selecting a " +
										"title"),
							},

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
							"While selecting a title"),
				};

		private void FirePropChanged(in string strWhichProp)
			=> PropertyChanged?.Invoke(this, new(strWhichProp));

		private void FireCurValChanged(in string strOldCurVal)
		{
			FirePropChanged(nameof(CurVal));

			evtCurValChanged?.Invoke(this, strOldCurVal, strCurVal);
		}

		private void FireStartingValChanged(in string strOldStartingVal)
		{
			FirePropChanged(nameof(StartingVal));

			evtStartingValChanged?.Invoke(this, strOldStartingVal, strStartingVal);
		}

		private void OnNameOfNetChanged(object objSender, System.EventArgs e)
		{
			FirePropChanged(nameof(IsCurNetKnown));
			FirePropChanged(nameof(IsCurNetUnknown));
			FirePropChanged(nameof(SpecifiedNet));
		}

		public System.Collections.IEnumerable GetErrors(string? strPropNameToGetErrorsFor)
			=> strCurVal == ""
				? [Rsrcs.strBncNetNameBlank]
				: ebncCtxt == null
					? []
					: statusForNet switch
						{
							NetStatuses.invalid
								=> throw new System.InvalidOperationException("Somehow we still have an invalid " +
									"status"),

							NetStatuses.allowed
								=> ebncCtxt.ProhibitedNets.Contains(strCurVal)
									? [Rsrcs.strBncNetAlreadyProhibited]
									: strStartingVal != strCurVal && ebncCtxt.AllowedNets.Contains(strCurVal)
										? [Rsrcs.strBncNetAlreadyAllowed]
										: [],

							NetStatuses.prohibited
								=> ebncCtxt.AllowedNets.Contains(strCurVal)
									? [Rsrcs.strBncNetAlreadyAllowed]
									: strStartingVal != strCurVal && ebncCtxt.ProhibitedNets.Contains(strCurVal)
										? [Rsrcs.strBncNetAlreadyProhibited]
										: System.Array.Empty<string>(),

							_
								=> throw new Platform.DataAndExt.Exceptions
									.UnknownOrInvalidEnumException<NetStatuses>(statusForNet, "While testing for " +
									"errors"),
						};

		protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
		{
			if(!e.IsProgrammatic && !IsOkToClose)
				e.Cancel = true;

			base.OnClosing(e);
		}

		private void OnCancelClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs
			e)
		{
			if(IsOkToClose)
				Close(false);
		}

		private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
			=> Close(true);
		private void OnCloseClicked(object? objSender
			, Avalonia.Interactivity.RoutedEventArgs e)
			=> Close(null);
	#endregion

	#region Event Handlers
	#endregion
}