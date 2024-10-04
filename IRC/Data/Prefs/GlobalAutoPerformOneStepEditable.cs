using BestChat.Platform.DataAndExt.Cmd;

namespace BestChat.IRC.Data.Prefs;

public class GlobalAutoPerformOneStepEditable : GlobalAutoPerformOneStep
{
	internal GlobalAutoPerformOneStepEditable(in GlobalAutoPerformOneStep stepOriginal) :
		base(stepOriginal.WhatToDo, stepOriginal.Parent)
		=> this.stepOriginal  = stepOriginal;

	public readonly GlobalAutoPerformOneStep stepOriginal;

	public GlobalAutoPerformOneStep OriginalStep
		=> stepOriginal;

	public bool WereChangesMade
	{
		get;

		private set;
	}

	public new CmdCall WhatToDo
	{
		get => base.WhatToDo;

		set
		{
			if(base.WhatToDo != value)
			{
				base.WhatToDo = value;

				WereChangesMade = true;

				FirePropChanged(nameof(WhatToDo));
			}
		}
	}

	public bool HasErrors
		=> WhatToDo.FullTextAsEntered == "";

	public bool IsValid
		=> !HasErrors;

	public void Save()
		=> stepOriginal.SaveFrom(this);
}