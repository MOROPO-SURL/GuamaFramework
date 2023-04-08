namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients
{
    public abstract class AbstractHabilitableEntityManagementFilteredApiConnectClient<TEntity, TId, TInputModel, TDisplayModel, TFilterModel> : AbstractEntityManagementFilteredApiConnectClient<TEntity, TId, TInputModel, TDisplayModel, TFilterModel>
        where TEntity : class, Persistence.Entities.Contract.IHabilitableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractHabilitableEntityManagementFilteredApiConnectClient(Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        public virtual async System.Threading.Tasks.Task EnableAsync(TId key)
        {
            await httpClientHelperProvider.HttpClientHelper.PutAsync(string.Format("{0}/{1}", TargetController, key));
        }

        public virtual async System.Threading.Tasks.Task DisableAsync(TId key)
        {
            await httpClientHelperProvider.HttpClientHelper.PutAsync(string.Format("{0}/{1}", TargetController, key));
        }
    }
}
