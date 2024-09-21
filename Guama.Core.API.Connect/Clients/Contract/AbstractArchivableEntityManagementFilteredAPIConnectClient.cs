namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients.Contract
{
	public abstract class AbstractArchivableEntityManagementFilteredAPIConnectClient<TEntity, TId, TInputModel, TDisplayModel, TFilterModel> : AbstractEntityManagementFilteredAPIConnectClient<TEntity, TId, TInputModel, TDisplayModel, TFilterModel>
        where TEntity : class, Persistence.Entities.Contract.IArchivableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractArchivableEntityManagementFilteredAPIConnectClient(Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        public virtual System.Threading.Tasks.Task ArchiveAsync(TId key)
        {
            return httpClientHelperProvider.HttpClientHelper.PutAsync(string.Format("{0}/archive/{1}", TargetController, key));
        }

        public virtual System.Threading.Tasks.Task UnarchiveAsync(TId key)
        {
            return httpClientHelperProvider.HttpClientHelper.PutAsync(string.Format("{0}/unarchive/{1}", TargetController, key));
        }
    }
}

