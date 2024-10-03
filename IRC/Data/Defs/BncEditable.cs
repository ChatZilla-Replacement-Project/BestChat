using System.Linq;

namespace BestChat.IRC.Data.Defs;

public class BncEditable : BNC
{
	#region Constructors & Deconstructors
		public BncEditable(in BNC bncOriginal) :
			base(bncOriginal)
			=> this.bncOriginal = bncOriginal;
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
		public readonly BNC bncOriginal;
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
				if (base.Name != value)
				{
					base.Name = value;

					WereChangedMade = true;
				}
			}
		}

		public new System.Uri? HomePage
		{
			get => base.HomePage;

			set
			{
				if (base.HomePage != value)
				{
					base.HomePage = value;

					WereChangedMade = true;
				}
			}
		}

		public new string? HomeNet
		{
			get => base.HomeNet;

			protected set
			{
				if(base.HomeNet == value)
					return;

				base.HomeNet = value;

				WereChangedMade = true;
			}
		}

		public new string? HomeChan
		{
			get => base.HomeChan;

			protected set
			{
				if(base.HomeChan == value)
					return;

				base.HomeChan = value;

				WereChangedMade = true;
			}
		}

		public new string? OwnBot
		{
			get => base.OwnBot;

			set
			{
				if(base.OwnBot == value)
					return;

				base.OwnBot = value;

				WereChangedMade = true;
			}
		}

		public new System.Collections.Generic.IReadOnlyDictionary<string, BncServerInfo> AllServersByName
			=> bncOriginal.AllServersByName;

		public new uint? MaxNetworksPerBouncerInstance
		{
			get => base.MaxNetworksPerBouncerInstance;

			set
			{
				if(base.MaxNetworksPerBouncerInstance == value)
					return;

				base.MaxNetworksPerBouncerInstance = value;

				WereChangedMade = true;
			}
		}
	#endregion

	#region Methods
		public new void AddAllowedNetwork(string strNewAllowedNetwork)
		{
			base.AddAllowedNetwork(strNewAllowedNetwork);

			WereChangedMade = true;
		}

		public new void RemoveAllowedNetwork(string strNetworkToRemoveFromAllowedList)
		{
			base.RemoveAllowedNetwork(strNetworkToRemoveFromAllowedList);

			WereChangedMade = true;
		}

		public new void ClearAllowedNetworks()
		{
			base.ClearAllowedNetworks();

			WereChangedMade = true;
		}

		public new void AddProhibitedNetwork(string strNewProhibitedNetwork)
		{
			base.AddAllowedNetwork(strNewProhibitedNetwork);

			WereChangedMade = true;
		}

		public new void RemoveProhibitedNetwork(string strNetworkToRemoveFromProhibitedList)
		{
			base.RemoveAllowedNetwork(strNetworkToRemoveFromProhibitedList);

			WereChangedMade = true;
		}

		public new void ClearProhibitedNetworks()
		{
			base.ClearAllowedNetworks();

			WereChangedMade = true;
		}

		public new void AddServer(BncServerInfo serverNew)
		{
			if(AllServersByName.ContainsKey(serverNew.Name))
				return;

			base.AddServer(serverNew);

			WereChangedMade = true;
		}

		public new void RemoveServer(string strNameOfServerToRemove)
		{
			if(!AllServersByName.ContainsKey(strNameOfServerToRemove))
				return;

			base.RemoveServer(strNameOfServerToRemove);

			WereChangedMade = true;
		}

		public new void RemoveServer(BncServerInfo serverBeingRemoved)
		{
			if(!AllServersByName.ContainsKey(serverBeingRemoved.Name))
				return;

			base.RemoveServer(serverBeingRemoved);

			WereChangedMade = true;
		}

		public new void ClearServers()
		{
			if(AllServersByName.Count <= 0)
				return;

			base.ClearServers();

			WereChangedMade = true;
		}

		public new void AddInstance(BncInstance instanceNew)
		{
			if(AllInstancesByName.ContainsKey(instanceNew.Name))
				return;

			base.AddInstance(instanceNew);

			WereChangedMade = true;
		}

		public new void RemoveInstance(string strNameOfInstanceToRemove)
		{
			if(!AllInstancesByName.ContainsKey(strNameOfInstanceToRemove))
				return;

			base.RemoveInstance(strNameOfInstanceToRemove);

			WereChangedMade = true;
		}

		public new void RemoveInstance(BncInstance instanceToRemove)
		{
			if(!AllInstancesByName.ContainsKey(instanceToRemove.Name))
				return;

			base.RemoveInstance(instanceToRemove);

			WereChangedMade = true;
		}

		public new void ClearInstances()
		{
			if(AllInstancesByName.Count <= 0)
				return;

			base.ClearInstances();

			WereChangedMade = true;
		}

		public new void AddPort(ushort usNewPort)
		{
			if(AllPorts.Contains(usNewPort))
				return;

			base.AddPort(usNewPort);

			WereChangedMade = true;
		}

		public new void RemovePort(ushort usPortToRemove)
		{
			if(!AllPorts.Contains(usPortToRemove))
				return;

			base.RemovePort(usPortToRemove);

			WereChangedMade = true;
		}

		public new void ClearPorts()
		{
			if(AllPorts.Count <= 0)
				return;

			base.ClearPorts();

			WereChangedMade = true;
		}

		public new void AddSslPort(ushort usNewSslPort)
		{
			if(AllSslPorts.Contains(usNewSslPort))
				return;

			base.AddSslPort(usNewSslPort);

			WereChangedMade = true;
		}

		public new void RemoveSslPort(ushort usSslPortToRemove)
		{
			if(!AllSslPorts.Contains(usSslPortToRemove))
				return;

			base.RemoveSslPort(usSslPortToRemove);

			WereChangedMade = true;
		}

		public new void ClearSslPorts()
		{
			if(AllSslPorts.Count <= 0)
				return;

			base.ClearSslPorts();

			WereChangedMade = true;
		}

		public void Save()
			=> bncOriginal.SaveFrom(this);
	#endregion

	#region Event Handlers
	#endregion
}