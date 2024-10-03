using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class GlobalAliasesOneAliasEditable : GlobalAliasesOneAlias
{
	public GlobalAliasesOneAliasEditable(GlobalAliasesOneAlias aliasOriginal)
		: base(aliasOriginal.Name, aliasOriginal.WhatToRun, aliasOriginal.Doc)
		=> this.aliasOriginal = aliasOriginal;

	public GlobalAliasesOneAlias aliasOriginal;

	public GlobalAliasesOneAlias OriginalAlias
		=> aliasOriginal;

	public void Save()
		=> aliasOriginal.SaveFrom(this);

	public bool WereChangesMade
	{
		get;

		private set;
	}

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

	public new Platform.DataAndExt.Cmd.AbstractCmdCall? Cmd
	{
		get => base.WhatToRun;

		set
		{
			if(base.WhatToRun != value)
			{
				base.WhatToRun = value;

				WereChangesMade = true;

				FirePropChanged(nameof(WereChangesMade));
			}
		}
	}

	public bool HasErrors
		=> false; // TODO: Come up with an actual value

	public bool IsValid
		=> !HasErrors;

	public new void PrependNewPositionedParameter(GlobalAliasesOneAliasOneParam paramNew)
	{
		if(!paramNew.IsRequired && PositionalParameters.First().IsRequired)
			throw new System.InvalidOperationException(PrefsRsrcs.strRequiredAliasParamsMustComeBeforeOptionalParams);

		base.PrependNewPositionedParameter(paramNew);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void AppendNewPositionedParameter(GlobalAliasesOneAliasOneParam paramNew)
	{
		if(paramNew.IsRequired && !PositionalParameters.Last().IsRequired)
			throw new System.InvalidOperationException(PrefsRsrcs.strRequiredAliasParamsMustComeBeforeOptionalParams);

		base.AppendNewPositionedParameter(paramNew);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void AddPositionedParameterAfter(GlobalAliasesOneAliasOneParam paramNew, in
		GlobalAliasesOneAliasOneParam paramExisting)
	{
		if(paramNew.IsRequired && !paramExisting.IsRequired)
			throw new System.InvalidOperationException(PrefsRsrcs.strRequiredAliasParamsMustComeBeforeOptionalParams);

		base.AddPositionedParameterAfter(paramNew, paramExisting);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void AddPositionedParameterBefore(GlobalAliasesOneAliasOneParam paramNew, in
		GlobalAliasesOneAliasOneParam paramExisting)
	{
		if(!paramNew.IsRequired && paramExisting.IsRequired)
			throw new System.InvalidOperationException(PrefsRsrcs.strRequiredAliasParamsMustComeBeforeOptionalParams);

		base.AddPositionedParameterBefore(paramNew, paramExisting);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void MovePositionedParameterToTop(GlobalAliasesOneAliasOneParam paramToMove)
	{
		base.MovePositionedParameterToTop(paramToMove);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void MovePositionedParameterUp(GlobalAliasesOneAliasOneParam paramToMove)
	{
		if(!paramToMove.IsRequired)
		{
			System.Collections.Generic.LinkedListNode<GlobalAliasesOneAliasOneParam> llnparamToMove = PositionalParameters
				.Find(paramToMove) ?? throw new System.InvalidOperationException(@"Can't find positional parameter to move in " +
				"the list.");
			if(llnparamToMove.Previous == null || llnparamToMove.Previous.Value.IsRequired)
				throw new System.InvalidOperationException(PrefsRsrcs.strRequiredAliasParamsMustComeBeforeOptionalParams);
		}

		base.MovePositionedParameterUp(paramToMove);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void MovePositionedParameterDown(GlobalAliasesOneAliasOneParam paramToMove)
	{
		if(paramToMove.IsRequired)
		{
			System.Collections.Generic.LinkedListNode<GlobalAliasesOneAliasOneParam> llnparamToMove = PositionalParameters
				.Find(paramToMove) ?? throw new System.InvalidOperationException(@"Can't find positional parameter to move in " +
				"the list.");
			if(llnparamToMove.Next == null || llnparamToMove.Next.Value.IsRequired)
				throw new System.InvalidOperationException(PrefsRsrcs.strRequiredAliasParamsMustComeBeforeOptionalParams);
		}

		base.MovePositionedParameterDown(paramToMove);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void MovePositonedParameterToBottom(GlobalAliasesOneAliasOneParam paramToMove)
	{
		base.MovePositonedParameterToBottom(paramToMove);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void RemovePositionedParameter(GlobalAliasesOneAliasOneParam paramToRemove)
	{
		base.RemovePositionedParameter(paramToRemove);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void AddNamedParameter(GlobalAliasesOneAliasOneParam paramToAdd)
	{
		base.AddNamedParameter(paramToAdd);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}

	public new void RemoveNamedParameter(GlobalAliasesOneAliasOneParam paramToRemove)
	{
		base.RemoveNamedParameter(paramToRemove);

		WereChangesMade = true;

		FirePropChanged(nameof(WereChangesMade));
	}
}