// Ignore Spelling: Dto evt Defs Bnc

using System.Linq;

namespace BestChat.IRC.Data.Defs;

public abstract class MgrBase<ItemType, ItemBaseType, ItemDtoType> : Platform.DataAndExt.Obj<MgrBase<ItemType,
			ItemBaseType, ItemDtoType>>
		where ItemType : ItemBaseType
		where ItemBaseType : Platform.DataAndExt.Obj<ItemBaseType>, IDataDef<ItemBaseType>
		where ItemDtoType : IDataDefBasic<ItemDtoType>
{
	#region Constructors & Deconstructors
		protected MgrBase(System.IO.FileInfo fileUserData, DGetItemFromDTO funcItemMaker)
		{
			if(fileUserData.Exists)
				try
				{
					LoadData(System.Text.Json.JsonSerializer.Deserialize<ItemDtoType[]>(fileUserData.OpenText().ReadToEnd())
						?? [], funcItemMaker);
				}
				catch(System.Exception e)
				{
					throw new System.Exception($"Unable to reload your custom IRC networks due to {e.Message}.", e);
				}
		}

		protected MgrBase(System.Uri uriPredefinedData, DGetItemFromDTO funcItemMaker)
			=> LoadData(System.Text.Json.JsonSerializer.Deserialize<ItemDtoType[]>(client
				.GetStringAsync(uriPredefinedData.AbsoluteUri).Result) ?? [], funcItemMaker);

		protected MgrBase(System.Uri uriPredefinedData, System.IO.FileInfo fileUserData, DGetItemFromDTO funcItemMaker)
		{
			LoadData(System.Text.Json.JsonSerializer.Deserialize<ItemDtoType[]>(client.GetStringAsync(uriPredefinedData
				.AbsoluteUri).Result) ?? [], funcItemMaker);

			if(fileUserData.Exists)
				try
				{
					LoadData(System.Text.Json.JsonSerializer.Deserialize<ItemDtoType[]>(fileUserData.OpenText().ReadToEnd())
						?? [], funcItemMaker);
				}
				catch(System.Exception e)
				{
					throw new System.Exception($"Unable to reload your custom IRC networks due to {e.Message}.", e);
				}
		}

		static MgrBase()
			=> client = new();
	#endregion

	#region Delegates
		protected delegate ItemType? DGetItemFromDTO(in ItemDtoType dto);
	#endregion

	#region Events
		public event DCollectionFieldChanged<System.Collections.Generic.IEnumerable<ItemType>>? evtListChanged;

		public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private static readonly System.Net.Http.HttpClient client;

		private static readonly System.Text.Json.JsonSerializerOptions jso = new()
		{
			AllowTrailingCommas = true,
			Converters =
			{
				new System.Text.Json.Serialization.JsonStringEnumConverter(),
				new Platform.DataAndExt.JSON.FileConverter(),
				new Platform.DataAndExt.JSON.FileThatMightBeNullConverter(),
				new Platform.DataAndExt.JSON.FolderConverter(),
				new Platform.DataAndExt.JSON.FolderThatMightBeNullConverter(),
			},
			WriteIndented = true,
			NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString | System.Text.Json
				.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
		};

		private readonly System.Collections.Generic.SortedDictionary<string, ItemType> mapAllItemsSortedByName =
			[];

		protected readonly object objLock = new();
	#endregion

	#region Properties
		public System.Collections.Generic.IEnumerable<ItemType> AllItemsSortedByName
			=> mapAllItemsSortedByName.Values;

		public System.Collections.Generic.IReadOnlyDictionary<string, ItemType> AllItems
			=> mapAllItemsSortedByName;
	#endregion

	#region Methods
		private void LoadData(ItemDtoType[] dto, DGetItemFromDTO itemMaker)
		{
			foreach(ItemDtoType dnetCur in dto)
			{
				ItemType? @new = itemMaker(dnetCur);

				if(@new != null)
					mapAllItemsSortedByName[dnetCur.Name] = @new;
			}
		}

		public void Add(ItemType @new)
		{
			lock(objLock)
			{
				if(mapAllItemsSortedByName.ContainsKey(@new.Name))
					throw new System.ArgumentException($"The IRC network manager already has a network named {@new.Name} and"
						+ " can't accommodate a second with the same name.", nameof(@new));

				mapAllItemsSortedByName[@new.Name] = @new;

				@new.evtNameChanged += OnChildNameChanged;

				evtListChanged?.Invoke(this, AllItemsSortedByName, CollectionChangeType.add);

				PropertyChanged?.Invoke(this, new(nameof(AllItemsSortedByName)));

				MakeDirty();
			}
		}

		public void Remove(string strNameOfItemToRemove)
		{
			lock(objLock)
			{
				if(!mapAllItemsSortedByName.ContainsKey(strNameOfItemToRemove))
					throw new System.ArgumentException($"The network manager can't remove the network named {
						strNameOfItemToRemove} as no such network exists.", nameof(strNameOfItemToRemove));

				mapAllItemsSortedByName.Remove(strNameOfItemToRemove);

				evtListChanged?.Invoke(this, AllItemsSortedByName, CollectionChangeType.removed);

				PropertyChanged?.Invoke(this, new(nameof(AllItemsSortedByName)));

				MakeDirty();
			}
		}
	#endregion

	#region Event Handlers
		private void OnChildNameChanged(in ItemBaseType sender, in string strOldVal, in string strNewVal)
		{
			if(sender is not ItemType)
				throw new System.InvalidProgramException($"Some how an item in {GetType().FullName} isn't of type {
					typeof(ItemType)} as was expected.  Instead, an item of type {sender.GetType()} was found.");
			ItemType senderAsDerivedType = (ItemType)sender;

			if(mapAllItemsSortedByName.ContainsKey(strOldVal))
				mapAllItemsSortedByName.Remove(strOldVal);

			mapAllItemsSortedByName[sender.Name] = senderAsDerivedType;
		}
	#endregion
}

public class PredefinedNetMgr : MgrBase<PredefinedNet, Net, DTO.PredefinedNetDTO>
{
	private PredefinedNetMgr() :
		base(new System.Uri("https://raw.githubusercontent.com/ChatZilla-Replacement-Project/" +
			"JSON-Data/main/Defaults/Network-def.json"), MakeNetworkFromDto)
	{
	}

	public static readonly PredefinedNetMgr mgr = new();

	private static PredefinedNet? MakeNetworkFromDto(in DTO.PredefinedNetDTO dpnet)
		=> new(dpnet);
}

public class UserNetMgr : MgrBase<UserNet, Net, DTO.UserNetDTO>
{
	protected UserNetMgr() :
		base(GetSettingsFile(), MakeNetworkFromDto)
		=> threadSave = new(SaveData);

	private static System.IO.FileInfo GetSettingsFile()
		=> fileSettings = new(System.IO.Path.Combine(Platform.DataAndExt.DataLoc.Instance?.ProfileLoc?.FullName
			?? throw new System.InvalidProgramException("DataLoc not ready.  Was it initialized yet?"), "User " +
			"Networks.json"));

	private static System.IO.FileInfo? fileSettings;

	public static readonly UserNetMgr mgr = new();

	private static UserNet? MakeNetworkFromDto(in DTO.UserNetDTO dunet)
		=> new(dunet);

	private readonly System.Threading.Thread threadSave;

	private void SaveData()
	{
		if((IsDirty || AllItems.Values.Any(unetCur => unetCur.IsDirty)) && fileSettings != null)
		{
			lock(objLock)
			{
				using System.IO.FileStream stream = fileSettings.OpenWrite();

				System.Text.Json.JsonSerializer.Serialize(
					stream,
					AllItemsSortedByName.Select(unetCur
						=> unetCur.ToDTO()
					),
					jsoStandard
				);
			}
		}

		System.Threading.Thread.Sleep(5000);
	}
}

public class BncMgr : MgrBase<BNC, BNC, DTO.UserBncDTO>
{
	private BncMgr() :
		base(new System.Uri("https://github.com/ChatZilla-Replacement-Project/JSON-Data/raw/main/" +
			"Defaults/bnc.json"), GetSettingsFile(), MakeBncFromDTO)
		=> threadSave = new(SaveData);

	private static System.IO.FileInfo GetSettingsFile()
		=> fileSettings = new(System.IO.Path.Combine(Platform.DataAndExt.DataLoc.Instance?.ProfileLoc?.FullName
			?? throw new System.InvalidProgramException("DataLoc not ready.  Was it initialized yet?"), "User " +
			"bouncers.json"));

	private static System.IO.FileInfo? fileSettings;

	public static readonly BncMgr mgr = new();

	private static BNC? MakeBncFromDTO(in DTO.UserBncDTO dubnc)
	{
		if(mgr.AllItems.TryGetValue(dubnc.Name, out BNC? bncExisting) && dubnc.Instances != null && bncExisting !=
			null)
		{
			foreach(DTO.UserBncDTO.InstanceDTO dinstanceCur in dubnc.Instances)
				bncExisting.AddInstanceInternal(new(bncExisting, dinstanceCur));

			return null;
		}

		return new(dubnc);
	}

	private readonly System.Threading.Thread threadSave;

	private void SaveData()
	{
		if((IsDirty || AllItems.Values.Any(unetCur => unetCur.IsDirty)) && fileSettings != null)
		{
			lock(objLock)
			{
				using System.IO.FileStream stream = fileSettings.OpenWrite();

				System.Text.Json.JsonSerializer.Serialize(
					stream,
					AllItemsSortedByName.Where(bncCur
						=> bncCur.AllInstancesByName.Count > 0
					).Select(bncCur
						=> bncCur.ToDTO()
					),
					jsoStandard
				);
			}
		}

		System.Threading.Thread.Sleep(5000);
	}
}