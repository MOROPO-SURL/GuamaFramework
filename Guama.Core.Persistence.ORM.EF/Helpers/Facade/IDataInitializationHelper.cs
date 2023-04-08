namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade
{
    public interface IDataInitializationHelper
    {
        void InitialiseData(Microsoft.EntityFrameworkCore.DbContext dbContext);
    }
}
