namespace MOROPO.Framework.Guama.Core.Persistence.Entities.Contract
{
    public interface IPriorizableEntity<TId> : IEntity<TId>
	{
		int Priority { get; set; }
	}
}

