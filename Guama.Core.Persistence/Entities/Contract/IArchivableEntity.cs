namespace MOROPO.Framework.Guama.Core.Persistence.Entities.Contract
{
    public interface IArchivableEntity<TId> : IEntity<TId>
	{
		bool Archived { get; set; }
	}
}

