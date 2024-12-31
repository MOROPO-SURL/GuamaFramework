using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract
{
    public abstract class AbstractBridgeEntityTypeConfigurator<TBridgeEntity, TProwEntity, TProwId, TSternEntity, TSternId> : AbstractEntityTypeConfigurator<TBridgeEntity>
        where TBridgeEntity : class, Persistence.Entities.Contract.IBridge<TProwEntity, TProwId, TSternEntity, TSternId>
        where TProwEntity : class, Persistence.Entities.Contract.IEntity<TProwId>
        where TSternEntity : class, Persistence.Entities.Contract.IEntity<TSternId>
    {
        protected System.Linq.Expressions.Expression<System.Func<TProwEntity, System.Collections.Generic.IEnumerable<TBridgeEntity>>> prowNavigationExpression;

        protected System.Linq.Expressions.Expression<System.Func<TSternEntity, System.Collections.Generic.IEnumerable<TBridgeEntity>>> sternNavigationExpression;

        protected override void ApplayRules(EntityTypeBuilder<TBridgeEntity> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ProwId, bridge.SternId });

            ExtendProwDefaultRelationshipConfiguration(entityTypeBuilder.HasOne(bridge => bridge.Prow).WithMany(prowNavigationExpression).HasForeignKey(bridge => bridge.ProwId));

            ExtendSternDefaultRelationshipConfiguration(entityTypeBuilder.HasOne(bridge => bridge.Stern).WithMany(sternNavigationExpression).HasForeignKey(bridge => bridge.SternId));
        }

        protected virtual ReferenceCollectionBuilder<TProwEntity, TBridgeEntity> ExtendProwDefaultRelationshipConfiguration(ReferenceCollectionBuilder<TProwEntity, TBridgeEntity> builder)
        {
            return builder;
        }

        protected virtual ReferenceCollectionBuilder<TSternEntity, TBridgeEntity> ExtendSternDefaultRelationshipConfiguration(ReferenceCollectionBuilder<TSternEntity, TBridgeEntity> builder)
        {
            return builder;
        }
    }
}
