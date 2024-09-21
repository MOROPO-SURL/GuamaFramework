namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients.Contract
{
    public abstract class AbstractEnableableEntityManagementFilteredAPIConnectClient<TEntity, TId, TInputModel, TDisplayModel, TFilterModel> : AbstractEntityManagementFilteredAPIConnectClient<TEntity, TId, TInputModel, TDisplayModel, TFilterModel>
        where TEntity : class, Persistence.Entities.Contract.IEnableableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        public AbstractEnableableEntityManagementFilteredAPIConnectClient(Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        public virtual System.Threading.Tasks.Task EnableAsync(TId key)
        {
            return httpClientHelperProvider.HttpClientHelper.PutAsync(string.Format("{0}/enable/{1}", TargetController, key));
        }

        public virtual System.Threading.Tasks.Task DisableAsync(TId key)
        {
            return httpClientHelperProvider.HttpClientHelper.PutAsync(string.Format("{0}/disable/{1}", TargetController, key));
        }
    }
}
