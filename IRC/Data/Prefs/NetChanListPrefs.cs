using System.Linq;
using BestChat.IRC.Data.Prefs.DTO;
using BestChat.Platform.DataAndExt.Ext;

namespace BestChat.IRC.Data.Prefs
{
public class NetChanListPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
	public NetChanListPrefs(NetPrefsBase mgrParent) :
		base(mgrParent, "Known Channels", PrefsRsrcs.strNetKnownChanTitle, PrefsRsrcs
			.strNetKnownChanDesc)
		=> this.mgrParent = mgrParent;

	public NetChanListPrefs(NetPrefsBase mgrParent, ChanDTO[]? dto) :
		base(mgrParent, "Known Channels", PrefsRsrcs.strNetKnownChanTitle, PrefsRsrcs
			.strNetKnownChanDesc)
	{
		this.mgrParent = mgrParent;

		if(dto != null)
			foreach(ChanDTO dchanCur in dto)
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

	public System.Collections.Generic.Dictionary<Chan, ChanPrefs> mapAllChanPrefsByChan = [];
	#endregion

	#region Properties
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

	public ChanDTO[]? ToDTO()
		=> mapAllChanPrefsByChan.Values.IsEmpty()
			? []
			: mapAllChanPrefsByChan.Values.Select(pchanCur
				=> pchanCur.ToDTO()
			).ToArray();
	#endregion

	#region Event Handlers
	#endregion
}
}