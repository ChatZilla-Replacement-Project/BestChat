using BestChat.Platform.DataAndExt.Ext;

namespace BestChat.IRC.Data.Prefs
{
public class ChanInheritedItemEnabledStatus<InheritedType, ReadOnlyInterfaceType>(InheritedType inheritedItem,
	bool bStatus, ChanInheritedItemEnabledStatus<InheritedType, ReadOnlyInterfaceType>.InheritedFromTypes ifSrc,
	string strDescOfInheritedType, Defs.Net net) : NetInheritedItemEnabledStatus<InheritedType,
	ReadOnlyInterfaceType>(inheritedItem, bStatus)
	where InheritedType : ReadOnlyInterfaceType, IKeyChanged<InheritedType, string>
{
	public enum InheritedFromTypes : byte
	{
		network,
		global,
	}

	public InheritedFromTypes InheritedFromSrc => ifSrc;

	public string InheritedFromAsText => ifSrc switch
	{
		InheritedFromTypes.network
			=> PrefsRsrcs.strNetChanInheritedFromNetText,
		InheritedFromTypes.global
			=> PrefsRsrcs.strNetChanInheritedFromGlobalText,
		var _
			=> throw new Platform.DataAndExt.Exceptions
				.UnknownOrInvalidEnumException<InheritedFromTypes>(ifSrc, "While returning a textual " +
					"description of the source of this inherited item"),
	};

	public string InheritedFromAsTextDesc => ifSrc switch
	{
		InheritedFromTypes.network
			=> PrefsRsrcs.strNetChanInheritedFromNetDesc.Fmt(strDescOfInheritedType, net.Name),
		InheritedFromTypes.global
			=> PrefsRsrcs.strNetChanInheritedFromGlobalText.Fmt(strDescOfInheritedType),
		var _
			=> throw new Platform.DataAndExt.Exceptions
				.UnknownOrInvalidEnumException<InheritedFromTypes>(ifSrc, "While returning a textual " +
					"description of the source of this inherited item"),
	};
}
}