namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Configurators
{
    public interface IEntityTypeConfigurator
    {
        void Configure(Microsoft.EntityFrameworkCore.ModelBuilder builder, string tableSchema = null);
    }
}
