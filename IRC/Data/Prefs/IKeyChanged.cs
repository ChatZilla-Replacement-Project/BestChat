namespace BestChat.IRC.Data.Prefs
{
public interface IKeyChanged<InheritedType, KeyType>
	where InheritedType : IKeyChanged<InheritedType, string>
{
	delegate void DKeyChanged(in InheritedType whatChanged, in KeyType oldKey, in KeyType newKey);

	event DKeyChanged evtKeyChanged;
}
}