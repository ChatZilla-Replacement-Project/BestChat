namespace BestChat.Platform.DataAndExt.Cmd.ParamTypes;

public abstract class Abstract(string strName, string strLocalizedName, string strLocalizedDesc, System.Type
	typeBackedBy) :	Obj<Abstract>
{
	public string Name
		=> strName;

	public string LocalizedName
		=> strLocalizedName;

	public string LocalizedDesc
		=> strLocalizedDesc;

	public System.Type BackedByType
		=> typeBackedBy;

	public virtual bool InstancesHaveVal
		=> true;

	public virtual bool IsValid()
		=> true;

	private static readonly System.Collections.Generic.List<Abstract> listInstances = [];

	public static System.Collections.Generic.IEnumerable<Abstract> Instances
		=> listInstances;

	public class Invalid : Abstract
	{
		private Invalid() :
			base(System.Guid.NewGuid().ToString(), @"Invalid", @"Invalid", typeof(object))
		{
		}

		public override bool IsValid()
			=> false;

		public static Invalid Instance
		{
			get;

			private set;
		} = new();
	}
}