using BestChat.Platform.DataAndExt.Ext;

namespace BestChat.IRC.Data.Defs;

public class BncInstanceEditable : BncInstance, System.ComponentModel.INotifyDataErrorInfo
{
	#region Constructors & Deconstructors
		internal BncInstanceEditable(BncInstance instanceWhatsBeingEdited) :
			base(instanceWhatsBeingEdited)
			=> instanceOriginal = instanceWhatsBeingEdited;
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly BncInstance instanceOriginal;
	#endregion

	#region Properties
		public bool WereChangedMade
		{
			get;

			private set;
		}

		public new string Name
		{
			get => base.Name;

			set
			{
				if(base.Name == value)
					return;

				base.Name = value;

				WereChangedMade = true;

				ErrorsChanged?.Invoke(this, new(nameof(Name)));
			}
		}

		public new BncServerInfo? AssignedServer
		{
			get => base.AssignedServer;

			set
			{
				if(base.AssignedServer == value)
					return;

				base.AssignedServer = value;

				WereChangedMade = true;

				ErrorsChanged?.Invoke(this, new(nameof(AssignedServer)));
			}
		}

		public bool HasErrors
			=> Name == "" || instanceOriginal.bncOwner.AllInstancesByName.ContainsKey(Name) &&
				instanceOriginal.bncOwner.AllInstancesByName[Name] != instanceOriginal || AssignedServer ==
				null;
	#endregion

	#region Methods
		public System.Collections.IEnumerable GetErrors(string? strPropToGetErrorsFor)
		{
			System.Collections.Generic.SortedSet<string> strsetErrors = [];

			string[] astrPropsToGetErrorsFor = strPropToGetErrorsFor == null
				? [nameof(Name), nameof(AssignedServer)]
				: [strPropToGetErrorsFor];

			foreach(string strCurPropToGetErrorsFor in astrPropsToGetErrorsFor)
				switch(strCurPropToGetErrorsFor)
				{
					case nameof(Name):
						if(Name == "")
							strsetErrors.Add(Rsrcs.strBncInstanceNameBlankFmt.Fmt(bncOwner.Name));
						else if(instanceOriginal.bncOwner.AllInstancesByName.ContainsKey(Name) &&
										instanceOriginal.bncOwner.AllInstancesByName[Name] != instanceOriginal)
							strsetErrors.Add(Rsrcs.strBncInstanceNameNotUniqueFmt.Fmt(Name, bncOwner.Name));
						break;

					case nameof(AssignedServer):
						if(AssignedServer == null)
							strsetErrors.Add(Rsrcs.strBncInstanceServerNeededFmt.Fmt(bncOwner.Name));
						break;

					default:
						throw new System.InvalidProgramException("Unknown property");
				}

			return strsetErrors;
		}

		public void Save()
			=> instanceOriginal.SaveFrom(this);
		#endregion

		#region Event Handlers

		#endregion
}