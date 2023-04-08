namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients
{
    public abstract class AbstractHabilitableEntityManagementApiConnectClient<TEntity, TId, TInputModel, TDisplayModel> : AbstractEntityManagementApiConnectClient<TEntity, TId, TInputModel, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IHabilitableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractHabilitableEntityManagementApiConnectClient(Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
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
