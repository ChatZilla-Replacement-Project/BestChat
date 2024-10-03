using System.Linq;

namespace BestChat.IRC.Data.Prefs;

using Platform.DataAndExt.Ext;

public class NetChanListPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public NetChanListPrefs(NetPrefsBase mgrParent) :
			base(mgrParent, "Known Channels", PrefsRsrcs.strNetKnownChanTitle, PrefsRsrcs.strNetKnownChanDesc)
			=> this.mgrParent = mgrParent;

		public NetChanListPrefs(NetPrefsBase mgrParent, DTO.ChanDTO[]? dto) :
			base(mgrParent, "Known Channels", PrefsRsrcs.strNetKnownChanTitle, PrefsRsrcs.strNetKnownChanDesc)
		{
			this.mgrParent = mgrParent;

			if(dto != null)
				foreach(DTO.ChanDTO dchanCur in dto)
					RegisterChan(Chan.AllChanByName[dchanCur.OwnerChan]);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public new readonly NetPrefsBase mgrParent;

		private readonly System.Collections.Generic.Dictionary<Chan, ChanPrefs> mapAllChanPrefsByChan = [];
	#endregion

	#region Properties
		public System.Collections.Generic.IReadOnlyDictionary<Chan, ChanPrefs> AllChanPrefs
			=> mapAllChanPrefsByChan;
	#endregion

	#region Methods
		public void RegisterChan(in Chan chanToBeRegistered)
		{
			ChanPrefs pchanNew = new(this, chanToBeRegistered);

			Add(pchanNew);

			mapAllChanPrefsByChan[chanToBeRegistered] = pchanNew;
		}

		public void RemovePrefsForChan(in Chan chanToRemovePrefsFor)
		{
			RemoveChildMgr(mapAllChanPrefsByChan[chanToRemovePrefsFor]);

			mapAllChanPrefsByChan.Remove(chanToRemovePrefsFor);
		}

		public DTO.ChanDTO[]? ToDTO()
			=> mapAllChanPrefsByChan.Values.IsEmpty()
				? []
				: mapAllChanPrefsByChan.Values.Select(pchanCur
					=> pchanCur.ToDTO()
				).ToArray();
	#endregion

	#region Event Handlers
	#endregion
}