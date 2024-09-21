namespace BestChat.IRC.Data.Defs;

public class BncServerInstance : Platform.DataAndExt.Obj<BncServerInstance>
{
	#region Constructors & Deconstructors
		public BncServerInstance(BncServerInstance instanceCopyThis)
		{
			bncOwner = instanceCopyThis.bncOwner;

			strName = instanceCopyThis.Name;
			serverAssigned = instanceCopyThis.AssignedServer;
		}

		public BncServerInstance(BNC bncOwner, DTO.UserBncDTO.InstanceDTO dinstance)
		{
			this.bncOwner = bncOwner;

			strName = dinstance.Name;
			serverAssigned = bncOwner.AllServersByName.ContainsKey(dinstance.AssignedServer)
				? bncOwner.AllServersByName[dinstance.AssignedServer]
				: throw new System.InvalidProgramException($"Can't find the assigned BNC server {
					dinstance.AssignedServer}");
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event DFieldChanged<string>? evtNameChanged;

		public event DFieldChanged<BncServerInfo?>? evtAssignedServerChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types

	#endregion

	#region Members
		public readonly BNC bncOwner;

		private string strName;

		private BncServerInfo? serverAssigned;
	#endregion

	#region Properties
		public BNC OwnerBNC
			=> bncOwner;

		public string Name
		{
			get => strName;

			set
			{
				if(strName != value)
				{
					string strOldName = strName;

					strName = value;

					MakeDirty();

					FireNameChanged(strOldName);
				}
			}
		}

		public BncServerInfo? AssignedServer
		{
			get => serverAssigned;

			protected set
			{
				if(serverAssigned == value || value is null)
					return;

				if(!bncOwner.AllServersByName.ContainsKey(value.Name))
					throw new System.InvalidOperationException($"Can't find the server {value.Name} on the BNC {
						bncOwner.Name}");

				BncServerInfo serverOldAssigned = serverAssigned;

				serverAssigned = value;

				MakeDirty();

				FireAssignedServerChanged(serverOldAssigned);
			}
		}
	#endregion

	#region Methods
		private void FireNameChanged(in string strOldName)
		{
			FirePropChanged(nameof(Name));

			evtNameChanged?.Invoke(this, strOldName, strName);
		}

		private void FireAssignedServerChanged(in BncServerInfo? serverOldAssignedServer)
		{
			FirePropChanged(nameof(AssignedServer));

			evtAssignedServerChanged?.Invoke(this, serverOldAssignedServer, serverAssigned);
		}

		public DTO.UserBncDTO.InstanceDTO ToDTO()
			=> new(strName, serverAssigned!.Name);

		public BncServerInstanceEditable MakeEditable()
			=> new(this);

		public void SaveFrom(BncServerInstanceEditable einstance)
		{
			if(einstance.HasErrors)
				throw new System.InvalidProgramException("We can't save until the errors are resolved");

			strName = einstance.Name;
			serverAssigned = einstance.AssignedServer;
		}
	#endregion

	#region Event Handlers
	#endregion
}