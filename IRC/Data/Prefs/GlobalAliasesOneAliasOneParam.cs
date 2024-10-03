namespace BestChat.IRC.Data.Prefs;

using System.Linq;

public class GlobalAliasesOneAliasOneNamedParam : Platform.DataAndExt.Obj<GlobalAliasesOneAliasOneNamedParam>
{
	public GlobalAliasesOneAliasOneNamedParam(in GlobalAliasesOneAlias aliasParent)
	{
		this.aliasParent = aliasParent;

		strName = "";
	}

	public GlobalAliasesOneAliasOneNamedParam(in GlobalAliasesOneAlias aliasParent, in string strName, in Platform
		.DataAndExt.Cmd.ParamTypes.Abstract pt, in string? strDoc = null)
	{
		this.aliasParent = aliasParent;

		this.strName = strName;
		this.pt = pt;
		this.strDoc = strDoc;
	}

	public GlobalAliasesOneAliasOneNamedParam(in GlobalAliasesOneAliasOneNamedParam aparamCopyFrom)
	{
		aliasParent = aparamCopyFrom.aliasParent;

		strName = aparamCopyFrom.Name;
		pt = aparamCopyFrom.ParamType;
		strDoc = aparamCopyFrom.Doc;
	}

	public GlobalAliasesOneAliasOneNamedParam(in GlobalAliasesOneAlias aliasParent, in Platform
		.DataAndExt.Cmd.Param paramCopyFrom)
	{
		this.aliasParent = aliasParent;

		strName = paramCopyFrom.Name;
		pt = paramCopyFrom.TypeOfParam;
		strDoc = paramCopyFrom.Doc;
	}

	public GlobalAliasesOneAliasOneNamedParam(in GlobalAliasesOneAlias aliasParent, DTO.GlobalAliasesOneAliasOneNamedParamDTO
		dto)
	{
		this.aliasParent = aliasParent;

		strName = dto.Name;
		pt = Platform.DataAndExt.Cmd.ParamTypes.Abstract.Instances.First(ptCur
			=> ptCur.Name == dto.ParamType);
		strDoc = dto.Doc;
	}

	public event DFieldChanged<string>? evtNameChanged;

	public event DFieldChanged<Platform.DataAndExt.Cmd.ParamTypes.Abstract?>? evtParamTypeChanged;

	public event DFieldChanged<string?>? evtDocChanged;

	public event System.Action<GlobalAliasesOneAliasOneNamedParam, Platform.DataAndExt.Cmd.Param?>? evtDeclaredParamChanged;

	public readonly GlobalAliasesOneAlias aliasParent;


	private string strName;

	private Platform.DataAndExt.Cmd.ParamTypes.Abstract? pt = null;

	private string? strDoc = null;

	public string Name
	{
		get => strName;

		protected set
		{
			if(strName != value)
			{
				string? strOldName = strName;

				strName = value;

				MakeDirty();

				FireNameChanged(strOldName);

				RecreateDeclaredParam();
			}
		}
	}

	public Platform.DataAndExt.Cmd.ParamTypes.Abstract? ParamType
	{
		get => pt;

		set
		{
			if(pt != value)
			{
				Platform.DataAndExt.Cmd.ParamTypes.Abstract? ptOld = pt;

				pt = value;

				MakeDirty();

				FireParamTypeChanged(ptOld);

				RecreateDeclaredParam();
			}
		}
	}

	public string? Doc
	{
		get => strDoc;

		set
		{
			if(strDoc != value)
			{
				string? strOldDoc = strDoc;

				strDoc = value;

				MakeDirty();

				FireDocChanged(strOldDoc);

				RecreateDeclaredParam();
			}
		}
	}

	public Platform.DataAndExt.Cmd.Param? DeclaredParam
	{
		get;

		private set;
	}

	private void RecreateDeclaredParam()
	{
		if(strName != "" && pt is not null)
		{
			DeclaredParam = new(strName, pt, strDoc ?? "");

			FireDeclaredParamChanged();
		}
	}

	private void FireNameChanged(in string strOldName)
	{
		FirePropChanged(nameof(Name));

		evtNameChanged?.Invoke(this, strOldName, strName);
	}

	private void FireParamTypeChanged(in Platform.DataAndExt.Cmd.ParamTypes.Abstract? ptOld)
	{
		FirePropChanged(nameof(ParamType));

		evtParamTypeChanged?.Invoke(this, ptOld, pt);
	}

	private void FireDocChanged(in string? strOldDoc)
	{
		FirePropChanged(nameof(Doc));

		evtDocChanged?.Invoke(this, strOldDoc, strDoc);
	}

	private void FireDeclaredParamChanged()
	{
		FirePropChanged(nameof(DeclaredParam));

		evtDeclaredParamChanged?.Invoke(this, DeclaredParam);
	}

	public GlobalAliasesOneAliasOneNamedParamEditable MakeEditable()
		=> new(this);

	public void SaveFrom(GlobalAliasesOneAliasOneNamedParamEditable eaparam)
	{
		Name = eaparam.Name;
		ParamType = eaparam.ParamType;
		Doc = eaparam.Doc;
	}

	public DTO.GlobalAliasesOneAliasOneNamedParamDTO ToDTO()
		=> new(
			strName,
			pt?.Name ?? throw new System.InvalidOperationException(@"Can't save this alias parameter as it =has no"
				+ @"type yet."),
			strDoc
		);
}