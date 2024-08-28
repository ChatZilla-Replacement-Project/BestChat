// Ignore Spelling: Esc evt Defs

namespace BestChat.IRC.Data.Defs;

public interface IModeState<StateType>
	where StateType : struct, System.Enum
{
	StateType State
	{
		get;
	}

	string TextualDescOfState
	{
		get;
	}

	char? CharToDescState
	{
		get;
	}

	IModeState<StateType> OffState
	{
		get;
	}

	IModeState<StateType> OnState
	{
		get;
	}
}

public enum BoolModeStates
{
	on,
	off,
}

public enum ThreeWayModeStates
{
	lockedOn,
	noPref,
	forcedOff
}

[System.ComponentModel.ImmutableObject(true)]
public class BoolModeState : IModeState<BoolModeStates>
{
	#region Constructors & Deconstructors
		private BoolModeState(in BoolModeStates stateInternal, in string strTextualDescOfState, in char? chCharToDescState)
		{
			this.stateInternal = stateInternal;
			this.strTextualDescOfState = strTextualDescOfState;
			this.chCharToDescState = chCharToDescState;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		public static readonly BoolModeState on = new(BoolModeStates.on, Rsrcs.strBoolModeStateOnDesc, '+');

		public static readonly BoolModeState off = new(BoolModeStates.off, Rsrcs.strBoolModeStateOffDesc, null);
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly BoolModeStates stateInternal;

		public readonly string strTextualDescOfState;

		public readonly char? chCharToDescState;

		private static readonly System.Collections.Generic.SortedDictionary<BoolModeStates, BoolModeState> mapEnumToInstance =
			new()
			{
				[BoolModeStates.on] = on,
				[BoolModeStates.off] = off,
			};
	#endregion

	#region Properties
		public BoolModeStates State
			=> stateInternal;

		public string TextualDescOfState
			=> strTextualDescOfState;

		public char? CharToDescState
			=> chCharToDescState;

		public IModeState<BoolModeStates> OffState
			=> off;

		public IModeState<BoolModeStates> OnState
			=> on;
	#endregion

	#region Methods
	#endregion

	#region Operators
		public static implicit operator BoolModeState(BoolModeStates stateInternalToLookUp)
			=> mapEnumToInstance[stateInternalToLookUp];

		public static implicit operator BoolModeStates(BoolModeState stateToConvert)
			=> stateToConvert.stateInternal;
	#endregion

	#region Event Handlers
	#endregion
}

[System.ComponentModel.ImmutableObject(true)]
public class ThreeWayModeState : IModeState<ThreeWayModeStates>
{
	#region Constructors & Deconstructors
		private ThreeWayModeState(in ThreeWayModeStates stateInternal, in string strTextualDescOfState, in char? chCharToDescState)
		{
			this.stateInternal = stateInternal;
			this.strTextualDescOfState = strTextualDescOfState;
			this.chCharToDescState = chCharToDescState;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		public static readonly ThreeWayModeState lockedOn = new(ThreeWayModeStates.lockedOn, Rsrcs
			.strThreeWayModeStateLockedOnDesc, '+');

		public static readonly ThreeWayModeState noPref = new(ThreeWayModeStates.noPref, Rsrcs
			.strThreeWayModeStateNoPrefDesc, null);

		public static readonly ThreeWayModeState forcedOff = new(ThreeWayModeStates.forcedOff, Rsrcs
			.strThreeWayModeStateForcedOff, '-');
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly ThreeWayModeStates stateInternal;

		public readonly string strTextualDescOfState;

		public readonly char? chCharToDescState;

		private static readonly System.Collections.Generic.SortedDictionary<ThreeWayModeStates, ThreeWayModeState> mapEnumToInstance =
			new()
			{
				[ThreeWayModeStates.lockedOn] = lockedOn,
				[ThreeWayModeStates.noPref] = noPref,
				[ThreeWayModeStates.forcedOff] = forcedOff,
			};
	#endregion

	#region Properties
		public ThreeWayModeStates State
			=> stateInternal;

		public string TextualDescOfState
			=> strTextualDescOfState;

		public char? CharToDescState
			=> chCharToDescState;

		public IModeState<ThreeWayModeStates> OffState
			=> noPref;

		public IModeState<ThreeWayModeStates> OnState
			=> lockedOn;
	#endregion

	#region Methods
	#endregion

	#region Operators
		public static implicit operator ThreeWayModeState(ThreeWayModeStates stateInternalToLookUp)
			=> mapEnumToInstance[stateInternalToLookUp];

		public static implicit operator ThreeWayModeStates(ThreeWayModeState stateToConvert)
			=> stateToConvert.stateInternal;
	#endregion

	#region Event Handlers
	#endregion
}

public interface IReadOnlyMode<ModeStateType, ModeStateTypeInternal> : System.ComponentModel.INotifyPropertyChanged
	where ModeStateType : IModeState<ModeStateTypeInternal>
	where ModeStateTypeInternal : struct, System.Enum
{
	IMode Def
	{
		get;
	}

	ModeStateType State
	{
		get;
	}

	event Mode<ModeStateType, ModeStateTypeInternal>.DStateChanged evtStateChanged;

	public interface IReadOnlyParam : System.ComponentModel.INotifyPropertyChanged
	{
		IReadOnlyMode<ModeStateType, ModeStateTypeInternal> Owner
		{
			get;
		}

		ModeParam Def
		{
			get;
		}

		object? Val
		{
			get;
		}

		event Mode<ModeStateType, ModeStateTypeInternal>.Param.DValChanged evtValChanged;
	}

	System.Collections.Generic.IReadOnlyDictionary<string, IReadOnlyParam> AllParamsByName
	{
		get;
	}
}

public abstract class AbstractModeParam
{
	public AbstractModeParam(in ModeParam mpDef)
		=> this.mpDef = mpDef;

	public readonly ModeParam mpDef;

	public ModeParam Def
		=> mpDef;

	public abstract object? Val
	{
		get;

		set;
	}
}

public class Mode<ModeStateType, ModeStateTypeInternal> : System.ComponentModel.INotifyPropertyChanged,
		IReadOnlyMode<ModeStateType, ModeStateTypeInternal>
	where ModeStateType : IModeState<ModeStateTypeInternal>
	where ModeStateTypeInternal : struct, System.Enum
{
	#region Constructors & Deconstructors
		internal Mode(in IMode mdUs, in ModeStateType state)
		{
			this.mdUs = mdUs;
			this.state = state;

			if(mdUs.ParamsByName != null)
				foreach(ModeParam mpCur in mdUs.ParamsByName.Values)
					mapParamsByName[mpCur.Name] = new(this, mpCur);
		}
	#endregion

	#region Delegates
		public delegate void DStateChanged(in Mode<ModeStateType, ModeStateTypeInternal> modeSender, in ModeStateType stateOld, in ModeStateType
			stateNew);
	#endregion

	#region Events
		public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

		public event DStateChanged? evtStateChanged;
	#endregion

	#region Constants
		public const char chFieldStart = '{';

		public const char chFieldEnd = '}';

		public const char chEsc = '`';
	#endregion

	#region Helper Types
		public class Param : AbstractModeParam, System.ComponentModel.INotifyPropertyChanged,
			IReadOnlyMode<ModeStateType, ModeStateTypeInternal>.IReadOnlyParam
		{
			#region Constructors & Deconstructors
				internal Param(in Mode<ModeStateType, ModeStateTypeInternal> modeOwner, in ModeParam mpDef, in object? objVal = null) :
					base(mpDef)
				{
					this.modeOwner = modeOwner;
					this.objVal = objVal;
				}
			#endregion

			#region Delegates
				public delegate void DValChanged(in Param mpSender, in object? objOldVal, in object? objNewVal);
			#endregion

			#region Events
				public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

				public event DValChanged? evtValChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
			#endregion

			#region Members
				public readonly Mode<ModeStateType, ModeStateTypeInternal> modeOwner;

				private object? objVal = null;
			#endregion

			#region Properties
				public Mode<ModeStateType, ModeStateTypeInternal> Owner => modeOwner;

				public override object? Val
				{
					get => objVal;

					set
					{
						if(objVal != value)
						{
							if(value == null)
								modeOwner.State = (ModeStateType)modeOwner.State.OffState;
							else
							{
								object? objOldVal = objVal;

								objVal = value;

								FireValChanged(objOldVal);
							}
						}
					}
				}

				IReadOnlyMode<ModeStateType, ModeStateTypeInternal> IReadOnlyMode<ModeStateType, ModeStateTypeInternal>.IReadOnlyParam.Owner
					=> modeOwner;
			#endregion

			#region Methods
				private void FirePropChanged(in string strPropName)
					=> PropertyChanged?.Invoke(this, new(strPropName));

				private void FireValChanged(in object? objOldVal)
				{
					FirePropChanged(nameof(Val));

					evtValChanged?.Invoke(this, objOldVal, objVal);
				}
			#endregion

			#region Event Handlers
			#endregion
		}

		private enum FmtParseStates
		{
			none,
			fieldName,
			expectingFieldName,
			possibleEscStart,
		}
	#endregion

	#region Members
		public readonly IMode mdUs;

		private ModeStateType state;

		private readonly System.Collections.Generic.SortedDictionary<string, Param> mapParamsByName = [];
	#endregion

	#region Properties
		public IMode Def => mdUs;

		public ModeStateType State
		{
			get => state;

			set
			{
				if(!state.Equals(value))
				{
					ModeStateType stateOld = state;

					state = value;

					FireStateChanged(stateOld);

					if(value.Equals(state.OffState))
						foreach(Param paramCur in mapParamsByName.Values)
							paramCur.Val = null;
				}
			}
		}

		public bool StateAsBool
		{
			get => !State.State.Equals(State.OffState.State);

			set => State = (ModeStateType)(value
				? State.OnState
				: State.OffState);
		}

		public System.Collections.Generic.IReadOnlyDictionary<string, Param> AllParamsByName
			=> mapParamsByName;

		System.Collections.Generic.IReadOnlyDictionary<string, IReadOnlyMode<ModeStateType, ModeStateTypeInternal>.IReadOnlyParam>
				IReadOnlyMode<ModeStateType, ModeStateTypeInternal>.AllParamsByName
			=> (System.Collections.Generic.IReadOnlyDictionary<string, IReadOnlyMode<ModeStateType, ModeStateTypeInternal>
				.IReadOnlyParam>)mapParamsByName;

		public System.Collections.Generic.IEnumerable<IReadOnlyMode<ModeStateType, ModeStateTypeInternal>.IReadOnlyParam> AllParams
			=> mapParamsByName.Values;
	#endregion

	#region Methods
		private void FirePropChanged(in string strPropName)
			=> PropertyChanged?.Invoke(this, new(strPropName));

		private void FireStateChanged(in ModeStateType stateOld)
		{
			FirePropChanged(nameof(State));
			FirePropChanged(nameof(StateAsBool));

			evtStateChanged?.Invoke(this, stateOld, state);
		}

		public string ApplyFmt()
		{
			if(mdUs is Defs.ChanMode cmdUs && cmdUs.FmtAsSentToNetwork != null)
			{
				string strResult = "";
				string strNameOfCurField = "";

				FmtParseStates fmtParseState = FmtParseStates.none;
				for(int iCurFmtChar = 0; iCurFmtChar < cmdUs.FmtAsSentToNetwork.Length; iCurFmtChar++)
				{
					char chCurFmt = cmdUs.FmtAsSentToNetwork[iCurFmtChar];
					switch(chCurFmt)
					{
						case chEsc:
							switch(fmtParseState)
							{
								case FmtParseStates.none:
									fmtParseState = FmtParseStates.possibleEscStart;

									break;

								case FmtParseStates.possibleEscStart:
									fmtParseState = FmtParseStates.none;

									strResult += chEsc;

									break;

								case FmtParseStates.fieldName:
								case FmtParseStates.expectingFieldName:
									throw new System.InvalidProgramException($"Found invalid escape sequence in mode format string “{cmdUs
										.FmtAsSentToNetwork}”: The escape character was followed by {chCurFmt}, but can only be followed by another escape " +
										$"character, {chEsc}, or the field start character: {chFieldStart}.");

								default:
									throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<FmtParseStates>(fmtParseState, "While " +
										"parsing a format string for modes.");
							}

							break;

						case chFieldStart:
							switch(fmtParseState)
							{
								case FmtParseStates.none:
									fmtParseState = FmtParseStates.expectingFieldName;

									break;

								case FmtParseStates.possibleEscStart:
									fmtParseState = FmtParseStates.none;

									strResult += chFieldStart;

									break;

								case FmtParseStates.fieldName:
								case FmtParseStates.expectingFieldName:
									throw new System.InvalidProgramException($"The field start character, {chFieldStart}, isn't allowed inside a field "
										+ "but was found there.");

								default:
									throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<FmtParseStates>(fmtParseState, "While " +
										"parsing a format string for modes.");
							}

							break;

						case chFieldEnd:
							switch(fmtParseState)
							{
								case FmtParseStates.none:
									strResult += chFieldEnd;

									break;

								case FmtParseStates.possibleEscStart:
									strResult += chFieldEnd;

									fmtParseState = FmtParseStates.none;

									break;

								case FmtParseStates.fieldName:
									if(strNameOfCurField != "" && mapParamsByName.ContainsKey(strNameOfCurField))
										strResult += mapParamsByName[strNameOfCurField].Val;

									break;

								case FmtParseStates.expectingFieldName:
									throw new System.InvalidProgramException("The format string for a mode specified an empty field name.");

								default:
									throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<FmtParseStates>(fmtParseState, "While "
										+ "parsing a format string for modes.");
							}
							break;

						default:
							switch(fmtParseState)
							{
								case FmtParseStates.none:
									strResult += chCurFmt;

									break;

								case FmtParseStates.possibleEscStart:
									strResult += chFieldEnd;

									fmtParseState = FmtParseStates.none;

									break;

								case FmtParseStates.fieldName:
									strNameOfCurField += chCurFmt;

									break;

								case FmtParseStates.expectingFieldName:
									strNameOfCurField += chCurFmt;

									fmtParseState = FmtParseStates.fieldName;

									break;

								default:
									throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<FmtParseStates>(fmtParseState, "While " +
										"parsing a format string for modes.");
							}

							break;
					}
				}

				return strResult;
			}

			return "";
		}

		public override string? ToString()
			=> state.CharToDescState == null
				? ""
				: $"{state.CharToDescState}{mdUs.ModeChar} {ApplyFmt()}";
	#endregion

	#region Event Handlers
	#endregion
}

public class TwoWayMode : Mode<BoolModeState, BoolModeStates>
{
	public TwoWayMode(in IMode mdUs, in BoolModeStates state) : base(mdUs, state)
	{
	}
}

public class ThreeWayMode : Mode<ThreeWayModeState, ThreeWayModeStates>
{
	public ThreeWayMode(in IMode mdUs, in ThreeWayModeStates state) : base(mdUs, state)
	{
	}
}