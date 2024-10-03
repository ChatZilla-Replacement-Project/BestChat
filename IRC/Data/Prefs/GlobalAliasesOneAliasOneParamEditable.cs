using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class GlobalAliasesOneAliasOneParamEditable : GlobalAliasesOneAliasOneParam, System.ComponentModel.INotifyDataErrorInfo
{
	internal GlobalAliasesOneAliasOneParamEditable(GlobalAliasesOneAliasOneParam aparamOriginal) :
		base(aparamOriginal.aliasParent, aparamOriginal.Name, aparamOriginal?.ParamType ?? throw new System
			.InvalidOperationException(@"How did an existing parameter get a null type?"), aparamOriginal.Doc)
		=> this.aparamOriginal = aparamOriginal;

	public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;

	public readonly GlobalAliasesOneAliasOneParam aparamOriginal;

	public new string Name
	{
		get => base.Name;

		set
		{
			if(base.Name != value)
			{
				base.Name = value;

				WereChangesMade = true;

				FirePropChanged(nameof(WereChangesMade));
			}
		}
	}

	public new Platform.DataAndExt.Cmd.ParamTypes.Abstract? ParamType
	{
		get => base.ParamType;

		set
		{
			if(base.ParamType != value)
			{
				base.ParamType = value;

				WereChangesMade = true;

				FirePropChanged(nameof(WereChangesMade));
			}
		}
	}

	public new string? Doc
	{
		get => base.Doc;

		set
		{
			if(base.Doc != value)
			{
				base.Doc = value;

				WereChangesMade = true;

				FirePropChanged(nameof(WereChangesMade));
			}
		}
	}

	public bool WereChangesMade
	{
		get;

		private set;
	}

	public bool IsNameValid
		=> base.Name != "" && !aparamOriginal.aliasParent.AllParametersByName.TryGetValue(base.Name, out
			GlobalAliasesOneAliasOneParam? aparamFoundThis) && aparamFoundThis != aparamOriginal;

	public bool HasErrors
		=> base.ParamType == null || !IsNameValid;

	public bool IsValid
		=> !HasErrors;

	public System.Collections.IEnumerable GetErrors(string? strPropNameToCheck)
	{
		System.Collections.Generic.LinkedList<string> llistErrors = [];

		string[] astrPropNamesToCheck = strPropNameToCheck is null
			? [nameof(Name), nameof(ParamType),]
			: [strPropNameToCheck];

		foreach(string strCurPropNameToCheck in astrPropNamesToCheck)
			switch(strCurPropNameToCheck)
			{
				case nameof(Name):
					if(base.Name is null)
						break;

					if(base.Name == "")
						llistErrors.AddLast(PrefsRsrcs.strAliasParamNameBlank);
					else if(IsNameValid)
						llistErrors.AddLast(PrefsRsrcs.strAliasParamNameNotUnique);

					break;

				case nameof(ParamType):
					if(base.ParamType == null)
						llistErrors.AddLast(PrefsRsrcs.strAliasParamTypeNotSet);

					break;

				default:
					throw new System.InvalidProgramException($@"Can't find the property {strCurPropNameToCheck} while checking " +
						@"for errors");
			}

		return llistErrors;
	}

	public void Save()
		=> aparamOriginal.SaveFrom(this);
}