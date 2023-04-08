namespace MOROPO.Framework.Guama.Core.Persistence.Entities.Contract
{
    public interface IBridge<TProwEntity, TProwId, TSternEntity, TSternId>
        where TProwEntity : IEntity<TProwId>
        where TSternEntity : IEntity<TSternId>
    {
        TProwId ProwId { get; set; }

        TProwEntity Prow { get; set; }

        TSternId SternId { get; set; }

        TSternEntity Stern { get; set; }
    }
}
