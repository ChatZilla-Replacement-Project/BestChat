using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public partial class GlobalAliasesOneAlias : Platform.DataAndExt.Obj<GlobalAliasesOneAlias>, IReadOnlyOneAlias,
	IKeyChanged<GlobalAliasesOneAlias, string>
{
	#region Constructors & Deconstructors
		public GlobalAliasesOneAlias()
		{
			strName = "";
			strDoc = "";
			rlistPositionedParameters = [];
		}

		public GlobalAliasesOneAlias(in string strName, in Platform.DataAndExt.Cmd.AbstractCmdCall? cmdcWhatToRun, in string strDoc,
			in System.Collections.Generic.IEnumerable<GlobalAliasesOneAliasOneParam>? ePositionedParams = null, in
				System.Collections.Generic.IEnumerable<GlobalAliasesOneAliasOneParam>? eAllParams = null)
		{
			this.strName = strName;
			this.cmdcWhatToRun = cmdcWhatToRun;
			this.strDoc = strDoc;
			rlistPositionedParameters = ePositionedParams is null
				? []
				: new(ePositionedParams.Select(aparamCur
					=> new GlobalAliasesOneAliasOneParam(aparamCur)));
			if(eAllParams is not null)
				foreach(GlobalAliasesOneAliasOneParam aparamCur in eAllParams)
					mapAllParametersByName[aparamCur.Name] = new(aparamCur);
			this.cmdcWhatToRun = cmdcWhatToRun;
			this.strDoc = Doc ?? "";
		}

		public GlobalAliasesOneAlias(in DTO.GlobalAliasesOneAliasDTO dto) :
			base(dto.GUID)
		{
			strName = dto.Name;
			if(dto.NamedParams is not null)
				foreach(DTO.GlobalAliasesOneAliasOneParamDTO daparamCur in dto.NamedParams)
					mapAllParametersByName[daparamCur.Name] = new(this, daparamCur);
			rlistPositionedParameters = dto.PositionalParams is null
				? []
				: new(dto.PositionalParams.Select(strCurPositionedParam
					=> mapAllParametersByName[strCurPositionedParam]).ToArray());
			cmdcWhatToRun = dto.WhatToRun;
			strDoc = dto.Doc ?? "";
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event DFieldChanged<string>? evtNameChanged;

		public event DFieldChanged<Platform.DataAndExt.Cmd.AbstractCmdCall?>? evtCmdChanged;

		public event DFieldChanged<string>? evtDocChanged;

		public event IKeyChanged<GlobalAliasesOneAlias, string>.DKeyChanged? evtKeyChanged;

		public event DCollectionFieldChanged<System.Collections.Generic
			.IReadOnlyCollection<GlobalAliasesOneAliasOneParam>, GlobalAliasesOneAliasOneParam>?
			evtPositionedParametersChanged;

		public event DMapFieldChanged<System.Collections.Generic.IReadOnlyDictionary<string, GlobalAliasesOneAliasOneParam>,
			string, GlobalAliasesOneAliasOneParam>? evtNamedParametersChanged;
	#endregion

	#region Constants
		public const string strOneAliasFileExt = @".alias.json";

		public const string strManyAliasesFileExt = @".aliases.json";
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private string strName;

		private Platform.DataAndExt.Cmd.AbstractCmdCall? cmdcWhatToRun;

		private string strDoc;

		private readonly Platform.DataAndExt.Collections.ReorderableList<GlobalAliasesOneAliasOneParam>
			rlistPositionedParameters;

		private readonly System.Collections.Generic.SortedDictionary<string, GlobalAliasesOneAliasOneParam>
			mapAllParametersByName = [];
	#endregion

	#region Properties
		public string Name
		{
			get => strName;

			set
			{
				if(strName != value)
				{
					string strOldName = strName;

					strName = value;

					FireNameChanged(strOldName);

					MakeDirty();
				}
			}
		}

		public Platform.DataAndExt.Cmd.AbstractCmdCall? WhatToRun
		{
			get => cmdcWhatToRun;

			set
			{
				if(cmdcWhatToRun != value)
				{
					Platform.DataAndExt.Cmd.AbstractCmdCall? cmdcOldWhatToRun = cmdcWhatToRun;

					cmdcWhatToRun = value;

					FireCmdChanged(cmdcOldWhatToRun);

					MakeDirty();
				}
			}
		}

		public string Doc
		{
			get => strDoc;

			set
			{
				if(strDoc != value)
				{
					string strOldDoc = strDoc;

					strDoc = value;

					MakeDirty();

					FireDocChanged(strOldDoc);
				}
			}
		}

		public Platform.DataAndExt.Collections.IReadOnlyLinkedList<GlobalAliasesOneAliasOneParam> PositionalParameters
			=> rlistPositionedParameters;

		public System.Collections.Generic.IReadOnlyDictionary<string, GlobalAliasesOneAliasOneParam> AllParametersByName
			=> mapAllParametersByName;

		public Platform.DataAndExt.Cmd.CmdDef? DeclaredCmd
		{
			get;

			private set;
		}
	#endregion

	#region Methods
		private void FireNameChanged(in string strOldName)
		{
			FirePropChanged(nameof(Name));

			evtNameChanged?.Invoke(this, strOldName, strName);
			evtKeyChanged?.Invoke(this, strOldName, strName);
		}

		private void FireCmdChanged(in Platform.DataAndExt.Cmd.AbstractCmdCall? cmdcOldWhatToRun)
		{
			FirePropChanged(nameof(WhatToRun));

			evtCmdChanged?.Invoke(this, cmdcOldWhatToRun, cmdcWhatToRun);
		}

		private void FireDocChanged(in string strOldDoc)
		{
			FirePropChanged(nameof(Doc));

			evtDocChanged?.Invoke(this, strOldDoc, strDoc);
		}

		private void FirePositionedParametersChanged(in System.Collections.Generic.IReadOnlyCollection<
			GlobalAliasesOneAliasOneParam>? eNewPositionalParameters = null, in System.Collections.Generic
			.IReadOnlyCollection<GlobalAliasesOneAliasOneParam>? eRemovedPositionalParameters = null, in System
			.Collections.Generic.IReadOnlyCollection<GlobalAliasesOneAliasOneParam>? eMovedPositionedParameters = null)
		{
			FirePropChanged(nameof(PositionalParameters));

			evtPositionedParametersChanged?.Invoke(this, PositionalParameters, eNewPositionalParameters,
				eRemovedPositionalParameters, eMovedPositionedParameters);
		}

		private void FireNamedParametersChanged(in System.Collections.Generic.IReadOnlyCollection<string>?
			eNewNamedParameters = null, in System.Collections.Generic.IReadOnlyCollection<System.Tuple<string,
			GlobalAliasesOneAliasOneParam>>? eRemovedNamedParameters = null, in System.Collections.Generic
			.IReadOnlyCollection<System.Tuple<string, string, GlobalAliasesOneAliasOneParam>>? eParamsWithNewNames = null)
		{
			FirePropChanged(nameof(AllParametersByName));

			evtNamedParametersChanged?.Invoke(this, mapAllParametersByName, eNewNamedParameters, eRemovedNamedParameters,
				eParamsWithNewNames);
		}

		protected void PrependNewPositionedParameter(GlobalAliasesOneAliasOneParam paramNew)
		{
			rlistPositionedParameters.AddFirst(paramNew);

			FirePositionedParametersChanged([paramNew,]);
		}

		protected void AppendNewPositionedParameter(GlobalAliasesOneAliasOneParam paramNew)
		{
			rlistPositionedParameters.AddLast(paramNew);

			FirePositionedParametersChanged([paramNew,]);
		}

		protected void AddPositionedParameterAfter(GlobalAliasesOneAliasOneParam paramNew, in
			GlobalAliasesOneAliasOneParam paramExisting)
		{
			System.Collections.Generic.LinkedListNode<GlobalAliasesOneAliasOneParam> llnparamExisting =
				rlistPositionedParameters.Find(paramExisting) ?? throw new System.InvalidOperationException(@"Can't find the " +
				"existing positional parameter");

			rlistPositionedParameters.AddAfter(llnparamExisting, paramExisting);

			FirePositionedParametersChanged([paramNew,]);
		}

		protected void AddPositionedParameterBefore(GlobalAliasesOneAliasOneParam paramNew, in
			GlobalAliasesOneAliasOneParam paramExisting)
		{
			System.Collections.Generic.LinkedListNode<GlobalAliasesOneAliasOneParam> llnparamExisting =
				rlistPositionedParameters.Find(paramExisting) ?? throw new System.InvalidOperationException(@"Can't find the " +
				"existing positional parameter");

			rlistPositionedParameters.AddBefore(llnparamExisting, paramNew);

			FirePositionedParametersChanged([paramNew,]);
		}

		protected void MovePositionedParameterToTop(GlobalAliasesOneAliasOneParam paramToMove)
		{
			if(!rlistPositionedParameters.Contains(paramToMove))
				throw new System.InvalidOperationException("Can't find the positional parameter to move");

			System.Collections.Generic.LinkedListNode<GlobalAliasesOneAliasOneParam> llnparamToMove =
				rlistPositionedParameters.Find(paramToMove) ?? throw new System.InvalidOperationException(@"Can't find the " +
					@"positional parameter to move");

			if(llnparamToMove.Previous == null)
				return;

			if(llnparamToMove.Previous.Value.IsRequired && !paramToMove.IsRequired)
				return;

			if(!paramToMove.IsRequired)
				while(rlistPositionedParameters.Find(paramToMove) is
						{
							Previous: not null,
							Value.IsRequired: false,
						})
					rlistPositionedParameters.MoveEntryUp(paramToMove);
			else
				rlistPositionedParameters.MoveEntryToTop(paramToMove);

			FirePositionedParametersChanged(eMovedPositionedParameters: [paramToMove, ]);
		}

		protected void MovePositionedParameterUp(GlobalAliasesOneAliasOneParam paramToMove)
		{
			if(!rlistPositionedParameters.Contains(paramToMove))
				throw new System.InvalidOperationException("Can't find the positional parameter to move");

			rlistPositionedParameters.MoveEntryUp(paramToMove);

			FirePositionedParametersChanged(eMovedPositionedParameters: [paramToMove, ]);
		}

		protected void MovePositionedParameterDown(GlobalAliasesOneAliasOneParam paramToMove)
		{
			if(!rlistPositionedParameters.Contains(paramToMove))
				throw new System.InvalidOperationException("Can't find the positional parameter to move");

			rlistPositionedParameters.MoveEntryDown(paramToMove);

			FirePositionedParametersChanged(eMovedPositionedParameters: [paramToMove, ]);
		}

		protected void MovePositonedParameterToBottom(GlobalAliasesOneAliasOneParam paramToMove)
		{
			if(!rlistPositionedParameters.Contains(paramToMove))
				throw new System.InvalidOperationException("Can't find the positional parameter to move");

			System.Collections.Generic.LinkedListNode<GlobalAliasesOneAliasOneParam> llnparamToMove =
				rlistPositionedParameters.Find(paramToMove) ?? throw new System.InvalidOperationException(@"Can't find the " +
					@"positional parameter to move");

			if(llnparamToMove.Next == null)
				return;

			if(!llnparamToMove.Next.Value.IsRequired && paramToMove.IsRequired)
				return;

			if(paramToMove.IsRequired)
				while(rlistPositionedParameters.Find(paramToMove) is
						{
							Next: not null,
							Value.IsRequired: true,
						})
					rlistPositionedParameters.MoveEntryDown(paramToMove);
			else
				rlistPositionedParameters.MoveEntryToBottom(paramToMove);

			FirePositionedParametersChanged(eMovedPositionedParameters: [paramToMove, ]);
		}

		protected void RemovePositionedParameter(GlobalAliasesOneAliasOneParam paramToRemove)
		{
			if(!rlistPositionedParameters.Contains(paramToRemove))
				throw new System.InvalidOperationException("Can't find the positional parameter to remove");

			rlistPositionedParameters.Remove(paramToRemove);

			evtPositionedParametersChanged?.Invoke(this, rlistPositionedParameters, eRemovedEntries: [paramToRemove, ]);
		}

		protected void AddNamedParameter(GlobalAliasesOneAliasOneParam paramToAdd)
		{
			if(mapAllParametersByName.ContainsKey(paramToAdd.Name))
				throw new System.InvalidOperationException("The named parameter is already added.");

			mapAllParametersByName[paramToAdd.Name] = paramToAdd;

			paramToAdd.evtNameChanged += OnNameOfParamChanged;
		}

		protected void RemoveNamedParameter(GlobalAliasesOneAliasOneParam paramToRemove)
		{
			mapAllParametersByName.Remove(paramToRemove.Name);

			paramToRemove.evtNameChanged -= OnNameOfParamChanged;

			FireNamedParametersChanged(eRemovedNamedParameters: [new(paramToRemove.Name, paramToRemove), ]);
		}

		public GlobalAliasesOneAliasEditable MakeEditable()
			=> new(this);

		public void SaveFrom(GlobalAliasesOneAliasEditable ealiasWhatToSaveFrom)
		{
			Name = ealiasWhatToSaveFrom.Name;
			WhatToRun = ealiasWhatToSaveFrom.Cmd;
			Doc = ealiasWhatToSaveFrom.Doc;
		}

		private void RecreateDeclaredCmd()
			=> DeclaredCmd = new(
				strName,
				strDoc,
				[..
					from GlobalAliasesOneAliasOneParam aparamCur in
						rlistPositionedParameters
					where aparamCur.DeclaredParam is not null
					select aparamCur.DeclaredParam,
				],
				[..
					from GlobalAliasesOneAliasOneParam aparamCur in mapAllParametersByName.Values
					where aparamCur.DeclaredParam is not null
					select aparamCur.DeclaredParam,
			]);

		public DTO.GlobalAliasesOneAliasDTO ToDTO()
			=> new(
				guid,
				strName,
				cmdcWhatToRun,
				[..
					from GlobalAliasesOneAliasOneParam aparamCur in rlistPositionedParameters
					select aparamCur.Name,
				],
				[..
					from GlobalAliasesOneAliasOneParam aparamCur in mapAllParametersByName.Values
					select aparamCur.ToDTO(),
				],
				strDoc
			);

		public string ExportAsString(in bool bWithTypeIndicator = true)
			=> bWithTypeIndicator
				? $@"{{'$objType': 'alias',\r\n\t'Alias': ${System.Text.Json.JsonSerializer.Serialize(ToDTO(),
					jsoStandard)}\r\n}}"
				: System.Text.Json.JsonSerializer.Serialize(ToDTO(), jsoStandard);

		public static string ExportManyAliasesAsString(in System.Collections.Generic.IEnumerable<IReadOnlyOneAlias>
				aliasesToExport)
			=> $@"{{'$objType': 'aliasArray',\r\n\t'Ctnts': {System.Text.Json.JsonSerializer.Serialize(aliasesToExport
				.Select(aliasCur
					=> aliasCur.ToDTO()), jsoStandard)}\r\n}}";

		public static bool IsStringCtntsOneAlias(in string strCtntsToTest)
			=> GetRegexToTestForSingleAlias().IsMatch(strCtntsToTest);

		public static bool IsStringCtntsMultipleAliases(in string strCtntsToTest)
			=> GetRegexToTestForMultipleAliases().IsMatch(strCtntsToTest);

		public static GlobalAliasesOneAlias ImportOneAliasFromString(string strImportFrom)
		{
			if(GetRegexToTestForSingleAlias().IsMatch(strImportFrom))
			{
				strImportFrom = GetTypeStrippingForSingleAliasRegex().Replace(strImportFrom, "$1");
				return new(System.Text.Json.JsonSerializer.Deserialize<DTO.GlobalAliasesOneAliasDTO>(strImportFrom)
					?? throw new System.InvalidOperationException(PrefsRsrcs
						.strImportingAliasFailedBecauseTheStringIsNotAnAlias));
			}
			throw new System.InvalidOperationException(PrefsRsrcs.strImportingAliasFailedBecauseTheStringIsNotAnAlias);
		}

		public static System.Collections.Generic.IEnumerable<GlobalAliasesOneAlias> ImportManyAliasesFromString(string
			strImportFrom)
		{
			if(GetRegexToTestForMultipleAliases().IsMatch(strImportFrom))
			{
				strImportFrom = GetTypeStrippingForMultipleAliasesRegex().Replace(strImportFrom, "$1");
				return System.Text.Json.JsonSerializer.Deserialize<DTO.GlobalAliasesOneAliasDTO[]>(strImportFrom)?
					.Select(dalaiasCur
						=> new GlobalAliasesOneAlias(dalaiasCur))
					?? throw new System.InvalidOperationException(PrefsRsrcs.strImportingAliasFailedBecauseTheStringIsNotAnAlias);
			}
			throw new System.InvalidOperationException(PrefsRsrcs.strImportingAliasFailedBecauseTheStringIsNotAnAlias);
		}

		[System.Text.RegularExpressions.GeneratedRegex(@"^\s*{\s*'$objType':\s*'alias'\s*,\s*'Alias'\s*:\s*{")]
		private static partial System.Text.RegularExpressions.Regex GetRegexToTestForSingleAlias();

		[System.Text.RegularExpressions.GeneratedRegex(@"^\s*{\s*'$objType':\s*'aliasArray'\s*,\s*'Ctnts'\s*:\s*\[")]
		private static partial System.Text.RegularExpressions.Regex GetRegexToTestForMultipleAliases();

		[System.Text.RegularExpressions.GeneratedRegex(@"^\s*{\s*'$objType':\s*'.*?'\s*,\s*'Alias'\s*:{\s*(.*)}$")]
		private static partial System.Text.RegularExpressions.Regex GetTypeStrippingForSingleAliasRegex();

		[System.Text.RegularExpressions.GeneratedRegex(@"^\s*{\s*'$objType':\s*'.*?'\s*,\s*'Ctnts'\s*:\s*\[\s*(.*)]}$")]
		private static partial System.Text.RegularExpressions.Regex GetTypeStrippingForMultipleAliasesRegex();
	#endregion

	#region Event Handlers
	private void OnNameOfParamChanged(in GlobalAliasesOneAliasOneParam aliasSender, in string strOldName, in string
			strNewName)
		{
			mapAllParametersByName.Remove(strOldName);
			mapAllParametersByName[strNewName] = aliasSender;
		}

	#endregion
}