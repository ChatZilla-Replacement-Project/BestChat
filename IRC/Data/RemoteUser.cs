// Ignore Spelling: evt Chans Loc

using System.Linq;

namespace BestChat.IRC.Data;

public class RemoteUser : System.ComponentModel.INotifyPropertyChanged
{
	#region Constructors & Deconstructors
		internal RemoteUser(in ActiveNetwork anetOwner, string strCurNick, System.Collections.Generic.IEnumerable<Defs.IReadOnlyMode<Defs
			.BoolModeState, Defs.BoolModeStates>> modes, System.Collections.Generic.IEnumerable<Chan> echanMemberOfThese)
		{
			this.anetOwner = anetOwner;
			this.strCurNick = strCurNick;

			foreach(Defs.IReadOnlyMode<Defs.BoolModeState, Defs.BoolModeStates> modeCur in modes)
				mapLastKnownModes[modeCur.Def.ModeChar] = modeCur;

			foreach(Chan chanCur in echanMemberOfThese)
				NoteJoiningNewChan(chanCur);
		}
	#endregion

	#region Delegates
		public delegate void DCurNickChanged(in RemoteUser ruSender, in string strOldNick, in string strNewNick);
	#endregion

	#region Events
		public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

		public event DCurNickChanged? evtCurNickChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly ActiveNetwork anetOwner;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Later, we'll "
			+ "be updating this at times.")]
		private string strCurNick;

		private readonly System.Collections.Generic.SortedDictionary<char, Defs.IReadOnlyMode<Defs.BoolModeState, Defs.BoolModeStates>>
			mapLastKnownModes = [];

		private readonly System.Collections.Generic.SortedDictionary<string, Chan> mapPresentInChans = [];

		private System.DateTime? dtLastJoined = null;

		private readonly System.Collections.Generic.Dictionary<Chan, System.DateTime?> mapChanToWhenMostRecentPostHappened =
			[];
	#endregion

	#region Properties
		public ActiveNetwork Owner
			=> anetOwner;

		public string CurNick
			=> strCurNick; // TODO: This value needs to be updated somehow.

		public System.Collections.Generic.IReadOnlyDictionary<char, Defs.IReadOnlyMode<Defs.BoolModeState, Defs.BoolModeStates>>
				AllLastKnownModes
			=> mapLastKnownModes;

		public System.Collections.Generic.IReadOnlyDictionary<string, Chan> PresentInChans
			=> mapPresentInChans;

		public System.DateTime? LastJoined
			=> dtLastJoined;

		public System.Collections.Generic.IReadOnlyDictionary<Chan, System.DateTime?> WhenMostRecentPostHappenedByChan
			=> mapChanToWhenMostRecentPostHappened;

		public System.DateTime? TimeOfMostRecentPostInAnyChan
			=>
				(from System.Collections.Generic.KeyValuePair<Chan, System.DateTime?> kvCur in mapChanToWhenMostRecentPostHappened
					where kvCur.Value != null
					orderby kvCur.Value descending
					select kvCur.Value).First();

		public Chan LocOfMostRecentPost
			=>
			(from System.Collections.Generic.KeyValuePair<Chan, System.DateTime?> kvCur in mapChanToWhenMostRecentPostHappened
				where kvCur.Value != null
				orderby kvCur.Value descending
				select kvCur.Key).First();
	#endregion

	#region Methods
		private void FirePropChanged(in string strWhichProp) => PropertyChanged?.Invoke(this, new(strWhichProp));

		[System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification =
			"Once we figure out how to update the nick, this will get called.")]
		private void FireCurNickChanged(in string strOldNick)
			{
				FirePropChanged(nameof(CurNick));

				evtCurNickChanged?.Invoke(this, strOldNick, strCurNick);
			}

		internal void NoteJoiningNewChan(in Chan chanWhichOne)
		{
			if(chanWhichOne.Owner != anetOwner)
				throw new System.InvalidProgramException($"For some reason, the code is trying to associate {chanWhichOne.ProperName} with the user"
					+ $" {strCurNick}, but {chanWhichOne.ProperName}'s network, “{chanWhichOne.Owner.Def.Name}” isn't the same as the one associated "
					+ $"with the new user, “{anetOwner.Def.Name}”!");

			mapPresentInChans[chanWhichOne.Name] = chanWhichOne;
			mapChanToWhenMostRecentPostHappened[chanWhichOne] = null;
		}

		internal void NoteLeavingChan(in Chan chanWhichOne)
		{
			if(mapPresentInChans.ContainsKey(chanWhichOne.Name))
				mapPresentInChans.Remove(chanWhichOne.Name);
		}

		internal void NoteOnNetwork()
			=> dtLastJoined = System.DateTime.Now; // TODO: Handle /notify

		internal void NotePost(in Chan chanWhichOne)
		{
			if(!mapPresentInChans.ContainsKey(chanWhichOne.Name))
				throw new System.InvalidProgramException($"Supposedly, the user {strCurNick} posted in {chanWhichOne.ProperName}, but that " +
					"user doesn't seem to be in that channel");

			mapChanToWhenMostRecentPostHappened[chanWhichOne] = System.DateTime.Now;
		}
	#endregion

	#region Event Handlers
	#endregion
}