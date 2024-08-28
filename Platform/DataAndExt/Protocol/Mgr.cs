// Ignore Spelling: Defs Loc evt dt iprot

using System.Linq;

namespace BestChat.Platform.DataAndExt.Protocol;

using Ext;

public abstract class MgrBase
{
	protected MgrBase()
	{
		instance = this;
	}

	private static MgrBase? instance;

	public static MgrBase Instance
		=> instance ?? throw new System.InvalidProgramException("No protocol manager created yet");

	public abstract void TellAllProtocolsToSave(in System.IO.DirectoryInfo dirDataLoc);
}

public abstract class Mgr<ProtocolInterfaceType> : MgrBase, System.ComponentModel.INotifyPropertyChanged
	where ProtocolInterfaceType : IProtocolDef
{
	#region Constructors & Deconstructors
		protected Mgr(in string strMaskForModules, in System.IO.DirectoryInfo dirProfileLoc, in System.Func<ProtocolMetaData, bool>
			funcNewProtocolEnabler)
		{
			this.funcNewProtocolEnabler = funcNewProtocolEnabler;

			System.Reflection.Assembly? assemblyEntry = System.Reflection.Assembly.GetEntryAssembly() ?? throw new System
				.InvalidProgramException("For some reason, BestChat.ProtocolMgr.ProtocolMgr was called from non-managed code.");

			string? strEntryAssemblyLoc = System.IO.Path.GetDirectoryName(assemblyEntry.Location);
			if(strEntryAssemblyLoc == null || strEntryAssemblyLoc == "")
				throw new System.InvalidProgramException("For some reason, we can't get the location of the executable that's creating a " +
					"BestChat.ProtocolMgr.ProtocolMgr instance.  We're expecting protocol module DLLs to be in a subdirectory thereof.");
			System.IO.DirectoryInfo dirProtocolModuleLoc = new(System.IO.Path.Combine(strEntryAssemblyLoc,
				strProtocolModuleSubdirectory));

			string strSetingsPath = System.IO.Path.Combine(dirProfileLoc.FullName, strEnabledProtocolsFileName);
			if(dirProfileLoc.Exists && System.IO.File.Exists(strSetingsPath))
			{
				if(dirProtocolModuleLoc.Exists)
					foreach(System.IO.FileInfo fileCurProtocolModule in dirProtocolModuleLoc.GetFiles(strMaskForModules))
						AddNewProtocolModule(fileCurProtocolModule, System.IO.File.ReadAllLines(strSetingsPath));
			}

			fsw = new(dirProtocolModuleLoc.FullName, strMaskForModules);

			fsw.Created += OnProtocolModuleFileCreated;
		}

		static Mgr()
		{
			if(strProtocolInterfaceTypeName == null)
				throw new System.InvalidProgramException("The protocol interface type has no name.");
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
		public const string strProtocolModuleSubdirectory = "Protocol Modules";

		public const string strEnabledProtocolsFileName = "Enabled Protocols.txt";
	#endregion

	#region Helper Types
		public class ProtocolMetaData : Obj<ProtocolMetaData>
		{
			#region Constructors & Deconstructors
				internal ProtocolMetaData(in System.Reflection.Assembly assembly)
				{
					this.assembly = Assembly;

					if(strProtocolInterfaceTypeName == null)
						throw new System.InvalidProgramException("The protocol interface type has no name.");

					typeCurProtocolInCurProtocolModule = assembly.ExportedTypes
						.Where((typeCurProtocolInCurProtocolModule)
							=> typeCurProtocolInCurProtocolModule.GetInterface(strProtocolInterfaceTypeName) != null &&
								!typeCurProtocolInCurProtocolModule.IsAbstract && typeCurProtocolInCurProtocolModule.GetConstructor(System.Reflection
								.BindingFlags.Public | System.Reflection.BindingFlags.Instance, []) != null).First();

					Attr.ProtocolAssemblyInfoAttribute attr = assembly.GetCustomAttributes(typeof(ProtocolInterfaceType), true)
						.Select(objCurAttr
							=> (Attr.ProtocolAssemblyInfoAttribute)objCurAttr).FirstOrDefault() ?? throw new UnableToLoadAssemblyInfoException(assembly);

					strProtocolName = attr.strName;
					strPublisher = attr.strPublisher;
					strDesc = attr.strDesc;
					strVersion = attr.strVersion;
					dtLastUpdatedOn = attr.dtLastUpdatedOn;
					uriAuthorHomePage = attr.uriAuthorHomePage;
					uriProductHomePage = attr.uriProjectHomePage;
				}
			#endregion

			#region Delegates
			#endregion

			#region Events
				public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

				public event DFieldChanged<bool>? evtIsEnabledChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
				public class UnableToLoadAssemblyInfoException : System.Exception
				{
					internal UnableToLoadAssemblyInfoException(System.Reflection.Assembly assembly)
						=> this.assembly = assembly;

					public readonly System.Reflection.Assembly assembly;

					public override string Message => Rsrcs.strUnableToLoadAssemblyInfoExceptionMsgFmt.Fmt(assembly.FullName ?? throw new System
						.Exception("Can't get full name of assembly as it doesn't seem to have one."), typeof(ProtocolInterfaceType));
				}

				public class FailedToCreateInstanceOfProtocolMgrForProtocolException : System.Exception
				{
					internal FailedToCreateInstanceOfProtocolMgrForProtocolException(ProtocolMetaData iprotIssuer)
						=> this.iprotIssuer = iprotIssuer;

					public readonly ProtocolMetaData iprotIssuer;

					public override string Message
						=> Rsrcs.strFailedToCreateInstanceOfProtocolMgrForProtocolMsgFmt.Fmt(iprotIssuer.assembly.FullName ?? throw new System
						.Exception("Can't get full name of assembly as it doesn't seem to have one."), iprotIssuer
						.typeCurProtocolInCurProtocolModule);
				}
			#endregion

			#region Members
				public readonly System.Type typeCurProtocolInCurProtocolModule;

				public readonly System.Reflection.Assembly assembly;

				public readonly string strProtocolName;

				public readonly string strPublisher;

				public readonly string strDesc;

				public readonly string strVersion;

				public readonly System.DateTime? dtLastUpdatedOn;

				public readonly System.Uri? uriAuthorHomePage;

				public readonly System.Uri? uriProductHomePage;

				private ProtocolInterfaceType? protocol = default;

				private bool bIsEnabled = false;
			#endregion

			#region Properties
				public System.Reflection.Assembly Assembly
					=> assembly;

				public string ProtocolName
					=> strProtocolName;

				public string Publisher
					=> strPublisher;

				public string Desc
					=> strDesc;

				public string Version
					=> strVersion;

				public System.DateTime? LastUpdatedOn
					=> dtLastUpdatedOn;

				public System.Uri? AuthorHomePage
					=> uriAuthorHomePage;

				public System.Uri? ProductHomePage
					=> uriProductHomePage;

				public ProtocolInterfaceType? Protocol
					=> protocol;

				public bool IsEnabled
				{
					get => bIsEnabled;

					set
					{
						if(bIsEnabled != value)
						{
							bIsEnabled = value;

							MakeDirty();

							FireIsEnabledChanged();

							if(bIsEnabled)
								LoadProtocol();
							else
								protocol = default;
						}
					}
				}
			#endregion

			#region Methods
				private void FirePropChanged(in string strPropName)
					=> PropertyChanged?.Invoke(this, new(strPropName));

				private void FireIsEnabledChanged()
				{
					FirePropChanged(nameof(bIsEnabled));

					evtIsEnabledChanged?.Invoke(this, !bIsEnabled, bIsEnabled);
				}

				private void LoadProtocol()
				{
					if(strProtocolInterfaceTypeName != null)
						protocol = (ProtocolInterfaceType)(assembly.CreateInstance(strProtocolInterfaceTypeName, false, System.Reflection
							.BindingFlags.CreateInstance, System.Type.DefaultBinder, [], System.Globalization.CultureInfo.CurrentCulture,
							[]) ?? throw new FailedToCreateInstanceOfProtocolMgrForProtocolException(this));
				}
			#endregion

			#region Event Handlers
			#endregion
		}
	#endregion

	#region Members
		private readonly System.IO.FileSystemWatcher fsw;

		private readonly System.Collections.Generic.Dictionary<string, ProtocolMetaData> mapNameToProtocolDefs =
			[];

		private readonly System.Collections.ObjectModel.ObservableCollection<ProtocolMetaData> ocUnsortedProtocolDefs =
			[];

		public readonly System.Func<ProtocolMetaData, bool> funcNewProtocolEnabler;

		private static string? strProtocolInterfaceTypeName = typeof(ProtocolInterfaceType).FullName;
	#endregion

	#region Properties
		public System.Collections.Generic.IReadOnlyDictionary<string, ProtocolMetaData> AllProtocolDefsByName
			=> mapNameToProtocolDefs;

		public System.Collections.Generic.IEnumerable<ProtocolMetaData> AllUnsortedProtocols
			=> ocUnsortedProtocolDefs;

		public System.Collections.Generic.IEnumerable<ProtocolInterfaceType> AllEnabledProtocols
			=> mapNameToProtocolDefs.Values.Where(iprotCur => iprotCur.IsEnabled).Select(iprotCur => iprotCur
				.Protocol).Cast<ProtocolInterfaceType>();
	#endregion

	#region Methods
		private void AddNewProtocolModule(in System.IO.FileInfo fileProtocolModule, in string[]? astrProtocolNamesToEnable = default)
		{
			System.Reflection.Assembly assemblyCurProtocolModule = System.Reflection.Assembly.LoadFile(fileProtocolModule.FullName);

			string? strProtocolInterfaceTypeName = typeof(ProtocolInterfaceType).FullName ?? throw new System.InvalidProgramException("The type" +
				" provided to BestChat.Platform.DataAndExt.Protocol.Mgr doesn't have a name.");

			ProtocolMetaData iprotNew = new(assemblyCurProtocolModule);
			iprotNew.evtDirtyChanged += OnProtocolInfoDirtyChanged;
			iprotNew.evtIsEnabledChanged += OnProtocolEnabledStatusChanged;

			if(mapNameToProtocolDefs.ContainsKey(iprotNew.ProtocolName))
				// TODO: Log the event.  We already have a protocol with the same name.
				return;

			mapNameToProtocolDefs[iprotNew.strProtocolName] = iprotNew;
			ocUnsortedProtocolDefs.Add(iprotNew);

			iprotNew.IsEnabled = astrProtocolNamesToEnable == null
				? funcNewProtocolEnabler(iprotNew)
				: astrProtocolNamesToEnable.Contains(iprotNew.ProtocolName);
		}

		public override void TellAllProtocolsToSave(in System.IO.DirectoryInfo dirDataLoc)
		{
			foreach(ProtocolInterfaceType iprotCur in AllEnabledProtocols)
				iprotCur.SaveAllData(dirDataLoc);
		}
	#endregion

	#region Event Handlers
		private void OnProtocolModuleFileCreated(object objSender, System.IO.FileSystemEventArgs e)
			=> AddNewProtocolModule(new(e.FullPath));

		private void OnProtocolInfoDirtyChanged(in ProtocolMetaData iprotSender, in bool bIsNowDirty)
			=> System.IO.File.WriteAllLinesAsync(strEnabledProtocolsFileName, AllProtocolDefsByName.Values.Where(iprotCur
				=> iprotCur.IsDirty).Select(iprotCur
					=> iprotCur.ProtocolName));

		private void OnProtocolEnabledStatusChanged(in ProtocolMetaData objSender, in bool oldVal, in bool newVal)
		{
		}
	#endregion
}