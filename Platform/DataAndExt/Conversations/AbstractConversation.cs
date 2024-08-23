// Ignore Spelling: evt evti

namespace BestChat.Platform.DataAndExt.Conversations;

public abstract class AbstractConversation : IViewOrConversation, System.ComponentModel.INotifyPropertyChanged, Dieable.IDieable
{
	#region Constructors & Deconstructors
		public AbstractConversation(in string strName, in string strLongDesc, in System.Collections.Generic.IEnumerable<IEventInfo>? events =
			null)
		{
			Name = strName;
			LocalizedLongDesc = LongDesc = strLongDesc;

			if(events != null)
				foreach(IEventInfo evtiCur in events)
					RecordEvent(evtiCur);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public abstract event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

		public abstract event System.Action<Dieable.IDieable>? evtDieing;
	#endregion

	#region Constants
		public static readonly System.Collections.Generic.IReadOnlySet<IViewOrConversation.Types> setTypesThatCanBeSelected = new System
			.Collections.Generic.SortedSet<IViewOrConversation.Types>()
		{
				IViewOrConversation.Types.channelOrRoom,
				IViewOrConversation.Types.group,
				IViewOrConversation.Types.user,
				IViewOrConversation.Types.client,
		};
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private readonly System.Collections.Generic.SortedDictionary<System.DateTime, IEventInfo> mapEventsByTime =
			[];

		private readonly System.Collections.ObjectModel.ObservableCollection<IEventInfo> ocEvents = [];
	#endregion

	#region Properties
		public string Name
		{
			get;

			private init;
		}

		public abstract string ProperName
		{
			get;
		}

		public abstract string SafeName
		{
			get;
		}

		public abstract string LocalizedName
		{
			get;
		}

		public string LongDesc
		{
			get;

			private init;
		}

		public string LocalizedLongDesc
		{
			get;
		}

		public abstract string Path
		{
			get;
		}

		public abstract IViewOrConversation.Types Type
		{
			get;
		}

		public System.Collections.Generic.IReadOnlyDictionary<System.DateTime, IEventInfo> AllEventsByWhenTheyHappened
			=> mapEventsByTime;

		public System.Collections.Generic.IReadOnlyList<IEventInfo> UnsortedEvents
			=> ocEvents;

		public abstract string Icon
		{
			get;
		}

		public bool CanBeSelected => setTypesThatCanBeSelected.Contains(Type);
	#endregion

	#region Methods
		protected abstract void FirePropChanged(in string strPropName);

		protected void RecordEvent(in IEventInfo evti)
		{
			mapEventsByTime[evti.WhenItHappened] = evti;

			ocEvents.Add(evti);
		}
	#endregion

	#region Event Handlers
	#endregion
}