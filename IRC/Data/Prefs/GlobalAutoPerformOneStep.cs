namespace BestChat.IRC.Data.Prefs;

public class GlobalAutoPerformOneStep : Platform.DataAndExt.Obj<GlobalAutoPerformOneStep>, IReadOnlyOneStep,
	IKeyChanged<GlobalAutoPerformOneStep, string>
{
	#region Constructors & Deconstructors
		public GlobalAutoPerformOneStep(in IAutoPerformOnEvtPrefs cmgrParent)
		{
			this.cmgrParent  = cmgrParent;

			cmdcWhatToDo = CmdCall.cmdcInvalid;
		}

		public GlobalAutoPerformOneStep(in Platform.DataAndExt.Cmd.AbstractCmdCall? cmdcWhatToDo, in IAutoPerformOnEvtPrefs
				cmgrParent, in System.Guid guid = default) :
			base(guid)
		{
			this.cmgrParent  = cmgrParent;

			this.cmdcWhatToDo = new(cmdcWhatToDo ?? CmdCall.cmdcBlank);
		}

		public GlobalAutoPerformOneStep(in DTO.GlobalAutoPerformOneStepDTO dto, in IAutoPerformOnEvtPrefs cmgrParent) :
			base(dto.GUID)
		{
			this.cmgrParent  = cmgrParent;

			cmdcWhatToDo = new(dto.WhatToDo);
		}
		#endregion

	#region Delegates
	#endregion

	#region Events
		public event DFieldChanged<CmdCall>? evtWhatToDoChanged;

		public event IKeyChanged<GlobalAutoPerformOneStep, string>.DKeyChanged? evtKeyChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public class CmdCall : Platform.DataAndExt.Cmd.AbstractCmdCall
		{
			public CmdCall(in Platform.DataAndExt.Cmd.CmdDef cmd, in string strFullTextOfCmd = "") :
				base(cmd, strFullTextOfCmd)
			{
			}

			public CmdCall(Platform.DataAndExt.Cmd.AbstractCmdCall cmdc) :
				base(cmdc.CmdDef, cmdc.FullTextAsEntered)
			{
			}

			internal static CmdCall cmdcInvalid = new(cmdcBlank);
		}
	#endregion

	#region Members
		public readonly IAutoPerformOnEvtPrefs cmgrParent;

		private CmdCall cmdcWhatToDo;
	#endregion

	#region Properties
		public IAutoPerformOnEvtPrefs Parent
			=> cmgrParent;

		public CmdCall WhatToDo
		{
			get => cmdcWhatToDo;

			set
			{
				if(cmdcWhatToDo != value)
				{
					CmdCall? cmdcOldWhatToDo = cmdcWhatToDo;

					cmdcWhatToDo = value;

					MakeDirty();

					FireWhatToDoChanged(cmdcOldWhatToDo);
				}
			}
		}
	#endregion

	#region Methods
		private void FireWhatToDoChanged(in CmdCall cmdcOldWhatToDo)
		{
			FirePropChanged(nameof(cmdcWhatToDo));

			evtWhatToDoChanged?.Invoke(this, cmdcOldWhatToDo, cmdcWhatToDo);

			evtKeyChanged?.Invoke(this, cmdcOldWhatToDo.FullTextAsEntered, cmdcWhatToDo.FullTextAsEntered);
		}

		public GlobalAutoPerformOneStepEditable MakeEditable()
			=> new(this);

		public void SaveFrom(GlobalAutoPerformOneStepEditable estep)
			=> WhatToDo = estep.WhatToDo;

		public DTO.GlobalAutoPerformOneStepDTO ToDTO()
			=> new(guid, cmdcWhatToDo ?? throw new System.InvalidOperationException(@"Make sure the what to do is set before "
				+ @"making a DTO"));
	#endregion

	#region Event Handlers
	#endregion
}